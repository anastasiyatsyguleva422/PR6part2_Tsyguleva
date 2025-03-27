using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using testing;

namespace testing
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base("ModernArtEntities") { }

        public DbSet<User> Users { get; set; }
    }
}