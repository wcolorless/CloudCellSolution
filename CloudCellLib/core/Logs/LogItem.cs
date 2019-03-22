using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CloudCellLib.Logs
{

    [Serializable]
    public class LogItem : ILogItem, INotifyPropertyChanged
    {
        private string _Description;
        private LogItemType _ItemType;
        private string _FunctionName;
        private DateTime _Date;

        private LogItem(string Description, LogItemType ItemType, string FunctionName)
        {
            _Description = Description;
            _ItemType = ItemType;
            _FunctionName = FunctionName;
            _Date = DateTime.Now;
        }

        public static ILogItem Create(string Description, LogItemType ItemType, string FunctionName)
        {

            return new LogItem(Description, ItemType, FunctionName);
        }

        public string Description
        {
            get
            {
                return _Description;
            }
        }

        public LogItemType ItemType
        {
            get
            {
                return _ItemType;
            }
        }

        public string FunctionName
        {
            get
            {
                return _FunctionName;
            }
        }

        public DateTime Date
        {
            get
            {
                return _Date;
            }
        }

        [field:NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
