using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System.Data;

namespace StudentComment.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string Picture { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
        }
        public DbSet<Student> Students { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}