
using System.Collections.Generic;
using CurlingTracker.Models;

namespace CurlingTracker
{
    public interface ILogger
    {
        void Log(string message);
        void Log(System.Exception ex);
        void Log(string listTitle, List<string> strings);
        void Log(string listTitle, IEnumerable<IPrintable> iPrintables);
        void Log(string message, IPrintable iPrintable);
    }
}