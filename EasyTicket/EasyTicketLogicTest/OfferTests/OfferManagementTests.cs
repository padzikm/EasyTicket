//using System;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using EasyTicketLogic;
//using Moq;

//namespace EasyTicketLogicTest.OfferTests
//{
//    [TestClass]
//    public class OfferManagementTests
//    {
//        [TestMethod]
//        public void Can_Add_Completed_Offer()
//        {
//            User user = new User();
//            decimal price = 27;
//            Address depAddress = new Address("Warszawa");
//            Address arrAddress = new Address("Gdansk");
//            DateTime depDate = new DateTime();
//            Ticket ticket = new Ticket(price, depAddress, depDate, arrAddress);
//            Offer offer = new Offer(ticket, 1);
//            OfferManagement offerManagement = new OfferManagement();
//            Mock<IOfferRepository> offerRepo = new Mock<IOfferRepository>();
//            offerManagement.OfferRepository = offerRepo.Object;

//            offerManagement.AddOffer(offer, user);

//            offerRepo.Verify(p => p.AddOffer(offer), Times.Exactly(1));
//        }

//        [TestMethod]
//        [ExpectedException(typeof(ArgumentNullException))]
//        public void Cannot_Add_Offer_Without_Owner()
//        {
//            User user = null;
//            decimal price = 27;
//            Address depAddress = new Address("Warszawa");
//            Address arrAddress = new Address("Gdansk");
//            DateTime depDate = new DateTime();
//            Ticket ticket = new Ticket(price, depAddress, depDate, arrAddress);
//            Offer offer = new Offer(ticket, 1);
//            OfferManagement offerManagement = new OfferManagement();
//            Mock<IOfferRepository> offerRepo = new Mock<IOfferRepository>();
//            offerManagement.OfferRepository = offerRepo.Object;

//            offerManagement.AddOffer(offer, user);            
//        }

//        [TestMethod]
//        [ExpectedException(typeof(ArgumentNullException))]
//        public void Cannot_Add_Offer_Without_Offer()
//        {
//            User user = new User();
//            Offer offer = null;
//            OfferManagement offerManagement = new OfferManagement();
//            Mock<IOfferRepository> offerRepo = new Mock<IOfferRepository>();
//            offerManagement.OfferRepository = offerRepo.Object;

//            offerManagement.AddOffer(offer, user);            
//        }
//    }
//}
