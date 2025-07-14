using Microsoft.Extensions.DependencyInjection;
using VituraHealth.PrescriptionManagement.Infrastructure;
using VituraHealth.PrescriptionManagement.Infrastructure.DataAccess;
using VituraHealth.PrescriptionManagement.Infrastructure.DataAccess.Json;
using VituraHealth.PrescriptionManagement.Infrastructure.Settings;
using Xunit;

namespace VituraHealth.PrescriptionManagement.Infrastructure.Test
{
    public class ExtensionsTests
    {
        [Fact]
        public void UseJsonInMemoryPrescriptionData_RegistersJsonPrescriptionDataAccessorAsSingleton()
        {
            // Arrange
            var services = new ServiceCollection();
            services.AddSingleton<JsonDataStoreConfig>(new JsonDataStoreConfig { /* set properties as needed */ });

            // Act
            services.UseJsonInMemoryPrescriptionData();
            var provider = services.BuildServiceProvider();
            var accessor1 = provider.GetService<IPrescriptionDataAccessor>();
            var accessor2 = provider.GetService<IPrescriptionDataAccessor>();

            // Assert
            Assert.NotNull(accessor1);
            Assert.IsType<JsonPrescriptionDataAccessor>(accessor1);
            Assert.Same(accessor1, accessor2); // Singleton: same instance
        }
    }
}