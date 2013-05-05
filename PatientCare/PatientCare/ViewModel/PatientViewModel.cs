using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Input;

using PatientCare.UtilityClass;
using DataLayer;
using DataLayer.EntityModel;


namespace PatientCare.ViewModel
{
    public class PatientViewModel : ViewModelBase
    {
        #region Field

        private PatientInfoViewModel _patientDetailViewModel;
        private PatientAddressViewModel _patientAddressViewModel;
        public event Action ClosePateintEvent;

        #endregion

        #region Ctor

        public PatientViewModel(int patientId)
        {
            LoadPatient(patientId);
            PatientInfo.PatentInsertedAction += (id) => { PatientAddressInfo.PatientId = id; };
            closePatientCommand = new RelayCommand(ClosePatient);
        }

        #endregion

        #region Properties

        public PatientInfoViewModel PatientInfo
        {
            get { return _patientDetailViewModel; }
            set
            {
                _patientDetailViewModel = value;
                this.RaisePropertyChangedEvent("PatientInfo");
            }
        }

        public PatientAddressViewModel PatientAddressInfo
        {
            get { return _patientAddressViewModel; }
            set
            {
                _patientAddressViewModel = value;
                this.RaisePropertyChangedEvent("PatientAddressInfo");
            }
        }

        #endregion

        #region Command

        private ICommand closePatientCommand;
        public ICommand ClosePatientCommand
        {
            get { return closePatientCommand; }
        }

        #endregion

        #region Private Method

        private void LoadPatient(int patientId)
        {
            if (patientId == 0) // newly entry
            {
                PatientInfo = new PatientInfoViewModel(new tPatient());
                PatientAddressInfo = new PatientAddressViewModel(new tAddress());
            }
            else // Modification need to existing patient;
            {
                DB_SERVER _db = new DataLayer.DB_SERVER();

                var patientInfo = _db.Patient.FirstOrDefault(e => e.PatientId == patientId);
                var patientAddress = _db.Address.FirstOrDefault(e => e.PatientId == patientId);

                PatientInfo = (patientInfo != null) ? new PatientInfoViewModel(patientInfo) : new PatientInfoViewModel(new tPatient());
                PatientAddressInfo = (patientAddress != null) ? new PatientAddressViewModel(patientAddress) : new PatientAddressViewModel(new tAddress());
                PatientAddressInfo.PatientId = PatientInfo.PatientId;

            }
        }

        private void ClosePatient()
        {
            if (ClosePateintEvent != null)
                ClosePateintEvent.Invoke();
        }

        #endregion

    }
}
