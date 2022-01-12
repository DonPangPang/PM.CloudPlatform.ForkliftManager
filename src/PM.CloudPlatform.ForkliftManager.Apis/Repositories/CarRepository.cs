using PM.CloudPlatform.ForkliftManager.Apis.Data;
using PM.CloudPlatform.ForkliftManager.Apis.Entities;
using PM.CloudPlatform.ForkliftManager.Apis.Repositories.Base;

namespace PM.CloudPlatform.ForkliftManager.Apis.Repositories
{
    /// <summary>
    /// 车辆档案
    /// </summary>
    public class CarRepository : RepositoryBase<Car>
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="dbContext"></param>
        public CarRepository(ForkliftManagerDbContext dbContext) : base(dbContext)
        {
        }
    }
}