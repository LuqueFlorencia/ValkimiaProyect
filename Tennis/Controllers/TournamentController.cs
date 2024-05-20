using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Tennis.Helpers;
using Tennis.Mappers;
using Tennis.Middlewares;
using Tennis.Models.Entity;
using Tennis.Models.Request;
using Tennis.Models.Response;
using Tennis.Repository.Interfaces;
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
        private readonly IMatchService _matchService;

        public TournamentController(ITournamentRepository tournamentRepository, IMatchRepository matchRepository, IPlayerRepository playerRepository, IMatchService matchService)
        {
            _tournamentRepository = tournamentRepository;
            _matchRepository = matchRepository;
            _playerRepository = playerRepository;
            _matchService = matchService;
        }

        [HttpGet]
        [Route("GetWinnerTournament{id}")]
        //Juega un torneo especifico y muestra el ganador del cruce de los jugadores registrados 
        public async Task<IActionResult> GetWinnerTournament(int id)
        {
            var tournament = new Tournament();
            tournament = await _tournamentRepository.GetTournamentById(id);
            if (tournament == null)
            {
                throw new BadRequestException("The tournament doesn't exist.");
            }
            var player = new Player();
            var players = await _playerRepository.GetRegisteredPlayersByTournament(id);
            player = await _matchService.PlayTournament(tournament, players);
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
            var tournament = new Tournament();
            //VALIDACION DE INPUTS
            if (!(TournamentExtension.IsValidName(tournamentRequest.Name))) { throw new Exception("The tournament's name is invalid."); };
            if (!(tournamentRequest.Gender == Gender.Male || tournamentRequest.Gender == Gender.Female)) { throw new Exception("The Gender must be 1=Male or 2=Female."); }
            if (!(TournamentExtension.IsValidCapacity(tournamentRequest.Capacity))) { throw new Exception("The tournament's capacity must be a power of 2."); }
            if (tournamentRequest.Prize < 1) { throw new Exception("The tournament's prize must be greater than 0."); }

            tournament = await _tournamentRepository.CreateNewTournament(tournamentRequest);
            var tournamentResponse = tournament.ToTournamentResponse();
            string tournament_string = JsonConvert.SerializeObject(tournamentResponse);
            return Ok(tournament_string);
        }

        [HttpDelete]
        [Route("DeleteTournamentBy{id}")]
        //Borra un torneo
        public async Task<IActionResult> DeleteTournament(int id)
        {
            var tournament = new Tournament();
            tournament = await _tournamentRepository.DeleteTournament(id);
            return Ok(tournament);
        }

        [HttpGet]
        [Route("GetHistorialOfTournaments")]
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
        [Route("GetHistorialOfTournamentsFinishes")]
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
