using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BestCodify.DataAccess.Data
{
    public class BestCodifyContext : IdentityDbContext
    {
        public BestCodifyContext(DbContextOptions<BestCodifyContext> options)
                : base(options)
        {

        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseImage> CourseImages { get; set; }
    }
}
