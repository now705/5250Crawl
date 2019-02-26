using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using Crawl.Models;
using Crawl.ViewModels;
using System.Diagnostics;

namespace Crawl.GameEngine
{
    class RoundEngine : TurnEngine
    {
        // Hold the list of players (monster, and character by guid), and order by speed
        public List<PlayerInfo> PlayerList;

        // Player currently engaged
        public PlayerInfo PlayerCurrent;

        public RoundEngine()
        {
            ClearLists();
        }

        private void ClearLists()
        {
            ItemPool = new List<Item>();
            MonsterList = new List<Monster>();
        }

        // Start the round, need to get the ItemPool, and Characters
        public void StartRound()
        {
            // Start on 0, the turns will increment...
            BattleScore.RoundCount = 0;

            // Start the first round...
            NewRound();

            Debug.WriteLine("Start Round :" + BattleScore.RoundCount);
        }

        // Call to make a new set of monsters...
        public void NewRound()
        {
            // End the existing round
            EndRound();

            // Populate New Monsters...
            AddMonstersToRound();

            // Make the PlayerList
            MakePlayerList();

            // Update Score for the RoundCount
            BattleScore.RoundCount++;

            Debug.WriteLine("New Round # :" + BattleScore.RoundCount);
        }

        // Add Monsters
        // Scale them to meet Character Strength...
        public void AddMonstersToRound()
        {

            // Check to see if the monster list is full, if so, no need to add more...
            if (MonsterList.Count() >= 6)
            {
                return;
            }

            if (MonstersViewModel.Instance.Dataset.Count() > 0)
            {
                // Get 6 monsters
                do
                {
                    var myData = MonstersViewModel.Instance.Dataset[0];
                    MonsterList.Add(myData);

                    Debug.WriteLine("Added Monster # :" + myData.FormatOutput());

                } while (MonsterList.Count() < 6);
            }
        }

        // At the end of the round
        // Clear the Item List
        // Clear the Monster List
        public void EndRound()
        {
            // Have each character pickup items...
            foreach (var character in CharacterList)
            {
                PickupItemsFromPool(character);
            }

            ClearLists();
        }

        // Get Round Turn Order

        // Rember Who's Turn

        // RoundNextTurn
        public RoundEnum RoundNextTurn()
        {
            // No characters, game is over...
            if (CharacterList.Count < 1)
            {
                // Game Over

                Debug.WriteLine("Round says : Game Over");

                return RoundEnum.GameOver;
            }

            // Check if round is over
            if (MonsterList.Count < 1)
            {

                Debug.WriteLine("Round says : New Round");

                // If over, New Round
                return RoundEnum.NewRound;
            }

            // Decide Who gets next turn
            // Remember who just went...
            PlayerCurrent = GetNextPlayerTurn();

            // Decide Who to Attack
            // Do the Turn         
            if (PlayerCurrent.PlayerType == PlayerTypeEnum.Character)
            {
                // Get the player
                var myPlayer = CharacterList.Where(a => a.Guid == PlayerCurrent.Guid).FirstOrDefault();

                // Do the turn....
                TakeTurn(myPlayer);
            }
            // Add Monster turn here...
            else if (PlayerCurrent.PlayerType == PlayerTypeEnum.Monster)
            {
                // Get the player
                var myPlayer = MonsterList.Where(a => a.Guid == PlayerCurrent.Guid).FirstOrDefault();

                // Do the turn....
                TakeTurn(myPlayer);
            }

            Debug.WriteLine("Round says : Next Turn");

            return RoundEnum.NextTurn;
        }

        public PlayerInfo GetNextPlayerTurn()
        {
            // Recalculate Order
            OrderPlayerListByTurnOrder();

            var PlayerCurrent = GetNextPlayerInList();
            // Lookup CurrentPlayer in the list
            // Find the player next to Current Player in order

            return PlayerCurrent;
        }

        public void OrderPlayerListByTurnOrder()
        {
            var myReturn = new List<PlayerInfo>();

            // Order is based by... 
            // Order by Speed (Desending)
            // Then by Highest level (Descending)
            // Then by Highest Experience Points (Descending)
            // Then by Character before Monster (enum assending)
            // Then by Alphabetic on Name (Assending)
            // Then by First in list order (Assending

            MakePlayerList();

            PlayerList = PlayerList.OrderByDescending(a => a.Speed)
                .ThenByDescending(a => a.Level)
                .ThenByDescending(a => a.ExperiencePoints)
                .ThenByDescending(a => a.PlayerType)
                .ThenBy(a => a.Name)
                .ThenBy(a => a.ListOrder)
                .ToList();
        }

        private void MakePlayerList()
        {
            PlayerList = new List<PlayerInfo>();
            PlayerInfo tempPlayer;

            var ListOrder = 0;

            foreach (var data in CharacterList)
            {
                if (data.Alive)
                {
                    tempPlayer = new PlayerInfo(data);

                    // Remember the order
                    tempPlayer.ListOrder = ListOrder;

                    PlayerList.Add(tempPlayer);

                    ListOrder++;
                }
            }

            foreach (var data in MonsterList)
            {
                if (data.Alive)
                {

                    tempPlayer = new PlayerInfo(data);

                    // Remember the order
                    tempPlayer.ListOrder = ListOrder;

                    PlayerList.Add(tempPlayer);

                    ListOrder++;
                }
            }
        }

        public PlayerInfo GetNextPlayerInList()
        {
            Debug.WriteLine("Round says : Next Player");

            // Return the first in the list...
            return PlayerList.FirstOrDefault();
        }

        public void PickupItemsFromPool(Character character)
        {
            Debug.WriteLine("Round says : Picked Up Nothing");

            return;
        }
    }
}
