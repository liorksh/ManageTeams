using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeamMngtWS.Log
{
    public interface ILogger
    {
        void Init(params string[] parameters);
        void Log(string messag);
        string Read(int numOfMessages);
    }
}
