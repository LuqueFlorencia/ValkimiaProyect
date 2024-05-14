using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using Tennis.Helpers;
using Tennis.Models.Entity;
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
        [Route("AllTournaments")]
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
        [Route("GetWinnerTournament{id}")]
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
    }
}
