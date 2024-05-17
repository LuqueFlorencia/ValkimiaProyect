using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using Tennis.Models.Entity;
using Tennis.Services.Interfaces;
using Newtonsoft.Json;
using Tennis.Models.Request;

namespace Tennis.Repository
{
    public class TournamentRepository : ITournamentRepository
    {
        private readonly TennisContext _context;
        public TournamentRepository(TennisContext context)
        {
            _context = context;
        }

        public async Task<Tournament> CreateNewTournament(TournamentRequest tournamentRequest)
        {
            var tournament = new Tournament();
            tournament.StartDate = new DateOnly(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day); //tournamentRequest.StartDate;
            tournament.EndDate = new DateOnly(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day+1);//tournamentRequest.EndDate;
            tournament.Gender = tournamentRequest.Gender;
            tournament.Capacity = tournamentRequest.Capacity;
            tournament.Name = tournamentRequest.Name;
            tournament.WinnerId = null;
            tournament.Prize = tournamentRequest.Prize;

            _context.Add(tournament);
            await _context.SaveChangesAsync();
            return tournament;
        }

        public async Task<List<Tournament>> GetHistorialTournaments()
        {
            var tournament = new List<Tournament>();
            tournament = await _context.Set<Tournament>()
                                .Include(t => t.Player).ThenInclude(tp => tp.Person)
                                .ToListAsync();
            return tournament;
        }

        public async Task<List<Tournament>> GetHistorialTournamentsFinishes()
        {
            var tournament = new List<Tournament>();
            tournament = await _context.Set<Tournament>()
                .Where(t => t.WinnerId != null)
                .Include(t => t.Player)
                .ThenInclude(t => t.Person)
                .ToListAsync();
            return tournament;
        }

        public async Task<Tournament> GetTournamentById(int id)
        {
            var tournament = new Tournament();
            tournament = _context.Set<Tournament>().Include(t => t.Player).ThenInclude(tp => tp.Person)
                                                   .FirstOrDefault(t => t.IdTournament == id);
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
