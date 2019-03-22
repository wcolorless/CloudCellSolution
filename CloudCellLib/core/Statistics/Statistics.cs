using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CloudCellLib.Statistics
{
    [Serializable]
    public class FunctionStatistics : INotifyPropertyChanged
    {

        private int _RequestCount;
        private long _DataReceive;
        private long _TickCounter;

        public long TickCounter
        {
            get
            {
                return _TickCounter;
            }
            set
            {
                if (_TickCounter != value)
                {
                    _TickCounter = value;
                    NotifyPropertyChanged("TickCounter");
                }
            }
        }

        public long DataReceive
        {
            get
            {
                return _DataReceive;
            }
            set
            {
                if (_DataReceive != value)
                {
                    _DataReceive = value;
                    NotifyPropertyChanged("DataReceive");
                }
            }
        }

        public int RequestCount
        {
            get
            {
                return _RequestCount;
            }
            set
            {
                if (_RequestCount != value)
                {
                    _RequestCount = value;
                    NotifyPropertyChanged("RequestCount");
                }
            }
        }


        [field: NonSerialized]
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
