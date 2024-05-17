using System.Net;
using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using Tennis.Helpers;
using Tennis.Mappers;
using Tennis.Middlewares;
using Tennis.Models.Entity;
using Tennis.Models.Request;
using Tennis.Models.Response;
using Tennis.Repository;
using Tennis.Services;
using Tennis.Services.Interfaces;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
        public async Task<IActionResult> GetRegisteredPlayersByTournament(int id)
        {
            var tournament = await _tournamentRepository.GetTournamentById(id);
            if(tournament == null)
            {
                throw new BadRequestException("The tournament doesn't exist.");
            }
            var players = new List<Player>();
            players = await _playerRepository.GetRegisteredPlayersByTournament(id);
            var registersResponse = new List<RegisteredPlayerResponse>();
            foreach (var player in players)
            {
                var registerResponse = new RegisteredPlayerResponse();
                registerResponse.PlayerId = player.IdPlayer;
                registerResponse.FirstName = player.Person.FirstName;
                registerResponse.LastName = player.Person.LastName;

                registersResponse.Add(registerResponse);
            }
            string register_string = JsonConvert.SerializeObject(registersResponse);
            return Ok(register_string);
        }

        [HttpPost]
        [Route("RegisterPlayerInTournament")]
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
        [Route("CreateNewPlayer")]
        public async Task<IActionResult> Create([FromBody] PlayerRequest playerRequest)
        {
            try
            {
                var player = new Player();
                player = await _playerRepository.Create(playerRequest);
                var playerResponse = player.ToPlayerResponse();
                string player_string = JsonConvert.SerializeObject(playerResponse);
                return Ok(player_string);
            }
            catch (Exception exception)
            {
                throw new Exception("The player is already registrated");
            }
        }
    }
}
