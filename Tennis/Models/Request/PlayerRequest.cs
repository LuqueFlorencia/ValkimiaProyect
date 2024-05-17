using Tennis.Helpers;

namespace Tennis.Models.Request
{
    public class PlayerRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public Hand Hand { get; set; }
        public int Strength { get; set; }
        public int Speed { get; set; }
        public int ReactionTime { get; set; }
    }
}
