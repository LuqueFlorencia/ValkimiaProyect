using System.Runtime.CompilerServices;
using Tennis.Helpers;
using Tennis.Models.Entity;

namespace Tennis.Services
{
    public static class PlayerExtension
    {
        public static double GetAbilityByGender (this Player player)
        {
            double ability = 0;
            if (player.Gender == Gender.Male)
            {
                ability = player.AbilityLevel + player.Strength + player.Speed;
            }
            if (player.Gender == Gender.Female)
            { 
                ability = player.AbilityLevel + player.ReactionTime;
            }
            return (ability + player.GetRandomLuck() * 0.5);
        }
        private static readonly Random random = new Random();
        public static int GetRandomLuck(this Player player)
        {
            // Obtener un número aleatorio entre 1 y 100 como dato adicional de suerte
            return random.Next(1, 101);
        }

        public static string GetFullName(this Player player)
        {
            string fullName = string.Empty;
            fullName = player.Person.LastName +", "+ player.Person.FirstName;
            return fullName;
        }
    }
}
