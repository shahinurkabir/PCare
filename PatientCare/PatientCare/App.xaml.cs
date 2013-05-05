using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.IO;

using PatientCare.ViewModel;
using PatientCare.View;
using DataLayer.EntityModel;

/// Develop By  : K.M. Shahinur Kabir 
/// E-Mail        : shahinurkabir@gmail.com 
/// This application medical patient data management  system.
/// Technology is used in this application  :SQL server, Entity Framework,WPF with MVVM concept.
/// Two project in this solution-one is DataLayer,Patient Care
/// Here DataLayer project responsible all communication with database server and sended to view model.
/// and view model communicate with UI .

namespace PatientCare
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private string _currentTheme;
        private string _settingTheme;
        private const string APP_SETTING_PATH = "PatientCARE_TEST_THEME.txt";

        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            try
            {
                string error = e.Exception.Message;
                e.Handled = true;
                MessageBox.Show(error, "System Error");
            }
            catch (Exception)
            {

                //throw;
            }
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            try
            {
                // open the Users screen
                MainWindow mainWindow = new MainWindow();
                MainViewModel mainviewModel = new MainViewModel();
                PatientListView patientView = null;

                string themeName = "Green";//default theme
                if (System.IO.File.Exists(APP_SETTING_PATH))
                {
                    var applicationThemeSetting = File.ReadAllText("PatientCARE_TEST_THEME.txt");
                    applicationThemeSetting = applicationThemeSetting.Replace(System.Environment.NewLine, "");
                    var listTheme = new string[] { "White", "Green", "Red", "Blue" };
                    foreach (var item in listTheme)
                    {
                        if (applicationThemeSetting.ToUpper() == item.ToUpper())
                        {
                            themeName = item;
                            _settingTheme = themeName;
                            break;
                        }
                    }
                }
                // Loading theme
                LoadTheme(themeName);


                mainviewModel.ThemeChangeEvent += (changeTheme) => { LoadTheme(changeTheme); };
                mainviewModel.NewPateintEvent += (patientId) => { LoadPatient(patientId, mainWindow, mainviewModel); };
                mainviewModel.ClosePateintEvent += () => { ClosePatient(mainWindow, mainviewModel); };
                mainviewModel.ShowPateintListScreenAction += () =>
                {
                    try
                    {
                        // ensure only one view is loaded, and the same one remains open for multiple requests
                        patientView = patientView ?? new PatientListView();

                        if (!patientView.IsLoaded)
                        {
                            var patientListViewModel = new PatientListViewModel();
                            patientView.Close();
                            patientView = new PatientListView();
                            patientListViewModel.ClosePateintListScreenAction += () => { patientView.Close(); };
                            patientListViewModel.PatientSelectedAction += () =>
                            {
                                LoadPatient(patientListViewModel.SelectedPatient.PatientId, mainWindow, mainviewModel);
                                patientView.Close();
                            };
                            patientView.DataContext = patientListViewModel;
                        }

                        patientView.ShowDialog();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error");
                        Application.Current.Shutdown();
                    }
                };

                var loginView = new LoginView();

                var loginViewModel = new ViewModel.LoginViewModel();
                loginViewModel.LoginEvent += () =>
                {
                    loginView.Close();
                    TabControl addTabControl = new TabControl();
                    addTabControl.Name = "tcMain";
                    addTabControl.BorderThickness = new Thickness(0);
                   
                    mainWindow.mainPanel.Children.Add(addTabControl);
                    mainWindow.DataContext = mainviewModel;
                    mainWindow.Show();
                };
                loginViewModel.CancelLoginEvent += () => { App.Current.Shutdown(); };
                loginView.DataContext = loginViewModel;
                loginView.Closed += new EventHandler(loginView_Closed);
                loginView.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
                Application.Current.Shutdown();
            }
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            SaveThemeName();
        }

        private void loginView_Closed(object sender, EventArgs e)
        {
            var loginView = sender as LoginView;
            var loginViewDataContext = loginView.DataContext as LoginViewModel;
            if (!loginViewDataContext.LoginSucceed)
                Application.Current.Shutdown();
        }

        private void LoadPatient(int patientId, MainWindow mainWindow, MainViewModel mainViewModel)
        {

            TabControl tabControl = mainWindow.mainPanel.Children[0] as TabControl;

            foreach (var item in tabControl.Items)
            {
                var tabItem = item as TabItem;
                var patientViewExist = tabItem.Content as PatientView;
                var PatientViewModel = patientViewExist.DataContext as PatientViewModel;
                if (PatientViewModel.PatientInfo.PatientId == patientId)
                {
                    tabItem.IsSelected = true;
                    return;
                }
            }

            PatientViewModel patientViewModel = new PatientViewModel(patientId);
            var patientView = new PatientView();


            var patientInTab = new TabItem();
            patientInTab.Header = patientId == 0 ? "Patient ID:NewId()" : "Patient ID:" + patientId.ToString();

            patientView.DataContext = patientViewModel;
            patientInTab.Content = patientView;
            tabControl.Items.Add(patientInTab);
            patientInTab.IsSelected = true;

            mainViewModel.TotalPatientInWorkSpace++;
            var tabId = mainViewModel.TotalPatientInWorkSpace;
            patientInTab.Tag = mainViewModel.TotalPatientInWorkSpace;

            patientViewModel.PatientInfo.SaveAndNextTabAction += () =>
            {
                var tabItem = (TabItem)patientView.tabPatient.Items[1];
                tabItem.IsEnabled = true;
                tabItem.Focus();
                tabItem.IsEnabled = false;
            };
            patientViewModel.PatientAddressInfo.SaveAndNextTabAction += () =>
            {
                var tabItem = (TabItem)patientView.tabPatient.Items[0];
                tabItem.IsEnabled = true;
                tabItem.Focus();
                tabItem.IsEnabled = false;
            };
            patientViewModel.PatientInfo.ShowDoctorScreen += () =>
            {
                LoadDoctorScreen(patientViewModel.PatientInfo.DoctorListViewModel);
            };
            patientViewModel.ClosePateintEvent += () =>
            {
                ClosePatient(mainWindow, mainViewModel, tabId);
            };

        }

        private void ClosePatient(MainWindow mainWindow, MainViewModel mainViewModel, int tabId = 0)
        {

            TabControl tabControl = mainWindow.mainPanel.Children[0] as TabControl;
            TabItem itemToClose = null;
            foreach (var item in tabControl.Items)
            {
                var tabItem = item as TabItem;
                if (tabId != 0)
                {
                    if (tabId == (int)tabItem.Tag)
                    {
                        itemToClose = tabItem;
                        break;
                    }
                }
                else if (tabItem.IsFocused)
                {
                    itemToClose = tabItem;
                    break;
                }
            }
            if (itemToClose != null)
            {
                tabControl.Items.Remove(itemToClose);
                mainViewModel.TotalPatientInWorkSpace--;
            }
        }

        private void LoadDoctorScreen(DoctorListViewModel doctorListViewModel)
        {
            var doctorView = new DoctorListView();
            doctorListViewModel.CloseViewAction += () => { doctorView.Close(); };
            doctorView.DataContext = doctorListViewModel;

            doctorView.ShowDialog();
        }

        private void patientListViewModel_ClosePateintListScreenAction()
        {
            throw new NotImplementedException();
        }

        private void LoadTheme(string themeName)
        {
            // Load the ResourceDictionary into memory.
            string path = @"Styles\" + themeName + ".xaml";

            Uri skinDictUri = new Uri(path, UriKind.Relative);
            ResourceDictionary skinDict = Application.LoadComponent(skinDictUri) as ResourceDictionary;

            Collection<ResourceDictionary> mergedDicts = base.Resources.MergedDictionaries;

            if (mergedDicts.Count > 0)
                mergedDicts.Clear();

            mergedDicts.Add(skinDict);
            _currentTheme = themeName;
        }

        private void SaveThemeName()
        {
            if (_currentTheme != _settingTheme)
                File.WriteAllLines(APP_SETTING_PATH, new string[] { _currentTheme });
        }

       
    }
}
