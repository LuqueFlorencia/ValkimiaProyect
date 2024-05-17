namespace Tennis.Models.Response
{
    public class MatchResponse
    {
        public DateOnly Date { get; set; }
        public int MatchType { get; set; } //8vo, 4to, Semi, Final
        public int IdPlayer1 { get; set; }
        public int IdPlayer2 { get; set; }
        public int? WinnerId { get; set; }
        public string? WinnerFirstName { get; set; }
        public string? WinnerLastName { get; set; }
    }
}
