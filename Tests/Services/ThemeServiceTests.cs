using NUnit.Framework;
using DotNetProjectForAntigravity.Services;

namespace DotNetProjectForAntigravity.Tests.Services
{
    [TestFixture]
    public class ThemeServiceTests
    {
        private ThemeService _service;

        [SetUp]
        public void Setup()
        {
            _service = new ThemeService();
        }

        [Test]
        public void CurrentTheme_DefaultsToLight()
        {
            Assert.That(_service.CurrentTheme, Is.EqualTo("light"));
        }

        [Test]
        public void ToggleTheme_SwitchesFromLightToDark()
        {
            _service.ToggleTheme();
            Assert.That(_service.CurrentTheme, Is.EqualTo("dark"));
        }

        [Test]
        public void ToggleTheme_SwitchesFromDarkToLight()
        {
            _service.CurrentTheme = "dark";
            _service.ToggleTheme();
            Assert.That(_service.CurrentTheme, Is.EqualTo("light"));
        }

        [Test]
        public void OnThemeChanged_InvokesWhenThemeChanges()
        {
            bool eventRaised = false;
            _service.OnThemeChanged += () => eventRaised = true;
            
            _service.CurrentTheme = "dark";
            
            Assert.That(eventRaised, Is.True);
        }

        [Test]
        public void OnThemeChanged_DoesNotInvokeWhenThemeUnchanged()
        {
            bool eventRaised = false;
            _service.OnThemeChanged += () => eventRaised = true;
            
            _service.CurrentTheme = "light"; // Same as default
            
            Assert.That(eventRaised, Is.False);
        }
    }
}
