using F_DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace F_DAL.DATA
{
    public class ApplecationDbContext : IdentityDbContext<ApplecationUser>
    {
        public DbSet<Catgry> Catgries { get; set; }
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ApplecationDbContext(DbContextOptions<ApplecationDbContext> options, IHttpContextAccessor httpContextAccessor)
            : base(options)
        {

            _httpContextAccessor = httpContextAccessor;
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // تغيير أسماء جداول الهوية Identity
            builder.Entity<ApplecationUser>().ToTable("Users");
            builder.Entity<IdentityRole>().ToTable("Roles");
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");
            builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");
            builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims");
            builder.Entity<IdentityUserToken<string>>().ToTable("UserTokens");
        }


        public override int SaveChanges()
        {
            var entries = ChangeTracker.Entries<BaseModel>();
            var currentUserId = _httpContextAccessor.HttpContext?
                .User?
                .FindFirstValue(ClaimTypes.NameIdentifier);

            foreach (var entityEntry in entries)
            {
                if (entityEntry.State == EntityState.Added)
                {
                    entityEntry.Property(x => x.CreatedBy).CurrentValue = currentUserId;
                    entityEntry.Property(x => x.CreatedDate).CurrentValue = DateTime.UtcNow;
                }
                else if (entityEntry.State == EntityState.Modified)
                {
                    entityEntry.Property(x => x.UpdatedBy).CurrentValue = currentUserId;
                    entityEntry.Property(x => x.UpdatedDate).CurrentValue = DateTime.UtcNow;
                }
            }

            return base.SaveChanges();
        }

    }
}
