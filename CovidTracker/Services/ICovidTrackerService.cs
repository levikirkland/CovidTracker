using CovidTracker.Models;

namespace CovidTracker.Services
{
    public interface ICovidTrackerService
    {
        Task<IEnumerable<CovidStateModel>> GetByState_Current(string state);
        Task<IEnumerable<CovidStateModel>> GetByState_Date(string state, string dt);
        Task<IEnumerable<CovidStateModel>> GetCurrentStatesDaily();
    }
}