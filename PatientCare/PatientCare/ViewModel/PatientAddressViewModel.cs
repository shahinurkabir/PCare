using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.ComponentModel;

using PatientCare.UtilityClass;
using DataLayer.EntityModel;

namespace PatientCare.ViewModel
{
    public class PatientAddressViewModel : ViewModelBase, IDataErrorInfo
    {
        #region Field

        private int _PatientId;
        private string _Address1;
        private string _Address2;
        private string _PostalCode;
        private string _City;

        public event Action SaveAndNextTabAction;
        private readonly IDictionary<string, string> errors = new Dictionary<string, string>();
        #endregion


        #region Ctor

        public PatientAddressViewModel(tAddress address)
        {
            CopyAddressProperties(address);
            CommandSavePatientAddress = new RelayCommand<object>((parameter) => SavePatientAddressData(parameter),(parameter)=>CanSave(parameter));
        }

        #endregion

        #region Preoperties

        public int PatientId
        {
            get { return _PatientId; }
            set
            {
                _PatientId = value;
                this.RaisePropertyChangedEvent("PatientId");
            }
        }
        public string Address1
        {
            get { return _Address1; }
            set
            {
                _Address1 = value;
                this.RaisePropertyChangedEvent("Address1");
                this.Validate();
            }
        }
        public string Address2
        {
            get { return _Address2; }
            set
            {
                _Address2 = value;
                this.RaisePropertyChangedEvent("Address2");
                this.Validate();
            }
        }

        public string PostalCode
        {
            get { return _PostalCode; }
            set
            {
                _PostalCode = value;
                this.RaisePropertyChangedEvent("PostalCode");
                this.Validate();
            }
        }
        public string City
        {
            get { return _City; }
            set
            {
                _City = value;
                this.RaisePropertyChangedEvent("City");
                this.Validate();
            }
        }


        #endregion

        #region IDataErrorInfo Interface

        public string this[string columnName]
        {
            get
            {
                if (this.errors.ContainsKey(columnName))
                {
                    return this.errors[columnName];
                }
                return null;
            }
            set
            {
                this.errors[columnName] = value;
            }
        }

        public string Error
        {
            get
            {
                // Not implemented because we are not consuming it in this quick start. 
                // Instead, we are displaying error messages at the item level. 
                throw new NotImplementedException();
            }
        }

        #endregion

        #region Private Method

        private void Validate()
        {
            if (string.IsNullOrWhiteSpace(this.Address1))
            {
                this["Address1"] = "Address1 is required.";
            }
            else
            {
                this.ClearError("Address1");
            }

            if (string.IsNullOrWhiteSpace(this.Address2))
            {
                //this["Address2"] = "Address2 is required.";
            }
            else
            {
                this.ClearError("Address2");
            }

            if (string.IsNullOrWhiteSpace(this.PostalCode ))
            {
                this["PostalCode"] = "Postal Code is required.";
            }
            else
            {
                this.ClearError("PostalCode");
            }
            if (string.IsNullOrWhiteSpace(this.City))
            {
                this["City"] = "City is required.";
            }
            else
            {
                this.ClearError("City");
            }
        }
        private ICommand _saveCommand;
        public ICommand CommandSavePatientAddress
        {
            get { return _saveCommand; }
            set { _saveCommand = value; }
        }

        private void CopyAddressProperties(tAddress address)
        {
            PatientId = address.PatientId;
            Address1 = address.Address1;
            Address2 = address.Address2;
            PostalCode = address.PostalCode;
            City = address.City;
        }

        private void SavePatientAddressData(object parameter)
        {

            DataLayer.DB_SERVER db = new DataLayer.DB_SERVER();

            var patientAddressInDB = db.Address.FirstOrDefault(e => e.PatientId == PatientId);
            if (patientAddressInDB != null)
            {
                patientAddressInDB.Address1 = Address1;
                patientAddressInDB.Address2 = Address2;
                patientAddressInDB.PostalCode = PostalCode;
                patientAddressInDB.City = City;
                db.SaveChanges();
            }
            else
            {
                db.Address.Add(new tAddress { PatientId = PatientId, Address1 = Address1, Address2 = Address2, PostalCode = PostalCode, City = City });
                db.SaveChanges();
            }

            var commandParameter = Convert.ToInt16(parameter);

            if (commandParameter != null && commandParameter != 0) //save and previous tab
                if (SaveAndNextTabAction != null)
                    SaveAndNextTabAction.Invoke();
        }
        private void ClearError(string columnName)
        {
            if (this.errors.ContainsKey(columnName))
            {
                this.errors.Remove(columnName);
            }
        }
        private bool CanSave(object arg)
        {
            return this.errors.Count == 0;
        }

        #endregion

    }
}
