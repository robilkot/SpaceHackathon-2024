﻿using Microsoft.EntityFrameworkCore;
using SpaceHackathon_2024.Models;
using System.Diagnostics;

namespace SpaceHackathon_2024.Services
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext()
        {
            Database.EnsureCreated();
        }

        public DbSet<User> Users => Set<User>();

        public DbSet<News> News => Set<News>();

        public DbSet<Event> Events => Set<Event>();

        public DbSet<StoreItem> StoreItems => Set<StoreItem>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var sqlitePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"SpaceHakathon");
            Debug.WriteLine(sqlitePath);
            Directory.CreateDirectory(sqlitePath); var fileName = $"{sqlitePath}\fsh.db";
            if (!File.Exists(fileName))
                File.Create(fileName);

            optionsBuilder.UseSqlite($"Data Source={fileName}");
        }

        public async Task<List<News>> GetNewsAsync(int pageNumber, int pageSize)
        {
            int skipCount = (pageNumber - 1) * pageSize;

            var newsItems = await News
                .OrderByDescending(n => n.PublishDate)
                .Skip(skipCount)
                .Take(pageSize)
                .ToListAsync();

            return newsItems;
        }

        public async Task<List<StoreItem>> GetStoreItemsAsync(int pageNumber, int pageSize)
        {
            int skipCount = (pageNumber - 1) * pageSize;

            var storeItems = await StoreItems
                .OrderByDescending(n => n.Name)
                .Skip(skipCount)
                .Take(pageSize)
                .ToListAsync();

            return storeItems;
        }
    }
}