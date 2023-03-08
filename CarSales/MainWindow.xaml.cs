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

namespace CarSales
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<Ad> ads;

        public MainWindow()
        {
            InitializeComponent();
            ads = new List<Ad>();
            ads.Add(new Ad("Audi", "A5", 2019, 190000, "Diesel", "Automatic", 1989, 190, "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed eu justo a purus egestas vehicula et a odio. Aliquam a magna ac nibh malesuada rutrum. Aenean sollicitudin maximus est tempus gravida."));
            ads.Add(new Ad("BMW", "3er", 2022, 50000, "Diesel", "Automatic", 1985, 150, "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed eu justo a purus egestas vehicula et a odio. Aliquam a magna ac nibh malesuada rutrum. Aenean sollicitudin maximus est tempus gravida."));
            ads.Add(new Ad("BMW", "3er", 2022, 50000, "Diesel", "Automatic", 1985, 150, "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed eu justo a purus egestas vehicula et a odio. Aliquam a magna ac nibh malesuada rutrum. Aenean sollicitudin maximus est tempus gravida."));
            ads.Add(new Ad("BMW", "3er", 2022, 50000, "Diesel", "Automatic", 1985, 150, "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed eu justo a purus egestas vehicula et a odio. Aliquam a magna ac nibh malesuada rutrum. Aenean sollicitudin maximus est tempus gravida."));
            ads.Add(new Ad("BMW", "3er", 2022, 50000, "Diesel", "Automatic", 1985, 150, "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed eu justo a purus egestas vehicula et a odio. Aliquam a magna ac nibh malesuada rutrum. Aenean sollicitudin maximus est tempus gravida."));


            lvAds.ItemsSource = ads;

        }
        public List<Ad> Ads { get; set; }
        private void btnRun_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
