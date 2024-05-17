namespace Tennis.Models.Response
{
    public class MatchResponse
    {
        public DateOnly Date { get; set; }
        public string MatchType { get; set; } //8vo, 4to, Semi, Final
        public string Player1 { get; set; }
        public string Player2 {  get; set; }
        public string ? Winner {  get; set; }
    }
}
