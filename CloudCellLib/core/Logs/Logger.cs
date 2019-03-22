using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;

namespace CloudCellLib.Logs
{
    public class Logger : INotifyPropertyChanged
    {
        private ObservableCollection<ILogItem> _Items = new ObservableCollection<ILogItem>();

        public ObservableCollection<ILogItem> Items
        {
            get
            {
                return _Items;
            }
            set
            {
                _Items = value;
                NotifyPropertyChanged("Items");
            }
        }
        private Logger()
        { }

        public static Logger Create()
        {
            return new Logger();
        }

        public void AddItem(ILogItem item)
        {
            _Items.Add(item);
            NotifyPropertyChanged("Items");
        }

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
