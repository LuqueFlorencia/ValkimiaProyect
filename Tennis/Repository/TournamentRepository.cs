using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using Tennis.Models.Entity;
using Tennis.Services.Interfaces;
using Newtonsoft.Json;

namespace Tennis.Repository
{
    public class TournamentRepository : ITournamentRepository
    {
        private readonly TennisContext _context;
        public TournamentRepository(TennisContext context)
        {
            _context = context;
        }

        public async Task<List<Tournament>> GetHistorialTournaments()
        {
            var tournament = new List<Tournament>();
            tournament = await _context.Set<Tournament>().ToListAsync();
            return tournament;
        }

        public async Task<Tournament> GetTournamentById(int id)
        {
            var tournament = new Tournament();
            tournament = _context.Set<Tournament>().FirstOrDefault(t => t.IdTournament == id);
            return tournament;
        }

        public async Task<Tournament> SetWinner(int tournamentId, int winnerPlayerId)
        {
            var tournament = new Tournament();
            tournament = _context.Set<Tournament>().FirstOrDefault(t => t.IdTournament == tournamentId);
            tournament.WinnerId = winnerPlayerId;
            await _context.SaveChangesAsync();
            return tournament;
        }
    }
}
