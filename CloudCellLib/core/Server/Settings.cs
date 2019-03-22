using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;

namespace CloudCellLib.Server
{
    [Serializable]
    public class Settings : INotifyPropertyChanged
    {
        private bool _EnableCors;
        private ObservableCollection<string> _CorsDomainNames = new ObservableCollection<string>();

        public Settings()
        {
           
        }


        public string GetCorsHeaderString
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < CorsDomainNames.Count; i++)
                {
                    if (i == 0) sb.Append(CorsDomainNames[i]);
                    else sb.Append(", " + CorsDomainNames[i]);
                }
                return sb.ToString();
            }
        }




        public ObservableCollection<string> CorsDomainNames
        {
            get
            {
                return _CorsDomainNames;
            }
            set
            {
                _CorsDomainNames = value;
                NotifyPropertyChanged("CorsDomainNames");
            }
        }

        public bool EnableCors
        {
            get
            {
                return _EnableCors;
            }
            set
            {
                _EnableCors = value;
                NotifyPropertyChanged("EnableCors");
            }
        }

        public void Clear()
        {
            EnableCors = false;
            _CorsDomainNames.Clear();
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
