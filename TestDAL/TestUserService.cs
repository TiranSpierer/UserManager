//// UserManager/TestDAL/TestUserService.cs
//// Created by Tiran Spierer
//// Created at 05/01/2023
//// Class propose:

//using DAL.Services.ConcreteServices;
//using DAL.Services.Interfaces;
//using DAL.Setup;
//using Domain.Models;
//using Microsoft.EntityFrameworkCore;
//using Moq;

//namespace TestDAL;

//public class TestUserService
//{
//    private UserService              _userService;
//    private Mock<DataBaseContext>    _mockContext;
//    private Mock<IDataService<User>> _mockDataService;

//    [SetUp]
//    public void SetUp()
//    {
//        _mockDataService = new Mock<IDataService<User>>();
//        var mockOptions = new Mock<DbContextOptions<DataBaseContext>>();

//        var context = new DataBaseContext(mockOptions.Object);

//        //_mockContext = new Mock<DataBaseContext>();
//        _userService     = new UserService(context);
//    }

//    [Test]
//    [TestCase("123", "John Doe", "password", true)]
//    [TestCase("456", "John Doe", "password", false)]
//    public async Task TestCreate(string id, string name, string password, bool expectedResult)
//    {
//        //Arrange
//        var expectedUser = new User {Id = "123", Name = name, Password = password};

//        //_mockDataService.Setup(service => service.Create(expectedUser).Result).Returns(false);

//        _mockDataService.Setup(service => service.GetById(expectedUser).Result).Returns(expectedUser);


//        //Act
//        var b = await _userService.Create(expectedUser);

//        //Assert
//    }

//    [Test]
//    public async Task TestCreate_ReturnsTrue_WithValidEntity()
//    {
//        // Arrange
//        var mockSet = new Mock<DbSet<User>>();
//        _mockContext.Setup(context => context.Set<User>()).Returns(mockSet.Object);
        
//        var user = new User
//                   {
//                       Id       = "123",
//                       Name     = "John",
//                       Password = "password"
//                   };

//        // Act
//        var result = await _userService.Create(user);

//        // Assert
//        Assert.IsTrue(result);
//    }

//}

////const string userId = "123";
////var expectedUser = new User
////                   {

////                       Id       = userId,
////                       Name     = "John Doe",
////                       Password = "password",
////                       UserPrivileges = new List<UserPrivilege>()
////                                        {
////                                            new ()
////                                            {
////                                                UserId    = userId,
////                                                Privilege = Privilege.AddUsers
////                                            }
////                                        }
////                   };