using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

using PatientCare.UtilityClass;
using DataLayer;
using DataLayer.EntityModel;

namespace PatientCare.ViewModel
{
    public class DoctorListViewModel : ViewModelBase
    {
        #region Fields

        private tDoctor _selectedDoctor;
        private ObservableCollection<tDoctor> _listDoctor;

        public event Action<tDoctor> DoctorSelectEvent;
        public event Action  CloseViewAction;

        #endregion

        #region Ctor

        public DoctorListViewModel()
        {
            Doctor = LoadDoctor();
            SetupCommand();
        }

        #endregion


        #region Command

        private ICommand selectDoctorCommand;
        public ICommand SelectDoctorCommand
        {
            get { return selectDoctorCommand; }
        }
        private ICommand closetDoctorCommand;
        public ICommand CloseDoctorCommand
        {
            get { return closetDoctorCommand; }
        }

        #endregion

        #region Properties

        public tDoctor SelectedDoctor
        {
            get { return _selectedDoctor; }
            set
            {
                _selectedDoctor = value;
                this.RaisePropertyChangedEvent("SelectedDoctor");

            }
        }

        public ObservableCollection<tDoctor> Doctor
        {
            get { return _listDoctor; }
            set
            {
                _listDoctor = value;
                this.RaisePropertyChangedEvent("Doctor");
            }
        }

        #endregion

        #region Private Method

        private void DoctorSelect()
        {
            if (DoctorSelectEvent != null)
                DoctorSelectEvent(_selectedDoctor);
            if (CloseViewAction != null)
                CloseViewAction.Invoke();
        }

        private void CloseDoctor()
        {
            if (CloseViewAction != null)
                CloseViewAction.Invoke();
        }

        private ObservableCollection<tDoctor> LoadDoctor()
        {
            var listDoctor = new ObservableCollection<tDoctor>();

            DB_SERVER db = new DB_SERVER();
            var listDoctorFromDB = db.Doctor;

            foreach (var item in listDoctorFromDB)
                listDoctor.Add(new tDoctor { DoctorId = item.DoctorId, FirstName = item.FirstName, LastName = item.LastName });

            return listDoctor;
        }
        
        private void SetupCommand()
        {
            selectDoctorCommand = new RelayCommand(DoctorSelect, () => { return SelectedDoctor != null; });
            closetDoctorCommand = new RelayCommand(CloseDoctor);
        }

        #endregion
    }
}
