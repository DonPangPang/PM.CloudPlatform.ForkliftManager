using PM.CloudPlatform.ForkliftManager.Apis.Data;
using PM.CloudPlatform.ForkliftManager.Apis.Entities;
using PM.CloudPlatform.ForkliftManager.Apis.Repositories.Base;

namespace PM.CloudPlatform.ForkliftManager.Apis.Repositories
{
    /// <summary>
    /// 用户
    /// </summary>
    public class UserRepository : RepositoryBase<User>
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="dbContext"></param>
        public UserRepository(ForkliftManagerDbContext dbContext) : base(dbContext)
        {
        }
    }
}