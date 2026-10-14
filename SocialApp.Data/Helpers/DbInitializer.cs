using Microsoft.AspNetCore.Identity;
using SocialApp.Data.Helpers.Constants;
using SocialApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.Data.Helpers
{
    public static class DbInitializer
    {
        public static async Task SeedUsersAndRolesAsync(UserManager<User> userManager, RoleManager<IdentityRole<int>> roleManager)
        {
            // Roles
            if (!roleManager.Roles.Any())
            {
                foreach(var roleName in AppRoles.All)
                {
                    if(!await roleManager.RoleExistsAsync(roleName))
                    {
                        await roleManager.CreateAsync(new IdentityRole<int>(roleName));
                    }
                }
            }

            // Users with roles
            if(!userManager.Users.Any(n => !string.IsNullOrEmpty(n.Email)))
            {
                var userPassword = "Coding@1234?";
                var newUser = new User()
                {
                    UserName = "randy.urriola",
                    Email = "randy@urriola.com",
                    FullName = "Randy Urriola",
                    ProfilePrictureUrl = "C:\\Users\\randy\\source\\SocialApp\\SocialApp\\wwwroot\\images\\avatar\\test.png",
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(newUser, userPassword);
                if (result.Succeeded)
                    await userManager.AddToRoleAsync(newUser, AppRoles.User);


                var newAdmin = new User()
                {
                    UserName = "admin.admin",
                    Email = "admin@urriola.com",
                    FullName = "Randy Admin",
                    ProfilePrictureUrl = "C:\\Users\\randy\\source\\SocialApp\\SocialApp\\wwwroot\\images\\avatar\\test.png",
                    EmailConfirmed = true
                };

                var resultNewAdmin = await userManager.CreateAsync(newAdmin, userPassword);
                if (resultNewAdmin.Succeeded)
                    await userManager.AddToRoleAsync(newAdmin, AppRoles.Admin);
            }
        }
        public static async Task SeedAsync(AppDbContext appDbContext)
        {
            //if (!appDbContext.Users.Any() && !appDbContext.Posts.Any())
            //{
            //    var newUser = new User()
            //    {
            //        FullName = "Randy Urriola",
            //        ProfilePrictureUrl = "C:\\Users\\randy\\source\\SocialApp\\SocialApp\\wwwroot\\images\\avatar\\test.png"
            //    };
            //    await appDbContext.Users.AddAsync(newUser);
            //    await appDbContext.SaveChangesAsync();

            //    var newPostWithoutImage = new Post()
            //    {
            //        Content = "This is going to be our first post which is being loaded from the database and it has been created using our test user.",
            //        ImageUrl = "",
            //        NrOfReports = 0,
            //        DateCreated = DateTime.Now,
            //        DateUpdated = DateTime.Now,

            //        UserId = newUser.Id
            //    };

            //    var newPostWithImage = new Post()
            //    {
            //        Content = "This is going to be our first post which is being loaded from the database and it has been created using our test user. This post has an image",
            //        ImageUrl = "https://images.pexels.com/photos/31064522/pexels-photo-31064522.jpeg",
            //        NrOfReports = 0,
            //        DateCreated = DateTime.Now,
            //        DateUpdated = DateTime.Now,

            //        UserId = newUser.Id
            //    };

            //    await appDbContext.Posts.AddRangeAsync(newPostWithoutImage, newPostWithImage);
            //    await appDbContext.SaveChangesAsync();
            //}
        }
    }
}
