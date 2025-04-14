using Moq;
using FluentAssertions;
using SF.Data.Repositories;
using SF.Model;
using SF.Data.Context; // For DapperContext
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace SF.ServerTests
{
    [TestFixture]
    public class RepositoryTests : IDisposable
    {
        private Mock<IHomeRepository<Home>> repositoryMock;
        private DbContextOptions<DapperContext> _options;
        private DapperContext _context;
        private int count = 100;
        private ILogger _logger;

        public RepositoryTests()
        {

            // Arrange: Set up the in-memory database in the constructor
            _options = (DbContextOptions<DapperContext>)new DbContextOptionsBuilder<DapperContext>()
                .UseInMemoryDatabase(databaseName: "HomesMockDbContext") //We can give any name to the InMemory database.
                .Options;
        }

        // Setup is called prior to each test
        [SetUp]
        public void Setup()
        {
            _logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .CreateLogger();


            _context = new DapperContext(this._options, _logger);
            List<Home> homes = [];

            _context.Database.EnsureCreated();

            // Add test data to the in-memory database
            this._context.Homes.AddRange(SeedTestData.GenerateFakeHomes(count));

            this._context.SaveChanges();
            repositoryMock = new Mock<IHomeRepository<Home>>();

        }
        [TearDown]
        public void Dispose()
        {
            _context.Dispose();
            _context.Database.EnsureDeleted();
        }

        [Test]
        public void GetByIdAsyncShouldReturnOneHome()
        {
            _logger.Information("GetByIdAsyncShouldReturnOneHome");

            // Arrange
            var Id = 1;
            repositoryMock.Setup(repo => repo.GetByIdAsync(Id))
                .ReturnsAsync(_context.Homes.FirstOrDefault<Home>(h => h.ID == Id));

            // Act
            // Use Result to block and wait for the async call to complete

            var home = repositoryMock.Object.GetByIdAsync(Id).Result;
            var address = _context.Homes.FirstOrDefault<Home>(h => h.ID == Id).Address;
            // Assert
            home.Should().NotBeNull();
            // count is 100
            home.Should().BeOfType<Home>();
            home.Address.Should().Be(address);
        }

        [Test]
        public async Task InsertAsyncReturnsHomeInserted()
        {
            // Arrange
            var newHome = new Home
            {
                ID = 101,
                Address = "123 Main St",
                City = "Anytown",
                State = "Anystate",
                ZipCode = 12345
            };

            // Act
            repositoryMock.Setup(repo => repo.InsertAsync(It.IsAny<Home>()))
                .ReturnsAsync(newHome);

            var insertedHome = await repositoryMock.Object.InsertAsync(newHome);
            // Assert

            insertedHome.Should().NotBeNull();
            insertedHome.Should().BeEquivalentTo(newHome);
        }

        [Test]
        public void UpdateAsyncReturns1Where1IsTheAmountOfDataAffected()
        {
            _logger.Information("UpdateAsyncReturns1Where1IsTheAmountOfDataAffected");

            // Arrange
            var updatedHome = SeedTestData.GenerateFakeHome();

            repositoryMock.Setup(repo => repo.UpdateAsync(updatedHome))
                .ReturnsAsync(updatedHome);

            // Act
            // Use Result to block and wait for the async call to complete
            var homes = repositoryMock.Object.UpdateAsync(updatedHome).Result; 

            var homeAffected = repositoryMock.Object.UpdateAsync(updatedHome).Result; 

            _logger.Information("{@HomesAffected}", homeAffected);

            // Assert
            homeAffected.Should().BeEquivalentTo(updatedHome);
        }

        [Test]
        public void DeleteAsyncReturns1Where1IsTheAmountOfDataAffected()
        {
            _logger.Information("DeleteAsyncReturns1Where1IsTheAmountOfDataAffected");

            // Arrange
            var Id = 1;
            // ReturnsAsync will return 1 as the number of rows affected
            repositoryMock.Setup(repo => repo.DeleteAsync(Id))
                .ReturnsAsync(1);

            // Act
            var homesAffected = repositoryMock.Object.DeleteAsync(Id).Result; // Use Result to block and wait for the async call to complete

            
            _logger.Information("{@HomesAffected}", homesAffected);

            // Assert
            homesAffected.Should().Be(1);
        }

        
    }
}