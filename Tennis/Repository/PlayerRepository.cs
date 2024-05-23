using System.Numerics;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Tennis.Helpers;
using Tennis.Mappers;
using Tennis.Middlewares;
using Tennis.Models.Entity;
using Tennis.Models.Request;
using Tennis.Repository.Interfaces;

namespace Tennis.Repository
{
    public class PlayerRepository:IPlayerRepository
    {
        public readonly TennisContext _context;
        public PlayerRepository(TennisContext context)
        {
            _context = context;
        }

        //Crea un nuevo jugador
        public async Task<Player> Create(PlayerRequest playerRequest)
        {
            var potencialDuplicated = await _context.Set<Player>()
                .Where(a => a.Person.FirstName == playerRequest.FirstName && a.Person.LastName == playerRequest.LastName)
                .FirstOrDefaultAsync();
            if (potencialDuplicated != null)
            {
                throw new BadRequestException("The player is already registrated");
            }
    
            var newPlayer = playerRequest.ToPlayer();
            _context.Add(newPlayer);
            await _context.SaveChangesAsync();
            return newPlayer;
        }

        //Borra un jugador
        public async Task<Player> DeletePlayer(int id)
        {
            var player = new Player();
            player = await _context.Set<Player>().FirstOrDefaultAsync(a => a.IdPlayer == id);
            if (player == null) 
            { 
                throw new BadRequestException("The player doesn't exist."); 
            }
            if (player.MatchesAsP1 == null || player.MatchesAsP2 == null || player.RegisteredPlayers == null)
            {
                throw new BadRequestException("The player is related to a tournament. It cannot be deleted.");
            }
            _context.Set<Player>().Remove(player);
            await _context.SaveChangesAsync();
            return player;
        }

        //Muestra todos los jugadores registrados para un torneo especifico
        public async Task<List<Player>> GetRegisteredPlayersByTournament(int id)
        {
            var registers = new List<RegisteredPlayer>();
            registers = await _context.Set<RegisteredPlayer>()
                .Where(x => x.TournamentId == id)
                .Include(x => x.Player)
                .ThenInclude(x=>x.Person)
                .ToListAsync();

            if (!registers.Any()) { Console.WriteLine("The tournament doesn't have registered players"); }

            var players = new List<Player>();
            foreach (var register in registers)
            {
                var player = new Player();
                player = register.Player;
                players.Add(player);
            }
            return players;
        }

        //Registra un jugador para que pueda jugar un torneo
        public async Task<RegisteredPlayer> RegisterInTournament(RegisteredPlayer registeredPlayer)
        {
            var potencialDuplicated = await _context.Set<RegisteredPlayer>()
                .Where(x => x.TournamentId == registeredPlayer.TournamentId && x.PlayerId == registeredPlayer.PlayerId)
                .FirstOrDefaultAsync();
            
            if (potencialDuplicated != null)
            {
                throw new BadRequestException("The player is already registered in the tournament.");
            }

            var player = new Player();
            player = await _context.Set<Player>().FirstOrDefaultAsync(p => p.IdPlayer == registeredPlayer.PlayerId);
            var tournament = new Tournament();
            tournament = await _context.Set<Tournament>()
                .FirstOrDefaultAsync(t => t.IdTournament == registeredPlayer.TournamentId);

            if (player == null) { throw new BadRequestException("The player doesn't exist."); }
            if (tournament == null) { throw new BadRequestException("The tournament doesn't exist."); }
            if (player.Gender != tournament.Gender) { throw new BadRequestException("The player's gender doesn't match with tournament's gender."); }  

            var players = new List<Player>();
            int totalPlayersRegistered = 0;
            players = await this.GetRegisteredPlayersByTournament(tournament.IdTournament);
            totalPlayersRegistered = players.Count();

            if (tournament.Capacity > totalPlayersRegistered) 
            {
                var newRegister = new RegisteredPlayer();
                newRegister.PlayerId = registeredPlayer.PlayerId;
                newRegister.TournamentId = registeredPlayer.TournamentId;
                _context.Add(registeredPlayer);
                await _context.SaveChangesAsync();
                return registeredPlayer;
            }
            else
            {
                throw new BadRequestException($"The tournament {tournament.Name} is full.");
            }
        }
    }
}
