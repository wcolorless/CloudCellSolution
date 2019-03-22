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

namespace CloudCell
{
    public class ListinerSettingsViewModel : INotifyPropertyChanged
    {
        public Listener _Listener;
        private Command _AddDomainNameCorsCommand;
        private Command _CreateNewEmptySettingsCommand;
        private Command _OpenSettingsCommand;
        private Command _SaveSettingsCommand;


        public Listener Listener
        {
            get
            {
                return _Listener;
            }
        }

        public Command AddDomainNameCorsCommand
        {
            get
            {
                return _AddDomainNameCorsCommand ?? (_AddDomainNameCorsCommand = new Command(o =>
                {
                    string newDomainName = o as string;
                    _Listener.FunctionSettings.CorsDomainNames.Add(newDomainName);
                }));
            }
        }

        public Command CreateNewEmptySettingsCommand
        {
            get
            {
                return _CreateNewEmptySettingsCommand ?? (_CreateNewEmptySettingsCommand = new Command(o =>
                {
                    string newDomainName = o as string;
                    _Listener.FunctionSettings.Clear();
                }));
            }
        }

        public Command OpenSettingsCommand
        {
            get
            {
                return _OpenSettingsCommand ?? (_OpenSettingsCommand = new Command(o =>
                {
                    OpenFileDialog of = new OpenFileDialog() { Filter = "Setting File|*.sf" };
                    if (of.ShowDialog() == true)
                    {
                        using (FileStream fs = new FileStream(of.FileName, FileMode.Open, FileAccess.Read))
                        {
                            BinaryFormatter bf = new BinaryFormatter();
                            _Listener.FunctionSettings = bf.Deserialize(fs) as Settings;
                            fs.Close();
                        }
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
                    SaveFileDialog sf = new SaveFileDialog() { Filter = "Setting File|*.sf" };
                    if(sf.ShowDialog() == true)
                    {
                        using (FileStream fs = new FileStream(sf.FileName, FileMode.Create, FileAccess.Write))
                        {
                            BinaryFormatter bf = new BinaryFormatter();
                            bf.Serialize(fs, _Listener.FunctionSettings);
                            fs.Close();
                        }
                    }
                    
                }));
            }
        }

        public ListinerSettingsViewModel(Listener Listener)
        {
            _Listener = Listener;
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
