using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyTicketLogic
{
    [DataContract]
    public class JourneyPreferences
    {
        public int JourneyPreferencesId { get; private set; }
        [DataMember]
        public Address DepartureAddress { get; set; }
        [DataMember]
        public Address ArrivalAddress { get; set; }
        [DataMember]
        public DateTime? DepartureDate { get; set; }
        [DataMember]
        public DateTime? ArrivalDate { get; set; }
        [DataMember]
        public bool PreferDirectedRoutes { get; set; }
        [DataMember]
        public int NumberOfAdults { get; set; }
        [DataMember]
        public int NumberOfChildren { get; set; }
        [DataMember]
        public int NumberOfInfants { get; set; }
        [DataMember]
        public string ComfortClass { get; set; }
        [DataMember]
        public int TransportType { get; set; }
        [DataMember]
        public decimal MaxTotalPrice { get; set; }

        public JourneyPreferences()
        {
            //DepartureAddress = new Address();
            //ArrivalAddress = new Address();
            //DepartureDate = new DateTime();
            //ArrivalDate = new DateTime();
        }

        public JourneyPreferences(UserPreferences userPrefs)
        {
            DepartureAddress = userPrefs.DepartureAddress;
            ArrivalAddress = userPrefs.ArrivalAddress;
            DepartureDate = userPrefs.DepartureDate;
            ArrivalDate = userPrefs.ArrivalDate;
            PreferDirectedRoutes = userPrefs.PreferDirectedRoutes;
            NumberOfAdults = userPrefs.NumberOfAdults;
            NumberOfChildren = userPrefs.NumberOfChildren;
            NumberOfInfants = userPrefs.NumberOfInfants;
            ComfortClass = userPrefs.ComfortClass;
            TransportType = userPrefs.TransportType;
            MaxTotalPrice = decimal.MaxValue;
        }
    }
}
