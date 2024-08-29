using EF_CRUD_Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

    
namespace EF_CRUD_Application.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-HSSFLUB\\SQLEXPRESS;Initial Catalog=db_shispare;Integrated Security=True;Pooling=False;Encrypt=False");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("tbl_products");  // Specify table name

                entity.HasKey(e => e.Id);  // Maps to prod_id

                entity.Property(e => e.Id)
                      .HasColumnName("prod_id") // Maps to prod_id
                      .ValueGeneratedOnAdd(); // Auto-increment

                entity.Property(e => e.Name)
                      .HasColumnName("prod_name") // Maps to prod_name
                      .IsRequired();

                entity.Property(e => e.Price)
                      .HasColumnName("prod_price") // Maps to prod_price

                      .HasColumnType("int");
            });
        }
    }
}
