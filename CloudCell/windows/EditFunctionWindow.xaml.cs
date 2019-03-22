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
using CloudCellLib.Server;

namespace CloudCell
{
    /// <summary>
    /// Логика взаимодействия для AddFunction.xaml
    /// </summary>
    public partial class EditFunctionWindow : Window
    {
        ListinerSettingsViewModel _ListinerSettingsViewModel;
        public EditFunctionWindow(Listener listener)
        {
            InitializeComponent();
            _ListinerSettingsViewModel = new ListinerSettingsViewModel(listener);
            DataContext = _ListinerSettingsViewModel;
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }


        private void CloseWindowBtn(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
