using Tennis.Helpers;
using Tennis.Models.Entity;
using Tennis.Models.Request;
using Tennis.Models.Response;

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
                ReactionTime = playerRequest.ReactionTime
            };
        }

        public static PlayerResponse ToPlayerResponse (this Player player)
        {
            return new PlayerResponse
            {
                FirstName = player.Person.FirstName,
                LastName = player.Person.LastName,
                Gender = player.Gender.GetDescription(),
                Hand = player.Hand.GetDescription(),
                Strength = player.Strength,
                Speed = player.Speed,
                ReactionTime = player.ReactionTime
            };
        }
    }
}

