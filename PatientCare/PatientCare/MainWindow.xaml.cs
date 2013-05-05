using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using PatientCare.ViewModel;

namespace PatientCare
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //private MainViewModel _mainViewModel;
        //private PatientListViewModel _patientListViewModel;
        public MainWindow()
        {
            InitializeComponent();
            //_mainViewModel = new MainViewModel();
            //_patientListViewModel = new PatientListViewModel();
        }

       

        private void mnuShowPatient_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
