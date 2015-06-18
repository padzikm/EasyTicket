using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.IO.Compression;
using System.Web.Script.Serialization;
using System.Web;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace EasyTicketLogic
{
    [DataContract]
    internal class SkyScannerAdapter : IJourneySystemAdapter
    {
        [DataMember]
        private string allStationsUrl;
        [DataMember]
        private string allRoutesUrl;
        [DataMember]
        private string routeDetailsUrl;
        [DataMember]
        private string postData;
        [DataMember]
        private string responseData;
        [DataMember]
        private Dictionary<string, object> json;
        [DataMember]
        private LinkedList<Offer> offers;

        internal SkyScannerAdapter()
        {
            allStationsUrl = "http://www.skyscanner.pl/dataservices/geo/v1.0/autosuggest/PL/pl/";
            allRoutesUrl = "http://www.skyscanner.pl/dataservices/routedate/v2.0/?use204=true";
            routeDetailsUrl = "http://www.skyscanner.pl/dataservices/routedate/v2.0/ServiceID/booking/RouteID";
        }

        private void CreatePostData(JourneyPreferences pref)
        {
            postData = "MergeCodeshares=false" +
                       "&SkipMixedAirport=" + pref.PreferDirectedRoutes +
                       "&OriginPlace=" + pref.DepartureAddress.SkyScannerPlaceId +
                       "&DestinationPlace=" + pref.ArrivalAddress.SkyScannerPlaceId +
                       "&OutboundDate=" + ((pref.DepartureDate != null) ? string.Format("{0}-{1}-{2}", pref.DepartureDate.Value.Year, pref.DepartureDate.Value.Month, pref.DepartureDate.Value.Day) : "") +
                       "&InboundDate=" +
                       "&Passengers.Adults=" + pref.NumberOfAdults +
                       "&Passengers.Children=" + pref.NumberOfChildren +
                       "&Passengers.Infants=" + pref.NumberOfInfants +
                       "&CabinClass=" + pref.ComfortClass +
                       "&UserInfo.ChannelId=transportfunnel" +
                       "&UserInfo.CountryId=PL" +
                       "&UserInfo.LanguageId=pl" +
                       "&UserInfo.CurrencyId=PLN" +
                       "&JourneyModes=flight";
        }

        private async Task ParseJsonToOffers()
        {
            int i = 0, j = 0, k = 0;
            try
            {
                string routeID, tmp, originID, destID, getResult, serviceID;
                string[] cos, date, time;
                int priceID = 0;
                decimal price;
                offers = new LinkedList<Offer>();
                Offer offer = null;
                Ticket ticket;
                Address depAddress = null, arrAddress = null;
                DateTime depDate, arrDate;
                serviceID = (string)json["SessionKey"];


                foreach (var itinerary in (object[])json["Itineraries"])
                {
                    //offer.TotalPrice = decimal.MaxValue;

                    routeID = (string)((Dictionary<string, object>)itinerary)["OutboundLegId"];

                    foreach (var outboundLeg in (object[])json["OutboundItineraryLegs"])
                    {
                        if ((string)(((Dictionary<string, object>)outboundLeg)["Id"]) == routeID)
                        {
                            if ((int)(((Dictionary<string, object>)outboundLeg)["StopsCount"]) == 0)
                            {
                                tmp = (string)(((Dictionary<string, object>)outboundLeg)["DepartureDateTime"]);
                                cos = tmp.Split(new char[] { 'T' });
                                date = cos[0].Split(new char[] { '-' });
                                time = cos[1].Split(new char[] { ':' });
                                depDate = new DateTime(int.Parse(date[0]), int.Parse(date[1]), int.Parse(date[2]), int.Parse(time[0]), int.Parse(time[1]), 0);
                                tmp = (string)(((Dictionary<string, object>)outboundLeg)["ArrivalDateTime"]);
                                cos = tmp.Split(new char[] { 'T' });
                                date = cos[0].Split(new char[] { '-' });
                                time = cos[1].Split(new char[] { ':' });
                                arrDate = new DateTime(int.Parse(date[0]), int.Parse(date[1]), int.Parse(date[2]), int.Parse(time[0]), int.Parse(time[1]), 0);
                                originID = (string)(((Dictionary<string, object>)outboundLeg)["OriginStation"]);
                                foreach (var station in (object[])json["Stations"])
                                {
                                    if (((string)((Dictionary<string, object>)station)["Id"]) == originID)
                                    {
                                        depAddress = new Address((string)((Dictionary<string, object>)station)["Name"]);
                                        break;
                                    }
                                }
                                destID = (string)(((Dictionary<string, object>)outboundLeg)["DestinationStation"]);
                                foreach (var station in (object[])json["Stations"])
                                {
                                    if (((string)((Dictionary<string, object>)station)["Id"]) == destID)
                                    {
                                        arrAddress = new Address((string)((Dictionary<string, object>)station)["Name"]);
                                        break;
                                    }
                                }
                                ticket = new Ticket(0, depAddress, depDate, arrAddress, arrDate);
                                LinkedList<Ticket> list = new LinkedList<Ticket>();
                                list.AddLast(ticket);
                                offer = new Offer(list, 1);
                                //offers.AddLast(offer);
                            }
                            else
                            {
                                routeDetailsUrl = "http://www.skyscanner.pl/dataservices/routedate/v2.0/" + serviceID + "/booking/" + routeID;
                                getResult = Web.MakeGetRequest(routeDetailsUrl);
                                Dictionary<string, object> json2 = (Dictionary<string, object>)Web.ConvertDataToJson(getResult);
                                LinkedList<Ticket> list = new LinkedList<Ticket>();
                                foreach (var outboundSegment in (object[])json2["OutboundSegments"])
                                {
                                    tmp = (string)(((Dictionary<string, object>)outboundSegment)["DepartureDateTime"]);
                                    cos = tmp.Split(new char[] { 'T' });
                                    date = cos[0].Split(new char[] { '-' });
                                    time = cos[1].Split(new char[] { ':' });
                                    depDate = new DateTime(int.Parse(date[0]), int.Parse(date[1]), int.Parse(date[2]), int.Parse(time[0]), int.Parse(time[1]), 0);
                                    tmp = (string)(((Dictionary<string, object>)outboundSegment)["ArrivalDateTime"]);
                                    cos = tmp.Split(new char[] { 'T' });
                                    date = cos[0].Split(new char[] { '-' });
                                    time = cos[1].Split(new char[] { ':' });
                                    arrDate = new DateTime(int.Parse(date[0]), int.Parse(date[1]), int.Parse(date[2]), int.Parse(time[0]), int.Parse(time[1]), 0);
                                    originID = (string)(((Dictionary<string, object>)outboundSegment)["OriginStation"]);
                                    foreach (var station in (object[])json2["Stations"])
                                    {
                                        if (((string)((Dictionary<string, object>)station)["Id"]) == originID)
                                        {
                                            depAddress = new Address((string)((Dictionary<string, object>)station)["Name"]);
                                            break;
                                        }
                                    }
                                    destID = (string)(((Dictionary<string, object>)outboundSegment)["DestinationStation"]);
                                    foreach (var station in (object[])json2["Stations"])
                                    {
                                        if (((string)((Dictionary<string, object>)station)["Id"]) == destID)
                                        {
                                            arrAddress = new Address((string)((Dictionary<string, object>)station)["Name"]);
                                            break;
                                        }
                                    }
                                    ticket = new Ticket(0, depAddress, depDate, arrAddress, arrDate);
                                    list.AddLast(ticket);
                                }
                                offer = new Offer(list, 1);
                                //offers.AddLast(offer);
                            }
                            ++j;
                            break;
                        }
                    }
                    offer.TotalPrice = decimal.MaxValue;
                    foreach (var pricingOption in (object[])((Dictionary<string, object>)itinerary)["PricingOptions"])
                    {
                        if (((Dictionary<string, object>)pricingOption).ContainsKey("QuoteIds"))
                        {
                            priceID = (int)((object[])((Dictionary<string, object>)pricingOption)["QuoteIds"])[0];

                            foreach (var quote in (object[])json["Quotes"])
                            {
                                if (int.Parse(((string)((Dictionary<string, object>)quote)["Id"])) == priceID)
                                {
                                    if ((((Dictionary<string, object>)quote)["Price"]).GetType() == typeof(decimal))
                                        price = (decimal)(((Dictionary<string, object>)quote)["Price"]);
                                    else if ((((Dictionary<string, object>)quote)["Price"]).GetType() == typeof(int))
                                        price = (decimal)((int)(((Dictionary<string, object>)quote)["Price"]));
                                    else
                                        price = decimal.MaxValue;
                                    //if (price < route.TotalPrice)
                                    //route.TotalPrice = price;
                                    if(price < offer.TotalPrice)
                                        offer.TotalPrice = price;
                                    break;
                                }
                            }
                        }
                        ++k;
                    }
                    offers.AddLast(offer);
                    //if (route.Offers.Count > 0)
                    //    offers.AddLast(route);
                    //++i;
                }
            }
            catch
            {
                int c = i + j + k;
            }
        }

        public async Task<IEnumerable<Offer>> GetOffers(JourneyPreferences pref)
        {
            CreatePostData(pref);
            responseData = Web.MakePostRequestGzipDecompression(allRoutesUrl, postData);
            json = (Dictionary<string, object>)Web.ConvertDataToJson(responseData);
            await ParseJsonToOffers();

            return offers;
        }

        public void MakeReservations()
        {
            throw new NotImplementedException();
        }

        public async Task<Dictionary<string, string>> GetAllAvailableDestinations()
        {
            string places, getResult, placeID, placeName, cityID;
            object[] json2;
            Dictionary<string, string> listaCelow = new Dictionary<string, string>();

            for (char letter = 'a'; letter <= 'z'; ++letter)
            {
                places = allStationsUrl + letter.ToString() + "?isDestination=false&ccy=pln";
                getResult = Web.MakeGetRequest(places);
                json2 = (object[])Web.ConvertDataToJson(getResult);
                foreach (var tmp in json2)
                {
                    placeID = (string)(((Dictionary<string, object>)tmp)["PlaceId"]);
                    placeName = (string)(((Dictionary<string, object>)tmp)["PlaceName"]);
                    cityID = (string)(((Dictionary<string, object>)tmp)["CityId"]);

                    if (!listaCelow.ContainsKey(placeID) && !string.IsNullOrEmpty(cityID))                    
                        listaCelow.Add(placeID, placeName);                                            
                }
            }
            return listaCelow.OrderBy(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
        }
    }
}
