using Crawl.Models;
using System.Diagnostics;

namespace Crawl.GameEngine
{
    class AutoBattleEngine
    {
        public BattleEngine BattleEngine = new BattleEngine();

        public bool AutoBattle()
        {
            // Auto Battle, does all the steps that a human would do.

            // Picks 6 Characters
            if (BattleEngine.AddCharactersToBattle() == false)
            {
                // Error, so exit...
                return false;
            }

            // Start
            BattleEngine.StartBattle(true);

            Debug.WriteLine("Battle Start" + " Characters :" + BattleEngine.CharacterList.Count);

            // Initialize the Rounds
            BattleEngine.StartRound();

            RoundEnum RoundResult;

            // Fight Loop. Continue until Game is Over...
            do
            {
                // Do the turn...
                RoundResult = BattleEngine.RoundNextTurn();

                // If the round is over start a new one...
                if (RoundResult == RoundEnum.NewRound)
                {
                    BattleEngine.NewRound();
                    Debug.WriteLine("New Round :" + BattleEngine.BattleScore.RoundCount);
                }

            } while (RoundResult != RoundEnum.GameOver);

            BattleEngine.EndBattle();

            Debug.WriteLine(
                "Battle Ended" +
                " Total Experience :" + BattleEngine.BattleScore.ExperienceGainedTotal +
                " Rounds :" + BattleEngine.BattleScore.RoundCount +
                " Turns :" + BattleEngine.BattleScore.TurnCount +
                " Monster Kills :" + BattleEngine.BattleScore.MonstersKilledList
                );

            return true;
        }
    }
}
