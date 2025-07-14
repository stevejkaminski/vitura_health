using Microsoft.Extensions.DependencyInjection;
using VituraHealth.PrescriptionManagement.Infrastructure.DataAccess;
using VituraHealth.PrescriptionManagement.Infrastructure.DataAccess.Json;

namespace VituraHealth.PrescriptionManagement.Infrastructure
{
    /// <summary>
    /// Extension methods for the Prescription Management Infrastructure layer
    /// Decouples the application layer from specific external dependencies
    /// </summary>
    public static class Extensions
    {
        public static IServiceCollection UseJsonInMemoryPrescriptionData(this IServiceCollection services)
        {
            services.AddSingleton<IPrescriptionDataAccessor, JsonPrescriptionDataAccessor>();
            return services;
        }
    }
}
