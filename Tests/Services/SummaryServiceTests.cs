using NUnit.Framework;
using Moq;
using Microsoft.AspNetCore.Hosting;
using DotNetProjectForAntigravity.Services;
using DotNetProjectForAntigravity.Data;
using System.Text.Json;

namespace DotNetProjectForAntigravity.Tests.Services
{
    [TestFixture]
    public class SummaryServiceTests
    {
        private Mock<IWebHostEnvironment> _mockEnvironment;
        private SummaryService _service;
        private string _testDir;

        [SetUp]
        public void Setup()
        {
            _testDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            _mockEnvironment = new Mock<IWebHostEnvironment>();
            _mockEnvironment.Setup(m => m.ContentRootPath).Returns(_testDir);

            _service = new SummaryService(_mockEnvironment.Object);
        }

        [TearDown]
        public void TearDown()
        {
            if (Directory.Exists(_testDir))
            {
                Directory.Delete(_testDir, true);
            }
        }

        [Test]
        public async Task SaveSummaryAsync_CreatesFile()
        {
            var summary = new DailySummary
            {
                Id = "test-1",
                ExecutionDate = DateTime.Now,
                Status = "Success",
                TotalTests = 10,
                PassedTests = 10
            };

            await _service.SaveSummaryAsync(summary);

            var files = Directory.GetFiles(Path.Combine(_testDir, "Data", "Summaries"), "summary_*.json");
            Assert.That(files.Length, Is.EqualTo(1));
            
            var json = File.ReadAllText(files[0]);
            var savedSummary = JsonSerializer.Deserialize<DailySummary>(json, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            Assert.That(savedSummary.Id, Is.EqualTo(summary.Id));
        }

        [Test]
        public async Task GetAllSummariesAsync_ReturnsStoredSummaries()
        {
            var summary1 = new DailySummary { Id = "1", ExecutionDate = DateTime.Now.AddDays(-1) };
            var summary2 = new DailySummary { Id = "2", ExecutionDate = DateTime.Now };

            await _service.SaveSummaryAsync(summary1);
            await _service.SaveSummaryAsync(summary2);

            var result = await _service.GetAllSummariesAsync();

            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result[0].Id, Is.EqualTo("2")); // Should be ordered by date descending
            Assert.That(result[1].Id, Is.EqualTo("1"));
        }

        [Test]
        public async Task GetLatestSummaryAsync_ReturnsMostRecent()
        {
            var summary1 = new DailySummary { Id = "1", ExecutionDate = DateTime.Now.AddDays(-1) };
            var summary2 = new DailySummary { Id = "2", ExecutionDate = DateTime.Now };

            await _service.SaveSummaryAsync(summary1);
            await _service.SaveSummaryAsync(summary2);

            var result = await _service.GetLatestSummaryAsync();

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo("2"));
        }

        [Test]
        public async Task GetStatisticsAsync_CalculatesCorrectStats()
        {
            var summaries = new List<DailySummary>
            {
                new DailySummary { Id = "1", Status = "Success", TotalTests = 10, PassedTests = 8, ExecutionDate = DateTime.Now.AddDays(-1) },
                new DailySummary { Id = "2", Status = "Failed", TotalTests = 10, PassedTests = 5, ExecutionDate = DateTime.Now }
            };

            foreach (var s in summaries) await _service.SaveSummaryAsync(s);

            var stats = await _service.GetStatisticsAsync();

            Assert.That(stats["TotalRuns"], Is.EqualTo(2));
            Assert.That(stats["SuccessfulRuns"], Is.EqualTo(1));
            Assert.That(stats["FailedRuns"], Is.EqualTo(1));
            Assert.That(stats["AverageTestsPerRun"], Is.EqualTo(10.0));
            Assert.That(stats["AveragePassRate"], Is.EqualTo(65.0)); // (8+5)/(10+10) * 100 = 13/20 * 100 = 65
        }

        [Test]
        public async Task GetSummaryByIdAsync_ReturnsCorrectSummary()
        {
            var summary1 = new DailySummary { Id = "1", ExecutionDate = DateTime.Now.AddDays(-1) };
            var summary2 = new DailySummary { Id = "2", ExecutionDate = DateTime.Now };

            await _service.SaveSummaryAsync(summary1);
            await _service.SaveSummaryAsync(summary2);

            var result = await _service.GetSummaryByIdAsync("1");

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo("1"));
        }

        [Test]
        public async Task GetSummariesByDateRangeAsync_ReturnsSummariesInRange()
        {
            var now = DateTime.Now;
            var summary1 = new DailySummary { Id = "1", ExecutionDate = now.AddDays(-5) };
            var summary2 = new DailySummary { Id = "2", ExecutionDate = now.AddDays(-2) };
            var summary3 = new DailySummary { Id = "3", ExecutionDate = now };

            await _service.SaveSummaryAsync(summary1);
            await _service.SaveSummaryAsync(summary2);
            await _service.SaveSummaryAsync(summary3);

            var result = await _service.GetSummariesByDateRangeAsync(now.AddDays(-3), now.AddDays(-1));

            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result[0].Id, Is.EqualTo("2"));
        }
    }
}
