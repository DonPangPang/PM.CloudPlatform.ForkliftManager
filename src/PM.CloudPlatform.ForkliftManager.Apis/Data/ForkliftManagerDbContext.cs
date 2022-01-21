using Microsoft.EntityFrameworkCore;
using PM.CloudPlatform.ForkliftManager.Apis.Entities.Base;
using PM.CloudPlatform.ForkliftManager.Apis.Extensions;

namespace PM.CloudPlatform.ForkliftManager.Apis.Data
{
    /// <summary>
    /// </summary>
    public class ForkliftManagerDbContext : DbContext
    {
        /// <summary>
        /// </summary>
        /// <param name="options"> </param>
        public ForkliftManagerDbContext(DbContextOptions<ForkliftManagerDbContext> options) : base(options)
        {
        }

        /// <summary>
        /// </summary>
        /// <param name="modelBuilder"> </param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.AddEntityTypes(typeof(EntityBase));

            base.OnModelCreating(modelBuilder);
        }
    }
}