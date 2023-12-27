using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp14
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext()
            : base("DbConnection")
        { }

        public DbSet<Preparat> Lekarstvos { get; set; }
        public  DbSet<MedInstrument> MedInstruments { get; set; }
        public DbSet<ListProviders> Providers { get; set; }
    }
   
}
