using System;

using Crawl.Views;
using Crawl.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Crawl.Views.Battle;
using System.Diagnostics;

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
            // Show the Character Select Page
            await Navigation.PushModalAsync(new Battle.BattleCharacterSelectPage());

            // Load up some monsters and show them
            BattleViewModel.Instance.BattleEngine.AddCharactersToBattle();

            Debug.WriteLine("Battle Start" + " Characters :" + BattleViewModel.Instance.BattleEngine.CharacterList.Count);

            BattleViewModel.Instance.BattleEngine.StartBattle(false);
            BattleViewModel.Instance.BattleEngine.StartRound();

            Debug.WriteLine("Round Start" + " Monsters:" + BattleViewModel.Instance.BattleEngine.MonsterList.Count);

            await Navigation.PushModalAsync(new Battle.MonsterListPage());

        }
    }
}