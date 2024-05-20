using System.Globalization;
using System.Runtime.Serialization;

namespace Tennis.Middlewares
{
    [Serializable]
    internal class BadRequestException : Exception
    {
        public BadRequestException() { }

        public BadRequestException(string? message) : base(message) { }

        public BadRequestException(string? message, params object[] args) : base(String.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }
}
