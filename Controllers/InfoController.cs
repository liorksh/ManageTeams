using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.NetworkInformation;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.IO;
using System.Text;
using TeamMngtWS.Model;
using TeamMngtWS.Log;

namespace TeamMngtWS.Controllers
{
    [Route("/")]
    public class InfoController : BaseController
    {
        private const string MESSAGE_FORMAT = "Hello, {0}!";
        
        public InfoController(ILogger logger):base(logger)
        {
        }

        //public InfoController() : base(null) { }

        [HttpGet()]
        public JsonResult Get([FromQuery] string name = "GET command")
        {
            Log("call Get method");

            return Json(new Response
            {
                output = string.Format(MESSAGE_FORMAT, name)
            });
        }


        [HttpGet(nameof(About))]
        public JsonResult About()
        {
            Log("call About method");

            return Json(new Response
            {
                output = string.Format("Process info: Path: {0}, Process ID: {1}", Environment.CurrentDirectory, Process.GetCurrentProcess()?.Id)
            });
        }

        [HttpGet(nameof(ListDir))]
        public JsonResult ListDir()
        {
            StringBuilder result = new StringBuilder();

            List<DirectoryInfo> info = DirectoryUtil.GetAllDirecrotiesUnderDirectory(Environment.CurrentDirectory);
            //result.AppendLine($"There are {info.GetFiles().Length} files in folder {Environment.CurrentDirectory}")

            foreach (DirectoryInfo directory in info)
            {
                result.AppendLine($"Directory {directory.FullName}, modified: {directory.LastWriteTimeUtc}");
            }

            return Json(result.ToString());
        }

        [HttpGet(nameof(ListFiles))]
        public JsonResult ListFiles()
        {
            StringBuilder result = new StringBuilder();

            List<FileInfo> info = DirectoryUtil.GetAllFileUnderDirectory(Environment.CurrentDirectory);
            //result.AppendLine($"There are {info.GetFiles().Length} files in folder {Environment.CurrentDirectory}")
                
            foreach (FileInfo file in info)
            {
                result.AppendLine($"File {file.FullName}, created: {file.CreationTimeUtc}, modified: {file.LastWriteTimeUtc}");
            }

            return Json(result.ToString());
        }

        // GET http://*:*/ReadLog/{rows}
        [HttpGet(nameof(ReadLog))]
        [Route(nameof(ReadLog) + "/{rows}")]
        public JsonResult ReadLog(int rows)
        {
            string content = _logger.Read(rows);
            return Json(string.Format("Log content: {0}", content));
        }

        [HttpGet(nameof(Ping))]
        public IActionResult Ping(string url)
        {
            if(Uri.IsWellFormedUriString(url, UriKind.RelativeOrAbsolute)==false)
            {
                return BadRequest($"Invalid URL: {url}");
            }

            try
            {
                Ping ping = new Ping();
                
                PingReply reply = ping.Send(url);
                
                return Json(new Response
                {
                    output = string.Format($"Ping result to URL {url}: {reply.Status}")
                });
            }
            catch(Exception ex)
            {
                return BadRequest($"Error while pinging to {url}: {ex.Message}");
            }
        }
    }
}
