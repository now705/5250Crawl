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
    public class BattleEngineTests
    {
        [Test]
        public void BattleEngine_Instantiate_Should_Pass()
        {
            // Can create a new battle engine...
            var Actual = new BattleEngine();
            Assert.AreNotEqual(null, Actual, TestContext.CurrentContext.Test.Name);
        }
        
        [Test]
        public void BattleEngine_AddCharactersToBattle_With_6_Characters_Should_Skip()
        {
            MockForms.Init();

            // Can create a new battle engine...
            var myBattleEngine = new BattleEngine();

            myBattleEngine.CharacterList.Add(new Character());
            myBattleEngine.CharacterList.Add(new Character());
            myBattleEngine.CharacterList.Add(new Character());
            myBattleEngine.CharacterList.Add(new Character());
            myBattleEngine.CharacterList.Add(new Character());
            myBattleEngine.CharacterList.Add(new Character());

            var Return = myBattleEngine.AddCharactersToBattle();

            var Actual = myBattleEngine.CharacterList.Count;
            var Expected = 6;

            Assert.AreEqual(true, Return, " Pass Fail " + TestContext.CurrentContext.Test.Name);
            Assert.AreEqual(Expected, Actual, TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void BattleEngine_AddCharactersToBattle_With_Empty_CharacterListView_Should_Fail()
        {
            MockForms.Init();

            // Can create a new battle engine...
            var myBattleEngine = new BattleEngine();

            // Clear the dataset...
            CharactersViewModel.Instance.Dataset.Clear();

            var Return = myBattleEngine.AddCharactersToBattle();

            // Reset
            MasterDataStore.ForceDataRestoreAll();

            Assert.AreEqual(false, Return, TestContext.CurrentContext.Test.Name);
        }
    }
}