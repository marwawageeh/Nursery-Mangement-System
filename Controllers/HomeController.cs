using Microsoft.AspNetCore.Mvc;
using mvc_project.Models;
using System.Diagnostics;
using System.Net.Mail;
using System.Net;

namespace mvc_project.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult About_US()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
        public IActionResult Error()
        {
            var model = new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };

            return View(model);
        }

        public IActionResult Complaints()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SendComplaint(string name, string email, string message)
        {
            try
            {
                var smtpClient = new SmtpClient("smtp.office365.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential("hdanty12@outlook.com", "HHdanty12"),
                    EnableSsl = true
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress("hdanty12@outlook.com"), // استخدم إيميل ثابتًا
                    Subject = "شكوى جديدة من " + name,
                    Body = $"اسم المرسل: {name}\nالبريد الإلكتروني: {email}\n\nمحتوى الشكوى:\n{message}",
                    IsBodyHtml = false,
                };

                mailMessage.To.Add("hdanty12@outlook.com"); // البريد الذي ستستقبل عليه الشكاوى

                smtpClient.Send(mailMessage);

                return Json(new { success = true, message = "تم إرسال الشكوى بنجاح!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "حدث خطأ أثناء الإرسال: " + ex.Message });
            }
        }


    }
}
