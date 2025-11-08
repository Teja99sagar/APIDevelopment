using Microsoft.EntityFrameworkCore;
using MusicApi.Models;
using System;
using System.Collections.Generic;




namespace MusicApi.Data
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext>options) : base(options)
        {
            
        }

        public DbSet<Song> Songs { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Song>().HasData(
                new Song
                {
                    Id = 1,
                    title = "D",
                    Language="E",
                    Duration="3.34"


                });
        }
    }
}
