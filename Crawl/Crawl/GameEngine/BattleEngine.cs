using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

using Crawl.Models;
using Crawl.ViewModels;

namespace Crawl.GameEngine
{
    // Battle is the top structure

    class BattleEngine : RoundEngine
    {
        // Constructor calls Init
        public BattleEngine()
        {
            BattleEngineInit();
        }

        // Sets the new state for the variables for Battle
        private void BattleEngineInit()
        {
            CharacterList = new List<Character>();
        }

        // Add Characters
        // Scale them to meet Character Strength...
        public bool AddCharactersToBattle()
        {
            if (CharactersViewModel.Instance.Dataset.Count < 1)
            {
                return false;
            }

            // Check to see if the Character list is full, if so, no need to add more...
            if (CharacterList.Count >= 6)
            {
                return true;
            }


            // Get 6 Characters
            do
            {
                var myData = new Character(CharactersViewModel.Instance.Dataset[0]);
                CharacterList.Add(myData);
                Debug.WriteLine("New Character : " + myData.FormatOutput());
            } while (CharacterList.Count < 6);

            return true;
        }
    }
}
