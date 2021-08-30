using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.DataAccess.SQL
{
    public class DataContext:DbContext
    {
        public DataContext() : base("DefaultConnection")
            //we put the defaultConnection becuse in the web.config file we have instance of defaultConnection name that tell him wich db to connect
        {

        }
        public DbSet<Product> Product { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
    }
}
