using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text;

namespace ASP5.Controllers
{
    public class CookiesController : Controller
    {

        private readonly FileLogger cookiesLogger;

        public CookiesController()
        {
            this.cookiesLogger = new FileLogger(@"log.txt");
        }
        [HttpGet]
        public IActionResult Check()
        {
            return View();
        }
        [HttpPost]
        public async Task Check(string key)
        {
            StringBuilder cookieCheck = new StringBuilder("<style>" +
                "body {background-color:cornflowerblue}" +
                "div {font-size:32px; font-weight:500; text-align:center}" +
                "</style>");
            if (HttpContext.Request.Cookies.ContainsKey(key))
            {
                string result;
                HttpContext.Request.Cookies.TryGetValue(key, out result);
                if (result != null)
                {
                    cookiesLogger.LogInformation($"Successful operation at {DateTime.Now}:\n" +
                        $"Value \"{result}\" appended to Cookies");
                    cookieCheck.Append($"<div>Value <b style=\"color:blue\"><i>\"{HttpContext.Request.Cookies[key]}\"" +
                        $"</i></b> is successfully stored in Cookies<div>" +
                        $"<div><a href=\"/Home/Index\">Back to home page</a></div>");
                }
                else
                {
                    cookiesLogger.LogError($"Error at {DateTime.Now}:\nCookies do not contain any information!");
                    cookieCheck.Append("<div><b style=\"color:red; font-size:32px\">Oops! Unexpected error occured! " +
                    "Check log for more info.</b></div>" +
                    "<div><a href=\"/Home/Index\">Back to home page</a></div>");
                }
            }
            else
            {
                cookiesLogger.LogError($"Error at {DateTime.Now}:\nUnable to find destined key in Cookies!");
                cookieCheck.Append("<div><b style=\"color:red; font-size:32px\">Oops! Unexpected error occured! " +
                    "Check log for more info.</b></div>" +
                    "<div><a href=\"/Home/Index\"></a>Back to home page</div>");
            }
            await HttpContext.Response.WriteAsync($"{cookieCheck.ToString()}");
        }
    }
}
