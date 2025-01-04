using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using MiniECommerce.Core.DTOs;
using MiniECommerce.Core.Entities;
using MiniECommerce.DAL.Abstracts;
using MiniECommerce.Services.Concrates;

public class CategoryServiceTest
{
    private readonly Mock<ICategoryRepository> _categoryRepositoryMock;
    private readonly CategoryService _categoryService;

    public CategoryServiceTest()
    {
        _categoryRepositoryMock = new Mock<ICategoryRepository>();
        _categoryService = new CategoryService(_categoryRepositoryMock.Object);
    }

    [Fact]
    public async Task GetCategoriesAsync_ShouldReturnCategories()
    {
        var mockCategories = new List<Category>
        {
            new Category { Id = 1, Name = "Electronics", Description = "Electronic items" },
            new Category { Id = 2, Name = "Books", Description = "Books and novels" }
        };
        _categoryRepositoryMock.Setup(repo => repo.GetCategoriesAsync())
            .ReturnsAsync(mockCategories);

        var result = await _categoryService.GetCategoriesAsync();

        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
        Assert.Contains(result, c => c.Name == "Electronics");
        _categoryRepositoryMock.Verify(repo => repo.GetCategoriesAsync(), Times.Once);
    }

    [Fact]
    public async Task GetCategoryByIdAsync_ShouldReturnCategory_WhenCategoryExists()
    {
        var mockCategory = new Category { Id = 1, Name = "Electronics", Description = "Electronic items" };
        _categoryRepositoryMock.Setup(repo => repo.GetCategoryByIdAsync(1))
            .ReturnsAsync(mockCategory);

        var result = await _categoryService.GetCategoryByIdAsync(1);

        Assert.NotNull(result);
        Assert.Equal(mockCategory.Name, result.Name);
        Assert.Equal(mockCategory.Description, result.Description);
        _categoryRepositoryMock.Verify(repo => repo.GetCategoryByIdAsync(1), Times.Once);
    }

    [Fact]
    public async Task GetCategoryByIdAsync_ShouldThrowException_WhenCategoryDoesNotExist()
    {
        _categoryRepositoryMock.Setup(repo => repo.GetCategoryByIdAsync(It.IsAny<int>()))
            .ReturnsAsync((Category)null);

        await Assert.ThrowsAsync<Exception>(() => _categoryService.GetCategoryByIdAsync(999));
    }

    [Fact]
    public async Task AddCategoryAsync_ShouldCallRepositoryMethod()
    {
        var categoryDto = new CategoryDto { Name = "New Category", Description = "Category description" };

        await _categoryService.AddCategoryAsync(categoryDto);

        _categoryRepositoryMock.Verify(repo => repo.AddCategoryAsync(It.Is<Category>(c =>
            c.Name == categoryDto.Name && c.Description == categoryDto.Description)), Times.Once);
    }

    [Fact]
    public async Task UpdateCategoryAsync_ShouldCallRepositoryMethod()
    {
        var categoryDto = new CategoryDto { Id = 1, Name = "Updated Name", Description = "Updated Description" };

        await _categoryService.UpdateCategoryAsync(categoryDto);

        _categoryRepositoryMock.Verify(repo => repo.UpdateCategoryAsync(It.Is<Category>(c =>
            c.Id == categoryDto.Id && c.Name == categoryDto.Name && c.Description == categoryDto.Description)), Times.Once);
    }

    [Fact]
    public async Task DeleteCategoryAsync_ShouldCallRepositoryMethod()
    {
        var categoryId = 1;

        await _categoryService.DeleteCategoryAsync(categoryId);

        _categoryRepositoryMock.Verify(repo => repo.DeleteCategoryAsync(categoryId), Times.Once);
    }
}
