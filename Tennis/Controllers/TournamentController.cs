using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using Tennis.Helpers;
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
            return Ok();
        }

        [HttpPost]
        [Route("CreateNewTournament")]
        //Crea un nuevo torneo
        public async Task<IActionResult> Create([FromBody] TournamentRequest tournamentRequest)
        {
            var options = new JsonSerializerOptions
            {
                Converters =
            {
                new DateOnlyJsonConverter()
            },
                WriteIndented = true
            };
            string json = System.Text.Json.JsonSerializer.Serialize(tournamentRequest, options);
            Console.WriteLine(json);

            var deserializedTournamentRequest = System.Text.Json.JsonSerializer.Deserialize<TournamentRequest>(json, options);
            Console.WriteLine(deserializedTournamentRequest.StartDate);

            var newTournament = new TournamentRequest();
            newTournament.Name = tournamentRequest.Name;
            newTournament.Gender = tournamentRequest.Gender;
            newTournament.StartDate = DateOnly.FromDateTime(DateTime.Now);
            newTournament.EndDate = DateOnly.FromDateTime(DateTime.Now);
            newTournament.Capacity = tournamentRequest.Capacity;
            newTournament.Prize = tournamentRequest.Prize;

            newTournament = await _tournamentRepository.CreateNewTournament(newTournament);
            string tournament_string = JsonConvert.SerializeObject(newTournament);
            return Ok(tournament_string);
        }

        [HttpGet]
        [Route("AllTournaments")]
        //Muestra el historial de todos los torneos: terminados con exito y los programados
        public async Task<IActionResult> GetHistorialTournaments()
        {
            var tournaments = new List<Tournament>();
            tournaments = await _tournamentRepository.GetHistorialTournaments();
            var TournamentsResponse = new List<TournamentResponse>();
            foreach (var tournament in tournaments)
            {
                var TournamentResponse = new TournamentResponse();
                TournamentResponse.Name = tournament.Name;
                TournamentResponse.StartDate = tournament.StartDate;
                TournamentResponse.EndDate = tournament.EndDate;
                TournamentResponse.Capacity = tournament.Capacity;
                TournamentResponse.Prize = tournament.Prize;
                TournamentResponse.WinnerId = tournament.WinnerId;

                TournamentsResponse.Add(TournamentResponse);
            }
            string tournament_string = JsonConvert.SerializeObject(TournamentsResponse);
            return Ok(tournament_string);
        }

        [HttpGet]
        [Route("AllTournamentsFinishes")]
        //Muestra el historial de los tornes finalizados con exito (es decir, ya tienen un ganador)
        public async Task<IActionResult> GetHistorialTournamentsFinishes()
        {
            var tournaments = new List<Tournament>();
            tournaments = await _tournamentRepository.GetHistorialTournamentsFinishes();
            var TournamentsResponse = new List<TournamentResponse>();
            foreach (var tournament in tournaments)
            {
                var TournamentResponse = new TournamentResponse();
                TournamentResponse.Name = tournament.Name;
                TournamentResponse.StartDate = tournament.StartDate;
                TournamentResponse.EndDate = tournament.EndDate;
                TournamentResponse.Capacity = tournament.Capacity;
                TournamentResponse.Prize = tournament.Prize;
                TournamentResponse.WinnerId = tournament.WinnerId;
                TournamentResponse.FirstName = tournament.Player.Person.FirstName;
                TournamentResponse.LastName = tournament.Player.Person.LastName;

                TournamentsResponse.Add(TournamentResponse);
            }
            string tournament_string = JsonConvert.SerializeObject(TournamentsResponse);
            return Ok(tournament_string);
        }
    }
}
