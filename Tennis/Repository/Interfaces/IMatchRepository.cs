using Tennis.Models.Entity;

namespace Tennis.Repository.Interfaces
{
    public interface IMatchRepository
    {
        Task<List<Match>> GetMatchesByTournamentId(int id); //Muestra todos los partidos de un torneo especifico
        Task<Match> Create(Match match); //Crea los partidos necesarios para jugar un torneo
    }
}
