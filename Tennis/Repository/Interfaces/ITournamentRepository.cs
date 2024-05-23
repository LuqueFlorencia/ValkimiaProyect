using Tennis.Models.Entity;
using Tennis.Models.Request;
using Tennis.Models.Response;

namespace Tennis.Repository.Interfaces
{
    public interface ITournamentRepository
    {
        Task<List<TournamentResponse>> GetHistorialTournaments(); //Muestra el historial de todos los torneos existentes (jugados y programados)
        Task<List<TournamentResponse>> GetHistorialTournamentsFinishes(); //Muestra el historial de todos los torneos finalizados con exito
        Task<Tournament> GetTournamentById(int id); //Muestra un torneo especifico 
        Task<Tournament> CreateNewTournament(TournamentRequest tournament); //Crea un nuevo torneo
        Task<Tournament> DeleteTournament(int id); //Borra un torneo
        Task<Tournament> SetWinner(int tournamentId, int winnerPlayerId); //Guarda el ganador del torneo en la entidad torneo
    }
}
