using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rain.Common.LogHelper
{
    internal struct LoggingModel : IDisposable
    {
        private string message;
        private LogType messageType;
        private Exception exception;

        public Exception Exception 
        {
            get { return this.exception; }
            set { this.exception = value; }
        }

        internal LogType MessageType
        {
            get { return this.messageType; }
            set { this.messageType = value; }
        }

        public string Message
        {
            get { return this.message; }
            set
            {
                this.message = value;
            }
        }

        public LoggingModel(string message, bool isError = false, Exception ex = null)
        {
            this.message = string.Format("[{0}],{1}", DateTime.UtcNow.ToString("HH:mm:ss,fff"), message);
            this.messageType = isError ? LogType.Error : LogType.Info;
            this.exception = ex;
        }

        public LoggingModel(string message, LogType type = LogType.Info, Exception ex = null)
        {
            this.message = string.Format("[{0}] {1}", DateTime.UtcNow.ToString("HH:mm:ss,fff"), message);
            this.messageType = type;
            this.exception = ex;
        }

        public void Dispose()
        {
            this.exception = null;
            this.message = null;
        }
}

    internal enum LogType
    {
        Debug = 0,
        Info = 1,
        Warn = 2,
        Error = 3,
        Fatal = 4,
    }
}
