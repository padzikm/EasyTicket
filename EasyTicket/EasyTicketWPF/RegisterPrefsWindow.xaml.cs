using EasyTicketLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace EasyTicketWPFClient
{
    /// <summary>
    /// Interaction logic for RegisterPrefsWindow.xaml
    /// </summary>
    public partial class RegisterPrefsWindow : Window
    {
        private ServiceReference.ServiceClient client = new ServiceReference.ServiceClient();
        private Register logicController;
        private Dictionary<string, string> listaCelow;
        private User currentUser;
        public RegisterPrefsWindow(User user, Register controller)
        {
            InitializeComponent();
            logicController = controller;
            currentUser = user;
        }

        private async void saveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                UserPreferences pref = new UserPreferences();
                pref.DepartureAddress = depAddress.SelectedIndex > -1 ? new Address(listaCelow.ElementAt(depAddress.SelectedIndex).Value) : null; //(depAddress.Text != "") ? new Address(depAddress.Text) : null;
                pref.DepartureAddress.SkyScannerPlaceId = depAddress.SelectedIndex > -1 ? listaCelow.ElementAt(depAddress.SelectedIndex).Key : null;
                pref.ArrivalAddress = arrAddress.SelectedIndex > -1 ? new Address(listaCelow.ElementAt(arrAddress.SelectedIndex).Value) : null; //(arrAddress.Text != "") ? new Address(arrAddress.Text) : null;
                pref.ArrivalAddress.SkyScannerPlaceId = arrAddress.SelectedIndex > -1 ? listaCelow.ElementAt(arrAddress.SelectedIndex).Key : null;
                pref.NumberOfAdults = int.Parse(numberOfAdults.Text);
                pref.NumberOfChildren = int.Parse(numberOfChildren.Text);
                pref.NumberOfInfants = int.Parse(numberOfInfants.Text);
                pref.PreferDirectedRoutes = true;
                pref.ComfortClass = "Economy";
                pref.ArrivalDate = arrDate.SelectedDate;
                pref.DepartureDate = depDate.SelectedDate;
                saveButton.IsEnabled = false;
                await client.RegisterPreferencesAsync(currentUser, pref);
                saveButton.IsEnabled = true;
                this.Close();
            }
            catch
            {
                MessageBox.Show("All fields must be filled");
            }


        }

        private void FillAvailableDestinations()
        {
            foreach (var el in listaCelow.Values)
            {
                depAddress.Items.Add(el);
                arrAddress.Items.Add(el);
            }
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            listaCelow = await Task<Dictionary<string, string>>.Run(() => logicController.GetAllAvailableDestinations());
            FillAvailableDestinations();
        }
    }
}
