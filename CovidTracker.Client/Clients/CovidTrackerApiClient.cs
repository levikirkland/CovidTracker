using CovidTracker.Client.Resovers;
using CovidTracker.Client.Responses;
using Polly.Retry;
using Polly;
using System.Net;
using CovidTracker.Client.JsonDeserializers;
using Newtonsoft.Json;

namespace CovidTracker.Client.Clients
{
    public class CovidTrackerApiClient : ICovidTrackerApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly StateDeserializer _jsonSerializer;

        public CovidTrackerApiClient(HttpClient httpClient, StateDeserializer jsonSerializer)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _jsonSerializer = jsonSerializer ?? throw new ArgumentNullException(nameof(jsonSerializer));
        }

        public async Task<IEnumerable<StateResponse>> GetCurrentStatesDaily(CancellationToken cancellationToken)
        {
            var request = CreateRequest($"states/current.json");
            using (var result = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false))
            {
                result.EnsureSuccessStatusCode();
                using (var streamReader = new StreamReader(await result.Content.ReadAsStreamAsync()))
                using (var jsonTextReader = new JsonTextReader(streamReader))
                {
                    return _jsonSerializer.Deserialize(jsonTextReader);
                }
            }
        }

        public async Task<IEnumerable<StateResponse>> GetByState_Date(string state, string dt, CancellationToken cancellationToken)
        {
            var request = CreateRequest($"states/{state}/{dt}.json");

            using (var result = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false))
            {
                result.EnsureSuccessStatusCode();
                using (var streamReader = new StreamReader(await result.Content.ReadAsStreamAsync()))
                using (var jsonTextReader = new JsonTextReader(streamReader))
                {
                    return _jsonSerializer.Deserialize(jsonTextReader);
                }
            }
        }

        public async Task<IEnumerable<StateResponse>> GetByState_Current<StateReponse>(string state, CancellationToken cancellationToken)
        {
            var request = CreateRequest($"states/{state}/current.json");

            using (var result = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false))
            {
                result.EnsureSuccessStatusCode();
                using (var streamReader = new StreamReader(await result.Content.ReadAsStreamAsync()))
                using (var jsonTextReader = new JsonTextReader(streamReader))
                {
                    return _jsonSerializer.Deserialize(jsonTextReader);
                }
            }
        }

        private static HttpRequestMessage CreateRequest(string uri)
        {
            return new HttpRequestMessage(HttpMethod.Get, uri);
        }
    }
}
