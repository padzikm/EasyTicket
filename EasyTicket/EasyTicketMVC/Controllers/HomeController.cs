using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EasyTicketLogic;
using System.Threading.Tasks;

namespace EasyTicketMVC.Controllers
{
    public class HomeController : Controller
    {
        public static Dictionary<string, string> destinations;
        public static Register register;
        private Register logicController = new Register();
        private ServiceReference.ServiceClient client = new ServiceReference.ServiceClient();
        public static Dictionary<string, string> listaCelow;
        public static IEnumerable<SelectListItem> selectList;

        public User CurrentUser { get; set; }

        public ActionResult Index()
        {
            if(register == null)
                register = new Register();
            if(destinations == null)
            {
                destinations = register.GetAllAvailableDestinations().Result;
                listaCelow = destinations;
            selectList =
                from s in listaCelow
                select new SelectListItem
                {
                    Text = s.Value,
                    Value = s.Key
                };
            }

            ViewBag.Destinations = destinations;
            
            return View();
        }
        
        public ActionResult SearchFlight()
        {
            JourneyPreferences journeyPref = new JourneyPreferences();

            return PartialView("SearchFlightPreferences", journeyPref);
        }

        [HttpPost]
        public ActionResult SearchFlight(string postData)
        {
            try
            {
                decimal price;
                JourneyPreferences journeyPref = new JourneyPreferences();

                journeyPref.DepartureAddress = new Address(Request.Form["depCityId"]);
                journeyPref.DepartureAddress.SkyScannerPlaceId = Request.Form["depCityId"];
                journeyPref.ArrivalAddress = new Address(Request.Form["arrCityId"]);
                journeyPref.ArrivalAddress.SkyScannerPlaceId = Request.Form["arrCityId"];
                journeyPref.PreferDirectedRoutes = true;
                journeyPref.ComfortClass = "Economy";
                if (decimal.TryParse(Request.Form["maxTotalPrice"], out price))
                    journeyPref.MaxTotalPrice = price > 0 ? price : decimal.MaxValue;
                else
                    journeyPref.MaxTotalPrice = decimal.MaxValue;
                journeyPref.NumberOfAdults = 1;
                journeyPref.NumberOfChildren = 0;
                journeyPref.NumberOfInfants = 0;
                string data = Request.Form["DepartureDate"];
                string[] dataTable = data.Split(new char[] { '/' });
                journeyPref.DepartureDate = new DateTime(int.Parse(dataTable[2]), int.Parse(dataTable[0]), int.Parse(dataTable[1]));

                Journey journey = register.SearchJourney(journeyPref).Result;
                ViewBag.Journey = journey;

                if (register == null)
                    register = new Register();
                if (destinations == null)
                    destinations = register.GetAllAvailableDestinations().Result;

                ViewBag.Destinations = destinations;
            }
            catch
            {                
                ViewBag.Error = true;
                ViewBag.ErrorMessage = "Uuuuuuuuuuuuuuuuuuuuupssssssssssssssssssss";                
            }
            
            return View("Index");
        }

        public ActionResult LoginUser()
        {
            EasyTicketLogic.User user = new EasyTicketLogic.User();

            if (Session["User"] != null)
                user = Session["User"] as User;

            return PartialView(user);
        }

        [HttpPost]
        public async Task<ActionResult> LoginUser(EasyTicketLogic.User userx)
        {
            //String value = Request.Form["Name"];
            //if (!ModelState.IsValid)
            //    return PartialView("LoginUser", userx);
            try
            {
                if (await logicController.LoginUserAsync(Request.Form["Login"], Request.Form["Password"]))
                {
                    CurrentUser = logicController.GetCurrentUser();
                }
                else
                    return Json(new { success = false }); 
            }
            catch (Exception ex)
            {
                return Json(new { success = false }); 
            }

            return Json(new { success = true, login = CurrentUser.Login }); 
        }

        public ActionResult RegisterUser()
        {
            EasyTicketLogic.User user = new EasyTicketLogic.User();

            if (Session["User"] != null)
                user = Session["User"] as User;

            return PartialView(user);
        }

        [HttpPost]
        public async Task<ActionResult> RegisterUser(User userx)
        {
            //String value = Request.Form["Name"];
            //if (!ModelState.IsValid)
             //   return PartialView("RegisterUser", userx);
            User user;
            try
            {
                try
                {
                    user = new User(Request.Form["Name"], Request.Form["Lastname"], Request.Form["Login"], Request.Form["Password"], Request.Form["Email"], bool.Parse(Request.Form["IsSignedForNewsletter"]));
                    if (await logicController.RegisterUserAsync(user))
                    {
                        Session["User"] = user;
                        return Json(new { success = true}); 
                    }
                    else
                        return PartialView("RegisterUser", user);

                }
                catch (ValidationException ex)
                {
                    //FillErrors(ex);
                }
                catch (Exception ex)
                {
                    return Json(new { success = false}); 
                }
            }
            catch (ValidationException ex)
            {
                return Json(new { success = false }); 
                //FillErrors(ex);
            }

            return Json(new { success = true }); 
        }

        public ActionResult RegisterPreferences()
        {
            EasyTicketLogic.UserPreferences userPrefs = new EasyTicketLogic.UserPreferences();

            if (Session["UserPreferences"] != null)
                userPrefs = Session["UserPreferences"] as UserPreferences;

            return PartialView(userPrefs);
        }

        [HttpPost]
        public async Task<ActionResult> RegisterPreferences(string data)
        {
            //String value = Request.Form["Name"];
            //if (!ModelState.IsValid)
            //    return PartialView("RegisterPreferences", userPrefs);
            try
            {
                UserPreferences pref = new UserPreferences();
                pref.DepartureAddress = new Address(listaCelow[Request.Form["DepartureAddress.CityName"]]); //(depAddress.Text != "") ? new Address(depAddress.Text) : null;
                pref.DepartureAddress.SkyScannerPlaceId = Request.Form["DepartureAddress.CityName"];
                pref.ArrivalAddress = new Address(listaCelow[Request.Form["DepartureAddress.CityName"]]); //(arrAddress.Text != "") ? new Address(arrAddress.Text) : null;
                pref.ArrivalAddress.SkyScannerPlaceId = Request.Form["DepartureAddress.CityName"];
                pref.NumberOfAdults = int.Parse(Request.Form["NumberOfAdults"]);
                pref.NumberOfChildren = int.Parse(Request.Form["NumberOfChildren"]);
                pref.NumberOfInfants = int.Parse(Request.Form["NumberOfInfants"]);
                pref.PreferDirectedRoutes = bool.Parse(Request.Form["PreferDirectedRoutes"]);
                pref.ComfortClass = "Economy";
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
                pref.ArrivalDate = Convert.ToDateTime(Request.Form["DepartureDate"]);
                pref.DepartureDate = Convert.ToDateTime(Request.Form["ArrivalDate"]);
                await client.RegisterPreferencesAsync(CurrentUser, pref);
            }
            catch
            {
                //MessageBox.Show("All fields must be filled");
                return Json(new { success = false });
            }

            //Session["UserPreferences"] = userPrefs;
            return Json(new { success = true });
        }

        public ActionResult MakeOrder()
        {
            return View();
        }

        [HttpPost]
        public ActionResult MakeOrder(string postData)
        {
            return View();
        }


    }
}
