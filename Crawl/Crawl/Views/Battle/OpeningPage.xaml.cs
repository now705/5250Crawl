﻿using System;

using Crawl.Views;   

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Crawl.Views.Battle;

namespace Crawl.Views.Battle
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class OpeningPage : ContentPage
	{
		public OpeningPage ()
		{
			InitializeComponent ();
		}

        async void OnNextClicked(object sender, EventArgs args)
        {
            await Navigation.PushModalAsync(new Battle.BattleCharacterSelect());
        }
    }
}