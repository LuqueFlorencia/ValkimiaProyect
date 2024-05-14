using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Tennis.Models.Entity;
using Tennis.Models.Request;
using Tennis.Models.Response;
using Tennis.Repository;
using Tennis.Services.Interfaces;

namespace Tennis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : Controller
    {
        private readonly IPlayerRepository _playerRepository;

        public PlayerController(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }

        [HttpGet]
        [Route("RegisteredPlayersByTournament{id}")]
        public async Task<IActionResult> GetRegisteredPlayersByTournament(int id)
        {
            var players = new List<Player>();
            players = await _playerRepository.GetRegisteredPlayersByTournament(id);
            var registersResponse = new List<RegisteredPlayerResponse>();
            foreach (var player in players)
            {
                var registerResponse = new RegisteredPlayerResponse();
                registerResponse.PlayerId = player.IdPlayer;

                registersResponse.Add(registerResponse);
            }
            string register_string = JsonConvert.SerializeObject(registersResponse);
            return Ok(register_string);
        }

        [HttpPost]
        [Route("RegisterInTournament")]
        public async Task<IActionResult> RegisterInTournament([FromBody] RegisteredPlayerRequest registeredPlayerRequest)
        {
            var newRegister = new RegisteredPlayer();
            newRegister.PlayerId = registeredPlayerRequest.PlayerId;
            newRegister.TournamentId = registeredPlayerRequest.TournamentId;

            newRegister = await _playerRepository.RegisterInTournament(newRegister);
            string register = "Player: "+ newRegister.PlayerId.ToString()+ " Registered";
            return Ok(register);
        }
    }
}
