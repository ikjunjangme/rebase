using log4net;
using log4net.Config;
using System;
using System.IO;
using System.Reflection;

namespace PublicUtility.Logger
{
    public class WriteLog
    {
        private readonly ILog _logger;
        private readonly string _logConfig = null;

        public void Info(string msg) => _logger.Info(msg);
        public void Warn(string msg) => _logger.Warn(msg);
        public void Error(string msg) => _logger.Error(msg);
        public void Fatal(string msg) => _logger.Fatal(msg);

        //static WriteLog()
        //{
        //    string path = $"{AppDomain.CurrentDomain.BaseDirectory}" + @"Logger\log4net.config";
        //    XmlConfigurator.Configure(LogManager.GetRepository(Assembly.GetEntryAssembly()), new FileInfo(path));
        //}

        public WriteLog(Type type, string name)
        {
            if (_logConfig == null)
            {
                string logfilePath = $"{AppDomain.CurrentDomain.BaseDirectory}" + $@"Logger\{name}.config";
                XmlConfigurator.Configure(LogManager.GetRepository(Assembly.GetEntryAssembly()), new FileInfo(logfilePath));
            }

            _logger = LogManager.GetLogger(type);
        }
    }
}
