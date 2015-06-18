using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using EasyTicketLogic;
using Microsoft.Maps.MapControl.WPF;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.IO.Compression;
using System.Web;
using System.Web.Script.Serialization;
using Hardcodet.Wpf.TaskbarNotification;
using System.Drawing;

namespace EasyTicketWPFClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Register logicController;

        public User CurrentUser { get; set; }
        EasyTicketWPFClient.RouteCalc.RouteViewModel model;
        private Dictionary<string, string> listaCelow;
        bool routeSearchingButtonClick;

        public MainWindow()
        {
            InitializeComponent();
            routeSearchingButtonClick = false;
            logicController = new Register();
            model = new EasyTicketWPFClient.RouteCalc.RouteViewModel();
            DataContext = model;
            map.Mode = new AerialMode(true);


            
        }

        private void FillAvailableDestinations()
        {
            //string places, getResult, placeID, placeName;
            //object[] json2;

            //for (char letter = 'a'; letter <= 'z'; ++letter)
            //{
            //    places = "http://www.skyscanner.pl/dataservices/geo/v1.0/autosuggest/PL/pl/" + letter.ToString() + "?isDestination=false&ccy=pln";
            //    getResult = EasyTicketLogic.Web.MakeGetRequest(places);
            //    json2 = ConvertDataToJson(getResult);
            //    foreach (var tmp in json2)
            //    {
            //        placeID = (string)(((Dictionary<string, object>)tmp)["PlaceId"]);
            //        placeName = (string)(((Dictionary<string, object>)tmp)["PlaceName"]);

            //        if (!listaCelow.ContainsKey(placeID))
            //        {
            //            listaCelow.Add(placeID, placeName);
            //            skad.Items.Add(placeName);
            //            dokad.Items.Add(placeName);                        
            //        }
            //    }
            //}            
            //taskBar.Icon = (Icon)new Bitmap("logo.jpg");
            //MessageBox.Show(string.Format("glowny watek nr: {0}", Thread.CurrentThread.ManagedThreadId));

            foreach (var el in listaCelow.Values)
            {
                skad.Items.Add(el);
                dokad.Items.Add(el);
            }
        }

        private void LogInButtonClick(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show(string.Format("glowny watek nr: {0}", Thread.CurrentThread.ManagedThreadId));
            var logInWindow = new LoginPage(logicController);
            logInWindow.ShowDialog();
            if (logInWindow.CurrentUser != null)
            {
                CurrentUser = logInWindow.CurrentUser;
                userName.Text = CurrentUser.Login;
            }
        }        

        private void RegisterButtonClick(object sender, RoutedEventArgs e)
        {
            var registerWindow = new RegisterWindow(logicController);
            registerWindow.ShowDialog();
        }

        private void AddOfferButtonClick(object sender, RoutedEventArgs e)
        {
            if (CurrentUser == null)
            {
                MessageBox.Show("You must be logged in to add offer!");
                return;
            }
            var addOfferPage = new AddOfferPage(logicController);
            addOfferPage.ShowDialog();
        }

        private async void SearchButtonClick(object sender, RoutedEventArgs e)
        {
            DisableAllExceptMap();            
            JourneyPreferences pref = new JourneyPreferences();
            Journey journey;
            decimal price;
            if (depDate.SelectedDate != null)
            {
                //try
                //{
                //    date = depDate.Text.Split(new char[] { '-' });
                //    pref.DepartureDate = new DateTime(int.Parse(date[2]), int.Parse(date[1]), int.Parse(date[0]));
                //}
                //catch
                //{
                //    MessageBox.Show("Data musi byc postaci dd-mm-yyyy");
                //}
                pref.DepartureDate = depDate.SelectedDate;
            }
            else
            {
                MessageBox.Show("Data wylotu musi byc wybrana");
                EnableAll();
                return;
            }
            if (arrDate.SelectedDate != null)
            {
                pref.ArrivalDate = arrDate.SelectedDate;
            }
            if (skad.SelectedIndex == -1 || dokad.SelectedIndex == -1)
            {
                MessageBox.Show("Miejsca docelowe nie moga byc puste");
                EnableAll();
                return;
            }
            pref.DepartureAddress = skad.SelectedIndex > -1 ? new Address(listaCelow.ElementAt(skad.SelectedIndex).Value) : null; //(depAddress.Text != "") ? new Address(depAddress.Text) : null;
            pref.DepartureAddress.SkyScannerPlaceId = skad.SelectedIndex > -1 ? listaCelow.ElementAt(skad.SelectedIndex).Key : null;
            pref.ArrivalAddress = dokad.SelectedIndex > -1 ? new Address(listaCelow.ElementAt(dokad.SelectedIndex).Value) : null; //(arrAddress.Text != "") ? new Address(arrAddress.Text) : null;
            pref.ArrivalAddress.SkyScannerPlaceId = dokad.SelectedIndex > -1 ? listaCelow.ElementAt(dokad.SelectedIndex).Key : null;
            pref.NumberOfAdults = 1;
            pref.NumberOfChildren = 0;
            pref.NumberOfInfants = 0;
            pref.PreferDirectedRoutes = true;
            pref.ComfortClass = "Economy";
            if (decimal.TryParse(maxPriceTextbox.Text, out price))
                pref.MaxTotalPrice = price;
            else
                pref.MaxTotalPrice = decimal.MaxValue;
            
            try
            {
                ShowWaintingCircle();
                journey = await Task<Journey>.Run(async () => { return await logicController.SearchJourney(pref); });
                if (journey != null)
                {
                    GridView gridView = new GridView();
                    gridView.Columns.Add(new GridViewColumn { Header = "CityFrom", DisplayMemberBinding = new Binding("DepartureAddress.CityName") });
                    gridView.Columns.Add(new GridViewColumn { Header = "CityTo", DisplayMemberBinding = new Binding("ArrivalAddress.CityName") });
                    gridView.Columns.Add(new GridViewColumn { Header = "DepDate", DisplayMemberBinding = new Binding("DepartureDate") });
                    gridView.Columns.Add(new GridViewColumn { Header = "ArrDate", DisplayMemberBinding = new Binding("ArrivalDate") });
                    gridView.Columns.Add(new GridViewColumn { Header = "Price", DisplayMemberBinding = new Binding("TotalPrice") });
                    offerListView.View = gridView;
                    routeSearchingButtonClick = true;
                    offerListView.Items.Clear();
                    routeSearchingButtonClick = false;

                    foreach (Offer offer in journey.Offers)
                        if(offer.Tickets.Count() == 1)
                        {
                            //logicController.AddOffer(offer);
                            offerListView.Items.Add(offer);
                        }                    
                }
                waitingViewBox.Visibility = Visibility.Hidden;
                EnableAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Route_MouseEnter(object sender, MouseEventArgs e)
        {
            FrameworkElement pin = sender as FrameworkElement;
            MapLayer.SetPosition(ContentPopup, MapLayer.GetPosition(pin));
            MapLayer.SetPositionOffset(ContentPopup, new System.Windows.Point(20, -15));

            var location = (EasyTicketWPFClient.RouteCalc.Direction)pin.Tag;

            ContentPopupText.Text = location.Description;
            ContentPopup.Visibility = Visibility.Visible;
        }

        private void Route_MouseLeave(object sender, MouseEventArgs e)
        {
            ContentPopup.Visibility = Visibility.Collapsed;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (map.ZoomLevel > 0)
                --map.ZoomLevel;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            ++map.ZoomLevel;
        }

        private async void connectionListView_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if (!routeSearchingButtonClick)
            {
                DisableAllExceptMap();
                ShowWaintingCircle();                
                ListView item = (ListView)sender;
                Offer offer = (Offer)item.SelectedItem;
                depAddress.Text = offer.DepartureAddress.CityName;
                arrAddress.Text = offer.ArrivalAddress.CityName;
                await Task.Run(() => model.CalculateRoute());
                EnableAll();
                waitingViewBox.Visibility = Visibility.Hidden;
            }            
        }               


        private void registerPrefsButton_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentUser != null)
            {
                var registerWindow = new RegisterPrefsWindow(CurrentUser, logicController);
                registerWindow.ShowDialog();
            }
            else
                MessageBox.Show("You must be logged in to add preferences!");
        }

        private void DisableAllExceptMap()
        {
            logInButton.IsEnabled = false;
            registerButton.IsEnabled = false;
            skad.IsEnabled = false;
            dokad.IsEnabled = false;
            depDate.IsEnabled = false;
            arrDate.IsEnabled = false;
            searchButton.IsEnabled = false;
            addOfferButton.IsEnabled = false;
            offerListView.IsEnabled = false;
        }

        private void EnableAll()
        {
            logInButton.IsEnabled = true;
            registerButton.IsEnabled = true;
            skad.IsEnabled = true;
            dokad.IsEnabled = true;
            depDate.IsEnabled = true;
            arrDate.IsEnabled = true;
            searchButton.IsEnabled = true;
            addOfferButton.IsEnabled = true;
            offerListView.IsEnabled = true;
        }

        private void ShowWaintingCircle()
        {
            waitingViewBox.Height = 300;// this.Height;
            waitingViewBox.Width = 300;// this.Width;            
            waitingViewBox.Visibility = Visibility.Visible;
        }

        async private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            listaCelow = await Task<Dictionary<string, string>>.Run(() => logicController.GetAllAvailableDestinations());
            FillAvailableDestinations();
        }
    }
}
