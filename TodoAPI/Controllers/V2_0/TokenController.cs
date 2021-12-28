using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TodoAPI.Models;

namespace TodoAPI.Controllers.V2_0
{
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly TodoDBContext _context;

        public TokenController(TodoDBContext context)
        {
            _context = context;
        }

        [HttpPost("GenerateToken")]
        [AllowAnonymous]


        public ActionResult<string> GenerateToken()
        {
            string issuer = "JwtAuthDemo";
            string signKey = "ASASA1@AAASASASASA1@AAASAS"; // 需要大於16字元
            string Account = "TestAccount";
          
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signKey));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            // 建立 SecurityTokenDescriptor
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = issuer,
                Subject = new ClaimsIdentity(new[]
                        {
                          new Claim(JwtRegisteredClaimNames.Sub, Account), // User.Identity.Name
                          new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), // JWT ID
                          new Claim("test", "123") // 可加入自訂內容在聲明裡
                        }),
                Expires = DateTime.Now.AddMinutes(60),
                SigningCredentials = signingCredentials
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var serializeToken = tokenHandler.WriteToken(securityToken);
            return serializeToken;
        }

        [Authorize]
        [HttpGet("GetClaims")]
        public IActionResult GetClaims()
        {
            return Ok(User.Claims.Select(p => new { p.Type, p.Value }));
        }

        [Authorize]
        [HttpGet("GetTokenID")]
        public IActionResult GetTokenID()
        {
            var jti = User.Claims.FirstOrDefault(p => p.Type == "jti");//每個token的識別值，如果要禁用某token就可以把識別值存到資料庫當黑名單
            return Ok(jti.Value);
        }
    }
}
