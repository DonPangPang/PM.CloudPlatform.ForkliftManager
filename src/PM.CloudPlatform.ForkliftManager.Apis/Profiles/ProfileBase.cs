using AutoMapper;
using PM.CloudPlatform.ForkliftManager.Apis.Entities;
using PM.CloudPlatform.ForkliftManager.Apis.Models;

namespace PM.CloudPlatform.ForkliftManager.Apis.Profiles
{
    /// <summary>
    /// 映射配置
    /// </summary>
    public class ProfileBase : Profile
    {
        /// <summary>
        ///
        /// </summary>
        public ProfileBase()
        {
            CreateMap<Car, CarDto>();
            CreateMap<CarDto, Car>();

            CreateMap<CarType, CarTypeDto>();
            CreateMap<CarTypeDto, CarType>();

            CreateMap<ElectronicFence, ElectronicFenceDto>();
            CreateMap<ElectronicFenceDto, ElectronicFence>();

            CreateMap<RentalCompany, RentalCompanyDto>();
            CreateMap<RentalCompanyDto, RentalCompany>();

            CreateMap<RentalRecord, RentalRecordDto>();
            CreateMap<RentalRecordDto, RentalRecord>();

            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();

            CreateMap<UseRecord, UseRecordDto>();
            CreateMap<UseRecordDto, UseRecord>();
        }
    }
}