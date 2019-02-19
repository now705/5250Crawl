using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Crawl.Models;
using Crawl.ViewModels;

namespace Crawl.Services
{
    public sealed class MockDataStore : IDataStore
    {

        // Make this a singleton so it only exist one time because holds all the data records in memory
        private static MockDataStore _instance;

        public static MockDataStore Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new MockDataStore();
                }
                return _instance;
            }
        }

        private List<Item> _itemDataset = new List<Item>();
        private List<Character> _characterDataset = new List<Character>();
        private List<Monster> _monsterDataset = new List<Monster>();
        private List<Score> _scoreDataset = new List<Score>();

        private MockDataStore()
        {
            InitilizeSeedData();
        }

        private async void InitilizeSeedData()
        {

            // Load Items.
            await AddAsync_Item(new Item("Mock Gold Sword", "Sword made of Gold, really expensive looking",
                "http://www.clker.com/cliparts/e/L/A/m/I/c/sword-md.png", 0, 10, 10, ItemLocationEnum.PrimaryHand, AttributeEnum.Defense));

            await AddAsync_Item(new Item("Mock Strong Shield", "Enough to hide behind",
                "http://www.clipartbest.com/cliparts/4T9/LaR/4T9LaReTE.png", 0, 10, 0, ItemLocationEnum.OffHand, AttributeEnum.Attack));

            await AddAsync_Item(new Item("Mock Bunny Hat", "Pink hat with fluffy ears",
                "http://www.clipartbest.com/cliparts/yik/e9k/yike9kMyT.png", 0, 10, -1, ItemLocationEnum.Head, AttributeEnum.Speed));

            // Characters
            await AddAsync_Character(new Character { Id = Guid.NewGuid().ToString(), Name = "Mock First Character", Description = "This is an Character description.", Level = 1 });
            await AddAsync_Character(new Character { Id = Guid.NewGuid().ToString(), Name = "Mock Second Character", Description = "This is an Character description.", Level = 1 });
            await AddAsync_Character(new Character { Id = Guid.NewGuid().ToString(), Name = "Mock Third Character", Description = "This is an Character description.", Level = 2 });
            await AddAsync_Character(new Character { Id = Guid.NewGuid().ToString(), Name = "Mock Fourth Character", Description = "This is an Character description.", Level = 2 });
            await AddAsync_Character(new Character { Id = Guid.NewGuid().ToString(), Name = "Mock Fifth Character", Description = "This is an Character description.", Level = 3 });
            await AddAsync_Character(new Character { Id = Guid.NewGuid().ToString(), Name = "Mock Sixth Character", Description = "This is an Character description.", Level = 3 });

            // Monsters
            await AddAsync_Monster(new Monster { Id = Guid.NewGuid().ToString(), Name = "Mock First Monster", Description = "This is an Monster description." });
            await AddAsync_Monster(new Monster { Id = Guid.NewGuid().ToString(), Name = "Mock Second Monster", Description = "This is an Monster description." });
            await AddAsync_Monster(new Monster { Id = Guid.NewGuid().ToString(), Name = "Mock Third Monster", Description = "This is an Monster description." });
            await AddAsync_Monster(new Monster { Id = Guid.NewGuid().ToString(), Name = "Mock Fourth Monster", Description = "This is an Monster description." });
            await AddAsync_Monster(new Monster { Id = Guid.NewGuid().ToString(), Name = "Mock Fifth Monster", Description = "This is an Monster description." });
            await AddAsync_Monster(new Monster { Id = Guid.NewGuid().ToString(), Name = "Mock Sixth Monster", Description = "This is an Monster description." });

            await AddAsync_Score(new Score { Id = Guid.NewGuid().ToString(), Name = "Mock  First Score", ScoreTotal = 111 });
            await AddAsync_Score(new Score { Id = Guid.NewGuid().ToString(), Name = "Mock  Second Score", ScoreTotal = 222 });
            await AddAsync_Score(new Score { Id = Guid.NewGuid().ToString(), Name = "Mock  Third Score", ScoreTotal = 333 });
            await AddAsync_Score(new Score { Id = Guid.NewGuid().ToString(), Name = "Mock  Fourth Score", ScoreTotal = 444 });
            await AddAsync_Score(new Score { Id = Guid.NewGuid().ToString(), Name = "Mock  Fifth Score", ScoreTotal = 555 });
            await AddAsync_Score(new Score { Id = Guid.NewGuid().ToString(), Name = "Mock  Sixth Score", ScoreTotal = 666 });
        }

        private void CreateTables()
        {
            // Do nothing...
        }

        // Delete the Datbase Tables by dropping them
        public void DeleteTables()
        {
            _characterDataset.Clear();
            _itemDataset.Clear();
            _monsterDataset.Clear();
        }

        // Tells the View Models to update themselves.
        private void NotifyViewModelsOfDataChange()
        {
            ItemsViewModel.Instance.SetNeedsRefresh(true);
            MonstersViewModel.Instance.SetNeedsRefresh(true);
            CharactersViewModel.Instance.SetNeedsRefresh(true);
            ScoresViewModel.Instance.SetNeedsRefresh(true);
        }

        public void InitializeDatabaseNewTables()
        {
            DeleteTables();

            // make them again
            CreateTables();

            // Populate them
            InitilizeSeedData();

            // Tell View Models they need to refresh
            NotifyViewModelsOfDataChange();
        }

        #region Item
        // Item
        public async Task<bool> InsertUpdateAsync_Item(Item data)
        {
            // Check to see if the item exist
            var oldData = await GetAsync_Item(data.Id);
            if (oldData == null)
            {
                await AddAsync_Item(data);
                return true;
            }

            // Compare it, if different update in the DB
            var UpdateResult = await UpdateAsync_Item(data);
            if (UpdateResult)
            {
                await AddAsync_Item(data);
                return true;
            }

            return false;
        }

        public async Task<bool> AddAsync_Item(Item data)
        {
            _itemDataset.Add(data);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateAsync_Item(Item data)
        {
            var myData = _itemDataset.FirstOrDefault(arg => arg.Id == data.Id);
            if (myData == null)
            {
                return false;
            }

            myData.Update(data);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteAsync_Item(Item data)
        {
            var myData = _itemDataset.FirstOrDefault(arg => arg.Id == data.Id);
            _itemDataset.Remove(myData);

            return await Task.FromResult(true);
        }

        public async Task<Item> GetAsync_Item(string id)
        {
            return await Task.FromResult(_itemDataset.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Item>> GetAllAsync_Item(bool forceRefresh = false)
        {
            return await Task.FromResult(_itemDataset);
        }

        #endregion Item

        #region Character
        // Character
        public async Task<bool> AddAsync_Character(Character data)
        {
            // Check to see if the item exist
            var oldData = await GetAsync_Character(data.Id);
            if (oldData == null)
            {
                _characterDataset.Add(data);
                return true;
            }

            // Compare it, if different update in the DB
            var UpdateResult = await UpdateAsync_Character(data);
            if (UpdateResult)
            {
                await AddAsync_Character(data);
                return true;
            }

            return false;
        }

        public async Task<bool> InsertUpdateAsync_Character(Character data)
        {

            // Check to see if the item exist
            var oldData = await GetAsync_Character(data.Id);
            if (oldData == null)
            {
                _characterDataset.Add(data);
                return true;
            }

            // Compare it, if different update in the DB
            var UpdateResult = await UpdateAsync_Character(data);
            if (UpdateResult)
            {
                await AddAsync_Character(data);
                return true;
            }

            return false;
        }

        public async Task<bool> UpdateAsync_Character(Character data)
        {
            var myData = _characterDataset.FirstOrDefault(arg => arg.Id == data.Id);
            if (myData == null)
            {
                return false;
            }

            myData.Update(data);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteAsync_Character(Character data)
        {
            var myData = _characterDataset.FirstOrDefault(arg => arg.Id == data.Id);
            _characterDataset.Remove(myData);

            return await Task.FromResult(true);
        }

        public async Task<Character> GetAsync_Character(string id)
        {
            return await Task.FromResult(_characterDataset.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Character>> GetAllAsync_Character(bool forceRefresh = false)
        {
            return await Task.FromResult(_characterDataset);
        }
        #endregion Character

        #region Monster
        //Monster
        public async Task<bool> InsertUpdateAsync_Monster(Monster data)
        {
            // Check to see if the item exist
            var oldData = await GetAsync_Monster(data.Id);
            if (oldData == null)
            {
                _monsterDataset.Add(data);
                return true;
            }

            // Compare it, if different update in the DB
            var UpdateResult = await UpdateAsync_Monster(data);
            if (UpdateResult)
            {
                await AddAsync_Monster(data);
                return true;
            }

            return false;
        }

        public async Task<bool> AddAsync_Monster(Monster data)
        {
            _monsterDataset.Add(data);
            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateAsync_Monster(Monster data)
        {
            var myData = _monsterDataset.FirstOrDefault(arg => arg.Id == data.Id);
            if (myData == null)
            {
                return false;
            }

            myData.Update(data);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteAsync_Monster(Monster data)
        {
            var myData = _monsterDataset.FirstOrDefault(arg => arg.Id == data.Id);
            _monsterDataset.Remove(myData);

            return await Task.FromResult(true);
        }

        public async Task<Monster> GetAsync_Monster(string id)
        {
            return await Task.FromResult(_monsterDataset.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Monster>> GetAllAsync_Monster(bool forceRefresh = false)
        {
            return await Task.FromResult(_monsterDataset);
        }

        #endregion Monster

        #region Score
        // Score
        public async Task<bool> InsertUpdateAsync_Score(Score data)
        {
            // Check to see if the item exist
            var oldData = await GetAsync_Score(data.Id);
            if (oldData == null)
            {
                _scoreDataset.Add(data);
                return true;
            }

            // Compare it, if different update in the DB
            var UpdateResult = await UpdateAsync_Score(data);
            if (UpdateResult)
            {
                await AddAsync_Score(data);
                return true;
            }

            return false;
        }

        public async Task<bool> AddAsync_Score(Score data)
        {
            _scoreDataset.Add(data);
            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateAsync_Score(Score data)
        {
            var myData = _scoreDataset.FirstOrDefault(arg => arg.Id == data.Id);
            if (myData == null)
            {
                return false;
            }

            myData.Update(data);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteAsync_Score(Score data)
        {
            var myData = _scoreDataset.FirstOrDefault(arg => arg.Id == data.Id);
            _scoreDataset.Remove(myData);

            return await Task.FromResult(true);
        }

        public async Task<Score> GetAsync_Score(string id)
        {
            return await Task.FromResult(_scoreDataset.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Score>> GetAllAsync_Score(bool forceRefresh = false)
        {
            return await Task.FromResult(_scoreDataset);
        }

        #endregion Score
    }
}