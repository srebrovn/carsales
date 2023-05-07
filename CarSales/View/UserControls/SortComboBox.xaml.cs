using System.Windows;
using System.Windows.Controls;


namespace CarSales.View.UserControls
{
    /// <summary>
    /// Interaction logic for SortComboBox.xaml
    /// </summary>
    public partial class SortComboBox : UserControl
    {
        public SortComboBox()
        {
            InitializeComponent();
            cbSort.SelectionChanged += OnSortItemSelected;
        }

        public delegate void SortItemSelectedEventHandler(object obj, string index);

        public event SortItemSelectedEventHandler? SortItemSelected;

        protected virtual void OnSortItemSelected(object sender, RoutedEventArgs e)
        {
            ComboBoxItem item = cbSort.SelectedItem as ComboBoxItem;

            if(item != null)
            {
                string tagValue = (string)item.Tag;
                if(tagValue != null)
                {
                    if(SortItemSelected != null)
                        SortItemSelected.Invoke(this, tagValue);
                }
            }
        }
    }
}
