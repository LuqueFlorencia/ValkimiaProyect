using Tennis.Models.Entity;
using Tennis.Models.Response;

namespace Tennis.Repository.Interfaces
{
    public interface IMatchRepository
    {
        Task<List<MatchResponse>> GetMatchesByTournamentId(int id); //Muestra todos los partidos de un torneo especifico
        Task<Match> Create(Match match); //Crea los partidos necesarios para jugar un torneo
    }
}
