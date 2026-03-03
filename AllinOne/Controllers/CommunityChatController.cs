using AllinOne.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScholarshipPlatform.Data;

namespace AllinOne.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class CommunityChatController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CommunityChatController(ApplicationDbContext context)
        {
            _context = context;
        }

        // 🔹 صفحة الكوميونيتي + الشات
        [HttpGet("/CommunityChat/Index/{id}")]
        public async Task<IActionResult> Index(int id) // id = CommunityId
        {
            var community = await _context.Communities
                .Include(c => c.Members)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (community == null)
                return NotFound();

            // تحقق عضوية المستخدم (لو عامل login)
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            bool userIsMember = false;

            if (!string.IsNullOrEmpty(userId))
            {
                userIsMember = community.Members.Any(m => m.UserId == userId);
            }

            ViewBag.IsMember = userIsMember;
            return View(community);
        }

        // 🔹 تحميل الرسائل القديمة (API)
        [HttpGet("GetMessages")]
        public async Task<IActionResult> GetMessages(int communityId)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
                return Unauthorized("You must log in to view messages.");

            var isMember = await _context.CommunityMembers
                .AnyAsync(x => x.CommunityId == communityId && x.UserId == userId);

            if (!isMember)
                return Unauthorized("You are not a member of this community.");

            var messages = await _context.ChatMessages
                .Where(m => m.CommunityId == communityId)
                .OrderBy(m => m.SentAt)
                .Select(m => new
                {
                    m.Id,
                    m.Message,
                    m.SentAt,
                    SenderName = m.Sender.Name,
                    m.SenderId
                })
                .ToListAsync();

            return Json(messages);
        }

        // 🔹 الانضمام للكوميونيتي
        [HttpPost("JoinCommunity")]
        public async Task<IActionResult> JoinCommunity(int communityId)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
                return Unauthorized("You must log in to join this community.");

            var exists = await _context.CommunityMembers
                .AnyAsync(cm => cm.CommunityId == communityId && cm.UserId == userId);

            if (exists) return BadRequest("You are already a member.");

            var member = new CommunityMember
            {
                CommunityId = communityId,
                UserId = userId,
                JoinedAt = DateTime.UtcNow
            };

            _context.CommunityMembers.Add(member);
            await _context.SaveChangesAsync();

            return Ok("Joined successfully");
        }
    }
}