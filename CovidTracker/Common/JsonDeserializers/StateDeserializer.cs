using CovidTracker.Models;
using Newtonsoft.Json;

namespace CovidTracker.Common.JsonDeserializers
{
    public sealed class StateDeserializer
    {
        public IReadOnlyCollection<CovidStateModel> Deserialize(JsonTextReader jsonTextReader)
        {
            var repositories = new List<CovidStateModel>();
            var currentPropertyName = string.Empty;
            CovidStateModel? model = null;
            while (jsonTextReader.Read())
            {
                switch (jsonTextReader.TokenType)
                {
                    case JsonToken.StartObject:
                        model = new CovidStateModel();
                        continue;
                    case JsonToken.EndObject:
                        repositories.Add(model);
                        continue;
                    case JsonToken.PropertyName:
                        currentPropertyName = jsonTextReader.Value!.ToString();
                        continue;
                    case JsonToken.String:
                        switch (currentPropertyName)
                        {
                            case "name":
                                model.grade = jsonTextReader.Value!.ToString();
                                continue;
                            case "url":
                                model.state = jsonTextReader.Value!.ToString();
                                continue;
                        }
                        continue;
                    case JsonToken.Integer:
                        switch (currentPropertyName)
                        {
                            case "stars":
                                model.hospitalized = int.Parse(jsonTextReader.Value.ToString());
                                continue;
                        }
                        continue;
                }
            }
            return repositories;
        }
    }
}
