using System;
using System.IO;

namespace NekoBeats
{
    public static class Logger
    {
        private static string logFile = "debug.log";
        
        public static void Log(string message)
        {
            try
            {
                File.AppendAllText(logFile, $"{DateTime.Now:HH:mm:ss} - {message}{Environment.NewLine}");
            }
            catch { }
        }
    }
}
