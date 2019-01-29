using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace TeamMngtWS.Controllers
{
    [Route("/")]
    public class InfoController : Controller
    {
        private const string MESSAGE_FORMAT = "Hello, {0}!";
                
        public JsonResult Get([FromQuery] string name = "GET command")
        {
            return Json(new Response
            {
                output = string.Format(MESSAGE_FORMAT, name)
            });
        }


        [HttpGet(nameof(About))]
        public JsonResult About()
        {
            return Json(new Response
            {
                output = string.Format("Process info: Path: {0}, Process ID: {1}", Environment.CurrentDirectory, Process.GetCurrentProcess()?.Id)
            });
        }
    }
}
