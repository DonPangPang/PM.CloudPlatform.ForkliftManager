using Microsoft.Extensions.DependencyInjection;

namespace PM.CloudPlatform.ForkliftManager.Apis.Extensions
{
    /// <summary>
    ///
    /// </summary>
    public static class JTT808Extensions
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddJTT808(this IServiceCollection services)
        {
            //services.AddSingleton<JT808Serializer>();
            return services;
        }
    }
}