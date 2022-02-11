using System;
using PM.CloudPlatform.ForkliftManager.Apis.Data;
using PM.CloudPlatform.ForkliftManager.Apis.Entities;
using PM.CloudPlatform.ForkliftManager.Apis.Repositories.Base;

namespace PM.CloudPlatform.ForkliftManager.Apis.Repositories
{
    public class CommandsRepository: RepositoryBase<Commands>
    {
        public CommandsRepository(ForkliftManagerDbContext dbContext) : base(dbContext)
        {
        }
    }
}
