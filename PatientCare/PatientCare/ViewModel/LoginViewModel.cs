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
    public class LoginViewModel : ViewModelBase
    {
        #region Field


        private string _userName;
        private string _password;
        private string _loginError;
        private bool _loginSucceed;

        public  event Action LoginEvent;
        public event Action CancelLoginEvent;

        #endregion

        #region Ctor

        public LoginViewModel()
        {
            loginCommand = new RelayCommand(Login, () => !string.IsNullOrWhiteSpace(UserName));
            cancelLoginCommand = new RelayCommand(CancelLogin);
        }

        #endregion

        #region Properties

        public string UserName
        {
            get { return _userName; }
            set
            {
                _userName = value;
                this.RaisePropertyChangedEvent("UserName");
            }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                this.RaisePropertyChangedEvent("Password");
            }
        }

        public string LoginError
        {
            get { return _loginError; }
            set
            {
                _loginError = value;
                this.RaisePropertyChangedEvent("LoginError");
            }
        }
        public bool LoginSucceed
        {
            get { return _loginSucceed; }
            set
            {
                _loginSucceed = value;
                this.RaisePropertyChangedEvent("LoginSucceed");
            }
        }

        #endregion

        #region Command

        private ICommand loginCommand;
        public ICommand LoginCommand
        {
            get { return loginCommand; }
        }
        private ICommand cancelLoginCommand;
        public ICommand CancelLoginCommand
        {
            get { return cancelLoginCommand; }
        }
        #endregion

        #region Private method

        private void Login()
        {
            LoginError = "";
            LoginSucceed = false;
            DataLayer.DB_SERVER db = new DataLayer.DB_SERVER();

            #region Database and  standing data creation
            if (!db.Database.Exists())
            {
                         string _populationStandingData="IF NOT EXISTS(SELECT *FROM tUser)"
                        +"BEGIN"
	                        +" Insert into tUser (UserId,Password) VALUES('sa','123')"
                        +" END"

                        +" IF NOT EXISTS(SELECT * FROM tDoctor)"
                        +" BEGIN"
	                        +" INSERT INTO tDoctor (FirstName,LastName)VALUES ('Daud','Ratul')"
	                        +" INSERT INTO tDoctor (FirstName,LastName)VALUES ('Shahinur','Kabir')"
                        +" END";
                db.Database.CreateIfNotExists();
                db.Database.ExecuteSqlCommand(_populationStandingData);
            }
            #endregion

            var user=db.User.FirstOrDefault(e => e.UserId==UserName);
            if (user == null)
            {
                LoginError = "Invalid Login User ID";
                return;
            }
            else
                if (user.Password != Password)
                {
                    LoginError = "Invalid Login User Password";
                    return;
                }
            LoginSucceed = true;
            if (LoginEvent != null)
                LoginEvent.Invoke();
        }

        private void CancelLogin()
        {
            if (CancelLoginEvent != null)
                CancelLoginEvent.Invoke();
        }

        #endregion

        
    }
}
