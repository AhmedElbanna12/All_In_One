//using Microsoft.AspNetCore.Identity;

//namespace AllinOne.SeedData
//{
//    public  static class SeedRoles
//    {
//        public static async Task InitializeAsync(IServiceProvider serviceProvider)
//        {
//            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
//            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

//            // 1️⃣ Create Roles
//            string[] roles = { "Admin", "Student" };

//            foreach (var role in roles)
//            {
//                if (!await roleManager.RoleExistsAsync(role))
//                {
//                    await roleManager.CreateAsync(new IdentityRole(role));
//                }
//            }

//            // 2️⃣ Create Admin User
//            var adminEmail = "admin@site.com";
//            var adminUser = await userManager.FindByEmailAsync(adminEmail);

//            if (adminUser == null)
//            {
//                var user = new IdentityUser
//                {
//                    UserName = adminEmail,
//                    Email = adminEmail,
//                    EmailConfirmed = true
//                };

//                var result = await userManager.CreateAsync(user, "Admin@123");

//                if (result.Succeeded)
//                {
//                    await userManager.AddToRoleAsync(user, "Admin");
//                }
//            }
//        }
//    }
//}
