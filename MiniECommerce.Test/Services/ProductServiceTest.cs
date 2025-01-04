using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using MiniECommerce.Core.DTOs;
using MiniECommerce.Core.Entities;
using MiniECommerce.Services.Concrates;
using MiniECommerce.DAL.Abstracts;

public class ProductServiceTest
{
    private readonly Mock<IProductRepository> _productRepositoryMock;
    private readonly ProductService _productService;

    public ProductServiceTest()
    {
        _productRepositoryMock = new Mock<IProductRepository>();
        _productService = new ProductService(_productRepositoryMock.Object);
    }

    [Fact]
    public async Task CreateProductAsync_ShouldAddProduct()
    {
        var productDto = new ProductDto
        {
            Title = "Test Product",
            Description = "Test Description",
            Image = "test-image.jpg",
            CategoryId = 1,
            Price = 10.99m
        };

        await _productService.CreateProductAsync(productDto);

        _productRepositoryMock.Verify(repo => repo.AddProductAsync(It.Is<Product>(
            p => p.Title == "Test Product" &&
                 p.Description == "Test Description" &&
                 p.Image == "test-image.jpg" &&
                 p.CategoryId == 1 &&
                 p.Price == 10.99m
        )), Times.Once);
    }

    [Fact]
    public async Task UpdateProductAsync_ShouldUpdateProduct_WhenProductExists()
    {

        var existingProduct = new Product
        {
            Id = 1,
            Title = "Old Product",
            Description = "Old Description",
            Image = "old-image.jpg",
            CategoryId = 1,
            Price = 5.99m
        };

        var updatedProductDto = new ProductDto
        {
            Title = "Updated Product",
            Description = "Updated Description",
            Image = "updated-image.jpg",
            CategoryId = 2,
            Price = 15.99m
        };

        _productRepositoryMock.Setup(repo => repo.GetProductByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(existingProduct);

        await _productService.UpdateProductAsync(1, updatedProductDto);

        _productRepositoryMock.Verify(repo => repo.UpdateProductAsync(It.Is<Product>(
            p => p.Id == 1 &&
                 p.Title == "Updated Product" &&
                 p.Description == "Updated Description" &&
                 p.Image == "updated-image.jpg" &&
                 p.CategoryId == 2 &&
                 p.Price == 15.99m
        )), Times.Once);
    }

    [Fact]
    public async Task GetProductByIdAsync_ShouldReturnProductDto_WhenProductExists()
    {
        var existingProduct = new Product
        {
            Id = 1,
            Title = "Test Product",
            Description = "Test Description",
            Image = "test-image.jpg",
            Category = new Category { Name = "Test Category" },
            Price = 10.99m
        };

        _productRepositoryMock.Setup(repo => repo.GetProductByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(existingProduct);

        var result = await _productService.GetProductByIdAsync(1);

        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal("Test Product", result.Title);
        Assert.Equal("Test Description", result.Description);
        Assert.Equal("test-image.jpg", result.Image);
        Assert.Equal("Test Category", result.CategoryName);
        Assert.Equal(10.99m, result.Price);
    }

    [Fact]
    public async Task GetAllProductsAsync_ShouldReturnAllProducts()
    {
        var products = new List<Product>
        {
            new Product { Id = 1, Title = "Product 1", Price = 10 },
            new Product { Id = 2, Title = "Product 2", Price = 20 }
        };

        _productRepositoryMock.Setup(repo => repo.GetAllProductsAsync())
            .ReturnsAsync(products);

        var result = await _productService.GetAllProductsAsync();

        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
        Assert.Equal("Product 1", result.First().Title);
    }

    [Fact]
    public async Task DeleteProductAsync_ShouldDeleteProduct_WhenProductExists()
    {
        await _productService.DeleteProductAsync(1);

        _productRepositoryMock.Verify(repo => repo.DeleteProductAsync(1), Times.Once);
    }
}
