using BankSystem.Entities;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace BankSystem.Data
{
    public class AppDbContext (DbContextOptions<BankDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasQueryFilter(u => !u.IsDeleted); // soft delete support
        }
    }
}
