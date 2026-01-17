using NUnit.Framework;
using Moq;
using Moq.Dapper;
using Dapper;
using System.Data;
using DotNetProjectForAntigravity.Services;
using DotNetProjectForAntigravity.Data;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace DotNetProjectForAntigravity.Tests.Services
{
    [TestFixture]
    public class DatabaseServiceTests
    {
        private Mock<IConfiguration> _mockConfig;
        private Mock<ISqlConnectionFactory> _mockFactory;
        private Mock<IDbConnection> _mockConnection;
        private DatabaseService _service;

        [SetUp]
        public void Setup()
        {
            _mockConfig = new Mock<IConfiguration>();
            _mockFactory = new Mock<ISqlConnectionFactory>();
            _mockConnection = new Mock<IDbConnection>();

            // Setup configuration
            var mockSection = new Mock<IConfigurationSection>();
            mockSection.Setup(s => s.Value).Returns("Server=test;");
            _mockConfig.Setup(c => c.GetSection("ConnectionStrings")).Returns(new Mock<IConfigurationSection>().Object);

            // Mock Open() (Synchronous, because Mock<IDbConnection> is not a DbConnection)
            _mockConnection.Setup(c => c.Open());

            _mockFactory.Setup(f => f.CreateConnection(It.IsAny<string>())).Returns(_mockConnection.Object);

            _service = new DatabaseService(_mockConfig.Object, _mockFactory.Object);
        }

        [Test]
        public async Task GetTablesAsync_ReturnsTables()
        {
            var expectedTables = new List<TableInfo> 
            { 
                new TableInfo { SchemaName = "dbo", TableName = "Users", RowCount = 100 } 
            };

            _mockConnection.SetupDapperAsync(c => c.QueryAsync<TableInfo>(It.IsAny<string>(), null, null, null, null))
                           .ReturnsAsync(expectedTables);

            var result = await _service.GetTablesAsync();

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result[0].TableName, Is.EqualTo("Users"));
        }

        [Test]
        public async Task GetIndexHealthAsync_ReturnsIndexes()
        {
            var expectedIndexes = new List<IndexInfo>
            {
                new IndexInfo { IndexName = "PK_Users", IndexType = "CLUSTERED", HealthStatus = "Good" }
            };

            _mockConnection.SetupDapperAsync(c => c.QueryAsync<IndexInfo>(It.IsAny<string>(), It.IsAny<object>(), null, null, null))
                           .ReturnsAsync(expectedIndexes);

            var result = await _service.GetIndexHealthAsync("dbo", "Users");

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result[0].IndexName, Is.EqualTo("PK_Users"));
        }

        [Test]
        public async Task GetStoredProceduresAsync_ReturnsProcedures()
        {
            var expectedProcs = new List<string> { "dbo.sp_GetUser" };

            _mockConnection.SetupDapperAsync(c => c.QueryAsync<string>(It.IsAny<string>(), null, null, null, null))
                           .ReturnsAsync(expectedProcs);

            var result = await _service.GetStoredProceduresAsync();

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result[0], Is.EqualTo("dbo.sp_GetUser"));
        }
    }
}
