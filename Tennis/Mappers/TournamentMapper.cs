using Tennis.Helpers;
using Tennis.Models.Entity;
using Tennis.Models.Request;
using Tennis.Models.Response;
using Tennis.Services;

namespace Tennis.Mappers
{
    public static class TournamentMapper
    {
        public static Tournament ToTournament(this TournamentRequest tournamentRequest)
        {
            return new Tournament
            {
                Name = tournamentRequest.Name,
                Gender = tournamentRequest.Gender,
                StartDate = new DateOnly(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day),
                EndDate = new DateOnly(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day + 1),
                Capacity = tournamentRequest.Capacity,
                Prize = tournamentRequest.Prize,
            };
        }
        public static TournamentResponse ToTournamentResponse(this Tournament tournament)
        {
            return new TournamentResponse
            {
                Name = tournament.Name,
                Gender = tournament.Gender.GetDescription(),
                StartDate = tournament.StartDate,
                EndDate = tournament.EndDate,
                Capacity = tournament.Capacity,
                Prize = tournament.Prize,
                Winner = tournament.Player.GetFullName()
            };
        }
    }
}
