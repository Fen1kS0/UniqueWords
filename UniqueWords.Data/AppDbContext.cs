using System.IO;
using Microsoft.EntityFrameworkCore;
using UniqueWords.Data.Entities;

namespace UniqueWords.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Site> Sites { get; set; }
        
        public DbSet<Word> Words { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string path = Directory.GetCurrentDirectory();
            path = path.Remove(path.Length - 20);
            path += "Data";
            
            optionsBuilder.UseSqlite(@$"Data Source={path}\App.db");
        }
    }
}