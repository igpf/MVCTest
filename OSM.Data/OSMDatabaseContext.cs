using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OSM.Data.Configurations;
using OSM.Data.Entities;

namespace OSM.Data
{
    public class OSMDatabaseContext : DbContext
    {
        public OSMDatabaseContext() : base("OSMConnection")
        {

        }

        public DbSet<Users> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new UsersConfiguration());
        }
    }
}
