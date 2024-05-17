using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using Tennis.Middlewares;
using Tennis.Models.Entity;
using Tennis.Models.Response;
using Tennis.Repository;
using Tennis.Services.Interfaces;

namespace Tennis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatchController : Controller
    {
        private readonly IMatchRepository _matchRepository;
        private readonly ITournamentRepository _tournamentRepository;
        public MatchController(IMatchRepository matchRepository, ITournamentRepository tournamentRepository)
        {
            _matchRepository = matchRepository;
            _tournamentRepository = tournamentRepository;
        }

        [HttpGet]
        [Route("MatchesByTournament{id}")]
        //Muestra todos los partidos programados para un torneo especifico
        public async Task<IActionResult> GetMatchesByTournamentId(int id)
        {
            var tournament = await _tournamentRepository.GetTournamentById(id);
            if (tournament == null)
            {
                throw new BadRequestException("The tournament doesn't exist.");
            }
            var matches = new List<Match>();
            matches = await _matchRepository.GetMatchesByTournamentId(id);
            var matchesResponse = new List<MatchResponse>();
            foreach (var match in matches)
            {
                var matchResponse = new MatchResponse();
                matchResponse.Date = match.Date;
                matchResponse.MatchType = match.MatchType;
                matchResponse.IdPlayer1 = match.IdPlayer1;
                matchResponse.IdPlayer2 = match.IdPlayer2;
                matchResponse.WinnerId = match.WinnerId;
                matchResponse.WinnerFirstName = match.PlayerWinner.Person.FirstName;
                matchResponse.WinnerLastName = match.PlayerWinner.Person.LastName;

                matchesResponse.Add(matchResponse);
            }
            string match_string = JsonConvert.SerializeObject(matchesResponse);
            return Ok(match_string);
        }
    }
}
