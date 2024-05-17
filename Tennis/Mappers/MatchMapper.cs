using System.Runtime.CompilerServices;
using Tennis.Models.Response;
using Tennis.Models.Entity;
using Tennis.Services;

namespace Tennis.Mappers
{
    public static class MatchMapper
    {
        public static MatchResponse ToMatchResponse (this Match match)
        {
            return new MatchResponse
            {
                Date = match.Date,
                MatchType = match.GetMatchTypeDescription(),
                Player1 = match.Player1.GetFullName(),
                Player2 = match.Player2.GetFullName(),
                Winner = match.PlayerWinner.GetFullName()
            };
        }
    }
}
