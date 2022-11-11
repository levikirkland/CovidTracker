using CovidTracker.Client.Responses;
using Newtonsoft.Json;

namespace CovidTracker.Client.JsonDeserializers
{
    public class StateDeserializer
    {
        public IReadOnlyCollection<StateResponse> Deserialize(JsonTextReader jsonTextReader)
        {
            var repositories = new List<StateResponse>();
            var currentPropertyName = string.Empty;
            StateResponse? model = null;
            while (jsonTextReader.Read())
            {
                switch (jsonTextReader.TokenType)
                {
                    case JsonToken.StartObject:
                        model = new StateResponse();
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
                            case "lastUpdateEt":
                                model.dateModified = DateTime.Parse(jsonTextReader.Value!.ToString());
                                continue;
                            case "state":
                                model.state = jsonTextReader.Value!.ToString();
                                continue;
                        }
                        continue;
                    case JsonToken.Integer:
                        switch (currentPropertyName)
                        {
                            case "positive":
                                model.positive = int.Parse(jsonTextReader.Value.ToString());
                                continue;
                            case "negative":
                                model.negative = int.Parse(jsonTextReader.Value.ToString());
                                continue;
                            case "total":
                                model.total = int.Parse(jsonTextReader.Value.ToString());
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
