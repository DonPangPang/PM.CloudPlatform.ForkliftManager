using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using PM.CloudPlatform.ForkliftManager.Apis.Data;
using PM.CloudPlatform.ForkliftManager.Apis.Entities;
using PM.CloudPlatform.ForkliftManager.Apis.Repositories.Base;

namespace PM.CloudPlatform.ForkliftManager.Apis.Repositories
{
    public class CarMaintenanceRecordRepository : RepositoryBase<CarMaintenanceRecord>
    {
        public CarMaintenanceRecordRepository(ForkliftManagerDbContext dbContext) : base(dbContext)
        {
        }
    }
}