using NUnit.Framework;
using DotNetProjectForAntigravity.Services;
using System.Threading.Tasks;

namespace DotNetProjectForAntigravity.Tests.Services
{
    [TestFixture]
    public class AIServiceTests
    {
        private AIService _service;

        [SetUp]
        public void Setup()
        {
            _service = new AIService();
        }

        [Test]
        public async Task OptimizeStoredProcedureAsync_ReturnsOptimizedString()
        {
            var input = "SELECT * FROM Table";
            var result = await _service.OptimizeStoredProcedureAsync(input);
            Assert.That(result, Does.Contain("AI Optimized Version"));
            Assert.That(result, Does.Contain(input));
        }

        [Test]
        public async Task GenerateQueryAsync_ReturnsSelectString()
        {
            var tables = "Users";
            var context = "Find all users";
            var result = await _service.GenerateQueryAsync(tables, context);
            Assert.That(result, Is.EqualTo($"SELECT * FROM {tables}"));
        }
    }
}
