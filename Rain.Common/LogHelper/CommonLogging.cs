using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Rain.Common.LogHelper
{
    public partial class CommonLogging
    {
        private static ConcurrentQueue<LoggingModel> messageQueue;

        private static Thread thread;

        private static void StartLogThread()
        {
            messageQueue = new ConcurrentQueue<LoggingModel>();
            thread = new Thread(InternalWriteLog);
            thread.SetApartmentState(ApartmentState.STA);
            thread.IsBackground = true;

            thread.Start();
        }

        private static void AddLog(string message, LogType type = LogType.Info, Exception ex = null)
        {
            messageQueue.Enqueue(new LoggingModel(message, type, ex));
            CommonLogging.Trigger();
        }

        private static void AddLogFormat(string format, string arg0, LogType type = LogType.Info, Exception ex = null)
        {
            try
            {
                messageQueue.Enqueue(new LoggingModel(string.Format(format, arg0), type, ex));
                CommonLogging.Trigger();
            }
            catch (Exception exception)
            {
                AddLog(string.Format("Add Log Format error, format string:'{0}' , arg0:{1}.", format, arg0), LogType.Error, exception);
            }
        }

        private static void AddLogFormat(string format, LogType type = LogType.Info, Exception ex = null, params string[] args)
        {
            try
            {
                messageQueue.Enqueue(new LoggingModel(string.Format(format, args), type, ex));
                CommonLogging.Trigger();
            }
            catch (Exception exception)
            {
                AddLog(
                    string.Format("Add Log Format error,format:'{0}', arg:{1}.", format, null == args ? null : string.Join(" , ", args)),
                    LogType.Error,
                    exception);
            }
        }

        public static void Trigger()
        {
            if (IsProcessing)
            {
                return;
            }
            else
            {
                Task.Factory.StartNew(() =>
                {
                    InternalWriteLog();
                });
            }
        }

        private volatile static bool IsProcessing = false;
        public static void InternalWriteLog()
        {
            LoggingModel model;
            while (messageQueue.TryDequeue(out model))
            {
                IsProcessing = true;

                switch (model.MessageType)
                {
                    case LogType.Info:
                        {
                            log.Info(model.Message, model.Exception);
                        }
                        break;
                    case LogType.Error:
                        {
                            log.Error(model.Message, model.Exception);
                        }
                        break;
                    case LogType.Warn:
                        {
                            log.Warn(model.Message, model.Exception);
                        }
                        break;
                    case LogType.Debug:
                        {
                            log.Debug(model.Message, model.Exception);
                        }
                        break;
                    default:
                        break;
                }

                model.Dispose();
            }

            IsProcessing = false;
        }
    }
}
