using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Runtime.Remoting.Contexts;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("WcfService")]
[assembly: InternalsVisibleTo("EasyTicketLogicTest")]
namespace EasyTicketLogic
{
    [Synchronization]
    public class EasyTicketContext : DbContext
    {
        public static EasyTicketContext Instance
        {
            get { return instance; }

        }

        private static EasyTicketContext instance = new EasyTicketContext();

        //cannot be internal
        public virtual IDbSet<User> Users { get; set; }
        public virtual IDbSet<UserPreferences> UserPreferences { get; set; }
        public virtual IDbSet<UserRole> UserRoles { get; set; }
        public virtual IDbSet<Offer> Offers { get; set; }
        public virtual IDbSet<OrderDetails> OrderDetails { get; set; }
        public virtual IDbSet<Address> Addresses { get; set; }
        public virtual IDbSet<Ticket> Tickets { get; set; }
        public virtual IDbSet<GPSCoordinates> GPSCoordinates { get; set; }

        public EasyTicketContext()
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<EasyTicketContext>());

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Configurations.Add(new UserConfiguration());
            modelBuilder.Configurations.Add(new TicketConfiguration());
            modelBuilder.Configurations.Add(new AddressConfiguration());
        }

        internal Dictionary<string, Dictionary<string, string>> Validate(Offer offer)
        {
            using (var dbContext = new EasyTicketContext())
            {
                Dictionary<string, Dictionary<string, string>> errorList = new Dictionary<string, Dictionary<string, string>>();

                dbContext.Offers.Add(offer);
                var validationResults = dbContext.GetValidationErrors();

                return CreateValidationErrorsDictionary(validationResults);
            }
        }

        internal Dictionary<string, Dictionary<string, string>> Validate(User user)
        {
            using (var dbContext = new EasyTicketContext())
            {
                Dictionary<string, Dictionary<string, string>> errorList = new Dictionary<string, Dictionary<string, string>>();

                dbContext.Users.Add(user);
                var validationResults = dbContext.GetValidationErrors();

                return CreateValidationErrorsDictionary(validationResults);
            }
        }

        //internal Dictionary<string, Dictionary<string, string>> Validate(JourneyHistory journeyHistory)
        //{
        //    using (var dbContext = new EasyTicketContext())
        //    {
        //        Dictionary<string, Dictionary<string, string>> errorList = new Dictionary<string, Dictionary<string, string>>();

        //        dbContext.Journeys.Add(journeyHistory);
        //        var validationResults = dbContext.GetValidationErrors();

        //        return CreateValidationErrorsDictionary(validationResults);
        //    }
        //}

        internal Dictionary<string, Dictionary<string, string>> CreateValidationErrorsDictionary(IEnumerable<DbEntityValidationResult> validationResults)
        {
            string className;
            Dictionary<string, Dictionary<string, string>> errorList = new Dictionary<string, Dictionary<string, string>>();

            foreach (var result in validationResults)
            {
                className = result.Entry.Entity.GetType().Name;
                if (!errorList.ContainsKey(className))
                    errorList.Add(className, new Dictionary<string, string>());
                foreach (var error in result.ValidationErrors)
                    if (!errorList[className].ContainsKey(error.PropertyName))
                        errorList[className].Add(error.PropertyName, error.ErrorMessage);
            }
            return errorList;
        }

        public async Task<User> GetUserByLoginAsync(string login)
        {
            return await Users.Where(p => p.Login == login).FirstOrDefaultAsync();
        }

        internal async Task AddNewUserAsync(User user)
        {
            Dictionary<string, Dictionary<string, string>> errorList;
            errorList = Validate(user);
            if (errorList.Count > 0)
                throw new ValidationException(errorList);

            try
            {
                Users.Add(user);
                await SaveChangesAsync();
            }
            catch (DbEntityValidationException ex)
            {
                errorList = CreateValidationErrorsDictionary(ex.EntityValidationErrors);

                throw new ValidationException(errorList);
            }
        }

        internal async Task AddOffer(Offer offer)
        {
            Dictionary<string, Dictionary<string, string>> errorList = Validate(offer);
            if (errorList.Count > 0)
                throw new ValidationException(errorList);

            try
            {
                Users.Attach(offer.Owner);
                Offers.Add(offer);
                await SaveChangesAsync();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                errorList = CreateValidationErrorsDictionary(ex.EntityValidationErrors);

                throw new ValidationException(errorList);
            }
        }

        internal async Task<IEnumerable<Offer>> GetOffers(Address depAddress, Address arrAddress)
        {

            if (depAddress == null && arrAddress == null)
                return await Offers.ToListAsync();
            if (depAddress != null && arrAddress == null)
                return await Offers.Where(p => p.DepartureAddress.CityName == depAddress.CityName).ToListAsync();
            if (depAddress == null && arrAddress != null)
                return await Offers.Where(p => p.ArrivalAddress.CityName == arrAddress.CityName).ToListAsync();
            return await Offers.Where(p => p.DepartureAddress.CityName == depAddress.CityName && p.ArrivalAddress.CityName == arrAddress.CityName).ToListAsync();
        }

        //internal async Task AddJourneyHistory(JourneyHistory journeyHistory)
        //{
        //    Dictionary<string, Dictionary<string, string>> errorList = Validate(journeyHistory);
        //    if (errorList.Count > 0)
        //        throw new ValidationException(errorList);

        //    if (journeyHistory.User != null)
        //        Users.Attach(journeyHistory.User);
        //    Journeys.Add(journeyHistory);
        //    await SaveChangesAsync();
        //}

        internal List<User> GetSignedUsers()
        {
            var query = Users.Include(x => x.Preferences).Include(x => x.Preferences.ArrivalAddress).Include(x => x.Preferences.DepartureAddress);

            return query.ToList<User>();
        }

        internal List<Offer> GetLastOffers(DateTime date)
        {
            try
            {
                var query = from o in instance.Offers
                            where DateTime.Compare(o.LastModificationDate, date) > 0
                            select o;
                return query.ToList<Offer>();
            }
            catch (Exception e)
            {
                throw;
            }

        }

        internal async void AddNewUserPrefs(User user, UserPreferences userPrefs)
        {

            var dbUser = Users
               .Include(x => x.Preferences)
               .Single(c => c.UserId == user.UserId);

            dbUser.UpdatePrerefences(userPrefs);
            Entry(dbUser.Preferences).State = EntityState.Modified;

            await SaveChangesAsync();



        }
    }
}
