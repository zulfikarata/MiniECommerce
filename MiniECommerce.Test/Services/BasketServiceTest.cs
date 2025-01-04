using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using MiniECommerce.Services.Concrates;
using MiniECommerce.Services.Abstracts;
using MiniECommerce.DAL.Abstracts;
using MiniECommerce.Core.Entities;
using MiniECommerce.Core.DTOs;

public class BasketServiceTest
{
    [Fact]
    public async Task GetBasketDetailsAsync_ShouldReturnBasketDto_WhenBasketExists()
    {
        var mockBasketRepository = new Mock<IBasketRepository>();

        var basketDto = new BasketDto
        {
            UserId = Guid.NewGuid(),
            Products = new List<ProductDto>
            {
                new ProductDto { Id = 1, Title = "Product 1", Price = 100, Image = "image1.jpg" }
            }
        };

        mockBasketRepository
            .Setup(repo => repo.GetBasketDetailsAsync(It.IsAny<Guid>()))
            .ReturnsAsync(new Basket
            {
                UserId = basketDto.UserId,
                Products = new List<Product>
                {
                    new Product { Id = 1, Title = "Product 1", Price = 100, Image = "image1.jpg" }
                }
            });

        var basketService = new BasketService(mockBasketRepository.Object);

        var result = await basketService.GetBasketDetailsAsync(Guid.NewGuid());

        Assert.NotNull(result);
        Assert.Equal(basketDto.UserId, result.UserId);
        Assert.Equal(basketDto.Products.Count, result.Products.Count);
        Assert.Equal(basketDto.Products[0].Id, result.Products[0].Id);
        Assert.Equal(basketDto.Products[0].Title, result.Products[0].Title);
        Assert.Equal(basketDto.Products[0].Price, result.Products[0].Price);
    }

    [Fact]
    public async Task AddProductToBasketAsync_ShouldCallAddProductToBasket_WhenProductIsAdded()
    {
        var mockBasketRepository = new Mock<IBasketRepository>();

        var userId = Guid.NewGuid();
        var productId = 1;

        var basketService = new BasketService(mockBasketRepository.Object);

        await basketService.AddProductToBasketAsync(userId, productId);

        mockBasketRepository.Verify(repo => repo.AddProductToBasketAsync(userId, productId), Times.Once);
    }

    [Fact]
    public async Task RemoveProductFromBasketAsync_ShouldCallRemoveProductFromBasket_WhenProductIsRemoved()
    {
        var mockBasketRepository = new Mock<IBasketRepository>();

        var userId = Guid.NewGuid();
        var productId = 1;

        var basketService = new BasketService(mockBasketRepository.Object);

        await basketService.RemoveProductFromBasketAsync(userId, productId);

        mockBasketRepository.Verify(repo => repo.RemoveProductFromBasketAsync(userId, productId), Times.Once);
    }
}
