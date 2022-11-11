using AutoMapper;
using CovidTracker.Client.Factories;
using CovidTracker.Models;
using System.Collections.Generic;
using System.Linq;

namespace CovidTracker.Services
{
    public class CovidTrackerService : ICovidTrackerService
    {
        private readonly IMapper _mapper;
        private readonly CovidTrackerFactory _client;
        private readonly ILogger<CovidTrackerService> _logger;

        public CovidTrackerService(IMapper mapper, CovidTrackerFactory client, ILogger<CovidTrackerService> logger)
        {
            _mapper = mapper;
            _client = client;
            _logger = logger;
        }

        public async Task<IEnumerable<CovidStateModel>> GetByState_Current(string state)
        {
            CancellationToken cancellationToken = new CancellationToken();
            var covidClient = _client.Create();

            try
            {
                var response = await covidClient.GetByState_Current<CovidStateModel>(state, cancellationToken);
                var stateResp = _mapper.Map<IEnumerable<CovidStateModel>>(response);
                return stateResp;
            }
            catch (HttpRequestException)
            {
                _logger.LogError("Covid Tracker Fatal Error: GetByState_Current {state}", state);
            }
            return default!;
        }

        public async Task<IEnumerable<CovidStateModel>> GetByState_Date(string state, string dt)
        {
            CancellationToken cancellationToken = new CancellationToken();

            var covidClient = _client.Create();
            try
            {
                var response = await covidClient.GetByState_Date(state, dt, cancellationToken);
                var stateResp = _mapper.Map<IEnumerable<CovidStateModel>>(response);
                return stateResp.ToList();
            }
            catch (HttpRequestException)
            {
                _logger.LogError("Covid Tracker Fatal Error: GetByState_Date {state},{date}", state, dt);
            }
            return default!;
        }

        public async Task<IEnumerable<CovidStateModel>> GetCurrentStatesDaily()
        {
            CancellationToken cancellationToken = new CancellationToken();
            try
            {

                var covidClient = _client.Create();
                var response = await covidClient.GetCurrentStatesDaily(cancellationToken);
                var stateResp = _mapper.Map<IEnumerable<CovidStateModel>>(response).OrderBy(x => x.positive);
                return stateResp.Reverse();
            }
            catch (HttpRequestException)
            {
                _logger.LogError("Covid Tracker Fatal Error: GetCurrentStatesDaily");
            }
            return default!;
        }


    }
}
