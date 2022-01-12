using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PM.CloudPlatform.ForkliftManager.Apis.Authorization;
using PM.CloudPlatform.ForkliftManager.Apis.Entities;
using PM.CloudPlatform.ForkliftManager.Apis.General;
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
        /// 获取Token
        /// </summary>
        /// <param name="request">用户名和密码</param>
        /// <returns>Token</returns>
        [HttpPost]
        public async Task<IActionResult> RequestToken([FromBody] UserDto request)
        {
            if (request.Username == null && request.Password == null)
                return BadRequest("Invalid Request");

            if (!(await _generalRepository.ExistAsync<User>(x=>x.Username.Equals(request.Username))))
            {
                return BadRequest("账号不存在");
            }

            var user = await _generalRepository.FindAsync<User>(x=>x.Username.Equals(request.Username));
            if (user.Password != request.Password)
            {
                return BadRequest("密码错误");
            }

            //生成Token和RefreshToken
            var token = GenUserToken(user.Id, request.Username, "admin");
            var refreshToken = "123456";

            return Ok(new[] { token, refreshToken });
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
    }
}