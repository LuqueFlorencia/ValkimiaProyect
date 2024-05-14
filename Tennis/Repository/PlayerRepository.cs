using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Tennis.Helpers;
using Tennis.Models.Entity;
using Tennis.Services.Interfaces;

namespace Tennis.Repository
{
    public class PlayerRepository:IPlayerRepository
    {
        public readonly TennisContext _context;
        public PlayerRepository(TennisContext context)
        {
            _context = context;
        }

        public async Task<List<Player>> GetAll(Gender gender)
        {
            var players = new List<Player>();
            players = await _context.Set<Player>().Where(p => p.Gender == gender).ToListAsync();
            return players;
        }

        public async Task<List<Player>> GetRegisteredPlayersByTournament(int id)
        {
            var registers = new List<RegisteredPlayer>();
            registers = await _context.Set<RegisteredPlayer>().Where(x => x.TournamentId == id).Include(x => x.Player).ToListAsync();
            var players = new List<Player>();
            foreach (var register in registers)
            {
                var player = new Player();
                player = register.Player;
                players.Add(player);
            }
            return players;
        }

        public async Task<RegisteredPlayer> RegisterInTournament(RegisteredPlayer registeredPlayer)
        {
            var player = new Player();
            player = await _context.Set<Player>().FirstOrDefaultAsync(p => p.IdPlayer == registeredPlayer.PlayerId);
            var tournament = new Tournament();
            tournament = await _context.Set<Tournament>().FirstOrDefaultAsync(t => t.IdTournament == registeredPlayer.TournamentId);

            if (player == null)
            {
                throw new Exception("The player doesn't exist.");
            }

            if (tournament == null)
            {
                throw new Exception("The tournament doesn't exist.");
            }

            if (player.Gender != tournament.Gender)
            {
                throw new Exception("The player's gender doesn't match with tournament's gender.");
            }
            var newRegister = new RegisteredPlayer();
            newRegister.PlayerId = registeredPlayer.PlayerId;
            newRegister.TournamentId = registeredPlayer.TournamentId;

            _context.Add(registeredPlayer);
            await _context.SaveChangesAsync();
            return registeredPlayer;
        }
    }
}
