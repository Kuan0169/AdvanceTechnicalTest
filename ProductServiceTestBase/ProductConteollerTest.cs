using Microsoft.AspNetCore.Mvc;
using Moq;
using MyCompany.Test.Api.Controllers;
using MyCompany.Test.Api.Exceptions;
using MyCompany.Test.Infrastructure.Models;

namespace Api.Tests
{
    public class ProductControllerTests : ProductControllerTestBase
    {
        [Fact]
        public async Task GetAll_ReturnsOk_WithProductList()
        {
            // Arrange
            var products = new List<ProductDto>
            {
                new ProductDto {
                    Id = Guid.NewGuid(), Name = "P1",
                    Price = 1,
                    CreatedAt = DateTime.UtcNow
                }
            };
            _mockService.Setup(s => s.GetAllAsync()).ReturnsAsync(products);

            // Act
            var result = await _controller.GetAllAsync();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returned = Assert.IsAssignableFrom<IEnumerable<ProductDto>>(okResult.Value);
            Assert.Single(returned);
        }

        [Fact]
        public async Task GetById_ExistingId_ReturnsOk()
        {
            // Arrange
            var id = Guid.NewGuid();
            var dto = new ProductDto
            {
                Id = id,
                Name = "Product",
                Price = 99,
                CreatedAt = DateTime.UtcNow
            };
            _mockService.Setup(s => s.GetByIdAsync(id)).ReturnsAsync(dto);

            // Act
            var result = await _controller.GetById(id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returned = Assert.IsType<ProductDto>(okResult.Value);
            Assert.Equal(id, returned.Id);
        }

        [Fact]
        public async Task GetById_NonExistingId_ThrowsNotFoundException()
        {
            // Arrange
            var nonExistentId = Guid.NewGuid();

            _mockService
                .Setup(s => s.GetByIdAsync(nonExistentId))
                             .ThrowsAsync(new NotFoundException("Product not found"));

            // Act & Assert
            var ex = await Assert.ThrowsAsync<NotFoundException>(() =>
                _controller.GetById(nonExistentId));

            Assert.Equal("Product not found", ex.Message);
        }

        [Fact]
        public async Task Create_ValidProduct_ReturnsCreated()
        {
            // Arrange
            var productModel = new ProductModel
            {
                Id = Guid.NewGuid(),
                Name = "New Product",
                Price = 10,
                Description = "Desc",
                CreatedAt = DateTime.UtcNow
            };

            var productDto = new ProductDto
            {
                Id = Guid.NewGuid(),
                Name = productModel.Name,
                Price = productModel.Price,
                Description = productModel.Description,
                CreatedAt = productModel.CreatedAt
            };
            _mockService.Setup(s => s.CreateAsync(productModel)).ReturnsAsync(productDto);

            // Act
            var result = await _controller.Create(productModel);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            var returned = Assert.IsType<ProductDto>(createdResult.Value);
            Assert.Equal(productDto.Id, returned.Id);
        }

        [Fact]
        public async Task Create_InvalidModel_ReturnsBadRequest()
        {
            // Arrange
            var productModel = new ProductModel
            {
                Name = "Invalid Product",
                Price = -10, // Invalid price
                Description = "Invalid Desc",
                CreatedAt = DateTime.UtcNow
            };
            _controller.ModelState.AddModelError("Price", "Price must be a positive value");
            // Act
            var result = await _controller.Create(productModel);
            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, badRequestResult.StatusCode);
        }

        [Fact]
        public async Task Update_ExistingProduct_ReturnsNoContent()
        {
            // Arrange
            var id = Guid.NewGuid();
            var model = new ProductModel
            {
                Name = "Updated",
                Price = 88,
                Description = "Updated Desc",
                CreatedAt = DateTime.UtcNow
            };

            _mockService.Setup(s => s.UpdateAsync(id, model)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.UpdateAsync(id, model);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task UpdateAsync_ProductNotFound_ThrowsException()
        {
            var id = Guid.NewGuid();
            var model = new ProductModel
            {
                Name = "Updated",
                Price = 88,
                Description = "Updated Desc",
                CreatedAt = DateTime.UtcNow
            };
            _mockService
                .Setup(s => s.UpdateAsync(id, model))
                .ThrowsAsync(new NotFoundException("Product not found"));

            // Act & Assert
            var ex = await Assert.ThrowsAsync<NotFoundException>(() =>
                _controller.UpdateAsync(id, model));

            Assert.Equal("Product not found", ex.Message);
        }

        [Fact]
        public async Task Delete_ExistingProduct_ReturnsNoContent()
        {
            // Arrange
            var id = Guid.NewGuid();
            _mockService.Setup(s => s.DeleteAsync(id)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.DeleteAsync(id);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Delete_NonExistingProduct_ThrowsNotFoundException()
        {
            // Arrange
            var id = Guid.NewGuid();
            _mockService.Setup(s => s.DeleteAsync(id))
                        .ThrowsAsync(new NotFoundException("Product not found"));

            // Act & Assert
            var ex = await Assert.ThrowsAsync<NotFoundException>(() =>
                _controller.DeleteAsync(id));

            Assert.Equal("Product not found", ex.Message);
        }
    }
}
