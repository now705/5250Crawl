using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Crawl.Views.Battle
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BattleCharacterSelectPage : ContentPage
	{
		public BattleCharacterSelectPage ()
		{
			InitializeComponent ();
		}

        async void OnNextClicked(object sender, EventArgs args)
        {
            // Go back a page.
            await Navigation.PopAsync();
        }

    }
}