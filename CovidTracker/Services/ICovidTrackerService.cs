using CovidTracker.Models;

namespace CovidTracker.Services
{
    public interface ICovidTrackerService
    {
        Task<IEnumerable<CovidStateModel>> GetByState_Current(string state);
        Task<IReadOnlyCollection<CovidStateModel>> GetByState_Date(string state, string dt);
        Task<IReadOnlyCollection<CovidStateModel>> GetCurrentStatesDaily();
    }
}