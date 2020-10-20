using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using HotelManagementSys.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MVCController.Controllers
{
    public class RoomController : Controller
    {
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(RoomController));

        public async Task<IActionResult> Index()
        {
            _log4net.Info("MVCRoomController - GetRooms");

            var at = new List<RoomType>();
            using (var httpclient = new HttpClient())
            {
                httpclient.BaseAddress = new Uri("https://localhost:44387/api/Room/");
                HttpResponseMessage res = await httpclient.GetAsync("GetRoomType");
                
                if (res.IsSuccessStatusCode)
                {
                    var result = res.Content.ReadAsStringAsync().Result;
                    at = JsonConvert.DeserializeObject<List<RoomType>>(result);
                }
            }
            return View(at);
        }

        public async Task<IActionResult> Index2()
        {
            _log4net.Info("MVCRoomsController - GetBooking");

            var st = new List<Booking>();
            using (var httpclient = new HttpClient())
            {
                httpclient.BaseAddress = new Uri("https://localhost:44387/api/Room/");
                HttpResponseMessage res = await httpclient.GetAsync("GetBooking");
                if (res.IsSuccessStatusCode)
                {
                    var result = res.Content.ReadAsStringAsync().Result;
                    st = JsonConvert.DeserializeObject<List<Booking>>(result);
                }
            }
            return View(st);
        }

        public IActionResult Create()
        {
            return View();
        }
    }
}
