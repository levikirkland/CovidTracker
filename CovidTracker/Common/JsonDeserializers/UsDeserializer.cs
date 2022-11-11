using CovidTracker.Models;
using Newtonsoft.Json;

namespace CovidTracker.Common.JsonDeserializers
{
    public sealed class UsDeserializer
    {
        public IReadOnlyCollection<CovidUsModel> Deserialize(JsonTextReader jsonTextReader)
        {
            var repositories = new List<CovidUsModel>();
            var currentPropertyName = string.Empty;
            CovidUsModel model = null;
            while (jsonTextReader.Read())
            {
                switch (jsonTextReader.TokenType)
                {
                    case JsonToken.StartObject:
                        model = new CovidUsModel();
                        continue;
                    case JsonToken.EndObject:
                        repositories.Add(model);
                        continue;
                    case JsonToken.PropertyName:
                        currentPropertyName = jsonTextReader.Value.ToString();
                        continue;
                    case JsonToken.String:
                        switch (currentPropertyName)
                        {
                            case "hash":
                                model.hash = jsonTextReader.Value.ToString();
                                continue;
                        }
                        continue;
                    case JsonToken.Integer:
                        switch (currentPropertyName)
                        {
                            case "hospitalized":
                                model.hospitalized = int.Parse(jsonTextReader.Value.ToString());
                                continue;
                            case "hospitalizedCurrently":
                                model.hospitalizedCurrently = int.Parse(jsonTextReader.Value.ToString());
                                continue;
                        }
                        continue;
                }
            }
            return repositories;
        }
    }
}
