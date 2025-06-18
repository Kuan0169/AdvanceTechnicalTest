using Microsoft.EntityFrameworkCore;
using MyCompany.Test.Core;
using MyCompany.Test.Infrastructure.Services;

namespace Core.Tests
{
    public class ProductServiceTestBase
    {
        protected readonly IProductService productService;
        protected readonly ProductContext context;

        public ProductServiceTestBase()
        {
            var options = new DbContextOptionsBuilder<ProductContext>()
               .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            context = new ProductContext(options);
            productService = new ProductService(context);
        }
    }
}
