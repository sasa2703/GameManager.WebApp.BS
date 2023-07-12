using GameManager.WebApp.BS.Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameManager.WebApp.BS.Repository
{
    public partial class RepositoryContext : DbContext
    {
        public RepositoryContext(): base()
        {
        }

        public RepositoryContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Chinook");
            }
        }
        public DbSet<Game> Game { get; set; }
        public DbSet<ApiAccessToken> ApiAccessToken { get; set; }
        public DbSet<Device> Device { get; set; }
        public DbSet<GameCategory> GameCategory { get; set; }
        public DbSet<GameCollection> GameCollection { get; set; }
        public DbSet<Subscription> Subscription { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet <UserCategory> UserCategory { get; set; }
        public DbSet <UserStatus> UserStatus { get; set; }


    }
}

