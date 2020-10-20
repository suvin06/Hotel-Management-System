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
    public class BookingController : Controller
    {
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(BookingController));

        public async Task<IActionResult> Index()
        {
            _log4net.Info("MVCBookingController - GetBooking");

            var st = new List<Booking>();
            using (var httpclient = new HttpClient())
            {
                httpclient.BaseAddress = new Uri("https://localhost:44359/api/Booking/");
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

        [HttpPost]
        public IActionResult Create(Booking book)
        {
            _log4net.Info("MVCBookingController - CreateBooking");

            using (var httpclient = new HttpClient())
            {
                httpclient.BaseAddress = new Uri("https://localhost:44359/api/Booking/");
                var postData = httpclient.PostAsJsonAsync<Booking>("PostBooking", book);
                var res = postData.Result;

                if (res.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(book);
        }

       

        public IActionResult Delete(int id)
        {
            _log4net.Info("MVCBookingController - DeleteBooking");
            using (var httpclient = new HttpClient())
            {
                httpclient.BaseAddress = new Uri("http://localhost:44359/api/Booking/");
                var delete = httpclient.DeleteAsync("DeleteBooking?Id=" + id);
                var res = delete.Result;
                return RedirectToAction("Index");
            }
        }


    }
}
