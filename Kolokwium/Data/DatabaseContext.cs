using Microsoft.EntityFrameworkCore;
using Kolokwium.Models;
using System.Collections.Generic;

namespace Kolokwium.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        public DbSet<Item> Items { get; set; }
        public DbSet<Character> Characters { get; set; }
        public DbSet<Backpack> Backpacks { get; set; }
        public DbSet<Title> Titles { get; set; }
        public DbSet<CharacterTitle> CharacterTitles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CharacterTitle>()
                .HasKey(ct => new { ct.CharacterId, ct.TitleId });

            modelBuilder.Entity<Backpack>()
                .HasKey(b => new { b.CharacterId, b.ItemId });

            // Sample Data
            modelBuilder.Entity<Character>().HasData(new List<Character>
            {
                new Character { Id = 1, FirstName = "John", LastName = "Yakuza", CurrentWeight = 43, MaxWeight = 200 }
            });

            modelBuilder.Entity<Item>().HasData(new List<Item>
            {
                new Item { Id = 1, Name = "Item1", Weight = 10 },
                new Item { Id = 2, Name = "Item2", Weight = 11 },
                new Item { Id = 3, Name = "Item3", Weight = 12 }
            });

            modelBuilder.Entity<Backpack>().HasData(new List<Backpack>
            {
                new Backpack { CharacterId = 1, ItemId = 1, Amount = 2 },
                new Backpack { CharacterId = 1, ItemId = 2, Amount = 1 },
                new Backpack { CharacterId = 1, ItemId = 3, Amount = 1 }
            });

            modelBuilder.Entity<Title>().HasData(new List<Title>
            {
                new Title { Id = 1, Name = "Title1" },
                new Title { Id = 2, Name = "Title2" },
                new Title { Id = 3, Name = "Title3" }
            });

            modelBuilder.Entity<CharacterTitle>().HasData(new List<CharacterTitle>
            {
                new CharacterTitle { CharacterId = 1, TitleId = 1, AcquiredAt = DateTime.Parse("2024-06-10T00:00:00") },
                new CharacterTitle { CharacterId = 1, TitleId = 2, AcquiredAt = DateTime.Parse("2024-06-09T00:00:00") },
                new CharacterTitle { CharacterId = 1, TitleId = 3, AcquiredAt = DateTime.Parse("2024-06-08T00:00:00") }
            });
        }
    }
}
