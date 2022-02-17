using PM.CloudPlatform.ForkliftManager.Apis.Data;
using PM.CloudPlatform.ForkliftManager.Apis.Entities;
using PM.CloudPlatform.ForkliftManager.Apis.Repositories.Base;

namespace PM.CloudPlatform.ForkliftManager.Apis.Repositories
{
    public class TerminalBindRecordsRepository : RepositoryBase<TerminalBindRecord>
    {
        // <summary>
        ///
        /// </summary>
        /// <param name="dbContext"></param>
        public TerminalBindRecordsRepository(ForkliftManagerDbContext dbContext) : base(dbContext)
        {
        }
    }
}
