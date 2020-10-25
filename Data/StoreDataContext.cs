using API.NETcore2.Data.Maps;
using API.NETcore2.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.NETcore2.Data
{
    public class StoreDataContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public DbSet<Category> Categories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=localhost\SQLEXPRESS;Database=Desenv;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) //Criar o banco baseado com a propiedades(configuração) da classe Map
        {

            modelBuilder.ApplyConfiguration(new ProductMap());
            modelBuilder.ApplyConfiguration(new CategoryMap());

        }

      
    }
}
