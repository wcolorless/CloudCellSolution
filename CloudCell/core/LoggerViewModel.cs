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
using Microsoft.Win32;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using CloudCellLib.Logs;

namespace CloudCell
{
    public class LoggerViewModel : INotifyPropertyChanged
    {
        private Logger _Logger;
        private Command _OpenSettingsCommand;
        private Command _SaveSettingsCommand;

        public LoggerViewModel(Logger logger)
        {
            _Logger = logger;
        }

        public Logger Logger
        {
            get
            {
                return _Logger;
            }
        }

        public Command OpenSettingsCommand
        {
            get
            {
                return _OpenSettingsCommand ?? (_OpenSettingsCommand = new Command(o =>
                {
                    OpenFileDialog of = new OpenFileDialog() { Filter = "Log File|*.sf" };
                    if (of.ShowDialog() == true)
                    {
                        _Logger.Items = ObjectLoaderSaver.Load<ObservableCollection<ILogItem>>(of.FileName);
                    }
                }));
            }
        }


        public Command SaveSettingsCommand
        {
            get
            {
                return _SaveSettingsCommand ?? (_SaveSettingsCommand = new Command(o =>
                {
                    SaveFileDialog sf = new SaveFileDialog() { Filter = "Log File|*.sf" };
                    if(sf.ShowDialog() == true)
                    {
                        ObjectLoaderSaver.Save(sf.FileName, _Logger.Items);
                    }
                    
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
