using Microsoft.Extensions.DependencyInjection;
using TestMaker.Business.Admin.Domain.Services;
using TestMaker.EventService.Infrastructure.Services;

namespace TestMaker.EventService.Infrastructure.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddTransient(this IServiceCollection service)
        {
            service.AddAutoMapperProfiles();

            service.AddTransient<ICandidatesService, CandidatesService>();
            service.AddTransient<IEventsService, EventsService>();
            return service;
        }
    }
}
