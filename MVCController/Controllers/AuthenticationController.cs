using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MVCController.Models;

namespace MVCController.Controllers
{
    public class AuthenticationController : Controller
    {

        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(AuthenticationController));
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(User users)
        {
            _log4net.Info("MVCAuthenticationController - Login");

            string token = "";
            using (var httpclient = new HttpClient())
            {
                httpclient.BaseAddress = new Uri("https://localhost:44354/");
                var postData = httpclient.PostAsJsonAsync<User>("api/TokenValidate/api/Token/LoginDetail", users);
                var res = postData.Result;
                if (res.IsSuccessStatusCode)
                {
                    token = await res.Content.ReadAsStringAsync();
                    TempData["token"] = token;
                    if (token != null)
                    {
                        return RedirectToAction("Index", "Booking");
                    }
                }

            }
            return View();
        }

    }
}
