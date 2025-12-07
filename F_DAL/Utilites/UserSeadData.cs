using F_DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F_DAL.Utilites
{
    public class UserSeadData : ISeedData
    {
        private readonly UserManager<ApplecationUser> _UserManager;
        public UserSeadData(UserManager<ApplecationUser> UserManager)
        {
            _UserManager = UserManager;
        }
        public async Task SeedData()
        {
            if (!await _UserManager.Users.AnyAsync()) {

                var user1 = new ApplecationUser
                {
                    UserName = "anas",
                    Email = "anas@email.com",
                    FullName = "anas ahmad nazzal",
                    EmailConfirmed = true,
                };
                var user2 = new ApplecationUser
                {
                    UserName = "ahmad",
                    Email = "ahmad@email.com",
                    FullName = "anas ahmad nazzal",
                    EmailConfirmed = true
                };
                var user3 = new ApplecationUser
                {
                    UserName = "mahmoud",
                    Email = "anas@email.com",
                    FullName = "anas ahmad nazzal",
                    EmailConfirmed = true
                };

                await _UserManager.CreateAsync(user1, "test@123");
                await _UserManager.CreateAsync(user2, "test@123");
                await _UserManager.CreateAsync(user3, "test@123");


                await _UserManager.AddToRoleAsync(user1, "SuperAdmin");
                await _UserManager.AddToRoleAsync(user2, "Admin");

                await _UserManager.AddToRoleAsync(user3, "user");



            }

        }
    }
}
