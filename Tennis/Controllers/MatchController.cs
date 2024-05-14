using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
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
        public MatchController(IMatchRepository matchRepository)
        {
            _matchRepository = matchRepository;
        }

        [HttpGet]
        [Route("MatchesByTournament{id}")]
        public async Task<IActionResult> GetMatchesByTournamentId(int id)
        {
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

                matchesResponse.Add(matchResponse);
            }
            string match_string = JsonConvert.SerializeObject(matchesResponse);
            return Ok(match_string);
        }
    }
}
