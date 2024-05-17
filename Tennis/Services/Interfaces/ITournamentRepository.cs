using Tennis.Models.Entity;
using Tennis.Models.Request;

namespace Tennis.Services.Interfaces
{
    public interface ITournamentRepository
    {
        Task<List<Tournament>> GetHistorialTournaments();
        Task<List<Tournament>> GetHistorialTournamentsFinishes(); 
        Task<Tournament> GetTournamentById(int id); 
        Task<TournamentRequest> CreateNewTournament(TournamentRequest tournament); 
        Task<Tournament> SetWinner(int tournamentId, int winnerPlayerId);
    }
}
