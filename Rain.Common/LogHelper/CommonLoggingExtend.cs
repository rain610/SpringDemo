using log4net;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rain.Common.LogHelper
{
    public partial class CommonLogging
    {
        private const string StringNewLineString = "-------------------------------------------------------";

        private static ILog log;

        static CommonLogging()
        {
            log = LogManager.GetLogger("common");
            StartLogThread();
        }

        public static void Info(string message)
        {
            AddLog(message);
        }

        public static void Info(string message, Exception ex)
        {
            AddLog(message, LogType.Info, ex);
        }

        public static void InfoLine(string message)
        {
            AddLogFormat("{0}\r\n{1}", LogType.Info, null, StringNewLineString, message);
        }

        public static void Warn(string message)
        {
            AddLog(message, LogType.Warn);
        }

        public static void Warn(string message, Exception ex)
        {
            AddLog(message, LogType.Warn, ex);
        }

        public static void Debug(string message)
        {
            AddLog(message, LogType.Debug);
        }

        public static void Debug(string message, Exception ex)
        {
            AddLog(message, LogType.Debug, ex);
        }

        public static void Error(string message)
        {
            AddLog(message, LogType.Error);
        }

        public static void Error(string message, Exception ex)
        {
            if (null == ex)
            {
                Error(message);
                return;
            }

            AddLog(message, LogType.Error, ex);
        }

        public static void Fatal(string message)
        {
            AddLog(message, LogType.Fatal);
        }

        public static void Fatal(string message, Exception ex)
        {
            AddLog(message, LogType.Fatal, ex);
        }

        public static void InfoFormat(string format, params string[] args)
        {
            AddLogFormat(format, LogType.Info, null, args);
        }

        public static void ErrorFormat(string format, params string[] args)
        {
            AddLogFormat(format, LogType.Error, null, args);
        }

        public static void ErrorFormat(string format, Exception ex, params string[] args)
        {
            AddLogFormat(format, LogType.Error, ex, args);
        }

        public static void WatchToInfoLog(string message, Action action)
        {
            Stopwatch sw = Stopwatch.StartNew();
            Info(string.Format("start to {0}", message));
            action();
            sw.Stop();
            Info(string.Format("{0} completed..., cost: {1}", message, sw.Elapsed.TotalSeconds));
        }

        public static bool CatchLog(Action action, string errorMsg, bool isThrowException = false)
        {
            if (null == action)
            {
                return true;
            }

            try
            {
                action();
                return true;
            }
            catch (Exception ex)
            {
                Error(errorMsg, ex);

                if (isThrowException)
                {
                    throw;
                }

                return false;
            }
        }

        private static string GetLogFileName(string tname)
        {
            string name;
            string basedir = AppDomain.CurrentDomain.BaseDirectory;
            int pos = basedir.IndexOf("\\inetpub\\");
            if (pos < 0)
            {
                // we are not running under an inetpub dir, log underneath the base dir
                string separator = basedir.EndsWith("\\") ? null : "\\";
                name = AppDomain.CurrentDomain.BaseDirectory + separator + @"logs\" + "nevmiss" + tname + ".log";
            }
            else
            {
                // we're running on an IIS server, so log under the logs directory so we can share it
                name = basedir.Substring(0, pos + 9) + "logs" + Path.DirectorySeparatorChar + "nevmiss_" + tname + ".log";
            }

            return name;
        }
    }
}
