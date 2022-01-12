using PM.CloudPlatform.ForkliftManager.Apis.Data;
using PM.CloudPlatform.ForkliftManager.Apis.Entities;
using PM.CloudPlatform.ForkliftManager.Apis.Repositories.Base;

namespace PM.CloudPlatform.ForkliftManager.Apis.Repositories
{
    /// <summary>
    /// 车辆类型
    /// </summary>
    public class CarTypeRepository : RepositoryBase<CarType>
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="dbContext"></param>
        public CarTypeRepository(ForkliftManagerDbContext dbContext) : base(dbContext)
        {
        }
    }
}