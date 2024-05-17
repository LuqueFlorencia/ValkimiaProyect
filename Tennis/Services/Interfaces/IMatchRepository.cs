using Tennis.Models.Entity;

namespace Tennis.Services.Interfaces
{
    public interface IMatchRepository
    {
        Task<List<Match>> GetMatchesByTournamentId(int id); 
        Task<Match> Create(Match match); //Crea los partidos necesarios para jugar un torneo
        Task<Player> PlayTournament(Tournament tournament, List<Player> players); //Juega un torneo
    }
}
