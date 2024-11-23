using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using DL.Entites;
using DL.Shared;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DL.Data
{
    public class KRSDBContext : IdentityDbContext<AppUser>
    {
        public KRSDBContext(DbContextOptions<KRSDBContext> options) : base(options) { }


        public DbSet<building> building { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
  
        }
    }
}
