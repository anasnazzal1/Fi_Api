using F_DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F_DAL.DATA
{
    public class ApplecationDbContext: DbContext
    {
        public DbSet<Catgry> catgries { get; set; }
        public ApplecationDbContext(DbContextOptions<ApplecationDbContext> options)
        : base(options)
        {
        }
    }
}
