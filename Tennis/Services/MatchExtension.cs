using Tennis.Models.Entity;

namespace Tennis.Services
{
    public static class MatchExtension
    {
        public static Player GetWinner (this Tennis.Models.Entity.Match match)
        {
            var playerWinner = new Player ();
            double player1Ability = match.Player1.GetAbilityByGender();
            double player2Ability = match.Player2.GetAbilityByGender();

            if (player1Ability == player2Ability)
            {
                player1Ability = match.Player1.GetAbilityByGender();
                player2Ability = match.Player2.GetAbilityByGender();
            }
            if (player1Ability > player2Ability)
            {
                playerWinner = match.Player1;
            }
            if (player1Ability < player2Ability)
            {
                playerWinner = match.Player2;
            }
            Console.WriteLine("Player 1 = PlayerId: "+ match.Player1.IdPlayer.ToString() +" - "+ player1Ability.ToString());
            Console.WriteLine("Player 2 = PlayerId: " + match.Player2.IdPlayer.ToString() +" - "+ player2Ability.ToString());
            return playerWinner;
        }

        public static string GetMatchTypeDescription(this Tennis.Models.Entity.Match match)
        {
            string description = "";
            switch (match.MatchType)
            {
                case 8:
                    description = "8vo";
                    break;
                case 4:
                    description = "4to";
                    break;
                case 2:
                    description = "Semifinal";
                    break;
                case 1:
                    description = "Final";
                    break;
            }
            return description;
        }
    }
}
