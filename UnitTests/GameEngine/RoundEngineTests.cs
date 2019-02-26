using NUnit.Framework;

using Crawl.GameEngine;
using Crawl.Models;
using Crawl.ViewModels;
using Crawl.Services;

using UnitTests.Models.Default;
using Xamarin.Forms.Mocks;
using UnitTests.Models;

namespace UnitTests.GameEngine
{
    [TestFixture]
    public class RoundEngineTests
    {
        [Test]
        public void RoundEngine_Instantiate_Should_Pass()
        {
            // Arrange

            // Act
            // Can create a new Round engine...
            var Actual = new RoundEngine();

            // Assert
            Assert.AreNotEqual(null, Actual, TestContext.CurrentContext.Test.Name);
        }

    }
}

