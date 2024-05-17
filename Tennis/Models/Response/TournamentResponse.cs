using Tennis.Models.Entity;

namespace Tennis.Models.Response
{
    public class TournamentResponse
    {
        public string Name { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public int Capacity { get; set; }
        public int Prize { get; set; }
        public int? WinnerId { get; set; }
        public string ? FirstName { get; set; }
        public string ? LastName { get; set; }

    }
}
