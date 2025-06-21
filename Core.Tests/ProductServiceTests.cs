using Microsoft.EntityFrameworkCore;
using MyCompany.Test.Api.Exceptions;
using MyCompany.Test.Core.Entities;
using MyCompany.Test.Infrastructure.Models;

namespace Core.Tests
{
    public class ProductServiceTests : ProductServiceTestBase
    {
        [Fact]
        public async Task CreateAsync_ShouldAddProduct()
        {
            // Arrange  
            var productModel = new ProductModel
            {
                Name = "Test Product",
                Description = "Test Description",
                Price = 99.99m,
                CreatedAt = DateTime.UtcNow
            };
            // Act  
            var createdProduct = await productService.CreateAsync(productModel);
            // Assert  
            Assert.NotNull(createdProduct);
            Assert.Equal(productModel.Name, createdProduct.Name);
            Assert.Equal(productModel.Description, createdProduct.Description);
            Assert.Equal(productModel.Price, createdProduct.Price);
            //Assert.NotEqual(Guid.Empty, createdProduct.Id);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllProducts()
        {
            // Arrange  
            var product1 = new Product
            {
                Name = "Product 1",
                Description = "Description 1",
                Price = 10.00m,
                CreatedAt = DateTime.UtcNow,
                ProductId = Guid.NewGuid()
            };
            var product2 = new Product
            {
                Name = "Product 2",
                Description = "Description 2",
                Price = 20.00m,
                CreatedAt = DateTime.UtcNow,
                ProductId = Guid.NewGuid()
            };
            context.Product.AddRange(product1, product2);
            context.SaveChanges();
            // Act  
            var products = await productService.GetAllAsync();
            // Assert  
            Assert.NotEmpty(products);
            Assert.Equal(2, products.Count());
            Assert.Contains(products, p => p.Name == product1.Name && p.Price == product1.Price);
            Assert.Contains(products, p => p.Name == product2.Name && p.Price == product2.Price);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnProduct()
        {
            // Arrange  
            var product = new Product
            {
                Name = "Test Product",
                Description = "Test Description",
                Price = 50.00m,
                CreatedAt = DateTime.UtcNow,
                ProductId = Guid.NewGuid()
            };
            context.Product.Add(product);
            context.SaveChanges();
            // Act  
            var retrievedProduct = await productService.GetByIdAsync(product.ProductId);
            // Assert  
            Assert.NotNull(retrievedProduct);
            Assert.Equal(product.Name, retrievedProduct.Name);
            Assert.Equal(product.Description, retrievedProduct.Description);
            Assert.Equal(product.Price, retrievedProduct.Price);
        }

        [Fact]
        public async Task UpdateAsync_ShouldModifyProduct()
        {
            // Arrange  
            var product = new Product
            {
                Name = "Old Product",
                Description = "Old Description",
                Price = 30.00m,
                CreatedAt = DateTime.UtcNow,
                ProductId = Guid.NewGuid()
            };
            context.Product.Add(product);
            context.SaveChanges();
            var updatedModel = new ProductModel
            {
                Name = "Updated Product",
                Description = "Updated Description",
                Price = 40.00m,
                CreatedAt = DateTime.UtcNow
            };
            // Act  
            await productService.UpdateAsync(product.ProductId, updatedModel);
            // Assert  
            var updatedProduct = await productService.GetByIdAsync(product.ProductId);
            Assert.Equal(updatedModel.Name, updatedProduct.Name);
            Assert.Equal(updatedModel.Description, updatedProduct.Description);
            Assert.Equal(updatedModel.Price, updatedProduct.Price);
        }

        [Fact]
        public async Task DeleteAsync_ShouldRemoveProduct()
        {
            // Arrange  
            var productId = Guid.NewGuid();
            var product = new Product
            {
                Name = "Product to Delete",
                Description = "Description to Delete",
                Price = 25.00m,
                CreatedAt = DateTime.UtcNow,
                ProductId = productId
            };
            context.Product.Add(product);
            await context.SaveChangesAsync();
            // Act  
            await productService.DeleteAsync(productId);
            // Assert  
            var deletedProduct = await context.Product.FirstOrDefaultAsync(p => p.ProductId == productId);
            Assert.Null(deletedProduct);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldThrowException_WhenProductNotFound()
        {
            // Arrange  
            var nonExistentId = Guid.NewGuid();

            // Act & Assert  
            await Assert.ThrowsAsync<NotFoundException>(() => productService.GetByIdAsync(nonExistentId));
        }

        [Fact]
        public async Task UpdateAsync_ShouldThrowException_WhenProductNotFound()
        {
            // Arrange  
            var nonExistentId = Guid.NewGuid();
            var productModel = new ProductModel
            {
                Name = "Updated Product",
                Description = "Updated Description",
                Price = 50.00m,
                CreatedAt = DateTime.UtcNow
            };
            // Act & Assert  
            await Assert.ThrowsAsync<NotFoundException>(() => productService.GetByIdAsync(nonExistentId));
        }
    }
}
