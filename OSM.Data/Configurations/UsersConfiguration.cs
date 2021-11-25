using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OSM.Data.Entities;

namespace OSM.Data.Configurations
{
    
    public class UsersConfiguration : EntityTypeConfiguration<Users>
    {
        public UsersConfiguration()
        {
            HasKey(p => p.Id);
            
            Property(p => p.Id)
                .HasColumnName("ID")
                .HasColumnType("NUMBER")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .IsRequired();

            Property(p => p.UserName)
                .HasColumnName("UserName")
                .HasColumnType("VARCHAR");


            Property(p => p.FirstName)
                .HasColumnName("FirstName")
                .HasColumnType("VARCHAR");

            Property(p => p.LastName)
                .HasColumnName("LastName")
                .HasColumnType("VARCHAR");

            Property(p => p.Email)
                .HasColumnName("Email")
                .HasColumnType("VARCHAR");

            Property(p => p.Phone)
                .HasColumnName("Phone")
                .HasColumnType("VARCHAR");

            Property(p => p.Passwd)
                .HasColumnName("Passwd")
                .HasColumnType("VARCHAR");

            Property(p => p.isActive)
                .HasColumnName("isActive")
                .HasColumnType("BIT");

            Property(p => p.isAdmin)
                .HasColumnName("isAdmin")
                .HasColumnType("BIT");

            ToTable("Users");
        }
    }
}
