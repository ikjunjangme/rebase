using System;

namespace NKLogger
{
    public enum LogLevel { Warning, /*Error, */Info, Debug }
    public class LogMessage
    {
        public DateTime DateTime { get; set; } = DateTime.Now;
        public LogLevel LogLevel { get; set; }
        public string Message { get; set; }
        public string Source { get; set; }
    }
}
