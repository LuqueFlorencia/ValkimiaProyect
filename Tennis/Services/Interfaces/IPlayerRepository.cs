using Tennis.Helpers;
using Tennis.Models.Entity;

namespace Tennis.Services.Interfaces
{
    public interface IPlayerRepository
    {
        Task<List<Player>> GetAll(Gender gender);
        Task<RegisteredPlayer> RegisterInTournament(RegisteredPlayer registeredPlayer);
        Task<List<Player>> GetRegisteredPlayersByTournament(int id);
    }
}
