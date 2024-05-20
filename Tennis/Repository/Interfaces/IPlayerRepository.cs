using Tennis.Helpers;
using Tennis.Models.Entity;
using Tennis.Models.Request;

namespace Tennis.Repository.Interfaces
{
    public interface IPlayerRepository
    {
        Task<RegisteredPlayer> RegisterInTournament(RegisteredPlayer registeredPlayer); //Registra un jugador a un torneo especifico
        Task<List<Player>> GetRegisteredPlayersByTournament(int id); //Muestra todos los jugadores de un torneo
        Task<Player> Create(PlayerRequest playerRequest); //Crea un nuevo jugador
        Task<Player> DeletePlayer(int id); //Borra un jugador de la bd
    }
}
