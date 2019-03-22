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
using System.Windows.Shapes;
using CloudCellLib.Service;
using CloudCellLib.FuncRunner;
using System.IO;
using Microsoft.Win32;
using System.Reflection;

namespace CloudCell
{
    /// <summary>
    /// Логика взаимодействия для AddFunction.xaml
    /// </summary>
    public partial class StatisticsWindow : Window
    {
        StatisticsViewModel _StatisticsViewModel;
        public StatisticsWindow(CloudService Service)
        {
            InitializeComponent();
            _StatisticsViewModel = new StatisticsViewModel(Service);
            DataContext = _StatisticsViewModel;
        }

        private void CloseAppBtn(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
