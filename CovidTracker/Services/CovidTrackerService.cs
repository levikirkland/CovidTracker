using AutoMapper;
using CovidTracker.Client.Factories;
using CovidTracker.Client.Responses;
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


            try
            {
                string uri = ($"states/{state}/current.json");
                var covidClient = _client.Create();
                var response = await covidClient.GetAsync(uri, cancellationToken);
                var stateResp = _mapper.Map<IEnumerable<CovidStateModel>>(response);
                return stateResp;
            }
            catch (HttpRequestException)
            {
                _logger.LogError("Covid Tracker Fatal Error: GetByState_Current {state}", state);
            }
            return default!;
        }

        public async Task<IReadOnlyCollection<CovidStateModel>> GetByState_Date(string state, string dt)
        {
            CancellationToken cancellationToken = new CancellationToken();


            try
            {
                var uri = ($"states/{state}/{dt}.json");
                var covidClient = _client.Create();
                var response = await covidClient.GetAsync(uri, cancellationToken);
                var stateResp = _mapper.Map<IReadOnlyCollection<CovidStateModel>>(response);
                return stateResp.ToList();
            }
            catch (HttpRequestException)
            {
                _logger.LogError("Covid Tracker Fatal Error: GetByState_Date {state},{date}", state, dt);
            }
            return default!;
        }

        public async Task<IReadOnlyCollection<CovidStateModel>> GetCurrentStatesDaily()
        {
            CancellationToken cancellationToken = new CancellationToken();
            try
            {

                var covidClient = _client.Create();
                var uri = $"states/current.json";
                var response = await covidClient.GetAsync(uri, cancellationToken);
                var stateResp = _mapper.Map<IReadOnlyCollection<CovidStateModel>>(response).OrderBy(x => x.positive);
                return stateResp.ToList();
            }
            catch (HttpRequestException)
            {
                _logger.LogError("Covid Tracker Fatal Error: GetCurrentStatesDaily");
            }
            return default!;
        }


    }
}