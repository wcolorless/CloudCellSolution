using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using CloudCellLib.Server;
using CloudCellLib.FuncRunner;
using CloudCellLib.Service;
using System.Windows.Threading;

namespace CloudCell
{
    public class CloudServiceViewModel : INotifyPropertyChanged
    {
        private CloudService _Service;
        private Listener _SelectedItem;
        private Command _SwitchStateCommand;
        private Command _EditListenerCommand;
        private Command _RemoveFunctionCommand;

        [NonSerialized]
        private Dispatcher CurrentDispatcher = Dispatcher.CurrentDispatcher;


        private CloudServiceViewModel(CloudService Service)
        {
            _Service = Service;
        }

        public static CloudServiceViewModel Create(CloudService _Service)
        {
            return new CloudServiceViewModel(_Service);
        }


        public ObservableCollection<Listener> Functions
        {
            get
            {
                return _Service.Functions;
            }
        }

        public Listener SelectedItem
        {
            get
            {
                return _SelectedItem;
            }
            set
            {
                _SelectedItem = value;
            }
        }
        public void StopFunction(Listener stopableListener)
        {
            stopableListener.StopListen();
            CurrentDispatcher.Invoke(() => { _Service.Functions.Remove(stopableListener); });
        }


        public Command SwitchStateCommand
        {
            get
            {
                return _SwitchStateCommand ?? (_SwitchStateCommand = new Command(o =>
                {
                    Listener _listener = o as Listener;
                    if(_listener.IsRun)
                    {
                        _listener.StopListen();
                    }
                    else
                    {
                        _listener.StartListen();
                    }
                }));
            }
        }


        public Command RemoveFunctionCommand
        {
            get
            {
                return _RemoveFunctionCommand ?? (_RemoveFunctionCommand = new Command(o => 
                {
                    Listener _listener = o as Listener;
                    StopFunction(_listener);
                }));
            }
        }

        public Command EditListenerCommand
        {
            get
            {
                return _EditListenerCommand ?? (_EditListenerCommand = new Command(o =>
                {
                    Listener _listener = o as Listener;
                    EditFunctionWindow EditWindow = new EditFunctionWindow(_listener);
                    EditWindow.ShowDialog();
                }));
            }
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
