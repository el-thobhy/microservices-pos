using FluentAssertions;
using Framework.Core.Enums;
using LookUp.Domain.Entities;
using LookUp.Domain.Repositories;
using LookUp.Test.MockData;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text.Json.Nodes;
using Xunit.Abstractions;

namespace LookUp.Test.Services
{
    public class AttributeRepositoryTest: IDisposable
    {
        protected readonly LookUpDbContext _context;
        private readonly ITestOutputHelper _output;
        public AttributeRepositoryTest(ITestOutputHelper output)
        {
            _output = output;
            var options = new DbContextOptionsBuilder<LookUpDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            _context = new LookUpDbContext(options);
            _context.Database.EnsureCreated();
        }

        [Fact]
        public async Task GetAllAsync_ReturnAttributeCollection()
        {
            //Arrange
            _context.Attributes.AddRange(AttributeMockData.GetAttributes());
            _context.SaveChanges();

            var virRepo = new AttributeRepositories(_context);
            //Act
            var result = await virRepo.GetAll();
            var count = AttributeMockData.GetAttributes().Count;
            _output.WriteLine($"Result {count}");

            //Assert
            result.Should().HaveCount(2);
        }

        [Fact]
        public async Task Add_ReturnAttributeCollection()
        {
            //Arrange
            _context.Attributes.AddRange(AttributeMockData.GetAttributes());
            _context.SaveChanges();

            var virRepo = new AttributeRepositories(_context);
            var newEntity = new AttributesEntity()
            {
                Id = new Guid("DA184DCB-54A5-4C0B-BC80-DCD255C2287D"),
                Type = AttributeTypeEnum.Decimal,
                Unit = "Liter",
                Status = RecordStatusEnum.Active
            };

            //Act
            var result = await virRepo.Add(newEntity);
            _context.SaveChanges();

            _output.WriteLine($"Result {_context.Attributes.Count()}");

            //Assert
            result?.Id.Should().Be(newEntity.Id);
        }

        [Fact]
        public async Task GetById_ReturnAttributeCollection()
        {
            //Arrange
            _context.Attributes.AddRange(AttributeMockData.GetAttributes());
            _context.SaveChanges();

            var virRepo = new AttributeRepositories(_context);
            
            //Act
            var entity = await virRepo.GetById(new Guid("CBB7C8F1-BC5B-47A9-8957-BF08E013447F"));
            _output.WriteLine($"Result {entity?.Unit}");

            //assert
            entity?.Unit.Should().Be("Pieces");

        }


        [Fact]
        public async Task Update_ReturnAttributeCollection()
        {
            //Arrange
            _context.Attributes.AddRange(AttributeMockData.GetAttributes());
            _context.SaveChanges();

            var virRepo = new AttributeRepositories(_context);
            
            var result = await virRepo.GetById(new Guid("CBB7C8F1-BC5B-47A9-8957-BF08E013447F"));

            result = new AttributesEntity()
            {
                Type = AttributeTypeEnum.Decimal,
                Unit = "Mililiter",
                Status = RecordStatusEnum.Inactive
            };
            //Act
            var entity = await virRepo.Update(result);

            _output.WriteLine($"Result {result?.Equals(entity)}");

            //assert
            result?.Equals(entity);

        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }


    }
}
