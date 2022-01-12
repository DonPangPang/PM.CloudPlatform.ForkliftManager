using PM.CloudPlatform.ForkliftManager.Apis.Data;
using PM.CloudPlatform.ForkliftManager.Apis.Entities;
using PM.CloudPlatform.ForkliftManager.Apis.Repositories.Base;

namespace PM.CloudPlatform.ForkliftManager.Apis.Repositories
{
    /// <summary>
    /// 电子围栏
    /// </summary>
    public class ElectronicFenceRepository : RepositoryBase<ElectronicFence>
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="dbContext"></param>
        public ElectronicFenceRepository(ForkliftManagerDbContext dbContext) : base(dbContext)
        {
        }
    }
}