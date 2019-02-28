﻿using System;

using Crawl.GameEngine;
using Crawl.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Crawl.Views.Battle
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AutoBattlePage : ContentPage
	{
		public AutoBattlePage ()
		{
			InitializeComponent ();
		}

        private async void AutoBattleButton_Command(object sender, EventArgs e)
        {
            // Can create a new battle engine...
            var myEngine = new AutoBattleEngine();

            var result = myEngine.RunAutoBattle();

            if (result == false)
            {
                await DisplayAlert("Error", "No Characters Avaialbe", "OK");
                return;
            }

            if (myEngine.GetRoundsValue() < 1)
            {
                await DisplayAlert("Error", "No Rounds Fought", "OK");
                return;
            }

            var myResult = myEngine.GetResultsOutput();
            var myScore = myEngine.GetScoreValue();

            var outputString = "Battle Over! Score " + myScore.ToString();

            var myScoreObject = myEngine.GetScoreObject();
            await Navigation.PushAsync(new ScoreDetailPage(new ScoreDetailViewModel(myScoreObject)));
        }
    }
}