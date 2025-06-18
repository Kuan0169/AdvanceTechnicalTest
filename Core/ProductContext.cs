using Microsoft.EntityFrameworkCore;
using MyCompany.Test.Core.Entities;
using System.Collections.Generic;

namespace MyCompany.Test.Core
{
    public class ProductContext : DbContext
    {
        public ProductContext(DbContextOptions<ProductContext> options) : base(options)
        {

        }
        public DbSet<Product> Product { get; set; }
    }
}
