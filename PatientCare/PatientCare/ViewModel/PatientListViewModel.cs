using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Windows.Input;

using PatientCare.UtilityClass;
using DataLayer;
using DataLayer.EntityModel;

namespace PatientCare.ViewModel
{
    public class PatientListViewModel : ViewModelBase
    {
        #region Fields

        private tPatient _selectedPatient;
        private ObservableCollection<tPatient> _listPatient;

        public event Action ClosePateintListScreenAction;
        public event Action PatientSelectedAction;

        #endregion

        #region Command

        private ICommand closePatientsCommand;
        public ICommand ClosePatientsCommand
        {
            get { return closePatientsCommand; }
        }

        private ICommand patientSelectedCommand;
        public ICommand PatienSelectedCommand
        {
            get { return patientSelectedCommand; }
        }

        #endregion

        #region Ctor

        public PatientListViewModel()
        {
            Patient = LoadPatient();
            SetupCommand();
        }

        #endregion

        #region Properties

        public tPatient SelectedPatient
        {
            get { return _selectedPatient; }
            set
            {
                _selectedPatient = value;
                this.RaisePropertyChangedEvent("SelectedPatient");
            }
        }

        public ObservableCollection<tPatient> Patient
        {
            get { return _listPatient; }
            set
            {
                _listPatient = value;
                this.RaisePropertyChangedEvent("Patient");
            }
        }

        #endregion

        #region Private Method

        private void SetupCommand()
        {
            closePatientsCommand = new UtilityClass.RelayCommand(ClosePatientScreen);
            patientSelectedCommand = new UtilityClass.RelayCommand(PatientSelected, () => { return SelectedPatient != null; });
        }

        private void ClosePatientScreen()
        {
            if (ClosePateintListScreenAction != null)
                ClosePateintListScreenAction.Invoke();
        }

        private void PatientSelected()
        {
            if (PatientSelectedAction != null)
                PatientSelectedAction.Invoke();
        }

        private ObservableCollection<tPatient> LoadPatient()
        {
            var listPatient = new ObservableCollection<tPatient>();

            DB_SERVER db = new DB_SERVER();
            var listPatientFromDB = db.Patient;

            foreach (var item in listPatientFromDB)
                listPatient.Add(new tPatient { PatientId = item.PatientId, FirstName = item.FirstName, LastName = item.LastName });

            return listPatient;
        }

        #endregion

    }
}
