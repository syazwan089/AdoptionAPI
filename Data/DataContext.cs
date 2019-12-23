using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdoptionApi.Models;

namespace AdoptionApi.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<AdoptionApi.Models.Users> Users { get; set; }
        public DbSet<AdoptionApi.Models.AdopItem> AdopItem { get; set; }
    }
}
