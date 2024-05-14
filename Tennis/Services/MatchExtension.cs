using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http.HttpResults;
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

        //public static Models.Entity.Match Create (Tournament tournament, Player player1, Player player2, int matchType)
        //{
        //    var match = new Models.Entity.Match();
        //    match.TournamentId = tournament.IdTournament;
        //    match.IdPlayer1 = player1.IdPlayer;
        //    match.IdPlayer2 = player2.IdPlayer;
        //    match.Date = new DateOnly(DateTime.Now.Year,DateTime.Now.Month,DateTime.Now.Day);
        //    match.MatchType = matchType;

        //    return match;
        //}
    }
}
