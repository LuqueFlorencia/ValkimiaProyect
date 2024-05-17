using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using Tennis.Helpers;
using Tennis.Mappers;
using Tennis.Models.Entity;
using Tennis.Models.Request;
using Tennis.Models.Response;
using Tennis.Services;
using Tennis.Services.Interfaces;

namespace Tennis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TournamentController : Controller
    {
        private readonly ITournamentRepository _tournamentRepository;
        private readonly IMatchRepository _matchRepository;
        private readonly IPlayerRepository _playerRepository;

        public TournamentController(ITournamentRepository tournamentRepository, IMatchRepository matchRepository, IPlayerRepository playerRepository)
        {
            _tournamentRepository = tournamentRepository;
            _matchRepository = matchRepository;
            _playerRepository = playerRepository;
        }

        [HttpGet]
        [Route("GetWinnerTournament{id}")]
        //Juega un torneo especifico y muestra el ganador del cruce de los jugadores registrados 
        public async Task<IActionResult> GetWinnerTournament(int id)
        {
            var tournament = new Tournament();
            tournament = await _tournamentRepository.GetTournamentById(id);
            var player = new Player();
            var players = await _playerRepository.GetRegisteredPlayersByTournament(id);
            player = await _matchRepository.PlayTournament(tournament, players);
            tournament = await _tournamentRepository.SetWinner(id, player.IdPlayer);
            Console.WriteLine("Player Winner: " + player.IdPlayer.ToString());

            var tournamentResponse = new TournamentResponse();
            tournamentResponse = tournament.ToTournamentResponse();
            string tournament_string = JsonConvert.SerializeObject(tournamentResponse);
            return Ok(tournament_string);
        }

        [HttpPost]
        [Route("CreateNewTournament")]
        //Crea un nuevo torneo
        public async Task<IActionResult> Create([FromBody] TournamentRequest tournamentRequest)
        {
            try
            {
                var tournament = new Tournament();
                tournament = await _tournamentRepository.CreateNewTournament(tournamentRequest);
                var tournamentResponse = tournament.ToTournamentResponse();
                string tournament_string = JsonConvert.SerializeObject(tournamentResponse);
                return Ok(tournament_string);
            }
            catch (Exception exception)
            {
                throw new Exception("The tournament is already exist.");
            }
        }

        [HttpGet]
        [Route("AllTournaments")]
        //Muestra el historial de todos los torneos: terminados con exito y los programados
        public async Task<IActionResult> GetHistorialTournaments()
        {
            var tournaments = new List<Tournament>();
            tournaments = await _tournamentRepository.GetHistorialTournaments();
            var tournamentsResponse = new List<TournamentResponse>();
            foreach (var tournament in tournaments)
            {
                var tournamentResponse = new TournamentResponse();
                tournamentResponse = tournament.ToTournamentResponse();
                tournamentsResponse.Add(tournamentResponse);
            }
            string tournament_string = JsonConvert.SerializeObject(tournamentsResponse);
            return Ok(tournament_string);
        }

        [HttpGet]
        [Route("AllTournamentsFinishes")]
        //Muestra el historial de los tornes finalizados con exito (es decir, ya tienen un ganador)
        public async Task<IActionResult> GetHistorialTournamentsFinishes()
        {
            var tournaments = new List<Tournament>();
            tournaments = await _tournamentRepository.GetHistorialTournamentsFinishes();
            var tournamentsResponse = new List<TournamentResponse>();
            foreach (var tournament in tournaments)
            {
                var tournamentResponse = new TournamentResponse();
                tournamentResponse = tournament.ToTournamentResponse();
                tournamentsResponse.Add(tournamentResponse);
            }
            string tournament_string = JsonConvert.SerializeObject(tournamentsResponse);
            return Ok(tournament_string);
        }
    }
}
