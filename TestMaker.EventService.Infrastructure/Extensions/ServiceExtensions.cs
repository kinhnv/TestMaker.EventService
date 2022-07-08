using Microsoft.Extensions.DependencyInjection;
using TestMaker.EventService.Domain.Services;
using TestMaker.EventService.Infrastructure.Repositories.CandidateAnswers;
using TestMaker.EventService.Infrastructure.Repositories.Candidates;
using TestMaker.EventService.Infrastructure.Repositories.Events;
using TestMaker.EventService.Infrastructure.Services;

namespace TestMaker.EventService.Infrastructure.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddTransientInfrastructure(this IServiceCollection service)
        {
            service.AddAutoMapperProfiles();
            
            service.AddTransient<IEventsRepository, EventsRepository>();
            service.AddTransient<ICandidatesRepository, CandidatesRepository>();
            service.AddTransient<ICandidateAnswersRepository, CandidateAnswersRepository>();

            service.AddTransient<IEventsService, EventsService>();
            service.AddTransient<ICandidatesService, CandidatesService>();
            return service;
        }
    }
}
