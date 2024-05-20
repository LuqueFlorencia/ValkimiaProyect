using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using Tennis.Models.Entity;
using Tennis.Services.Interfaces;
using Tennis.Repository;
using Tennis.Repository.Interfaces;

namespace Tennis.Services
{
    public class MatchService : IMatchService
    {
        public readonly TennisContext _context;
        private readonly IMatchRepository _matchRepository;

        public MatchService(TennisContext context, IMatchRepository matchRepository)
        {
            _context = context;
            _matchRepository = matchRepository;
        }
        public async Task<Player> PlayTournament(Tournament tournament, List<Player> players)
        {
            var winners = new List<Player>();
            if (tournament.Capacity == tournament.RegisteredPlayers.Count())
            {
                while (players.Count > 1)
                {
                    //MAKE MATCHES
                    for (int i = 0; i < players.Count; i = i + 2)
                    {
                        var match = new Models.Entity.Match();
                        match.TournamentId = tournament.IdTournament;
                        match.IdPlayer1 = players[i].IdPlayer;
                        match.Player1 = players[i];
                        match.IdPlayer2 = players[i + 1].IdPlayer;
                        match.Player2 = players[i + 1];
                        match.Date = new DateOnly(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                        match.MatchType = players.Count / 2;

                        //PLAY MATCHES
                        var winner = new Player();
                        winner = match.GetWinner();
                        match.WinnerId = winner.IdPlayer;
                        winners.Add(winner);

                        match = await _matchRepository.Create(match);
                    }
                    players.Clear();
                    players.AddRange(winners);
                    winners.Clear();
                }
                return players[0];
            }
            else
            {
                throw new Exception("The tournament doesn't have enought registered players to be play.");
            }
        }
    }
}
