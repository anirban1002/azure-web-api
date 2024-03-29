﻿using Microsoft.EntityFrameworkCore;
using Redis.API.Entities;
using Redis.API.Persistence.Seed;

namespace Redis.API.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Add the books entity to be added in our database
        public DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Seed database with new data if its not already present
            modelBuilder.Seed();

            base.OnModelCreating(modelBuilder);
        }
    }
}
