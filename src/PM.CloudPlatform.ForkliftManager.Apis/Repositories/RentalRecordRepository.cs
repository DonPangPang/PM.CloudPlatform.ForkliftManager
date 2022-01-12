using PM.CloudPlatform.ForkliftManager.Apis.Data;
using PM.CloudPlatform.ForkliftManager.Apis.Entities;
using PM.CloudPlatform.ForkliftManager.Apis.Repositories.Base;

namespace PM.CloudPlatform.ForkliftManager.Apis.Repositories
{
    /// <summary>
    /// 租借记录
    /// </summary>
    public class RentalRecordRepository : RepositoryBase<RentalRecord>
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="dbContext"></param>
        public RentalRecordRepository(ForkliftManagerDbContext dbContext) : base(dbContext)
        {
        }
    }
}