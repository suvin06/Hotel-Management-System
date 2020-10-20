using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using login.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MVCController.Models;

namespace login.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

   
    public class TokenValidateController : ControllerBase {

        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(TokenValidateController));

        public IConfiguration _config;
            private hoteldbContext _context;

            public TokenValidateController(IConfiguration config, hoteldbContext context)
            {
                _config = config;
                _context = context;
            }

            [HttpPost]
            [Route("api/Token/LoginDetail")]
            public IActionResult LoginResult([FromBody]User user)
            {
            _log4net.Info("adminController Http GET ALL LOGINS");

            if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                var existingUser = _context.LoginDetails.Where(u => u.UserName == user.UserName).FirstOrDefault();
                if (existingUser == null)
                {
                    return NotFound();
                }
                if (existingUser.UserName == user.UserName && existingUser.Password == user.Password)
                {
                    return Ok(new { token = GenerateJSONWebToken(existingUser) });
                }
                return BadRequest();
            }

            string GenerateJSONWebToken(LoginDetails user)
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
                var claims = new[]
                {
                new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
            };
                var token = new JwtSecurityToken(
                    _config["Jwt: Issuer"],
                    _config["Jwt: Issuer"], null,
                    expires: DateTime.Now.AddMinutes(120),
                    signingCredentials: credentials);

                var encodetoken = new JwtSecurityTokenHandler().WriteToken(token);
                return encodetoken;

            }
        }
    }


