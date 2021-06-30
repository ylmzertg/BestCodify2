using Microsoft.AspNetCore.Identity;

namespace BestCodify.DataAccess.Data
{
    public class AppUser:IdentityUser
    {
        public string Name { get; set; }

    }
}
