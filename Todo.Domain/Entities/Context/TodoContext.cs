using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Todo.Domain.Configurations;
using Todo.Domain.Configurations.Identity;
using Todo.Domain.Entities.Identity;

namespace Todo.Domain.Entities.Context
{
    public class TodoContext : IdentityDbContext<ApplicationUser, ApplicationRole, int, ApplicationUserClaim,
        ApplicationUserRole, ApplicationUserLogin, ApplicationRoleClaim, ApplicationUserToken>
    {

        public TodoContext(DbContextOptions<TodoContext> options) : base(options)
        {

        }
        public DbSet<TodoEntity> TodoEntities { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            ApplyConfigurationsToModel(modelBuilder);
        }
        private void ApplyConfigurationsToModel(ModelBuilder modelBuilder)
        {
            #region Identity

            modelBuilder.ApplyConfiguration(new ApplicationUserConfiguration());
            modelBuilder.ApplyConfiguration(new ApplicationRoleConfiguration());
            modelBuilder.ApplyConfiguration(new ApplicationUserRoleConfiguration());
            modelBuilder.ApplyConfiguration(new ApplicationUserClaimConfiguration());
            modelBuilder.ApplyConfiguration(new ApplicationUserTokenConfiguration());
            modelBuilder.ApplyConfiguration(new ApplicationUserLoginConfiguration());
            modelBuilder.ApplyConfiguration(new ApplicationRoleClaimConfiguration());

            #endregion

            modelBuilder.ApplyConfiguration(new TodoConfiguration());
        }
    }
}
