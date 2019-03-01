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
        BattleCharacterSelectPage _myModalCharacterSelectPage;
        MonsterListPage _myModalMonsterListPage;

        public OpeningPage ()
		{
			InitializeComponent ();
		}

        async void OnNextClicked(object sender, EventArgs args)
        {
            // Show the Character Select Page
            ShowModalPageCharcterSelect();
            

            // Load up some monsters and show them
            BattleViewModel.Instance.BattleEngine.AddCharactersToBattle();

            Debug.WriteLine("Battle Start" + " Characters :" + BattleViewModel.Instance.BattleEngine.CharacterList.Count);

            BattleViewModel.Instance.BattleEngine.StartBattle(false);
            BattleViewModel.Instance.BattleEngine.StartRound();

            Debug.WriteLine("Round Start" + " Monsters:" + BattleViewModel.Instance.BattleEngine.MonsterList.Count);
            ShowModalPageMonsterList();


        }

        private void HandleModalPopping(object sender, ModalPoppingEventArgs e)
        {
            if (e.Modal == _myModalMonsterListPage)
            {
                _myModalMonsterListPage = null;

                // remember to remove the event handler:
                Crawl.App.Current.ModalPopping -= HandleModalPopping;
            }

            if (e.Modal == _myModalCharacterSelectPage)
            {
                _myModalCharacterSelectPage = null;

                // remember to remove the event handler:
                Crawl.App.Current.ModalPopping -= HandleModalPopping;
            }
        }

        private async void ShowModalPageMonsterList()
        {
            // When you want to show the modal page, just call this method
            // add the event handler for to listen for the modal popping event:
            Crawl.App.Current.ModalPopping += HandleModalPopping;
            _myModalMonsterListPage = new MonsterListPage();
            await Crawl.App.Current.MainPage.Navigation.PushModalAsync(_myModalMonsterListPage);
        }

        private async void ShowModalPageCharcterSelect()
        {
            // When you want to show the modal page, just call this method
            // add the event handler for to listen for the modal popping event:
            Crawl.App.Current.ModalPopping += HandleModalPopping;
            _myModalCharacterSelectPage = new BattleCharacterSelectPage();
            await Crawl.App.Current.MainPage.Navigation.PushModalAsync(_myModalCharacterSelectPage);
        }

    }
}