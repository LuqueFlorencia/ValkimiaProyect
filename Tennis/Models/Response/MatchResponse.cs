namespace Tennis.Models.Response
{
    public class MatchResponse
    {
        public DateOnly Date { get; set; }
        public string MatchType { get; set; } //8vo, 4to, Semi, Final
        //public int IdPlayer1 { get; set; }
        public string Player1 { get; set; }
        //public int IdPlayer2 { get; set; }
        public string Player2 {  get; set; }
        //public int? WinnerId { get; set; }
        public string ? Winner {  get; set; }
        //public string? WinnerFirstName { get; set; }
        //public string? WinnerLastName { get; set; }
    }
}
