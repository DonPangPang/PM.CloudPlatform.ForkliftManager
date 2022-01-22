using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Npgsql.Replication;
using Pang.AutoMapperMiddleware;
using PM.CloudPlatform.ForkliftManager.Apis.Authorization;
using PM.CloudPlatform.ForkliftManager.Apis.Entities;
using PM.CloudPlatform.ForkliftManager.Apis.General;
using PM.CloudPlatform.ForkliftManager.Apis.Helper;
using PM.CloudPlatform.ForkliftManager.Apis.Models;

namespace PM.CloudPlatform.ForkliftManager.Apis.Controllers
{
    /// <summary>
    /// 授权接口
    /// </summary>
    [ApiController]
    [Route("[Controller]/[Action]")]
    [EnableCors("Any")]
    public class AuthorizationController : ControllerBase
    {
        private readonly IGeneralRepository _generalRepository;
        private PermissionRequirement _tokenParameter;

        public AuthorizationController(IGeneralRepository generalRepository)
        {
            _generalRepository = generalRepository;
            var config = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            _tokenParameter = config.GetSection("TokenParameter").Get<PermissionRequirement>();
        }

        /// <summary>
        /// 注册接口
        /// </summary>
        /// <param name="request"> </param>
        /// <returns> </returns>
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] UserAddOrUpdateDto request)
        {
            if (string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password))
            {
                return Success("账号或密码不能为空");
            }

            if (await _generalRepository.ExistAsync<User>(x => x.Username.Equals(request.Username)))
            {
                return Fail("账号已存在");
            }

            var user = request.MapTo<User>();
            user.Create();
            await _generalRepository.InsertAsync(user);
            if (await _generalRepository.SaveAsync())
            {
                return Success("注册成功");
            }
            else
            {
                return Fail("注册失败");
            }
        }

        /// <summary>
        /// 获取Token
        /// </summary>
        /// <param name="request"> 用户名和密码 </param>
        /// <returns> Token </returns>
        [HttpPost]
        public async Task<IActionResult> RequestToken([FromBody] LoginDto request)
        {
            if (string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password))
                return Fail("Invalid Request");

            if (!(await _generalRepository.ExistAsync<User>(x => x.Username.Equals(request.Username))))
            {
                return Fail("账号不存在");
            }

            var user = await _generalRepository.FindAsync<User>(x => x.Username.Equals(request.Username));
            if (user.Password != request.Password)
            {
                return Fail("密码错误");
            }

            //生成Token和RefreshToken
            var token = GenUserToken(user.Id, request.Username, "admin");
            var refreshToken = "123456";

            //LoginUserInfo.Set(user);

            return Success(new[] { token, refreshToken });
        }

        /// <summary>
        /// 获取登录用户信息
        /// </summary>
        /// <returns> </returns>
        [HttpGet]
        [Authorize]
        public IActionResult GetUserInfo()
        {
            var data = LoginUserInfo.Get<User>();

            return Success(data!);
        }

        //生成Token代码
        private string GenUserToken(Guid id, string username, string role)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, id.ToString()),
                new Claim(ClaimTypes.DateOfBirth,DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenParameter.Secret));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var jwtToken = new JwtSecurityToken(_tokenParameter.Issuer,
                                                _tokenParameter.Audience,
                                                claims,
                                                expires: DateTime.UtcNow.AddMinutes(_tokenParameter.AccessExpiration),
                                                signingCredentials: credentials);

            var token = new JwtSecurityTokenHandler().WriteToken(jwtToken);

            return token;
        }

        [NonAction]
        public virtual OkObjectResult Success([ActionResultObjectValue] object value)
        {
            return new OkObjectResult(new
            {
                code = 200,
                data = value
            });
        }

        [NonAction]
        public virtual OkObjectResult Success()
        {
            return new OkObjectResult(new
            {
                code = 200
            });
        }

        [NonAction]
        public virtual OkObjectResult Success(string msg)
        {
            return new OkObjectResult(new
            {
                code = 200,
                msg = msg
            });
        }

        [NonAction]
        public virtual OkObjectResult Success(string msg, [ActionResultObjectValue] object value)
        {
            return new OkObjectResult(new
            {
                code = 200,
                msg = msg,
                data = value
            });
        }

        [NonAction]
        public virtual OkObjectResult Fail([ActionResultObjectValue] object value)
        {
            return new OkObjectResult(new
            {
                code = 400,
                data = value
            });
        }

        [NonAction]
        public virtual OkObjectResult Fail()
        {
            return new OkObjectResult(new
            {
                code = 400
            });
        }

        [NonAction]
        public virtual OkObjectResult Fail(string msg)
        {
            return new OkObjectResult(new
            {
                code = 400,
                msg = msg
            });
        }

        [NonAction]
        public virtual OkObjectResult Fail(string msg, [ActionResultObjectValue] object value)
        {
            return new OkObjectResult(new
            {
                code = 400,
                msg = msg,
                data = value
            });
        }
    }
}