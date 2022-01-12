using PM.CloudPlatform.ForkliftManager.Apis.Data;
using PM.CloudPlatform.ForkliftManager.Apis.Entities;
using PM.CloudPlatform.ForkliftManager.Apis.Repositories.Base;

namespace PM.CloudPlatform.ForkliftManager.Apis.Repositories
{
    /// <summary>
    /// 租借公司
    /// </summary>
    public class RentalCompanyRepository : RepositoryBase<RentalCompany>
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="dbContext"></param>
        public RentalCompanyRepository(ForkliftManagerDbContext dbContext) : base(dbContext)
        {
        }
    }
}