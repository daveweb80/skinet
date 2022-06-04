using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Infrastructure.Data
{
    public class StoreContext : DbContext
    {

        public StoreContext(DbContextOptions<StoreContext> options) : base(options)
        {
            if (options is null)
            {
                throw new ArgumentNullException(nameof(options));
            }
            if (Products is null)
            {
                throw new ArgumentNullException(nameof(options));
            }
        }

      //  static DbContextOptions getOptions()
     //   {
          
     //       DbContextOptionsBuilder<DbContext> options = new DbContextOptionsBuilder<DbContext>();
     //       options.UseSqlite("Data source=skinet.db");

    //        return options.Options;
    //    }
     //   public StoreContext() : base(getOptions())
     //   {
//
 //       }

        public DbSet<Product> Products { get; set; }
    }
}