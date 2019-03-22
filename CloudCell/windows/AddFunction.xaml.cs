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
    public partial class AddFunction : Window
    {
        CloudService _Service;
        string PathToLib = "";
        Type ServiceType;
        public AddFunction(CloudService Service)
        {
            InitializeComponent();
            _Service = Service;
        }

        private void AddNewFunctionBtn(object sender, RoutedEventArgs e)
        {

            if(!string.IsNullOrEmpty(FunctionAddress.Text))
            {
                if (!string.IsNullOrEmpty(PathToLib))
                {
                    if(File.Exists(PathToLib))
                    {
                        if(SelectedFunctionFromLibBox.SelectedIndex != -1 || ServiceType == null)
                        {
                            _Service.AddFunction(FunctionAddress.Text + (string)SelectedFunctionFromLibBox.SelectedItem + "/", (string)SelectedFunctionFromLibBox.SelectedItem, ServiceType, (bool)AutoRun.IsChecked);
                            Close();
                        }
                        else MessageBox.Show("Необходимо выбрать имя рабочей функции в библиотеке");
                    }
                    else MessageBox.Show("Файл библиотеки отсутствует");
                }
                else MessageBox.Show("Нужно выбрат библиотеку");
            }
            else
            {
                MessageBox.Show("Адрес не должен быть пустой");
            }
        }

        private void LoadLibBtn(object sender, RoutedEventArgs e)
        {
            OpenFileDialog of = new OpenFileDialog() { Filter = "Service Library|*.dll" };
            if(of.ShowDialog() == true)
            {
                if(File.Exists(of.FileName))
                {
                    PathToLib = of.FileName;
                    var FuncNames = FuncFinder.Get(of.FileName);
                    SelectedFunctionFromLibBox.ItemsSource = FuncNames;
                }
            }
        }

        private void SelectedFunctionFromLibBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox Box = sender as ComboBox;
            var FuncName = Box.SelectedItem as string;
            ServiceType = FuncFinder.GetParentType(FuncName, PathToLib);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
