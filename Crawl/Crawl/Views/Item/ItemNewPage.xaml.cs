using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Crawl.Models;

namespace Crawl.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemNewPage : ContentPage
    {
        public Item Data { get; set; }

        public ItemNewPage()
        {
            InitializeComponent();

            Data = new Item
            {
                Text = "Item name",
                Description = "This is an item description."
            };

            BindingContext = this;
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "AddItem", Data);
            await Navigation.PopModalAsync();
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}