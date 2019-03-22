using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloudCellLib.Server;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using System.Runtime.Serialization.Formatters.Binary;
using CloudCellLib.Logs;


namespace CloudCellLib.Service
{
    [Serializable]
    public class CloudService : INotifyPropertyChanged
    {
        private ObservableCollection<Listener> _Functions = new ObservableCollection<Listener>();
        public Logger Logger;

        private CloudService()
        {
            Logger = Logger.Create();
        }

        public static CloudService Create()
        {
            return new CloudService();
        }


        public ObservableCollection<Listener> Functions
        {
            get
            {
                return _Functions;
            }
        }


        public void AddFunction(string address, string functionName, Type ServiceType, bool AutoRun)
        {
            var newFunc = Listener.Create(address, functionName, ServiceType, Logger);
            _Functions.Add(newFunc);
            if (AutoRun) newFunc.StartListen();
        }

        public void StartAllFunction()
        {
            for (int i = 0; i < _Functions.Count; i++)
            {
                _Functions[i].StartListen();
            }
        }

        public void StopAllFunction()
        {
            for(int i = 0; i < _Functions.Count; i++)
            {
                _Functions[i].StopListen();
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
