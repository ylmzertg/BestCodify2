using BestCodify.Common;
using BestCodify.DataAccess.Data;
using BestCodify2.Service.IService;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace BestCodify2.Service
{
    public class DbInitializer : IDbInitializer
    {
        private readonly BestCodifyContext _db;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbInitializer(BestCodifyContext db, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void SetDefaulValues()
        {
            try
            {
                if (_db.Database.GetPendingMigrations().Count() > 0)
                {
                    _db.Database.Migrate();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            if (_db.Roles.Any(x => x.Name == ResultConstant.Admin_Role)) return;
            _roleManager.CreateAsync(new IdentityRole(ResultConstant.Admin_Role)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(ResultConstant.Role_Customer)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(ResultConstant.Role_Employee)).GetAwaiter().GetResult();

            _userManager.CreateAsync(new AppUser
            {
                UserName = "admin@gmail.com",
                Email = "admin@gmail.com",
                EmailConfirmed = true
            }, "Password1!").GetAwaiter().GetResult();
            var user = _db.Users.FirstOrDefault(u => u.Email == "admin@gmail.com");
            _userManager.AddToRoleAsync(user, ResultConstant.Admin_Role).GetAwaiter().GetResult();
        }
    }
}
