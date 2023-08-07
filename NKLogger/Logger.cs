using log4net;
using log4net.Config;
using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace NKLogger
{
    public sealed class Logger
    {
        #region singleton
        private static Logger _instance = null;
        public static Logger Instance => _instance ??= new();

        private Logger()
        {
            var rootPath = $@"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}\NKAI\{nameof(NKLogger)}\";

            var baseDir = Directory.GetCurrentDirectory();
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            var configure = XmlConfigurator.Configure(logRepository, new FileInfo($@"{baseDir}\log4net.config"));

#if RELEASE
            log4net.Repository.Hierarchy.Hierarchy h =
            (log4net.Repository.Hierarchy.Hierarchy)LogManager.GetRepository();
            foreach (var a in h.Root.Appenders)
            {
                if (a is log4net.Appender.FileAppender)
                {
                    log4net.Appender.FileAppender fa = (log4net.Appender.FileAppender)a;

                    FileInfo fi = new(fa.File);
                    fa.File = $@"{rootPath}\logs\{fi.Name}";
                    fa.ActivateOptions();
                }
            }
#endif
        }
        #endregion


        public int RemainDays { get; set; } = 7;
        private readonly LogFileCleanupTask _cleanTask = new();
        private readonly ILog _logger = LogManager.GetLogger(typeof(Logger));

        public Task WriteLog(LogMessage msg)
        {
            return Task.Run(() =>
            {

                try
                {
                    var date = DateTime.Now.AddDays(-RemainDays);
                    var task = new LogFileCleanupTask();
                    task.CleanUp(date);

                    switch (msg.LogLevel)
                    {
                        case LogLevel.Warning:
                            _logger.Warn($"{msg.Source}\t{msg.Message}");
                            break;
                        case LogLevel.Info:
                            _logger.Info($"{msg.Source}\t{msg.Message}");
                            break;
                        case LogLevel.Debug:
                            _logger.Debug($"{msg.Source}\t{msg.Message}");
                            break;
                        default:
                            break;
                    }
                }
                catch
                {

                }
            });
        }
    }
}
