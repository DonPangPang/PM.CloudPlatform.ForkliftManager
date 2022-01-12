using PM.CloudPlatform.ForkliftManager.Apis.Data;
using PM.CloudPlatform.ForkliftManager.Apis.Entities;
using PM.CloudPlatform.ForkliftManager.Apis.Repositories.Base;

namespace PM.CloudPlatform.ForkliftManager.Apis.Repositories
{
    public class GpsPositionRecordRepository : RepositoryBase<GpsPositionRecord>
    {
        public GpsPositionRecordRepository(ForkliftManagerDbContext dbContext) : base(dbContext)
        {
        }
    }
}