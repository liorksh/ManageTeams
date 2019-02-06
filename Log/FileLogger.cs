using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamMngtWS.Log
{
    public class FileLogger:Logger
    {
        public string LogPath { get; private set; }
        public string LogFile { get; private set; }

        private string logFilePath;

        protected override void InitLogeer(params string[] parameters)
        {
            if(parameters==null || 
                parameters.Length<2 ||
                String.IsNullOrEmpty(parameters[0]) ||
                String.IsNullOrEmpty(parameters[1]))
            {
                throw new ArgumentException($"invalid arguments were sent to {nameof(InitLogeer)}");
            }

            
            if (Directory.Exists(parameters[0])==false)
            {
                Directory.CreateDirectory(parameters[0]);   
            }

            // assign the path after ensuring the directory exists
            LogPath = parameters[0];

            // check if the filename is valid
            LogFile = parameters[1];

            if(LogFile.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0)
            {
                LogFile = null;
                throw new ArgumentException($"file name contains invalid characters");
            }

            // try to create a dummy entry
            logFilePath = Path.Combine(LogPath, LogFile);

            string toDelete=string.Empty;

            if (File.Exists(logFilePath) &&
                (ArgumentExists("deleteOldLog", ref toDelete, parameters)))
            {
                if (toDelete == "deleteOldLog")
                {
                    File.Delete(logFilePath);
                }
            }
            else
            {
                // log the fist message is the file wasn't exist
                LogMessage("Log was created successfully");
            }

            LogFile = parameters[1];
        }

        public override void Log(string message)
        {
            if(IsInitiated==false)
            {
                throw new InvalidOperationException("Logger was not initialized");
            }
            LogMessage(message);
        }


        public override string Read(int numOfMessages)
        {
            StringBuilder logMessages = new StringBuilder();
            int rowCount = 0;

            foreach(string line in File.ReadLines(logFilePath))
            {
                logMessages.AppendLine(line);
                if (++rowCount > numOfMessages)
                    break;
            }

            return logMessages.ToString();
        }
        private void LogMessage(string message)
        {
            File.AppendAllText(logFilePath, $"{DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss:FFF")}: {message}{Environment.NewLine}");
        }
    }
}
