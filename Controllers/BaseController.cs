using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamMngtWS.Log;

namespace TeamMngtWS.Controllers
{
    public class BaseController : Controller
    {
        protected ILogger _logger;

        protected BaseController(ILogger logger)
        {
            _logger = logger;
        }

        protected void Log(string message)
        {
            if (_logger != null)
            {
                _logger.Log(message);
            }
        }
    }
}
