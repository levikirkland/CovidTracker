using CovidTracker.Client.Responses;

namespace CovidTracker.Client.Clients
{
    public interface ICovidTrackerApiClient
    {
        Task<IEnumerable<StateResponse>> GetByState_Current<StateReponse>(string state, CancellationToken cancellationToken);
        Task<IEnumerable<StateResponse>> GetByState_Date(string state, string dt, CancellationToken cancellationToken);
        Task<IEnumerable<StateResponse>> GetCurrentStatesDaily(CancellationToken cancellationToken);
    }
}