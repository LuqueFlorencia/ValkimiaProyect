using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using Tennis.Models.Entity;
using Tennis.Repository.Interfaces;

namespace Tennis.Repository
{
    public class MatchRepository : IMatchRepository
    {
        public readonly TennisContext _context;
        public MatchRepository(TennisContext context)
        {
            _context = context;
        }

        //Crea un nuevo partido 
        public async Task<Match> Create(Match match)
        {
            _context.Add(match);
            await _context.SaveChangesAsync();
            return match;
        }

        //Muestra todos los partidos de un torneo especifico
        public async Task<List<Match>> GetMatchesByTournamentId(int id)
        {
            var matches = new List<Match>();
            matches = await _context.Set<Match>()
                .Where(t => t.TournamentId == id)
                .Include(t => t.PlayerWinner).ThenInclude(pw => pw.Person)
                .Include(t => t.Player1).ThenInclude(p1 => p1.Person)
                .Include(t => t.Player2).ThenInclude(p2 => p2.Person)
                .OrderByDescending(x => x.MatchType)
                .ToListAsync();

            if (!matches.Any())
            {
                Console.WriteLine("The tournament doesn't have scheduled matches yet.");
            }
            return matches;
        }
    }
}
