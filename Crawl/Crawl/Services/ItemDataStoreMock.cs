using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Crawl.Models;

namespace Crawl.Services
{
    public class ItemDataStoreMock : ItemDataStoreInterface<Item>
    {
        List<Item> dataList;

        public ItemDataStoreMock()
        {
            dataList = new List<Item>();
            var mockItems = new List<Item>
            {
                new Item { Id = Guid.NewGuid().ToString(), Text = "First item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Second item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Third item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Fourth item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Fifth item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Sixth item", Description="This is an item description." },
            };

            foreach (var data in mockItems)
            {
                dataList.Add(data);
            }
        }

        public async Task<bool> AddDataAsync(Item data)
        {
            dataList.Add(data);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateDataAsync(Item data)
        {
            var oldItem = dataList.Where((Item arg) => arg.Id == data.Id).FirstOrDefault();
            dataList.Remove(oldItem);
            dataList.Add(data);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteDataAsync(string id)
        {
            var oldItem = dataList.Where((Item arg) => arg.Id == id).FirstOrDefault();
            dataList.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Item> GetDataAsync(string id)
        {
            return await Task.FromResult(dataList.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Item>> GetDataListAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(dataList);
        }
    }
}