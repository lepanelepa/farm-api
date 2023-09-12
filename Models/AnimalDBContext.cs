using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace farm_api.Models
{
    public class AnimalDBContext : DbContext
    {
        public DbSet<Animal> Animals { get; set; }

        public AnimalDBContext(DbContextOptions<AnimalDBContext> options)
            : base(options)
        {

        }
    }
}
