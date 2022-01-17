using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pang.AutoMapperMiddleware;
using PM.CloudPlatform.ForkliftManager.Apis.Controllers.Base;
using PM.CloudPlatform.ForkliftManager.Apis.Entities;
using PM.CloudPlatform.ForkliftManager.Apis.Enums;
using PM.CloudPlatform.ForkliftManager.Apis.General;
using PM.CloudPlatform.ForkliftManager.Apis.Models;
using PM.CloudPlatform.ForkliftManager.Apis.Repositories;

namespace PM.CloudPlatform.ForkliftManager.Apis.Controllers
{
    /// <summary>
    /// 车辆档案
    /// </summary>
    [ApiController]
    [EnableCors("Any")]
    [Route("api/[Controller]/[Action]")]
    [Authorize]
    public class CarController : MyControllerBase<CarRepository, Car, CarDto, CarAddOrUpdateDto>
    {
        private readonly CarRepository _repository;
        private readonly IMapper _mapper;
        private readonly IGeneralRepository _generalRepository;

        private readonly IQueryable<Car> Cars;

        /// <summary>
        /// </summary>
        /// <param name="repository">        </param>
        /// <param name="mapper">            </param>
        /// <param name="generalRepository"> </param>
        public CarController(CarRepository repository, IMapper mapper, IGeneralRepository generalRepository) : base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _generalRepository = generalRepository;
            Cars = _generalRepository.GetQueryable<Car>();
        }

        /// <summary>
        /// 获取车辆状态
        /// </summary>
        /// <returns> </returns>
        [HttpGet]
        public async Task<IActionResult> GetCarArchives()
        {
            //var res = await Users.SelectMany(u => u.Roles!.Select(r => new { u, r })).ToListAsync();

            //var res = await Cars.SelectMany(x =>
            //    x.RentalRecords!.Select(r => new { x, r }).Where(t => t.r.IsReturn || t.x.RentalRecords!.Count <= 0)).ToListAsync();

            var data = await Cars.Include(x => x.CarType)
                .Include(x => x.RentalRecords!.Where(t => !t.IsReturn))
                .ToListAsync();

            var returnDto = data.MapTo<CarDto>();

            return Success(returnDto);
        }

        /// <summary>
        /// 获取车辆和终端的信息
        /// </summary>
        /// <returns> </returns>
        [HttpGet]
        public async Task<IActionResult> GetCarTerminals()
        {
            var data = await _generalRepository.GetQueryable<Car>().Include(x => x.Terminal)
                .Select(x => new
                {
                    // 可以往里面填写自己需要的数据
                    CarId = x.Id,
                    CarTypeName = x.CarType!.ToString(),
                    IMEI = x.Terminal!.IMEI
                })
                .ToListAsync();

            return Success(data);
        }

        /// <summary>
        /// 给车辆绑定终端信息
        /// </summary>
        /// <param name="carId">       车辆Id </param>
        /// <param name="terminalId">  终端Id </param>
        /// <param name="description"> 描述 </param>
        /// <returns> </returns>
        [HttpGet]
        public async Task<IActionResult> SetCarTerminal(Guid carId, Guid terminalId, string description)
        {
            var car = await _generalRepository.FindAsync<Car>(x => x.Id.Equals(carId));

            car.TerminalId = terminalId;

            var bindRecord = new TerminalBindRecord()
            {
                CarId = carId,
                TerminalId = terminalId,
                Description = description
            };

            bindRecord.Create();

            await _generalRepository.InsertAsync(bindRecord);
            await _generalRepository.UpdateAsync(car);
            await _generalRepository.SaveAsync();

            return Success("保存成功");
        }
    }
}