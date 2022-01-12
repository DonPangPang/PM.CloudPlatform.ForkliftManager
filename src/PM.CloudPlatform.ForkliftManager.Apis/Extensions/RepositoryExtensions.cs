using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;

namespace PM.CloudPlatform.ForkliftManager.Apis.Extensions
{
    /// <summary>
    /// 仓储扩展
    /// </summary>
    public static class RepositoryExtensions
    {
        /// <summary>
        /// 使用命名空间加载仓储
        /// </summary>
        /// <param name="services"></param>
        /// <param name="repoNamespace"></param>
        /// <returns></returns>
        public static IServiceCollection AddRepositoryByNamespace(this IServiceCollection services, string repoNamespace)
        {
            var types = Assembly.GetExecutingAssembly().GetTypes().AsEnumerable();
            var repositories = types.Where(t => t.Namespace == repoNamespace
                                                && !t.IsAbstract && !t.IsInterface);

            foreach (var repo in repositories)
            {
                services.AddScoped(repo);
            }

            return services;
        }

        /// <summary>
        /// 使用基类加载仓储
        /// </summary>
        /// <param name="services"></param>
        /// <param name="repoType"></param>
        /// <returns></returns>
        public static IServiceCollection AddRepositoryByClass(this IServiceCollection services, Type repoType)
        {
            var types = Assembly.GetExecutingAssembly().GetTypes().AsEnumerable();
            var repositories = types.Where(t => t.IsSubclassOf(repoType)
                                                && !t.IsAbstract && !t.IsInterface);

            foreach (var repo in repositories)
            {
                services.AddScoped(repo);
            }

            return services;
        }
    }
}