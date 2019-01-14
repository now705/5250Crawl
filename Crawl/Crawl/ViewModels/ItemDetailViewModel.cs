using System;

using Crawl.Models;

namespace Crawl.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public Item Data { get; set; }
        public ItemDetailViewModel(Item data = null)
        {
            Title = data?.Text;
            Data = data;
        }
    }
}
