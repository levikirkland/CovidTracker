using CovidTracker.Client.JsonDeserializers;
using CovidTracker.Client.Responses;
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

        public async Task<IReadOnlyCollection<StateResponse>> GetAsync(string uri, CancellationToken cancellationToken)
        {
            var request = CreateRequest(uri);
            using var result = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
            using var responseStream = await result.Content.ReadAsStreamAsync(cancellationToken);
            using var streamReader = new StreamReader(responseStream);
            using var jsonTextReader = new JsonTextReader(streamReader);
            return _jsonSerializer.Deserialize(jsonTextReader)!;
        }

        private static HttpRequestMessage CreateRequest(string uri)
        {
            return new HttpRequestMessage(HttpMethod.Get, uri);
        }
    }
}
