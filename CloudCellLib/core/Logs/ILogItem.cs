using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudCellLib.Logs
{

    public enum LogItemType
    {
        Empty,
        Exchange,
        Error,
        Start,
        Stop
    }

    public interface ILogItem
    {
        LogItemType ItemType { get; }
        string Description { get; }
        string FunctionName { get; }
        DateTime Date { get; }
    }
}
