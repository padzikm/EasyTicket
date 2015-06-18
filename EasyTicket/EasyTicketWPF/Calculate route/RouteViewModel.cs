using System;
using System.ComponentModel;
using System.Windows.Input;
using System.Collections.ObjectModel;
using Microsoft.Maps.MapControl.WPF;
using System.Text.RegularExpressions;
using System.Windows;

namespace EasyTicketWPFClient.RouteCalc
{
    public class RouteViewModel : INotifyPropertyChanged
    {
        public ICommand CalculateRouteCommand { get; private set; }

        private string _from = "";
        public string From
        {
            get { return _from; }
            set
            {
                _from = value;
                OnPropertyChanged("From");
            }
        }

        private string _to = "";
        public string To
        {
            get { return _to; }
            set
            {
                _to = value;
                OnPropertyChanged("To");
            }
        }

        private BingMapsService.RouteResult _routeResult;
        public BingMapsService.RouteResult RouteResult
        {
            get { return _routeResult; }
            set
            {
                _routeResult = value;
                OnPropertyChanged("RouteResult");
            }
        }


        private ObservableCollection<Direction> _directions;
        public ObservableCollection<Direction> Directions
        {
            get { return _directions; }
            set
            {
                _directions = value;
                OnPropertyChanged("Directions");
            }
        }

        public RouteViewModel()
        {
            CalculateRouteCommand = new DelegateCommand(CalculateRoute);
        }

        public void CalculateRoute()
        {
            try
            {
                var from = GeocodeAddress(From);
                var to = GeocodeAddress(To);

                CalculateRoute(from, to);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Couldn't find any route!");
            }
        }

        private BingMapsService.GeocodeResult GeocodeAddress(string address)
        {
            BingMapsService.GeocodeResult result = null;

            using (BingMapsService.GeocodeServiceClient client = new BingMapsService.GeocodeServiceClient("CustomBinding_IGeocodeService"))
            {
                BingMapsService.GeocodeRequest request = new BingMapsService.GeocodeRequest();
                request.Credentials = new Credentials() { ApplicationId = (App.Current.Resources["MyCredentials"] as ApplicationIdCredentialsProvider).ApplicationId };
                request.Query = address;

                result = client.Geocode(request).Results[0];
            }

            return result;
        }

        private void CalculateRoute(BingMapsService.GeocodeResult from, BingMapsService.GeocodeResult to)
        {
            using (BingMapsService.RouteServiceClient client = new BingMapsService.RouteServiceClient("CustomBinding_IRouteService"))
            {
                BingMapsService.RouteRequest request = new BingMapsService.RouteRequest();
                request.Credentials = new Credentials() { ApplicationId = (App.Current.Resources["MyCredentials"] as ApplicationIdCredentialsProvider).ApplicationId };
                //ObservableCollection<BingMapsService.Waypoint> tmp = new ObservableCollection<BingMapsService.Waypoint>();                
                //tmp.Add(ConvertResultToWayPoint(from));
                //tmp.Add(ConvertResultToWayPoint(to));
                request.Waypoints = new BingMapsService.Waypoint[2];
                request.Waypoints[0] = ConvertResultToWayPoint(from);
                request.Waypoints[1] = ConvertResultToWayPoint(to);

                request.Options = new BingMapsService.RouteOptions();
                request.Options.RoutePathType = BingMapsService.RoutePathType.Points;

                RouteResult = client.CalculateRoute(request).Result;
            }

            //GetDirections();
        }

        private void GetDirections()
        {
            Directions = new ObservableCollection<Direction>();

            foreach (BingMapsService.ItineraryItem item in RouteResult.Legs[0].Itinerary)
            {
                var direction = new Direction();
                direction.Description = GetDirectionText(item);
                direction.Location = new Location(item.Location.Latitude, item.Location.Longitude);
                Directions.Add(direction);
            }
        }

        private static string GetDirectionText(BingMapsService.ItineraryItem item)
        {
            string contentString = item.Text;
            //Remove tags from the string
            Regex regex = new Regex("<(.|\n)*?>");
            contentString = regex.Replace(contentString, string.Empty);
            return contentString;
        }

        private BingMapsService.Waypoint ConvertResultToWayPoint(BingMapsService.GeocodeResult result)
        {
            BingMapsService.Waypoint waypoint = new BingMapsService.Waypoint();
            waypoint.Description = result.DisplayName;
            waypoint.Location = result.Locations[0];
            return waypoint;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}