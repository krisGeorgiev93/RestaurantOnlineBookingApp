using Microsoft.EntityFrameworkCore;
using RestaurantOnlineBooking.Services.Data;
using RestaurantOnlineBookingApp.Data;
using RestaurantOnlineBookingApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantBookingApp.Services.Tests
{
    public class CategoryServiceTests
    {
        private RestaurantBookingDbContext _dbContext;
        private CategoryService _categoryService;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<RestaurantBookingDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            _dbContext = new RestaurantBookingDbContext(options);
            _categoryService = new CategoryService(this._dbContext);
        }

        [TearDown]
        public void TearDown()
        {
            _dbContext.Database.EnsureDeleted();
        }

        [Test]
        public async Task AllCategoryNamesAsync_ReturnsAllCategoryNames()
        {
            // Arrange
            var categories = new List<Category>
            {
                new Category { Name = "Category1" },
                new Category { Name = "Category2" },
                new Category { Name = "Category3" }
            };
            _dbContext.Categories.AddRange(categories);
            _dbContext.SaveChanges();

            // Act
            var result = await _categoryService.AllCategoryNamesAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(categories.Count, result.ToList().Count);
            foreach (var category in categories)
            {
                Assert.Contains(category.Name, result.ToList());
            }
        }

        [Test]
        public async Task ExistByIdAsync_CategoryExists_ReturnsTrue()
        {
            // Arrange
            var category = new Category { Id = 1, Name = "Test Category" };
            _dbContext.Categories.Add(category);
            _dbContext.SaveChanges();

            // Act
            var result = await _categoryService.ExistByIdAsync(1);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public async Task ExistByIdAsync_CategoryDoesNotExist_ReturnsFalse()
        {
            // Arrange

            // Act
            var result = await _categoryService.ExistByIdAsync(1);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public async Task GetAllCategoriesAsync_ReturnsAllCategories()
        {
            // Arrange
            var categories = new List<Category>
            {
                new Category { Id = 1, Name = "Category 1" },
                new Category { Id = 2, Name = "Category 2" },
                new Category { Id = 3, Name = "Category 3" }
            };
            _dbContext.Categories.AddRange(categories);
            _dbContext.SaveChanges();

            // Act
            var result = await _categoryService.GetAllCategoriesAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(categories.Count, result.Count());

            foreach (var category in categories)
            {
                var categoryModel = result.FirstOrDefault(c => c.Id == category.Id);
                Assert.IsNotNull(categoryModel);
                Assert.AreEqual(category.Name, categoryModel.Name);
            }
        }
    }
}
