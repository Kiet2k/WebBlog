using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace razorweb.Models
{
   public class MyblogContext : IdentityDbContext<AppUser>
   {
        public MyblogContext(DbContextOptions<MyblogContext> options) : base(options)
        {
            //...
        }
        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            base.OnConfiguring(builder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder )
        {
            base.OnModelCreating(modelBuilder);

            foreach (var entitytype in modelBuilder.Model.GetEntityTypes())
            {
                var tableName =  entitytype.GetTableName();
                if(tableName.StartsWith("AspNet"))
                {
                    entitytype.SetTableName(tableName.Substring(6));

                }
            }
        }
        public DbSet<Acticle> articles {get;set;}
   }
}