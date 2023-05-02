using CarSales.Commands;
using CarSales.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System;
using System.Linq;
using CarSales.View.Windows;
using System.Collections.Generic;
using System.Collections;
using Microsoft.Win32;
using System.Windows.Media.Imaging;
using System.IO;
using System.Xml.Serialization;

namespace CarSales.ViewModels
{
    public class ViewModel : INotifyPropertyChanged, INotifyDataErrorInfo
    {
        public static string ADSFILE = "ads.xml";
        public event PropertyChangedEventHandler? PropertyChanged;
        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

        private readonly ErrorsViewModel errorsViewModel;

        private void NotifyPropertyChanged(string Name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(Name));
            }
        }

        public bool CanCreate => !HasErrors;
        public bool HasErrors => errorsViewModel.HasErrors;
        public IEnumerable GetErrors(string propertyName)
        {
            return errorsViewModel.GetErrors(propertyName);
        }

        private List<string> years; // Years for adding new AD.
        private ObservableCollection<Ad>? ads; // All ADs.
        private ObservableCollection<string> brands; // Brands available
        private Ad currentlySelectedAd; // Currently selected AD from ListView.

        // Main Window

        private readonly Window mainWindow;

        // Settings Window
        private string selectedBrandSettings; // Selected Brand in Settings Window. 
        private string inputBrandSettings; // Brand to be inserted into brands from Settings Window.

        // AddAdWindow
        AddAdWindow addAdWindow;

        private string addEditBtn;
        private BitmapImage image;

        private string errorMessage;
        private string brandAdd;
        private string modelAdd;
        private string yearAdd;
        private string mileageAdd;
        private string fuelAdd;
        private string capacityAdd;
        private string transmissionAdd;
        private string powerAdd;
        private string priceAdd;
        private string descriptionAdd;


        // EditAdWindow
        AddAdWindow editAdWindow;

        // COMMANDS
        public OpenWindowCommand? OpenSettingsWindowCommand{ get; set; }   
        public OpenWindowCommand OpenAddWindowCommand { get; set; }
        public OpenWindowCommand OpenEditWindowCommand { get; set; }
        public ImageCommand OpenImageDialogCommand { get; set; }
        public AddCommand? AddEditAdCommand { get; set; }
        public DeleteCommand? DeleteAdCommand { get; set; }
        public AddCommand? AddBrandCommand { get; set; }
        public DeleteCommand? DeleteBrandCommand { get ; set; } 
        public EditCommand? EditBrandCommand { get; set; }  

        public ViewModel(Window window)
        {

            this.mainWindow = window;

            errorsViewModel = new ErrorsViewModel();
            errorsViewModel.ErrorsChanged += ErrorsViewModel_ErrorsChanged;

            // CRUD Commands
            AddEditAdCommand = new AddCommand(AddEditAd);
            DeleteAdCommand = new DeleteCommand(DeleteAd);

            AddBrandCommand = new AddCommand(AddBrand);
            DeleteBrandCommand = new DeleteCommand(DeleteBrand);
            EditBrandCommand = new EditCommand(EditBrand);

            OpenImageDialogCommand = new ImageCommand(OpenImage);

            // Window Commands
            OpenSettingsWindowCommand = new OpenWindowCommand(OpenSettingsWindow);
            OpenAddWindowCommand = new OpenWindowCommand(OpenAddWindow);
            OpenEditWindowCommand = new OpenWindowCommand(OpenEditWindow);

            Ads = new ObservableCollection<Ad>();
            Brands = new ObservableCollection<string>();
            Years = new List<string>();

            fillBrandsFromSettings();
            Deserialize();
        }

        private void Serialize(ObservableCollection<Ad> ads)
        {
            
            // Insert code to set properties and fields of the object.  
            XmlSerializer mySerializer = new
            XmlSerializer(typeof(ObservableCollection<Ad>));
            // To write to a file, create a StreamWriter object.  
            StreamWriter myWriter = new StreamWriter("ads.xml");
            mySerializer.Serialize(myWriter, ads);
            myWriter.Close();

        }

        private void Deserialize()
        {
            if(File.Exists(ADSFILE))
            {
                var serializer = new XmlSerializer(typeof(ObservableCollection<Ad>));
                using (var stream = new StreamReader(ADSFILE))
                {
                    ads = (ObservableCollection<Ad>)serializer.Deserialize(stream);
                    NotifyPropertyChanged("Ads");
                }
            }
        }

        private void OpenImage()
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;";
            fileDialog.Title = "Pick Image...";

            bool? success = fileDialog.ShowDialog();

            if(success == true)
            {
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.UriSource = new Uri(fileDialog.FileName);
                bitmapImage.EndInit();

                Image = bitmapImage;
            }
        }

        // Errors
        private void ErrorsViewModel_ErrorsChanged(object sender, DataErrorsChangedEventArgs e)
        {
            ErrorsChanged?.Invoke(this, e);
            NotifyPropertyChanged(nameof(CanCreate));
        }

        public string ErrorMessage
        {
            get { return errorMessage; }
            set { errorMessage = value; }
        }

        // Global
        public List<string> Years { get { return years; } set { years = value; } }

        private void clearFields()
        {
            
            BrandAdd = null;
            ModelAdd = null;
            YearAdd = null;
            MileageAdd = null;
            FuelAdd = null;
            CapacityAdd = null;
            TransmissionAdd = null;
            PowerAdd = null;
            PriceAdd = null;
            DescriptionAdd = null;
            Image = null;
        }

        // MainWindow
        public ObservableCollection<Ad> Ads
        {
            get { return ads; }
            set { ads = value; }
        }

        public Ad CurrentlySelected
        {
            get { return currentlySelectedAd; }
            set
            {
                currentlySelectedAd = value;
                if(currentlySelectedAd != null)
                {
                    BrandAdd = currentlySelectedAd.Brand;
                    ModelAdd = currentlySelectedAd.Model;
                    YearAdd = (currentlySelectedAd.ProductionYear).ToString();
                    MileageAdd = currentlySelectedAd.Mileage.ToString();
                    FuelAdd = currentlySelectedAd.Fuel;
                    CapacityAdd = currentlySelectedAd.EngineCapacity.ToString();
                    TransmissionAdd = currentlySelectedAd.Transmission;
                    PowerAdd = currentlySelectedAd.EnginePower.ToString();
                    PriceAdd = currentlySelectedAd.Price.ToString();
                    DescriptionAdd = currentlySelectedAd.Description;
                }else
                {
                    clearFields();
                }
                
                NotifyPropertyChanged("CurrentlySelected");
            }
        }

        // Settings Window
        private void OpenSettingsWindow()
        {
            SettingsWindow settingsWindow = new SettingsWindow();
            settingsWindow.DataContext = this;
            
            settingsWindow.Show();
        }
        private void fillBrandsFromSettings()
        {
            string val = Properties.Settings.Default.Brands;

            string[] list = val.Split(',').Select(s => s).ToArray();

            for (int i = 0; i < list.Length; i++)
            {
                if (list[i].Length > 0)
                {
                    Brands.Add(list[i]);
                }
            }
        }

        private void editBrandSettings()
        {
            string val = String.Join(",", Brands.Select(i => i).ToArray());
            Properties.Settings.Default.Brands = val;
            Properties.Settings.Default.Save();
        }

        public void AddBrand()
        {
            if (InputBrandSettings != null)
            {
                if (InputBrandSettings.Length > 0 && !Brands.Contains(InputBrandSettings))
                {
                    Brands.Add(InputBrandSettings);
                    editBrandSettings();
                }
            }
        }

        public void DeleteBrand()
        {
            if (SelectedBrandSettings != null && Brands.Contains(SelectedBrandSettings))
            {
                Brands.Remove(SelectedBrandSettings);
                editBrandSettings();
            }

        }

        public void EditBrand()
        {
            if (SelectedBrandSettings != null && InputBrandSettings != null)
            {
                if (Brands.Contains(SelectedBrandSettings) && !Brands.Contains(InputBrandSettings))
                {
                    for (int i = 0; i < Brands.Count; i++)
                    {
                        if (SelectedBrandSettings == Brands[i])
                        {
                            Brands[i] = InputBrandSettings;
                            editBrandSettings();
                            break;
                        }
                    }
                }
            }
        }

        public ObservableCollection<string> Brands
        {
            get { return brands; }
            set
            {
                brands = value;
                NotifyPropertyChanged("Brands");
            }
        }

        
        public string SelectedBrandSettings
        {
            get { return selectedBrandSettings; }
            set
            {
                if (!string.Equals(selectedBrandSettings, value))
                {
                    selectedBrandSettings = value;
                    NotifyPropertyChanged("SelectedBrandSettings");
                }
            }
        }

        public string InputBrandSettings
        {
            get { return inputBrandSettings; }
            set
            {
                if (value != null)
                {
                    if (!string.Equals(inputBrandSettings, value))
                    {
                        inputBrandSettings = value;
                    }
                }
                else
                {
                    inputBrandSettings = "";
                }
                NotifyPropertyChanged("InputBrandSettings");
            }
        }

        // Add Ad Window
        private void OpenAddWindow()
        {
            if (editAdWindow == null)
            {
                fillYears(); // Fill years for Combo Box Items.
                clearFields();
                AddEditBtn = "Add";
                addAdWindow = new AddAdWindow();
                addAdWindow.DataContext = this;
                addAdWindow.ShowDialog();
            }
        }

        private void fillYears()
        {
            
            if (!years.Any())
            {
                for (int i = DateTime.Today.Year; i >= 1920;)
                {
                    years.Add(i.ToString());
                    if (i <= 1960)
                    {
                        i -= 5;
                    }
                    else
                    {
                        i--;
                    }

                }
            }
        }

        public void AddEditAd()
        {
            if (checkInputs())
            {
                if(addEditBtn == "Add")
                {
                    Ad tmp;
                    if (DescriptionAdd == null || DescriptionAdd == "")
                    {
                        string generatedDescription = $"I am selling my {YearAdd} {BrandAdd}, for more information contact me. (This text is auto generated!)";
                        tmp = new Ad(BrandAdd, ModelAdd, int.Parse(YearAdd), int.Parse(MileageAdd), FuelAdd, TransmissionAdd, int.Parse(CapacityAdd), int.Parse(PowerAdd), int.Parse(PriceAdd), generatedDescription, Image);
                    }
                    else
                    {

                        tmp = new Ad(BrandAdd, ModelAdd, int.Parse(YearAdd), int.Parse(MileageAdd), FuelAdd, TransmissionAdd, int.Parse(CapacityAdd), int.Parse(PowerAdd), int.Parse(PriceAdd), DescriptionAdd, Image);
                    }
                    Ads.Add(tmp);
                    Serialize(Ads);
                    System.Threading.Thread.Sleep(100); // BAD PRACTICE
                    if(addAdWindow != null)
                    {
                        addAdWindow.Close();
                        addAdWindow = null;
                    }
                   
                }else
                {
                    currentlySelectedAd.Brand = BrandAdd;
                    currentlySelectedAd.Model = ModelAdd;
                    currentlySelectedAd.ProductionYear = int.Parse(YearAdd);
                    currentlySelectedAd.Mileage = int.Parse(MileageAdd);
                    currentlySelectedAd.Fuel = FuelAdd;
                    currentlySelectedAd.EngineCapacity = int.Parse(CapacityAdd);
                    currentlySelectedAd.Transmission = TransmissionAdd;
                    currentlySelectedAd.EnginePower = int.Parse(PowerAdd);
                    currentlySelectedAd.Price = int.Parse(PriceAdd);
                    currentlySelectedAd.Description = DescriptionAdd;
                    currentlySelectedAd.Image = Image;
                    System.Threading.Thread.Sleep(100);
                    if(editAdWindow != null) {
                        editAdWindow.Close();
                        editAdWindow = null;
                    } 
                }
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

        public string BrandAdd
        {
            get { return this.brandAdd; }
            set { 
                brandAdd = value;
                NotifyPropertyChanged(nameof(BrandAdd));
            }
        }

        public string ModelAdd
        {
            get { return modelAdd; }
            set
            {
                modelAdd = value;
                NotifyPropertyChanged(nameof(ModelAdd));
            }
        }

        public string YearAdd
        {
            get { return yearAdd; }
            set
            {
                yearAdd = value;
                NotifyPropertyChanged(nameof(YearAdd));
            }
        }

        public string MileageAdd
        {
            get { return mileageAdd; }
            set
            {
                mileageAdd = value;
                if (mileageAdd != null)
                {
                    mileageAdd = value;
                    errorsViewModel.ClearErrors(nameof(MileageAdd));
                    if (mileageAdd.All(char.IsDigit) && mileageAdd != "")
                    {
                        if (int.Parse(mileageAdd) > 5000000 || int.Parse(mileageAdd) < 0)
                        {
                            errorsViewModel.AddError(nameof(MileageAdd), "Mileage error");
                        }
                    }
                    else
                    {
                        errorsViewModel.AddError(nameof(MileageAdd), "Mileage error");
                    }
                    NotifyPropertyChanged(nameof(MileageAdd));
                }
                
            }
        }
        public string FuelAdd
        {
            get { return fuelAdd; }
            set
            {
                fuelAdd = value;
                NotifyPropertyChanged(nameof(FuelAdd));
            }
        }
        public string CapacityAdd
        {
            get { return capacityAdd; }
            set
            {
                capacityAdd = value;
                if (capacityAdd != null)
                {   
                    errorsViewModel.ClearErrors(nameof(CapacityAdd));

                    if (capacityAdd.All(char.IsDigit) && capacityAdd != "")
                    {
                        if (int.Parse(capacityAdd) > 20000 || int.Parse(capacityAdd) < 0)
                        {
                            errorsViewModel.AddError(nameof(CapacityAdd), "Capacity error");
                        }
                    }
                    else
                    {
                        errorsViewModel.AddError(nameof(CapacityAdd), "Capacity error");
                    }

                    NotifyPropertyChanged(nameof(CapacityAdd));
                }

            }
        }

        public string TransmissionAdd
        {
            get { return transmissionAdd; }
            set
            {
                transmissionAdd = value;
                NotifyPropertyChanged(nameof(TransmissionAdd));
            }
        }

        public string PowerAdd
        {
            get { return powerAdd; }
            set
            {
                powerAdd = value;
                if (powerAdd != null)
                {   
                    errorsViewModel.ClearErrors(nameof(PowerAdd));
                    if (powerAdd.All(char.IsDigit) && powerAdd != "")
                    {
                        if (int.Parse(powerAdd) < 0 || int.Parse(powerAdd) > 9999)
                        {
                            errorsViewModel.AddError(nameof(PowerAdd), "Power error");
                        }
                    }
                    else
                    {
                        errorsViewModel.AddError(nameof(PowerAdd), "Power error");
                    }
                    NotifyPropertyChanged(nameof(PowerAdd));
                } 
            }
        }

        public string PriceAdd
        {
            get { return priceAdd; }
            set
            {
                priceAdd = value;
                if(priceAdd != null)
                {
                    errorsViewModel.ClearErrors(nameof(PriceAdd));
                    if(priceAdd.All(char.IsDigit) &&  priceAdd != "")
                    {
                        if(int.Parse(priceAdd) < 0)
                        {
                            errorsViewModel.AddError(nameof(PriceAdd), "Price error");
                        }
                    }else
                    {
                        errorsViewModel.AddError(nameof(PriceAdd), "Price error");
                    }
                    NotifyPropertyChanged(nameof(PriceAdd));
                }
            }
        }

        public string DescriptionAdd
        {
            get { return descriptionAdd; }
            set
            {
                descriptionAdd = value;
                NotifyPropertyChanged(nameof(DescriptionAdd));
            }
        }
        private bool checkInputs()
        {
            if (BrandAdd != null &&
               ModelAdd != null &&
               YearAdd != null &&
               MileageAdd != null &&
               FuelAdd != null &&
               CapacityAdd != null &&
               TransmissionAdd != null &&
               PowerAdd != null &&
               PowerAdd != null)
            {
                return true;
            }
            return false;
        }

        public string AddEditBtn
        {
            get { return addEditBtn; }
            set
            {
                if(value != addEditBtn)
                {
                    addEditBtn = value;
                }
                NotifyPropertyChanged(nameof(AddEditBtn));
            }
        }

        public BitmapImage Image
        {
            get { return image; } 
            set
            {
                image = value;
                NotifyPropertyChanged(nameof(Image));
            }
        }

        // Edit Window
        public void OpenEditWindow()
        {
            if(currentlySelectedAd != null && editAdWindow == null)
            {
                fillYears(); // Fill years for Combo Box Items.
                AddEditBtn = "Edit";
                editAdWindow = new AddAdWindow(); 
                editAdWindow.DataContext = this;
                editAdWindow.Owner = mainWindow;
                editAdWindow.Show();
            }
            else
            {
                MessageBox.Show("Please select an item!", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
