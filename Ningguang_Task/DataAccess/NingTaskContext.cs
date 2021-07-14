using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Ningguang.Models;

namespace Ningguang.DataAccess
{
    public class NingTaskContext:DbContext
    {

        public DbSet<NingItem> NingItems{ get; set; }
        public DbSet<NingTask> NingTasks{ get; set; }

        public NingTaskContext(DbContextOptions options) : base(options)
        {
        }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.LogTo(Console.WriteLine);
        }
        
    }
}