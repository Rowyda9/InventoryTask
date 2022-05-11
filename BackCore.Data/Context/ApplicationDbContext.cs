using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace BackCore.Data
{
    public class ApplicationDbContext: IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
                 : base(options) { }
      
         public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Status> Statuses { get; set; }
 
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            //builder.Entity<UserRole>(userRole =>
            //{
            //    userRole.HasKey(ur => new { ur.UserId, ur.RoleId });

            //    userRole.HasOne(ur => ur.Role)
            //    .WithMany(r => r.UserRoles)
            //    .HasForeignKey(f => f.RoleId)
            //    .IsRequired();

            //    userRole.HasOne(ur => ur.User)
            // .WithMany(r => r.UserRoles)
            // .HasForeignKey(f => f.UserId)
            // .IsRequired();

            //});
          
        }


    }
}
