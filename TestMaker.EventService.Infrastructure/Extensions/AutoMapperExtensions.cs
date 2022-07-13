using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using TestMaker.Common.Extensions;
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
            CreateMap<Event, EventForEditing>();
            CreateMap<Event, EventForList>()
                .ForMember(eventForList => eventForList.ScopeType, 
                    option => option.MapFrom(e => e.ScopeTypeAsEnum.GetEnumName())
                )
                .ForMember(eventForList => eventForList.QuestionContentType, 
                    option => option.MapFrom(e => e.QuestionContentTypeAsEnum.GetEnumName())
                );
            CreateMap<Event, EventForDetails>();

            CreateMap<CandidateForCreating, Candidate>();
            CreateMap<CandidateForEditing, Candidate>();
            CreateMap<Candidate, CandidateForEditing>();
            CreateMap<Candidate, CandidateForList>();
            CreateMap<Candidate, CandidateForDetails>();
        }
    }
}
