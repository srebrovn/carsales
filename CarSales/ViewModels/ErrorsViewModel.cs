using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSales.ViewModels
{
    public class ErrorsViewModel
    {
        private readonly Dictionary<string, string> _errors = new Dictionary<string,string>();

        public bool HasErrors => _errors.Any();

        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

        public IEnumerable GetErrors(string? propertyName)
        {
            return _errors.GetValueOrDefault(propertyName, null);
        }

        public void AddError(string propertyName, string errorMessage) 
        {
            if (!_errors.ContainsKey(propertyName))
            {
                _errors.Add(propertyName, "");
            }

            _errors[propertyName] = errorMessage;
            OnErrorsChanged(propertyName);
        }

        public void ClearErrors(string propertyName)
        {
            if (_errors.Remove(propertyName))
            {
                OnErrorsChanged(propertyName);
            }
        }
        private void OnErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }
    }
}
