using Microsoft.Extensions.DependencyInjection;
using TestMaker.Business.Admin.Domain.Services;
using TM.EventService.Infrastructure.Services;

namespace TM.EventService.Infrastructure.Extensions
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
