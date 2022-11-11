using CovidTracker.Client.Responses;
using CovidTracker.Models;
using AutoMapper;

namespace CovidTracker.Common.Profiles
{
    public class StateProfile : Profile
    {
        public StateProfile()
        {
            CreateMap<StateResponse, CovidStateModel>(); 
        }
    }
}
