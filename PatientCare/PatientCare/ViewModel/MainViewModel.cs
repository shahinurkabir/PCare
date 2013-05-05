using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

using PatientCare.UtilityClass;
using DataLayer.EntityModel;

namespace PatientCare.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        #region Fields

        private int _totalPatientInWorkSpace;

        public event Action ShowPateintListScreenAction;
        public event Action<int> NewPateintEvent;
        public event Action ClosePateintEvent;
        public event Action<string> ThemeChangeEvent;
        #endregion

        #region Properties

        public int TotalPatientInWorkSpace
        {
            get { return _totalPatientInWorkSpace; }
            set
            {
                _totalPatientInWorkSpace = value;
                RaisePropertyChangedEvent("TotalPatientInWorkSpace");
            }
        }

        #endregion

        #region Command

        public ICommand AddPatientToWorkSpaceCommand
        {
            get;
            private set;
        }
        private void AddPatient(object parameter)
        {
            var patient = parameter as tPatient;

        }

        private ICommand showPatientsCommand;
        public ICommand ShowPatientsCommand
        {
            get { return showPatientsCommand; }
        }

        private ICommand addNewPatientCommand;
        public ICommand AddNewPatientCommand
        {
            get { return addNewPatientCommand; }
        }

        private ICommand closePatientCommand;
        public ICommand ClosePatientCommand
        {
            get { return closePatientCommand; }
        }

        public ICommand ApplicationExitCommand
        {
            get { return new UtilityClass.RelayCommand(() => { App.Current.Shutdown(); }); }
        }
        public ICommand ThemeChangeCommand
        {

            get
            {
                return new UtilityClass.RelayCommand<object>((parameter) =>
                {
                    if (ThemeChangeEvent != null)
                        ThemeChangeEvent(parameter as string);
                });
            }
        }
        #endregion

        #region Ctor

        public MainViewModel()
        {
            SetupCommand();
        }

        #endregion

        #region Private Method

        private void SetupCommand()
        {
            showPatientsCommand = new UtilityClass.RelayCommand(ShowPatientScreen);
            addNewPatientCommand = new UtilityClass.RelayCommand(AddNewPatient);
            closePatientCommand = new UtilityClass.RelayCommand(ClosePatient, () => { return _totalPatientInWorkSpace > 0; });

        }

        private void ShowPatientScreen()
        {
            if (ShowPateintListScreenAction != null)
                ShowPateintListScreenAction.Invoke();
        }
        private void AddNewPatient()
        {
            //TotalPatientInWorkSpace++;
            if (NewPateintEvent != null)
                NewPateintEvent(0);
        }
        private void ClosePatient()
        {
            //TotalPatientInWorkSpace--;
            if (ClosePateintEvent != null)
                ClosePateintEvent.Invoke();
        }

        #endregion
    }
}
