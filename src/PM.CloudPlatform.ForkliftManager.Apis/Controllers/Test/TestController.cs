using System;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using PM.CloudPlatform.ForkliftManager.Apis.Controllers.Base;
using PM.CloudPlatform.ForkliftManager.Apis.Entities;
using PM.CloudPlatform.ForkliftManager.Apis.General;
using System.Threading.Tasks;
using NetTopologySuite.Geometries;

namespace PM.CloudPlatform.ForkliftManager.Apis.Controllers.Test
{
    [ApiController]
    [EnableCors("Any")]
    [Route("api/[Controller]/[Action]")]
    public class TestController : ControllerBase
    {
        private readonly IGeneralRepository _generalRepository;

        public TestController(IGeneralRepository generalRepository)
        {
            _generalRepository = generalRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> InsertTestDatas(int count)
        {
            for (var i = 0; i < count; i++)
            {
                //c2bc571a-0692-4c6b-8a15-b0795825501e,2022-02-15 16:20:07.000000,113.55176,34.8265844444444,0,460,0,10690,56472,1,0,0,37552,2022-02-15 16:20:09.165824,,868120278343188,,,,true,false,0,12,0,15,0,4096,1024,b2b29abd-e717-400d-8a4d-137b86dab974,,0101000000B722E284CD6941406CCF2C0950635C40
                var temp = new GpsPositionRecord()
                {
                    DateTime = DateTime.Now,
                    Lon = 113.55176M,
                    Lat = 34.8265844444444M,
                    Speed = 0,
                    MCC = 460,
                    MNC = 0,
                    LAC = 10690,
                    CellId = 56472,
                    AccState = (NbazhGPS.Protocol.Enums.AccState?)1,
                    DataReportingMode = 0,
                    GpsRealTimeHeadIn = 0,
                    Mileage = 37552,
                    EorWLon = 0,
                    GpsInfoLength = 12,
                    GpsLocatedFunc = 0,
                    GpsSatelliteCount = 15,
                    Heading = 0,
                    IsGpsLocated = (NbazhGPS.Protocol.Enums.PackageEnums0X22.IsGpsLocated?)4096,
                    SorNLat = (NbazhGPS.Protocol.Enums.PackageEnums0X22.SorNLat)1024,
                    TerminalId = Guid.Parse("90e54999-e03e-ec90-afd0-5ef5c9cab835"),
                    Point = new Point(113.55176, 34.8265844444444)
                };

                temp.Create();

                await _generalRepository.InsertAsync(temp);
            }
            await _generalRepository.SaveAsync();

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> InsertTestDatas2(int count)
        {
            await Task.Run(async () =>
            {
                for (var i = 0; i < count; i++)
                {
                    //c2bc571a-0692-4c6b-8a15-b0795825501e,2022-02-15 16:20:07.000000,113.55176,34.8265844444444,0,460,0,10690,56472,1,0,0,37552,2022-02-15 16:20:09.165824,,868120278343188,,,,true,false,0,12,0,15,0,4096,1024,b2b29abd-e717-400d-8a4d-137b86dab974,,0101000000B722E284CD6941406CCF2C0950635C40
                    var temp = new GpsPositionRecord()
                    {
                        Id = Guid.NewGuid(),
                        DateTime = DateTime.Now,
                        Lon = 113.55176M,
                        Lat = 34.8265844444444M,
                        Speed = 0,
                        MCC = 460,
                        MNC = 0,
                        LAC = 10690,
                        CellId = 56472,
                        AccState = (NbazhGPS.Protocol.Enums.AccState?)1,
                        DataReportingMode = 0,
                        GpsRealTimeHeadIn = 0,
                        Mileage = 37552,
                        EorWLon = 0,
                        GpsInfoLength = 12,
                        GpsLocatedFunc = 0,
                        GpsSatelliteCount = 15,
                        Heading = 999,
                        IsGpsLocated = (NbazhGPS.Protocol.Enums.PackageEnums0X22.IsGpsLocated?)4096,
                        SorNLat = (NbazhGPS.Protocol.Enums.PackageEnums0X22.SorNLat)1024,
                        TerminalId = Guid.Parse("90e54999-e03e-ec90-afd0-5ef5c9cab835"),
                        Point = new Point(113.55176, 34.8265844444444)
                    };

                    await _generalRepository.InsertAsync(temp);

                    if(i % 500 == 0)
                    {
                        await _generalRepository.SaveAsync();
                    }
                    
                }
            });
            return Ok();
        }
    }
}
