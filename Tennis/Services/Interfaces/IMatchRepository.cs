using Tennis.Models.Entity;

namespace Tennis.Services.Interfaces
{
    public interface IMatchRepository
    {
        Task<List<Match>> GetMatchesByTournamentId(int id);
        Task<Match> Create(Match match);
        Task<Player> PlayTournament(Tournament tournament, List<Player> players);
    }
}
