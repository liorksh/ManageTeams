using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using TeamMngtWS.MemoryCache;
using System.Text;
using TeamMngtWS.Model;

namespace TeamMngtWS.Controllers
{
    [Route("/[controller]")]
    public class TeamController : Controller
    {
        [HttpGet]
        public IActionResult Get(string name)
        {
            if (String.IsNullOrEmpty(name) == false)
            {
                // add new team or update an existing team 
                CacheItem<Team> itemTeam = (MemRepository<Team>.Repository.ContainsKey(name) == true) ? MemRepository<Team>.Repository[name] : null;
                string response = (itemTeam != null) ? (itemTeam.Value.ToJSON()) : $"Team '{name}' was not found";

                return Json(new Response
                {
                    output = response
                });
            }
            else
            {
                return NotFound( new { error = "invalid parameters"});
            }
        }

        // GET http://*:*/Team/{name}
        [HttpGet("{name}")]
        public IActionResult GetTeam(string name)
        {
            return Get(name);
        }

        // GET http://*:*/team/ShowCache
        [HttpGet(nameof(ShowCache))]
        public IActionResult ShowCache()
        {
            string content = TeamModel.PrintCache();
            //content.Append("]");
            var response = new
            {
                cache = content
            };


            return Ok(response);
        }

        // POST: http://*:*/team/AddTeam
        [HttpPost(nameof(AddTeam))]
        public IActionResult AddTeam(string json)
        {
            return Json(new Response
            {
                output = $"Total teams' members: {MemRepository<Team>.Repository.Values.Count}",
            });
        }

        // POST: http://*:*/team
        [HttpPost]
        public IActionResult Post(string team, string name)
        {
            if( String.IsNullOrWhiteSpace(team) || string.IsNullOrWhiteSpace(name))
            {
                var resoonse = new
                {
                    error = "invalid parameters"
                };

                return NotFound(resoonse);
            }

            TeamModel.AddMemberToTeam(team, name);
            
            return Json(new Response
            {
                output = $"Total teams' members: {MemRepository<Team>.Repository.Values.Count}",
            });
        }
    }
}
