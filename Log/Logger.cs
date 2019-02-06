using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeamMngtWS.Log
{
    public abstract class Logger : ILogger
    {        
        public bool IsInitiated { get; private set; }

        public void Init(params string[] parameters)
        {
            lock (this)
            {
                if (IsInitiated)
                {
                    throw new InvalidOperationException("Logger is already initialized");
                }

                InitLogeer(parameters);
                IsInitiated = true;
            }
        }

        protected bool ArgumentExists(string argName, ref string argValue, params string[] parameters)
        {
            if(parameters==null || parameters.Length==0)
            {
                return false;
            }

            argValue = parameters.FirstOrDefault(e => String.Compare(e, argName, true)==0);

            return argValue != null;
        }

        protected abstract void InitLogeer(params string[] parameters);
        public abstract void Log(string messag);
        public abstract string Read(int numOfMessages);
    }
}
