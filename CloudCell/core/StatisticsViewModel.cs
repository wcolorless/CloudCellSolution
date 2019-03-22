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
using CloudCellLib.Statistics;

namespace CloudCell
{
    public class StatisticsViewModel : INotifyPropertyChanged
    {
        private CloudService _Service;
        private Command _OpenStatisticsCommand;
        private Command _SaveStatisticsCommand;

        public StatisticsViewModel(CloudService Service)
        {
            _Service = Service;
        }

        public CloudService CloudService
        {
            get
            {
                return _Service;
            }
        }

        public Command OpenStatisticsCommand
        {
            get
            {
                return _OpenStatisticsCommand ?? (_OpenStatisticsCommand = new Command(o =>
                {
                    OpenFileDialog of = new OpenFileDialog() { Filter = "Statistics File|*.sf" };
                    if (of.ShowDialog() == true)
                    {
                        var obj = ObjectLoaderSaver.Load<List<FunctionStatistics>>(of.FileName);
                        for (int i = 0; i < _Service.Functions.Count; i++)
                        {
                            _Service.Functions[i].Statistics = obj[i];
                        }
                    }
                }));
            }
        }


        public Command SaveStatisticsCommand
        {
            get
            {
                return _SaveStatisticsCommand ?? (_SaveStatisticsCommand = new Command(o =>
                {
                    SaveFileDialog sf = new SaveFileDialog() { Filter = "Statistics File|*.sf" };
                    if(sf.ShowDialog() == true)
                    {
                        List<FunctionStatistics> items = new List<FunctionStatistics>();
                        for(int i = 0; i < _Service.Functions.Count; i++)
                        {
                            items.Add(_Service.Functions[i].Statistics);
                        }
                        ObjectLoaderSaver.Save(sf.FileName, items);
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
