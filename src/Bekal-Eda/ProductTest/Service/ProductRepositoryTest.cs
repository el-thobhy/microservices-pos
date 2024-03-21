using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Product.Domain.Entity;
using Product.Domain.Repositories;
using ProductTest.MockData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace ProductTest.Service
{
    public class ProductRepositoryTest: IDisposable
    {
        protected readonly ProductDbContext _context;
        private readonly ITestOutputHelper _output;
        public ProductRepositoryTest(ITestOutputHelper output)
        {
            _output = output;
            var options = new DbContextOptionsBuilder<ProductDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            _context = new ProductDbContext(options);
            _context.Database.EnsureCreated();
        }


        [Fact]
        public async Task GetAllAsync_ReturnProductCollection()
        {
            //Arrange
            _context.Products.AddRange(ProductMockData.GetProduct());
            _context.SaveChanges();

            var virRepo = new ProductRepository(_context);
            //Act
            var result = await virRepo.GetAll();
            var count = ProductMockData.GetProduct().Count;
            _output.WriteLine($"Result {count}");

            //Assert
            result.Should().HaveCount(2);
        }
        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}
