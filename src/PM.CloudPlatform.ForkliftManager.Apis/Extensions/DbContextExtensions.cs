using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PM.CloudPlatform.ForkliftManager.Apis.Config;
using PM.CloudPlatform.ForkliftManager.Apis.Data;
using System;
using System.Linq;

namespace PM.CloudPlatform.ForkliftManager.Apis.Extensions
{
    /// <summary>
    /// 数据库上下文扩展
    /// </summary>
    public static class DbContextExtensions
    {
        /// <summary>
        /// 添加实体到数据库上下文中
        /// </summary>
        /// <param name="modelBuilder"> </param>
        /// <param name="entityType">   </param>
        /// <returns> </returns>
        public static ModelBuilder AddEntityTypes(this ModelBuilder modelBuilder, Type entityType)
        {
            var types = entityType.Assembly.GetTypes().AsEnumerable();
            var entityTypes = types.Where(t => !t.IsAbstract && t.IsSubclassOf(entityType));

            foreach (var type in entityTypes)
            {
                if (modelBuilder.Model.FindEntityType(type) is null)
                {
                    modelBuilder.Model.AddEntityType(type);
                }
            }

            return modelBuilder;
        }

        /// <summary>
        /// 配置数据库上下文
        /// <para> 可切换数据库 </para>
        /// </summary>
        /// <param name="services"> </param>
        /// <param name="config">   </param>
        /// <returns> </returns>
        public static IServiceCollection AddDb(this IServiceCollection services, IConfiguration config)
        {
            var dbSettings = config.GetSection("DbSettings").Get<DbSettings>().Settings;
            var setting = dbSettings.FirstOrDefault(x => x.IsEnable);

            services.AddDbContext<ForkliftManagerDbContext>(opts =>
            {
                switch (setting.Name)
                {
                    case "Sqlite":
                        opts.UseSqlite(setting.ConnectionString);
                        break;

                    case "Npgsql":
                        opts.UseNpgsql(setting.ConnectionString);
                        break;

                    case "SqlServer":
                    default:
                        opts.UseSqlite(setting.ConnectionString);
                        break;
                }
                //opts.UseNpgsql("Host=192.168.31.39;Database=forklift;Username=postgres;Password=postgres");
                //opts.UseSqlite("Data Source=forklift.db");
            });
            return services;
        }
    }
}