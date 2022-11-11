using CovidTracker.Client.Clients;
using Microsoft.Extensions.DependencyInjection;

namespace CovidTracker.Client.Factories
{
    public class CovidTrackerFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public CovidTrackerFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public CovidTrackerApiClient Create()
        {
            return _serviceProvider.GetRequiredService<CovidTrackerApiClient>();
        }
    }
}
