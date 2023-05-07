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
using CarSales.Models;
using CarSales.View.UserControls;
using CarSales.ViewModels;

namespace CarSales
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ViewModel viewModel;
        public MainWindow()
        {
            InitializeComponent();
            viewModel = new ViewModel(this);
            DataContext = viewModel;
        }

        private void SortComboBox_SortItemSelected(object obj, string indexStr)
        {

            if(indexStr.All(char.IsDigit) && indexStr != "")
            {
                int index = int.Parse(indexStr);
                viewModel.SortBoxTag = index;
            }
        }

        private void SearchBox_ButtonPressed(object obj, string text)
        {
            if(text != null)
            {
                text = text.Trim();
                viewModel.SearchBoxText = text;
            }
        }
    }
}
