using System;

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

            var result = myEngine.AutoBattle();

            if (result == false)
            {
                await DisplayAlert("Error", "No Characters Avaialbe", "OK");
                return;
            }

            if (myEngine.BattleEngine.BattleScore.RoundCount < 1)
            {
                await DisplayAlert("Error", "No Rounds Fought", "OK");
                return;
            }

            var outputString = "Battle Over! Score " + myEngine.BattleEngine.BattleScore.ScoreTotal;

            var action = await DisplayActionSheet(outputString, "Cancel", null, "View Score");
            if (action == "View Score")
            {
                await Navigation.PushAsync(new ScoreDetailPage(new ScoreDetailViewModel(myEngine.BattleEngine.BattleScore)));
            }
        }
    }
}