using login.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MVCController.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace login.Repository
{
    public class loginrepo : ilogin
    {
        private IConfiguration _config;
        private hoteldbContext _context;
        public loginrepo(IConfiguration config, hoteldbContext context)
        {
            _config = config;
            _context = context;

        }
        public LoginDetails AuthenticateUser(User users)
        {
            LoginDetails user = null;


            List<LoginDetails> alluser = _context.LoginDetails.ToList();
            foreach (var v in alluser)
            {
                if (v.UserName == users.UserName && v.Password == users.Password)
                {
                    user = new LoginDetails { UserName = users.UserName, Password = users.Password };
                }
            }
            return user;
        }

        public string GenerateJSONWebToken(LoginDetails userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                null,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
