using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using Tennis.Models.Entity;
using Tennis.Services.Interfaces;
using Newtonsoft.Json;
using Tennis.Models.Request;
using Tennis.Mappers;

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
            var potencialDuplicated = await _context.Set<Tournament>()
                .Where(t => t.Name == tournamentRequest.Name && t.Gender == tournamentRequest.Gender)
                .FirstOrDefaultAsync();
            if (potencialDuplicated != null)
            {
                throw new Exception();
            }

            //var options = new JsonSerializerOptions
            //{
            //    Converters =
            //{
            //    new DateOnlyJsonConverter()
            //},
            //    WriteIndented = true
            //};
            //string json = System.Text.Json.JsonSerializer.Serialize(tournamentRequest, options);
            //Console.WriteLine(json);

            //var deserializedTournamentRequest = System.Text.Json.JsonSerializer.Deserialize<TournamentRequest>(json, options);
            //Console.WriteLine(deserializedTournamentRequest.StartDate);

            var newTournament = tournamentRequest.ToTournament();
            _context.Add(newTournament);
            await _context.SaveChangesAsync();
            return newTournament;
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
