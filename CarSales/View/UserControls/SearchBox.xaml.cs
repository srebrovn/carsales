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

namespace CarSales.View.UserControls
{
    /// <summary>
    /// Interaction logic for SearchBox.xaml
    /// </summary>
    public partial class SearchBox : UserControl
    {
        public SearchBox()
        {
            InitializeComponent();
            btnSearch.Click += onSearchBoxButtonPressed;
        }

        public delegate void SearchBoxSubmitButtonEventHandler(object obj, string text);
        public event SearchBoxSubmitButtonEventHandler ButtonPressed;

        protected virtual void onSearchBoxButtonPressed(object sender, RoutedEventArgs e)
        {
            string text = tbInput.Text;
            if(text != null)
            {
                ButtonPressed.Invoke(this, text);
            }
        }
    }
}
