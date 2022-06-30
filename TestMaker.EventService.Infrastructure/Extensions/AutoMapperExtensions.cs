using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using TestMaker.EventService.Domain.Models.Candidate;
using TestMaker.EventService.Domain.Models.Event;
using TestMaker.EventService.Infrastructure.Entities;

namespace TestMaker.EventService.Infrastructure.Extensions
{
    public static class AutoMapperExtensions
    {
        public static IServiceCollection AddAutoMapperProfiles(this IServiceCollection service)
        {
            service.AddAutoMapper(typeof(AutoMapperProfile).Assembly);
            return service;
        }
    }
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<EventForCreating, Event>();
            CreateMap<EventForEditing, Event>();
            CreateMap<Event, EventForList>()
                .ForMember(eventForList => eventForList.Type, 
                    option => option.MapFrom(e => e.TypeAsEnum.GetName())
                );
            CreateMap<Event, EventForDetails>();

            CreateMap<CandidateForCreating, Candidate>();
            CreateMap<CandidateForEditing, Candidate>();
            CreateMap<Candidate, CandidateForList>();
            CreateMap<Candidate, CandidateForDetails>();
        }
    }
}
