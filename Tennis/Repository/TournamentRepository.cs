using Microsoft.EntityFrameworkCore;
using Tennis.Models.Entity;
using Tennis.Models.Request;
using Tennis.Mappers;
using Tennis.Repository.Interfaces;

namespace Tennis.Repository
{
    public class TournamentRepository : ITournamentRepository
    {
        private readonly TennisContext _context;
        public TournamentRepository(TennisContext context)
        {
            _context = context;
        }

        //Crea un nuevo torneo
        public async Task<Tournament> CreateNewTournament(TournamentRequest tournamentRequest)
        {
            var potencialDuplicated = await _context.Set<Tournament>()
                .Where(t => t.Name == tournamentRequest.Name && t.Gender == tournamentRequest.Gender)
                .FirstOrDefaultAsync();
            if (potencialDuplicated != null)
            {
                throw new Exception("The tournament is already exist.");
            }

            var newTournament = tournamentRequest.ToTournament();
            _context.Add(newTournament);
            await _context.SaveChangesAsync();
            return newTournament;
        }

        //Borra un torneo
        public async Task<Tournament> DeleteTournament(int id)
        {
            var tournament = new Tournament();
            tournament = await _context.Set<Tournament>().Include(x => x.RegisteredPlayers).FirstOrDefaultAsync(t => t.IdTournament == id);
            if (tournament == null) 
            { 
                throw new Exception("The tournament doesn't exist."); 
            }
            if (tournament.Matches == null || tournament.RegisteredPlayers == null)
            {
                throw new Exception("The tournament has matches and/or players registered. It cannot be deleted.");
            }
            _context.Set<Tournament>().Remove(tournament);
            await _context.SaveChangesAsync();
            return tournament;
        }

        //Muestra el historial de todos los torneos existentes (ya jugados y programados)
        public async Task<List<Tournament>> GetHistorialTournaments()
        {
            var tournament = new List<Tournament>();
            tournament = await _context.Set<Tournament>()
                                .Include(t => t.Player).ThenInclude(tp => tp.Person)
                                .ToListAsync();
            return tournament;
        }

        //Muestra el historial de todos los torneos completados exitosamente (ya tienen un ganador)
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
