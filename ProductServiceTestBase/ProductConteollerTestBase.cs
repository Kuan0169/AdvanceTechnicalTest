using Moq;
using MyCompany.Test.Api.Controllers;
using MyCompany.Test.Infrastructure.Services;

namespace Api.Tests
{
    public class ProductControllerTestBase
    {
        protected readonly Mock<IProductService> _mockService;
        protected readonly ProductController _controller;

        public ProductControllerTestBase()
        {
            _mockService = new Mock<IProductService>();
            _controller = new ProductController(_mockService.Object);
        }
    }
}