using CovidTracker.Client.Responses;

namespace CovidTracker.Client.Clients
{
    public interface ICovidTrackerApiClient
    {
        Task<IReadOnlyCollection<StateResponse>> GetAsync(string uri, CancellationToken cancellationToken);
    }
}