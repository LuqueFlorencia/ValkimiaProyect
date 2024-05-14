using Tennis.Models.Entity;

namespace Tennis.Services.Interfaces
{
    public interface ITournamentRepository
    {
        Task<List<Tournament>> GetHistorialTournaments();
        Task<Tournament> GetTournamentById(int id);
        Task<Tournament> SetWinner(int tournamentId, int winnerPlayerId);
    }
}
