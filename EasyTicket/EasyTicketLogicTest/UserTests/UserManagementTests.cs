//using System;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using EasyTicketLogic;
//using Moq;
//using System.Threading.Tasks;
//using System.Threading;

//namespace EasyTicketLogicTest.UserTests
//{
//    [TestClass]
//    public class UserManagementTests
//    {
//        [TestMethod]
//        [ExpectedException(typeof(NullReferenceException))]
//        public async Task Cannot_Log_In_Non_Existing_User()
//        {
//            User user = new User("nieistniejacygosc92", "", "", "bardzozlehaslo92", "");

//            Mock<IUserRepository> userRepo = new Mock<IUserRepository>(); 

//            userRepo.Setup(p => p.GetUserByLoginAsync(user.Login))
//                    .Callback(() => Thread.Sleep(1000))
//                    .Returns<Task<User>>(null)
//                    .Verifiable();  
            
//            UserManagement userManagment = new UserManagement();
//            userManagment.UserRepository = userRepo.Object;

//            await userManagment.LoginUserAsync(user.Login, user.Password);
//            userRepo.VerifyAll();
//        }

//        [TestMethod]
//        async public Task Can_Login_Registered_User()
//        {
//            User user = new User("Jan", "Kowalski", "jestemzarejestorwany92", "bardzodobrehaslo92", "jan.kowalski@gmail.com");

//            Mock<IUserRepository> userRepo = new Mock<IUserRepository>();

//            userRepo.Setup(p => p.GetUserByLoginAsync(user.Login))
//                    .Callback(() => Thread.Sleep(1000))
//                    .Returns(Task.FromResult(user))
//                    .Verifiable();

//            UserManagement userManagment = new UserManagement();
//            userManagment.UserRepository = userRepo.Object;

//            await userManagment.LoginUserAsync(user.Login, user.Password);
//            userRepo.VerifyAll();
//        }

//        [TestMethod]
//        [ExpectedException(typeof(NullReferenceException))]
//        public async Task Cannot_Register_Empty_User()
//        {
//            User user = new User();

//            var userRepo = new Mock<IUserRepository>();

//            userRepo.Setup(p => p.AddNewUserAsync(user))
//                    .Callback(() => Thread.Sleep(3000))
//                    .Returns<Task<User>>(null)
//                    .Verifiable();

//            userRepo.Setup(p => p.AddNewUserAsync(user))
//                    .Returns<Task>(null)
//                    .Verifiable();

//            var userMgm = new UserManagement();
//            userMgm.UserRepository = userRepo.Object;
//            await userMgm.RegisterUserAsync(user);

//            userRepo.VerifyAll();
//        }

//        [TestMethod]
//        public async Task Can_Add_Full_User()
//        {
//            User user = new User("Jan", "Kowalski", "kowalski123", "bardzotajne92", "jankowalski@wp.com");

//            var userRepo = new Mock<IUserRepository>();

//            userRepo.Setup(p => p.GetUserByLoginAsync(user.Login))
//                    .Returns(Task.FromResult(user))
//                    .Verifiable();

//            var userMgm = new UserManagement();
//            userMgm.UserRepository = userRepo.Object;

//            await userMgm.RegisterUserAsync(user);

//            userRepo.VerifyAll();
//        }


//    }
//}
