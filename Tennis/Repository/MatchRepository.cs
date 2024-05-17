using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using Tennis.Models.Entity;
using Tennis.Services.Interfaces;
using Tennis.Services;
using System.Reflection;
using Tennis.Helpers;

namespace Tennis.Repository
{
    public class MatchRepository : IMatchRepository
    {
        public readonly TennisContext _context;
        public MatchRepository(TennisContext context)
        {
            _context = context;
        }

        public async Task<Match> Create(Match match)
        {
            _context.Add(match);
            await _context.SaveChangesAsync();
            return match;
        }

        public async Task<List<Match>> GetMatchesByTournamentId(int id)
        {
            var matches = new List<Match>();
            matches = await _context.Set<Match>()
                .Where(m => m.TournamentId == id)
                .Include(t => t.PlayerWinner)
                .ThenInclude(t => t.Person)
                .ToListAsync();
            return matches;
        }

        public async Task<Player> PlayTournament(Tournament tournament, List<Player> players)
        {
            var winners = new List<Player>();

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
                    match.MatchType = players.Count/2;

                    //PLAY MATCHES
                    var winner = new Player();
                    winner = match.GetWinner();
                    match.WinnerId = winner.IdPlayer;
                    winners.Add(winner);

                    match = await this.Create(match);
                }
                players.Clear();
                players.AddRange(winners);
                winners.Clear();
            }
            return players[0];
        }
    }
}
