//// UserManager/TestDAL/TestUserServiceTemp.cs
//// Created by Tiran Spierer
//// Created at 05/01/2023
//// Class propose:
//using System.Threading.Tasks;
//using DAL.Services.ConcreteServices;
//using DAL.Services.Interfaces;
//using DAL.Setup;
//using Domain.Models;
//using Microsoft.EntityFrameworkCore;
//using Moq;
//using NUnit.Framework;

//namespace TestDAL;

//public class TestUserServiceTemp
//{
//    public class TestUserService
//    {
//        private UserService              _userService;
//        private Mock<DataBaseContext>    _mockContext;
//        private Mock<IDataService<User>> _mockDataService;

//        [SetUp]
//        public void SetUp()
//        {
//            // Create a mock object for the DbSet
//            var mockSet = new Mock<DbSet<User>>();

//            // Create a mock object for the DataBaseContext and set up the DbSet property
//            _mockContext = new Mock<DataBaseContext>();
//            _mockContext.Setup(context => context.Set<User>()).Returns(mockSet.Object);

//            // Create an instance of the UserService class
//            _userService = new UserService(_mockContext.Object);
//        }

//        [Test]
//        public async Task TestCreate_ReturnsTrue_WithValidEntity()
//        {
//            // Arrange
//            var user = new User
//                       {
//                           Id       = "123",
//                           Name     = "John",
//                           Password = "password"
//                       };

//            // Act
//            var result = await _userService.Create(user);

//            // Assert
//            Assert.IsTrue(result);
//        }

//        [Test]
//        public async Task TestCreate_ReturnsFalse_WithInvalidEntity()
//        {
//            // Arrange
//            // Set up the mock object to throw an exception when SaveChangesAsync is called
//            _mockContext.Setup(context => context.SaveChangesAsync(default)).Throws(new Exception());

//            var user = new User
//                       {
//                           Id       = "123",
//                           Name     = "John",
//                           Password = "password"
//                       };

//            // Act
//            var result = await _userService.Create(user);

//            // Assert
//            Assert.IsFalse(result);
//        }
//    }
//}