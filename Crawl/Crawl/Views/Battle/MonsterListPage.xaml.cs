using System;
using System.Collections;
using System.Collections.Generic;

using Crawl.GameEngine;
using Crawl.ViewModels;
using Crawl.Models;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Crawl.Views.Battle
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MonsterListPage : ContentPage
	{
        public List<Monster> Datalist = new List<Monster>();

		public MonsterListPage()
		{
			InitializeComponent ();

            Datalist = BattleViewModel.Instance.BattleEngine.MonsterList;
            BindingContext = Datalist;

        }

    }
}