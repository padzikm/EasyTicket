using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;


namespace EasyTicketLogic
{
    [Serializable]
    [DataContract]
    public class User
    {
        [DataMember]
        public int UserId { get; private set; }
        [DataMember]
        [Required] 
        [StringLength(30, MinimumLength=1)]
        public string Name { get; private set; }
        [DataMember]
        [Required]
        [StringLength(30, MinimumLength=2)]
        public string Lastname { get; private set; }
        [Required]
        [RegularExpression(@"^[a-z0-9_-]{3,15}$", ErrorMessage = "Invalid login")]
        public string Login { get; private set; }
        [Required]
        public string Password { get; private set; }
        [DataMember]
        [Required]
        [StringLength(40, MinimumLength = 1)]
        [RegularExpression(@"^[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@" +
                             @"(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?$", ErrorMessage = "Invalid E-mail address")]
        public string Email { get; private set; }
        [Column(TypeName = "image")]
        public byte[] Avatar { get; private set; }
        public bool IsSignedForNewsletter { get; set; }

        [DataMember]
        public UserPreferences Preferences { get; set; }
        public UserRole Role { get; private set; }
        public LinkedList<OrderDetails> Orders { get; private set; }

        public User() { }

        public User(string name, string lastname, string login, string password, string email, bool isSigned)
        {
            Name = name;
            Lastname = lastname;
            Login = login;
            Email = email;
            Password = !String.IsNullOrEmpty(password) ? Encode.Hash(password) : null;
            Role = new UserRole();
            Preferences = new UserPreferences();
            Orders = new LinkedList<OrderDetails>();
            IsSignedForNewsletter = isSigned;
        }

        public void UpdatePrerefences(UserPreferences userPrefs)
        {
            Preferences.DepartureAddress = userPrefs.DepartureAddress;
            Preferences.ArrivalAddress = userPrefs.ArrivalAddress;
            Preferences.DepartureDate = userPrefs.DepartureDate;
            Preferences.ArrivalDate = userPrefs.ArrivalDate;
            Preferences.PreferDirectedRoutes = userPrefs.PreferDirectedRoutes;
            Preferences.NumberOfAdults = userPrefs.NumberOfAdults;
            Preferences.NumberOfChildren = userPrefs.NumberOfChildren;
            Preferences.NumberOfInfants = userPrefs.NumberOfInfants;
            Preferences.ComfortClass = userPrefs.ComfortClass;

        }

    }

}
