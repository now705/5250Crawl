﻿using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using Crawl.Models;
using Crawl.Views;
using System.Linq;

namespace Crawl.ViewModels
{
    public class CharactersViewModel : BaseViewModel
    {
        // Make this a singleton so it only exist one time because holds all the data records in memory
        private static CharactersViewModel _instance;

        public static CharactersViewModel Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new CharactersViewModel();
                }
                return _instance;
            }
        }

        public ObservableCollection<Character> Dataset { get; set; }
        public Command LoadDataCommand { get; set; }

        private bool _needsRefresh;

        public CharactersViewModel()
        {
            Title = "Character List";
            Dataset = new ObservableCollection<Character>();
            LoadDataCommand = new Command(async () => await ExecuteLoadDataCommand());

            // Load Data
            ExecuteLoadDataCommand().GetAwaiter().GetResult();

            MessagingCenter.Subscribe<CharacterDeletePage, Character>(this, "DeleteData", async (obj, data) =>
            {
                await DeleteAsync(data);
            });

            MessagingCenter.Subscribe<CharacterNewPage, Character>(this, "AddData", async (obj, data) =>
            {
                await AddAsync(data);
            });

            MessagingCenter.Subscribe<CharacterEditPage, Character>(this, "EditData", async (obj, data) =>
            {
                await UpdateAsync(data);
            });
        }

        // Return True if a refresh is needed
        // It sets the refresh flag to false
        public bool NeedsRefresh()
        {
            if (_needsRefresh)
            {
                _needsRefresh = false;
                return true;
            }
            return false;
        }

        // Sets the need to refresh
        public void SetNeedsRefresh(bool value)
        {
            _needsRefresh = value;
        }

        /// <summary>
        /// Force the dataset to reload
        /// Used after clearning the data so it can reset and reload
        /// </summary>
        public async void ReloadData()
        {
            await ExecuteLoadDataCommand();
        }

        // Command that Loads the Data
        private async Task ExecuteLoadDataCommand()
        {
            if (IsBusy)
            {
                return;
            }

            IsBusy = true;

            try
            {
                Dataset.Clear();
                var dataset = await DataStore.GetAllAsync_Character(true);
                foreach (var data in dataset)
                {
                    Dataset.Add(data);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        #region DataOperations

        public async Task<bool> AddAsync(Character data)
        {
            Dataset.Add(data);
            var myReturn = await DataStore.AddAsync_Character(data);
            return myReturn;
        }

        public async Task<bool> DeleteAsync(Character data)
        {
            Dataset.Remove(data);
            var myReturn = await DataStore.DeleteAsync_Character(data);
            return myReturn;
        }

        public async Task<bool> UpdateAsync(Character data)
        {
            // Find the Character, then update it
            var myData = Dataset.FirstOrDefault(arg => arg.Id == data.Id);
            if (myData == null)
            {
                return false;
            }

            myData.Update(data);
            await DataStore.UpdateAsync_Character(myData);

            _needsRefresh = true;

            return true;
        }

        // Call to database to ensure most recent
        public async Task<Character> GetAsync(string id)
        {
            var myData = await DataStore.GetAsync_Character(id);
            return myData;
        }

        // Having this at the ViewModel, because it has the DataStore
        // That allows the feature to work for both SQL and the MOCk datastores...
        public async Task<bool> InsertUpdateAsync(Character data)
        {
            var myReturn = await DataStore.InsertUpdateAsync_Character(data);
            return myReturn;
        }
        #endregion DataOperations

    }
}