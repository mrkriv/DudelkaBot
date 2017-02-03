using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DudelkaBot.Models
{
    public class DudelkaContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Channel> Channels { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=dudelka;Trusted_Connection=True;");
        }
    }
    
    public class User
    {
        [Key]
        public int UserID { get; set; }
        public string Username { get; set; }
    }

    public class Channel
    {
        [Key]
        public int ChannelId { get; set; }
        public string Name { get; set; }
    }
}
