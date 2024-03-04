using ASP5.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace ASP5.Controllers
{
    public class HomeController : Controller
    {

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task Index(CookiesModel cookie)
        {
            HttpContext.Response.Cookies.Append(cookie.Key, cookie.Value, new CookieOptions
            {
                Expires = DateTime.SpecifyKind(cookie.Date, DateTimeKind.Utc),
            });
            HttpContext.Response.ContentType = "text/html; charset=utf-8";
            await HttpContext.Response.WriteAsync("<style>" +
                "body {background-color:cornflowerblue}" +
                "div {font-size:32px; font-weight:500; text-align:center}" +
                "</style>" +
                "<div>Cookie with the key <b style=\"color:blue\">\"" + cookie.Key + "\"</b> " +
                "and value of <b style=\"color:blue\">\"" + cookie.Value + "\"</b> was added</div>" +
                "<div>Expiration date set at <span style=\"color:blue\">" + cookie.Date + "</span></div>" +
                "<div><a href=\"/Cookies/Check/" + cookie.Key + "\">Check if it was properly stored</a></div>");
        }


    }
}
