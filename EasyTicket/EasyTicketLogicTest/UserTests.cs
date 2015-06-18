//using System;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using System.Threading.Tasks;
//using EasyTicketLogic;
//using Moq;
//using System.Data.Entity;
//using System.Data.Entity.Infrastructure;
//using System.Collections.Generic;
//using System.Linq;
//using FluentAssertions;
//using NUnit.Framework;

//namespace EasyTicketLogicTest
//{
//    [TestFixture]
//    public class DbContextTests
//    {
//        private static Mock<IDbSet<T>> CreateMockSet<T>(IQueryable<T> data) where T : class
//        {
//            var mockSet = new Mock<IDbSet<T>>();

//            mockSet.Setup(m => m.Expression).Returns(data.Expression);
//            mockSet.Setup(m => m.ElementType).Returns(data.ElementType);
//            mockSet.Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

//            mockSet.As<IDbAsyncEnumerable<T>>()
//            .Setup(m => m.GetAsyncEnumerator())
//            .Returns(new TestDbAsyncEnumerator<T>(data.GetEnumerator()));

//            mockSet.As<IQueryable<T>>()
//                .Setup(m => m.Provider)
//                .Returns(new TestDbAsyncQueryProvider<T>(data.Provider));

//            return mockSet;
//        }
//       //[Test]
//       // public async Task Can_Get_User_By_Login()
//       // {
//       //     var kowalski = new User();
//       //     var data = new List<User> 
//       //     {
//       //        kowalski
//       //     }.AsQueryable();

//       //     var mockContext = new Mock<EasyTicketContext>();

//       //     var mockSet = CreateMockSet(data);

//       //     mockContext.SetupGet(c => c.Users).Returns(mockSet.Object);

//       //     var user = await mockContext.Object.Users.Where(p => p.Login == "kowalski92").FirstOrDefaultAsync();

//       //     user.Should().BeNull();
            
//       // }
//        //[TestMethod]
//        //public async Task Can_Add_New_User()
//        //{
//        //    var mockSet = new Mock<IDbset<User>>();

//        //    var mockContext = new Mock<EasyTicketContext>();
//        //    mockContext.Setup(m => m.Users).Returns(mockSet.Object);

//        //    var service = mockContext.Object;
//        //    await service.AddNewUserAsync(new User("Jan", "Kowalski", "kowalski92", "tajnehaslo92", "jan.kowalski@gmail.com", true));

//        //    mockSet.Verify(m => m.Add(It.IsAny<User>()), Times.Once());
//        //    mockContext.Verify(m => m.SaveChanges(), Times.Once());
//        //}
//    }
//}
