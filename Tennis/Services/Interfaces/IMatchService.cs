using Tennis.Models.Entity;

namespace Tennis.Services.Interfaces
{
    public interface IMatchService
    {
        Task<Player> PlayTournament(Tournament tournament, List<Player> players); //Juega un torneo
    }
}
