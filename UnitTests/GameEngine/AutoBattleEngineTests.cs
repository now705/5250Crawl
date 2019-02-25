using NUnit.Framework;

using Crawl.GameEngine;
using Crawl.Models;
using Crawl.ViewModels;
using Crawl.Services;

using Xamarin.Forms.Mocks;

using UnitTests.Models.Default;

namespace UnitTests.GameEngine
{
    [TestFixture]
    public class AutoBattleEngineTests
    {

        /// <summary>
        /// Can Create a new Auto Battle Instace.  
        /// Constructor should not be null
        /// </summary>
        [Test]
        public void AutoBattleEngine_Instantiate_Should_Pass()
        {

            // Arrange

            // Act
            var Actual = new AutoBattleEngine();

            // Reset

            // Assert
            Assert.AreNotEqual(null, Actual, TestContext.CurrentContext.Test.Name);
        }

        /// <summary>
        /// Call for the Score Value
        /// Because the battle has not run, it will be the default value which is zero
        /// </summary>
        [Test]
        public void AutoBattleEngine_GetScoreValue_Should_Pass()
        {

            // Arrange
            var myEngine = new AutoBattleEngine();
            var Expected = 0;   // 1 because it is not defined yet

            // Act
            var Actual = myEngine.GetScoreValue();

            // Reset

            // Assert
            Assert.AreEqual(Expected, Actual, TestContext.CurrentContext.Test.Name);
        }

        /// <summary>
        /// Call for Get Rounds
        /// Because the battle has not run, it will be the default value which is zero
        /// </summary>
        [Test]
        public void AutoBattleEngine_GetRoundsValue_Should_Pass()
        {

            // Arrange
            var myEngine = new AutoBattleEngine();
            var Expected = 0;   // 1 because it is not defined yet

            // Act
            var Actual = myEngine.GetRoundsValue();

            // Reset

            // Assert
            Assert.AreEqual(Expected, Actual, TestContext.CurrentContext.Test.Name);
        }

        /// <summary>
        /// Call for Get Output
        /// Because the battle has not run, it will be the default value which empty string
        /// </summary>
        [Test]
        public void AutoBattleEngine_GetResultsOutput_Should_Pass()
        {

            // Arrange
            var myEngine = new AutoBattleEngine();

            // Act
            var Actual = myEngine.GetResultsOutput();

            // Reset

            // Assert
            Assert.NotNull(Actual, TestContext.CurrentContext.Test.Name);
        }


        /// <summary>
        /// Call for Get the Score Object
        /// Because the battle has not run, it will be the default value which empty string
        /// </summary>
        [Test]
        public void AutoBattleEngine_GetScoreObject_Should_Pass()
        {

            // Arrange
            var myEngine = new AutoBattleEngine();

            // Act
            var Actual = myEngine.GetScoreObject();

            // Reset

            // Assert
            Assert.NotNull(Actual, TestContext.CurrentContext.Test.Name);
        }

        /// <summary>
        /// Run Auto Battle
        /// No Characters will exit false
        /// </summary>
        [Test]
        public void AutoBattleEngine_RunAutoBattle_No_Characters_Should_Fail()
        {

            // Arrange
            MockForms.Init();

            var myEngine = new AutoBattleEngine();
            bool Expect = false;

            // Delete all Characters from the View Model
            CharactersViewModel.Instance.Dataset.Clear();

            // Act
            var Actual = myEngine.RunAutoBattle();

            // Reset
            CharactersViewModel.Instance.ReloadData();

            // Assert
            Assert.AreEqual(Expect,Actual, TestContext.CurrentContext.Test.Name);
        }

        /// <summary>
        /// Run Auto Battle
        /// With standard characters
        /// </summary>
        [Test]
        public void AutoBattleEngine_RunAutoBattle_Should_Pass()
        {

            // Arrange
            MockForms.Init();

            var myEngine = new AutoBattleEngine();
            bool Expect = true;
            
            // Act
            var Actual = myEngine.RunAutoBattle();

            // Assert
            Assert.AreEqual(Expect, Actual, TestContext.CurrentContext.Test.Name);
        }

    }
}