using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.ComponentModel;

using PatientCare.UtilityClass;
using DataLayer;
using DataLayer.EntityModel;

namespace PatientCare.ViewModel
{
    public class PatientInfoViewModel : ViewModelBase, IDataErrorInfo
    {
        #region Field

        private int _PatientId;
        private string _FirstName;
        private string _LastName;
        private int _DoctorId;
        private string _doctorName;
        private DateTime _DOB;
        private string _SSN;

        public event Action<int> PatentInsertedAction;
        public event Action SaveAndNextTabAction;
        public event Action ShowDoctorScreen;
        private DoctorListViewModel _doctorListViewModel;

        private readonly IDictionary<string, string> errors = new Dictionary<string, string>();

        #endregion

        #region Ctor

        public PatientInfoViewModel(tPatient patientInfo)
        {
            _doctorListViewModel = new DoctorListViewModel();
            _doctorListViewModel.DoctorSelectEvent += (selectedDoctor) =>
            {
                if (selectedDoctor != null)
                {
                    DoctorId = selectedDoctor.DoctorId;
                    DoctorName = selectedDoctor.FirstName + " " + selectedDoctor.LastName;
                }
            };
            CopyPatientProperties(patientInfo);
            CommandSavePatientDetail = new RelayCommand<object>((parameter) => SavePatientData(parameter), (parameter)=> CanSave(parameter));
            showDoctorCommand = new RelayCommand(ShowDoctor);
        }

        #endregion

        #region Command

        private ICommand _saveCommand;
        public ICommand CommandSavePatientDetail
        {
            get { return _saveCommand; }
            set { _saveCommand = value; }
        }

        private ICommand showDoctorCommand;
        public ICommand ShowDoctorCommand
        {
            get { return showDoctorCommand; }
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
                this.Validate();
            }
        }
        public string FirstName
        {
            get { return _FirstName; }
            set
            {
                _FirstName = value;
                this.RaisePropertyChangedEvent("FirstName");
                this.Validate();
            }
        }
        public string LastName
        {
            get { return _LastName; }
            set
            {
                _LastName = value;
                this.RaisePropertyChangedEvent("LastName");
                this.Validate();
            }
        }
        public int DoctorId
        {
            get { return _DoctorId; }
            set
            {
                _DoctorId = value;
                this.RaisePropertyChangedEvent("DoctorId");
                this.Validate();
            }
        }
        public string DoctorName
        {
            get { return _doctorName; }
            set
            {
                _doctorName = value;
                this.RaisePropertyChangedEvent("DoctorName");
                this.Validate();
            }
        }
        public DateTime DOB
        {
            get { return _DOB; }
            set
            {
                _DOB = value;
                this.RaisePropertyChangedEvent("DOB");
                this.Validate();
            }
        }
        public string SSN
        {
            get { return _SSN; }
            set
            {
                _SSN = value;
                this.RaisePropertyChangedEvent("SSN");
                this.Validate();
            }
        }

        public DoctorListViewModel DoctorListViewModel
        {
            get { return _doctorListViewModel; }
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
            if (string.IsNullOrWhiteSpace( this.FirstName ))
            {
                this["FirstName"] = "First name is required.";
            }
            else
            {
                this.ClearError("FirstName");
            }

            if (string.IsNullOrWhiteSpace(this.LastName))
            {
                this["LastName"] = "Last name is required.";
            }
            else
            {
                this.ClearError("LastName");
            }

            if (this.DoctorId==0)
            {
                this["DoctorId"] = "Doctor is required.";
            }
            else
            {
                this.ClearError("DoctorId");
            }
            if (string.IsNullOrWhiteSpace( this.SSN ))
            {
                this["SSN"] = "SSN is required.";
            }
            else
            {
                this.ClearError("SSN");
            }
        }

        private void ClearError(string columnName)
        {
            if (this.errors.ContainsKey(columnName))
            {
                this.errors.Remove(columnName);
            }
        }

        private void CopyPatientProperties(tPatient patientInfo)
        {
            PatientId = patientInfo.PatientId;
            FirstName = patientInfo.FirstName;
            LastName = patientInfo.LastName;
            SSN = patientInfo.SSN;
            DOB = patientInfo.PatientId == 0 ? System.DateTime.Now : patientInfo.DOB;
            DoctorId = patientInfo.DoctorId;
            if (patientInfo.PatientId != 0)
            {
                DB_SERVER _db = new DataLayer.DB_SERVER();
                var patientDoctor= _db.Doctor.FirstOrDefault(e => e.DoctorId == patientInfo.DoctorId);
                DoctorId = patientDoctor.DoctorId;
                DoctorName = patientDoctor.FirstName + " " + patientDoctor.LastName;
            }
        }

        private void SavePatientData(object parameter)
        {
            bool newlyAdd = PatientId == 0 ? true : false;

           // DoctorId = _DoctorId;
            var patient = new tPatient { PatientId = PatientId, FirstName = FirstName, LastName = LastName, DOB = DOB, SSN = SSN, DoctorId = DoctorId };

            DataLayer.DB_SERVER db = new DataLayer.DB_SERVER();

            if (newlyAdd)
            {
                db.Patient.Add(patient);
                db.SaveChanges();
                PatientId = patient.PatientId;
            }
            else
            {
                var patientInDB = db.Patient.FirstOrDefault(e => e.PatientId == PatientId);
                if (patientInDB != null)
                {
                    patientInDB.FirstName = FirstName;
                    patientInDB.LastName = LastName;
                    patientInDB.DOB = DOB;
                    patientInDB.SSN = SSN;
                    patientInDB.DoctorId = DoctorId;
                    db.SaveChanges();
                }
            }

            if (newlyAdd)
                if (PatentInsertedAction != null)
                    PatentInsertedAction(PatientId);

            var commandParameter = Convert.ToInt16(parameter);

            if (commandParameter != null && commandParameter != 0) //save and next tab
                if (SaveAndNextTabAction != null)
                    SaveAndNextTabAction.Invoke();
        }

        private void ShowDoctor()
        {
            if (ShowDoctorScreen != null)
                ShowDoctorScreen.Invoke();
        }

        private bool CanSave(object arg)
        {
            return this.errors.Count == 0;
        }

        #endregion

    }
}
