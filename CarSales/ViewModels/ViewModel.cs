using CarSales.Commands;
using CarSales.Models;
using CarSales.View.UserControls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace CarSales.ViewModels
{
    public class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private void NotifyPropertyChanged(string Name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(Name));
            }
        }

        public ObservableCollection<Ad>? ads;
        public ObservableCollection<Ad> Ads
        {
            get { return ads; }
            set { ads = value; }
        }

        private Ad currentlySelectedAd;
        public Ad CurrentlySelected
        {
            get { return currentlySelectedAd; }
            set
            { 
               
                currentlySelectedAd = value;
                NotifyPropertyChanged("CurrentlySelected");
                
            }
        }
        public AddCommand? AddItemCommand { get; set; }
        public DeleteCommand? DeleteItemCommand { get; set; }
        public EditCommand? EditItemCommand { get; set; }

        public ViewModel()
        {
            AddItemCommand = new AddCommand(AddAd);
            DeleteItemCommand = new DeleteCommand(DeleteAd);
            EditItemCommand = new EditCommand(EditAd);
            Ads = new ObservableCollection<Ad>
            {
                new Ad("Audi", "A5", 2019, 190000, "Diesel", "Automatic", 1989, 190, "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed eu justo a purus egestas vehicula et a odio. Aliquam a magna ac nibh malesuada rutrum. Aenean sollicitudin maximus est tempus gravida."),
                new Ad("BMW", "3er", 2022, 50000, "Diesel", "Automatic", 1985, 150, "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed eu justo a purus egestas vehicula et a odio. Aliquam a magna ac nibh malesuada rutrum. Aenean sollicitudin maximus est tempus gravida.")
               
            };
            
        }

        public void AddAd()
        {
            if (currentlySelectedAd != null)
            {
                Ads.Add(currentlySelectedAd);
            }
            else
            {
                MessageBox.Show("Please select an item!", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void DeleteAd()
        {
            if (currentlySelectedAd != null)
            {
                Ads.Remove(currentlySelectedAd);
            }
            else
            {
                MessageBox.Show("Please select an item!", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void EditAd()
        {
            if (currentlySelectedAd != null)
            {
                currentlySelectedAd.Brand = "Edited!";
            }
            else
            {
                MessageBox.Show("Please select an item!", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
