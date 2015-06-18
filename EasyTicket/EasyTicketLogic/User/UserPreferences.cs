using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyTicketLogic
{
    [Serializable]
    [DataContract]
    public class UserPreferences
    {
        [DataMember]
        public int UserPreferencesId { get; set; }
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

        public UserPreferences() { }
    }

   
}
