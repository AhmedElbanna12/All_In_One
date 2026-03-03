using AllinOne.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using ScholarshipPlatform.Data;

public class CommunityChatHub : Hub
{
    private readonly ApplicationDbContext _context;

    // Dictionary لحفظ Online Users لكل Community
    private static readonly Dictionary<int, HashSet<string>> OnlineUsers = new();

    public CommunityChatHub(ApplicationDbContext context)
    {
        _context = context;
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        foreach (var kvp in OnlineUsers)
        {
            if (kvp.Value.Remove(Context.UserIdentifier!))
            {
                await Clients.Group($"Community-{kvp.Key}")
                    .SendAsync("UpdateOnlineUsers", kvp.Value.ToList());
            }
        }

        await base.OnDisconnectedAsync(exception);
    }

    public async Task JoinCommunity(int communityId)
    {
        var userId = Context.UserIdentifier;
        var user = await _context.Users.FindAsync(userId);

        var isMember = await _context.CommunityMembers
            .AnyAsync(x => x.CommunityId == communityId && x.UserId == userId);

        if (!isMember)
            throw new HubException("Unauthorized");

        await Groups.AddToGroupAsync(Context.ConnectionId, $"Community-{communityId}");

        // Add online user
        if (!OnlineUsers.ContainsKey(communityId))
            OnlineUsers[communityId] = new HashSet<string>();
        OnlineUsers[communityId].Add(user!.Name);

        await Clients.Group($"Community-{communityId}")
            .SendAsync("UpdateOnlineUsers", OnlineUsers[communityId].ToList());
    }

    public async Task SendMessage(int communityId, string message)
    {
        var userId = Context.UserIdentifier;
        var user = await _context.Users.FindAsync(userId);

        var isMember = await _context.CommunityMembers
            .AnyAsync(x => x.CommunityId == communityId && x.UserId == userId);

        if (!isMember)
            throw new HubException("Unauthorized");

        var chatMessage = new ChatMessage
        {
            CommunityId = communityId,
            SenderId = userId!,
            Message = message,
            SentAt = DateTime.UtcNow
        };

        _context.ChatMessages.Add(chatMessage);
        await _context.SaveChangesAsync();

        await Clients.Group($"Community-{communityId}")
            .SendAsync("ReceiveMessage", new
            {
                SenderName = user!.Name,
                Message = message,
                SentAt = chatMessage.SentAt
            });
    }

    // Typing indicator
    public async Task Typing(int communityId)
    {
        var user = await _context.Users.FindAsync(Context.UserIdentifier);
        await Clients.OthersInGroup($"Community-{communityId}")
            .SendAsync("UserTyping", user!.Name);
    }
}