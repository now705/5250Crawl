using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Crawl.Services
{
    public interface ItemDataStoreInterface<T>
    {
        Task<bool> AddDataAsync(T item);
        Task<bool> UpdateDataAsync(T item);
        Task<bool> DeleteDataAsync(string id);
        Task<T> GetDataAsync(string id);
        Task<IEnumerable<T>> GetDataListAsync(bool forceRefresh = false);
    }
}
