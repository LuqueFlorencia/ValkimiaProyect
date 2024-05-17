using Tennis.Helpers;
using Tennis.Models.Entity;
using Tennis.Models.Request;

namespace Tennis.Mappers
{
    public static class PlayerMapper
    {
        public static Player ToPlayer(this PlayerRequest playerRequest)
        {
            return new Player
            {
                Person = new Person
                {
                    FirstName = playerRequest.FirstName,
                    LastName = playerRequest.LastName
                },
                Gender = playerRequest.Gender,
                Hand = playerRequest.Hand,
                Strength = playerRequest.Strength,
                Speed = playerRequest.Speed,
                ReactionTime = playerRequest.ReactionTime,
            };
        }
    }
}

