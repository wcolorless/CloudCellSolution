using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Reflection;


using CloudCellLib.Server;
using CloudCellLib.FuncRunner;
using CloudCellLib.Service;
using Microsoft.Win32;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace CloudCell
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        CloudService _Service;
        CloudServiceViewModel _CloudServiceViewModel;
        public MainWindow()
        {
            InitializeComponent();
            _Service = CloudService.Create();
            _CloudServiceViewModel = CloudServiceViewModel.Create(_Service);
            ServiceListBlock.DataContext = _CloudServiceViewModel;

        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void CloseAppBtn(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void CreateNewServiceBtn(object sender, RoutedEventArgs e)
        {
            if(_Service != null)
            {
                SaveFileDialog sf = new SaveFileDialog() { Filter = "Cloud Service|*.csf" };
                if(sf.ShowDialog() == true)
                {
                    using (FileStream fs = new FileStream(sf.FileName, FileMode.Create, FileAccess.Write))
                    {
                        BinaryFormatter bf = new BinaryFormatter();
                        bf.Serialize(fs, _Service);
                        fs.Close();
                    }
                }

            }
            _Service = CloudService.Create();

        }

        private void AddNewFunctionBtn(object sender, RoutedEventArgs e)
        {
            AddFunction addFunction = new AddFunction(_Service);
            addFunction.ShowDialog();
        }

        private void OpenStatisticsBtn(object sender, RoutedEventArgs e)
        {
            StatisticsWindow Statistics = new StatisticsWindow(_Service);
            Statistics.Show();
        }

        private void OpenExistServiceBtn(object sender, RoutedEventArgs e)
        {
            OpenFileDialog of = new OpenFileDialog() { Filter = "Cloud Service|*.csf" };
            if(of.ShowDialog() == true)
            {
                if(File.Exists(of.FileName))
                {
                    using (FileStream fs = new FileStream(of.FileName, FileMode.Open, FileAccess.Read))
                    {
                        BinaryFormatter bf = new BinaryFormatter();
                        _Service = bf.Deserialize(fs) as CloudService;
                        fs.Close();
                    }
                }
            }
        }

        private void SaveCurrentServiceBtn(object sender, RoutedEventArgs e)
        {
            if (_Service != null)
            {
                SaveFileDialog sf = new SaveFileDialog() { Filter = "Cloud Service|*.csf" };
                if (sf.ShowDialog() == true)
                {
                    using (FileStream fs = new FileStream(sf.FileName, FileMode.Create, FileAccess.Write))
                    {
                        BinaryFormatter bf = new BinaryFormatter();
                        bf.Serialize(fs, _Service);
                        fs.Close();
                    }
                }

            }
            else MessageBox.Show("Нет сервиса для сохранения");
        }

        private void StopAllFunctionBtn(object sender, RoutedEventArgs e)
        {
            if(_Service != null)
            {
                _Service.StopAllFunction();
            }
            else MessageBox.Show("Нет сервиса для остановки");
        }

        private void WrapPanel_Executed(object sender, ExecutedRoutedEventArgs e)
        {

        }

        private void StartAllFunctionBtn(object sender, RoutedEventArgs e)
        {
            if (_Service != null)
            {
                _Service.StartAllFunction();
            }
            else MessageBox.Show("Нет сервиса для запуска");
        }

        private void OpenLogWindowBtn(object sender, RoutedEventArgs e)
        {
            LoggerWindow LoggerWin = new LoggerWindow(_Service.Logger);
            LoggerWin.Show();

        }
    }
}
