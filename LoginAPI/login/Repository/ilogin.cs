using login.Models;
using MVCController.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace login.Repository
{
    public interface ilogin
    {
        public LoginDetails AuthenticateUser(User users);
        public string GenerateJSONWebToken(LoginDetails userInfo);
    }
}
