using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using Tennis.Helpers;
using Tennis.Mappers;
using Tennis.Middlewares;
using Tennis.Models.Entity;
using Tennis.Models.Request;
using Tennis.Models.Response;
using Tennis.Repository.Interfaces;
using Tennis.Services;

namespace Tennis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : Controller
    {
        private readonly IPlayerRepository _playerRepository;
        private readonly ITournamentRepository _tournamentRepository;

        public PlayerController(IPlayerRepository playerRepository, ITournamentRepository tournamentRepository)
        {
            _playerRepository = playerRepository;
            _tournamentRepository = tournamentRepository;
        }

        [HttpGet]
        [Route("GetRegisteredPlayersByTournament{id}")]
        //Muestra todos los jugadores registrados para jugar un torneo especifico.
        public async Task<IActionResult> GetRegisteredPlayersByTournament(int id)
        {
            var tournament = await _tournamentRepository.GetTournamentById(id);
            if(tournament == null) {throw new BadRequestException("The tournament doesn't exist."); }
            
            var players = new List<Player>();
            players = await _playerRepository.GetRegisteredPlayersByTournament(id);
            var registersResponse = new List<RegisteredPlayerResponse>();
            foreach (var player in players)
            {
                var registerResponse = new RegisteredPlayerResponse();
                registerResponse.FullName = player.GetFullName();

                registersResponse.Add(registerResponse);
            }
            return Ok(registersResponse);
        }

        [HttpPost]
        [Route("RegisterPlayerInTournament")]
        //Registra un jugador a un torneo 
        public async Task<IActionResult> RegisterInTournament([FromBody] RegisteredPlayerRequest registeredPlayerRequest)
        {
            var newRegister = new RegisteredPlayer();
            newRegister.PlayerId = registeredPlayerRequest.PlayerId;
            newRegister.TournamentId = registeredPlayerRequest.TournamentId;

            newRegister = await _playerRepository.RegisterInTournament(newRegister);
            string register = "Player " + newRegister.PlayerId + " Registered";
            return Ok(register);
        }

        [HttpPost]
        //Crea un nuevo jugador 
        [Route("CreateNewPlayer")]
        public async Task<IActionResult> Create([FromBody] PlayerRequest playerRequest)
        {
            var player = new Player();
            //VALIDACION DE INPUTS
            if (!(PlayerExtension.IsValidName(playerRequest.FirstName))) { throw new BadRequestException("The player's first name is invalid."); };
            if (!(PlayerExtension.IsValidName(playerRequest.LastName))) { throw new BadRequestException("The player's last name is invalid."); };
            if (!(playerRequest.Gender == Gender.Male || playerRequest.Gender == Gender.Female)) { throw new BadRequestException("The Gender must be 1=Male or 2=Female."); }
            if (!(playerRequest.Hand == Hand.Left || playerRequest.Hand == Hand.Right)) { throw new BadRequestException("The Hand must be 1=Right or 2=Left."); }
            if (playerRequest.Strength < 1 || playerRequest.Strength > 100) { throw new BadRequestException("The Strenght must be between 1 to 99."); }
            if (playerRequest.Speed < 1 || playerRequest.Speed > 100) { throw new BadRequestException("The Speed must be between 1 to 99.");  };
            if (playerRequest.ReactionTime < 1 || playerRequest.ReactionTime > 100) { throw new BadRequestException("The ReactionTime must be between 1 to 99."); }

            player = await _playerRepository.Create(playerRequest);
            var playerResponse = player.ToPlayerResponse();
            return Ok(playerResponse);
        }

        [HttpDelete]
        //Borra un jugador
        [Route("DeletePlayerBy{id}")]
        public async Task<IActionResult> DeletePlayer(int id)
        {
            var player = new Player();
            player = await _playerRepository.DeletePlayer(id);
            return Ok(player);
        }
    }
}
