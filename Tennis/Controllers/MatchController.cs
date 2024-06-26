﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using Tennis.Mappers;
using Tennis.Middlewares;
using Tennis.Models.Entity;
using Tennis.Models.Response;
using Tennis.Repository;
using Tennis.Repository.Interfaces;
using Tennis.Services;

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
        [Route("GetMatchesByTournament{id}")]
        //Muestra todos los partidos de un torneo especifico
        public async Task<IActionResult> GetMatchesByTournamentId(int id)
        {
            var tournament = await _tournamentRepository.GetTournamentById(id);
            if (tournament == null) { throw new BadRequestException("The tournament doesn't exist."); }
            var matches = new List<MatchResponse>();
            matches = await _matchRepository.GetMatchesByTournamentId(id);

            return Ok(matches);
        }
    }
}
