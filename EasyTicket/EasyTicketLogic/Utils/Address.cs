using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;


namespace EasyTicketLogic
{
    [DataContract]
    public class Address
    {
        [DataMember]
        public int AddressId { get; private set; }
        [DataMember]
        public string Number { get; internal set; }
        [DataMember]
        public string Street { get; internal set; }
        [DataMember]
        public string FlatNumber { get; internal set; }
        [DataMember]
        public string ZipCode { get; internal set; }
        [DataMember]        
        [MinLength(1)]
        [Required]
        public string CityName { get; internal set; }
        [DataMember]
        public string CountryRegion { get; internal set; }
        [DataMember]
        public string Country { get; internal set; }
        [DataMember]
        public string SkyScannerPlaceId { get; set; }
        [DataMember]
        public string DBahnPlaceId { get; set; }

        internal Address() { }

        public Address(string cityName)
        {
            CityName = cityName;
        }

        public override string ToString()
        {
            return CityName;
        }
    }
}

