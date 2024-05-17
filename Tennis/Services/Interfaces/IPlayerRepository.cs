using Tennis.Helpers;
using Tennis.Models.Entity;
using Tennis.Models.Request;

namespace Tennis.Services.Interfaces
{
    public interface IPlayerRepository
    {
        Task<List<Player>> GetAll(Gender gender); //Muestra todos los jugadores de un genero determinado
        Task<RegisteredPlayer> RegisterInTournament(RegisteredPlayer registeredPlayer); //Registra un jugador a un torneo especifico
        Task<List<Player>> GetRegisteredPlayersByTournament(int id); //Muestra todos los jugadores de un torneo
        Task<Player> Create(PlayerRequest playerRequest);
    }
}
