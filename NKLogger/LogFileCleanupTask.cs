using log4net;
using log4net.Appender;
using System;
using System.IO;
using System.Linq;

namespace NKLogger
{
    internal class LogFileCleanupTask
    {
        public LogFileCleanupTask()
        {
        }

        public void CleanUp(DateTime date)
        {
            string directory = string.Empty;
            string filePrefix = string.Empty;

            var repo = LogManager.GetAllRepositories().FirstOrDefault();
            if (repo == null)
                throw new NotSupportedException("Log4Net has not been configured yet.");

            var app = repo.GetAppenders().Where(x => x.GetType() == typeof(RollingFileAppender)).FirstOrDefault();
            if (app != null)
            {
                var appender = app as RollingFileAppender;

                directory = Path.GetDirectoryName(appender.File);
                filePrefix = Path.GetFileName(appender.File);

                CleanUp(directory, filePrefix, date);
            }
        }

        public void CleanUp(string logDirectory, string logPrefix, DateTime date)
        {
            if (string.IsNullOrEmpty(logDirectory))
                throw new ArgumentException("logDirectory is missing");

            if (string.IsNullOrEmpty(logPrefix))
                throw new ArgumentException("logPrefix is missing");

            var dirInfo = new DirectoryInfo(logDirectory);
            if (!dirInfo.Exists)
                return;

            var fileInfos = dirInfo.GetFiles("{0}*.*".Sub(logPrefix));
            if (fileInfos.Length == 0)
                return;

            foreach (var info in fileInfos)
            {
                if (info.CreationTime < date)
                {
                    try
                    {
                        info.Delete();
                    }
                    catch (Exception e)
                    {
                        NKLogger.Logger.Instance.WriteLog(new LogMessage()
                        {
                            LogLevel = LogLevel.Warning,
                            Source = nameof(LogFileCleanupTask),
                            Message = e.Message
                        });
                    }
                }
            }

        }

    }
}
