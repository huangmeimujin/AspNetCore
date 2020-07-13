using LocationService.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace LocationService.Repositorys
{
    public class LocationDbContext:DbContext
    {
        public LocationDbContext(DbContextOptions<LocationDbContext> options):base(options)
        {
           
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = Startup.Configuration.GetSection("ConnectionStrings:SqlServerConnection").Value;
            optionsBuilder.UseSqlServer(connectionString, providerOptions => providerOptions.CommandTimeout(60)).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
           

        }
        public DbSet<LocationRecord> locationRecords { get; set; } = null!;
       

    }

  

}
