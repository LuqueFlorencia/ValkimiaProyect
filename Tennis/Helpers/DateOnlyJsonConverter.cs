using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;

namespace Tennis.Helpers
{
    public class DateOnlyJsonConverter : Newtonsoft.Json.JsonConverter<DateOnly>
    {
        private const string Format = "yyyy-MM-dd";

        public override DateOnly ReadJson(JsonReader reader,
            Type objectType,
            DateOnly existingValue,
            bool hasExistingValue,
            Newtonsoft.Json.JsonSerializer serializer) =>
            DateOnly.ParseExact((string)reader.Value, Format, CultureInfo.InvariantCulture);

        public override void WriteJson(JsonWriter writer, DateOnly value, JsonSerializer serializer) =>
            writer.WriteValue(value.ToString(Format, CultureInfo.InvariantCulture));
    }
}
