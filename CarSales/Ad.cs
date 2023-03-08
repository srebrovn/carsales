using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSales
{
    public class Ad : INotifyPropertyChanged
    {
        private string brand;
        private string model;
        private int productionYear;
        private int mileage;
        //private short owners;
        private string fuel;
        private string transmission;
        private int engineCapacity;
        private int enginePower;
        private string description;
        //private int enginePowerKw;
        //private int views;
        private string image = "";

        public Ad(string brand,
            string model,
            int productionYear,
            int mileage,
            string fuel,
            string transmission,
            int engineCapacity,
            int enginePower,
            string description
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
            this.description = description;
        }

        public string Brand
        {
            get { return brand; }
            set
            {
                brand = value;
                NotifyPropertyChanged("Brand");
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
                NotifyPropertyChanged("Model");
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
                    NotifyPropertyChanged("ProductionYear");
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
                    NotifyPropertyChanged("Mileage");
                }
                else
                {
                    mileage = 1;
                }
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
                if (value == "Diesel" || value == "Petrol" || value == "Electric" || value == "Hybrid")
                {
                    fuel = value;
                }
                else
                {
                    fuel = "Unknown";
                }
                NotifyPropertyChanged("Fuel");
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
                NotifyPropertyChanged("Transmission");
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
                NotifyPropertyChanged("EngineCapacity");
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
                if ((value > 0) && (value < 10000))
                {
                    enginePower = value;
                }
                else
                {
                    enginePower = 1;
                }
                NotifyPropertyChanged("EnginePower");
            }
        }

        public string Image
        {
            get
            {
                return image;
            }
            set
            {
                image = value;
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
