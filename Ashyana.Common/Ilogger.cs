using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ashyana.Common
{
    public enum LogLevel
    {
        ALL = 1,
        DEBUG = 2,
        INFO = 3,
        WARN = 4,
        ERROR = 5,
        FATAL = 6,
        OFF = 500,
        UNKNOWN = 1000
    };

    public interface ILogger
    {
        void LogException(string msg);
     
    }
}