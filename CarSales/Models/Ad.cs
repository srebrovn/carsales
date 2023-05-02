using System;
using System.ComponentModel;
using System.Windows.Media.Imaging;

namespace CarSales.Models
{
    public class Ad : INotifyPropertyChanged
    {
        private string brand;
        private string model;
        private int productionYear;
        private int mileage;
        private string fuel;
        private string transmission;
        private int engineCapacity;
        private int enginePower;
        private int enginePowerKW;
        private int price;
        private string description;
        private BitmapImage image;

        public Ad(string brand,
            string model,
            int productionYear,
            int mileage,
            string fuel,
            string transmission,
            int engineCapacity,
            int enginePower,
            int price,
            string description,
            BitmapImage image
            )
        {
            Brand = brand;
            Model = model;
            ProductionYear = productionYear;
            Mileage = mileage;
            Fuel = fuel;
            Transmission = transmission;
            EngineCapacity = engineCapacity;
            EnginePower = enginePower;
            Price = price;
            Description = description;
            Image = image;
        }

        public string Brand
        {
            get { return brand; }
            set
            {
                brand = value;
                NotifyPropertyChanged(nameof(Brand));
            }
        }

        public string Model
        {
            get
            {
                return model;
            }
            set
            {
                model = value;
                NotifyPropertyChanged(nameof(Model));
            }
        }

        public int ProductionYear
        {
            get
            {
                return productionYear;
            }
            set
            {
                if (value > 1900 && value <= DateTime.Now.Year)
                {
                    productionYear = value;
                    NotifyPropertyChanged(nameof(ProductionYear));
                }else
                {
                    productionYear = DateTime.Now.Year;
                }

            }
        }

        public int Mileage
        {
            get
            {
                return mileage;
            }
            set
            {
                if (value < 1000000 && value > 0)
                {
                    mileage = value;
                }
                else
                {
                    mileage = 1;
                }
                NotifyPropertyChanged(nameof(Mileage));
            }
        }

        public string Fuel
        {
            get
            {
                return fuel;
            }
            set
            {
                if (value == "Diesel" || value == "Petrol" || value == "Electric" || value == "Hybrid (Petrol/Electric)" || value == "Hybrid (Diesel/Electric)")
                {
                    fuel = value;
                }
                else
                {
                    fuel = "Unknown";
                }
                NotifyPropertyChanged(nameof(Fuel));
            }
        }

        public string Transmission
        {
            get
            {
                return transmission;
            }
            set
            {
                if (value == "Automatic" || value == "Manual")
                {
                    transmission = value;
                }
                else
                {
                    transmission = "Unknown";
                }
                NotifyPropertyChanged(nameof(Transmission));
            }
        }

        public int EngineCapacity
        {
            get
            {
                return engineCapacity;
            }
            set
            {
                if (value > 0 && value < 20000)
                {
                    engineCapacity = value;
                }
                else
                {
                    engineCapacity = 2000;
                }
                NotifyPropertyChanged(nameof(EngineCapacity));
            }
        }

        public int EnginePower
        {
            get
            {
                return enginePower;
            }
            set
            {
                if (value > 0 && value < 10000)
                {
                    enginePower = value;
                    EnginePowerKW = (int)(value * 0.7368); // From PS to KW.
                }
                else
                {
                    enginePower = 1;
                    enginePowerKW = 1;
                }
                NotifyPropertyChanged(nameof(EnginePower));
                NotifyPropertyChanged(nameof(EnginePowerKW));
            }
        }

        public int EnginePowerKW
        {
            get { return enginePowerKW; }
            set
            {
                enginePowerKW = value;
            }
        }

        public int Price
        {
            get { return price;}
            set
            {
                if( value < 0)
                {
                    price = 0;
                }else
                {
                    price = value;
                }
                NotifyPropertyChanged(nameof(Price));   
            }
        }

        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                if (value.Length != 0)
                {
                    description = value;
                }
                else
                {
                    description = "";
                }
                NotifyPropertyChanged("Description");
            }
        }

        public BitmapImage Image
        {

            get
            {
                return image;
            }
            set
            {
                
                image = value;
                if(image == null)
                {
                    string brandLower = brand.ToLower();
                    image = new BitmapImage();
                    image.BeginInit();
                    if (brandLower.Contains("audi"))
                    { 
                        image.UriSource = new Uri("pack://application:,,,/CarSales;component/Resources/Images/audi.jpg");
                        
                    }else if(brandLower.Contains("bmw")) {
                        image.UriSource = new Uri("pack://application:,,,/CarSales;component/Resources/Images/bmw.png");
                    }else if (brandLower.Contains("mercedes"))
                    {
                        image.UriSource = new Uri("pack://application:,,,/CarSales;component/Resources/Images/mercedes.png");
                    }else
                    {
                        image.UriSource = new Uri("pack://application:,,,/CarSales;component/Resources/Images/car.png");
                    }
                    image.EndInit();
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void NotifyPropertyChanged(string Name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(Name));
            }
        }

    }
}
