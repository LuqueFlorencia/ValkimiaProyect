using Tennis.Helpers;
using Tennis.Models.Entity;

namespace Tennis.Models.Response
{
    public class PlayerResponse
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
