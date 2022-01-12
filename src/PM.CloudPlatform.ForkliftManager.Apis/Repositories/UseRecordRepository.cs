using PM.CloudPlatform.ForkliftManager.Apis.Data;
using PM.CloudPlatform.ForkliftManager.Apis.Entities;
using PM.CloudPlatform.ForkliftManager.Apis.Repositories.Base;

namespace PM.CloudPlatform.ForkliftManager.Apis.Repositories
{
    /// <summary>
    /// 使用记录
    /// </summary>
    public class UseRecordRepository : RepositoryBase<UseRecord>
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="dbContext"></param>
        public UseRecordRepository(ForkliftManagerDbContext dbContext) : base(dbContext)
        {
        }
    }
}