using System.Text.RegularExpressions;

namespace Tennis.Services
{
    public class TournamentExtension
    {
        public static bool IsValidName(string name)
        {
            string pattern = @"^[a-zA-Z]+$";
            return Regex.IsMatch(name, pattern);
        }

        public static bool IsValidCapacity(int capacity)
        {
            return capacity > 0 && Math.Log(capacity, 2) % 1 == 0;
        }
    }
}
