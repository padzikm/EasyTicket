using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Input;
using Microsoft.Maps.MapControl.WPF;

namespace EasyTicketWPFClient
{
    public class GeocodeViewModel : INotifyPropertyChanged
    {
        public ICommand GeocodeAddressCommand { get; private set; }

        private BingMapsService.GeocodeResult _geocodeResult;
        public BingMapsService.GeocodeResult GeocodeResult
        {
            get { return _geocodeResult; }
            set
            {
                _geocodeResult = value;
                OnPropertyChanged("GeocodeResult");
            }
        }

        public GeocodeViewModel()
        {
            GeocodeAddressCommand = new DelegateCommand<String>(GeocodeAddress);
        }        

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private void GeocodeAddress(string address)
        {
            using (BingMapsService.GeocodeServiceClient client = new BingMapsService.GeocodeServiceClient("CustomBinding_IGeocodeService"))
            {
                client.GeocodeCompleted += (o, e) =>
                {
                    if (e.Error == null)
                    {
                        GeocodeResult = e.Result.Results[0];
                    }
                };

                BingMapsService.GeocodeRequest request = new BingMapsService.GeocodeRequest();
                request.Credentials = new Credentials() { ApplicationId =  (App.Current.Resources["MyCredentials"] as ApplicationIdCredentialsProvider).ApplicationId };
                request.Query = address;
                client.GeocodeAsync(request);
            }
        }
    }
}
