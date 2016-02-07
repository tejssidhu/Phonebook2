using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Phonebook.Domain.Exceptions;
using Phonebook.Domain.Interfaces.Repositories;
using Phonebook.Domain.Model;
using Phonebook.Domain.Services;

namespace Phonebook.Tests
{
    [TestClass]
    public class UserServiceTests
    {

        #region Test Initialise and Cleanup

        private List<User> _users;
        private User _user;

        [TestInitialize]
        public void TestUserServiceTests()
        {
            _user = new User { Id = new Guid("26e31dde-4bcb-47d4-be80-958676c5cafd"), Password = "789", Username = "User789" };

            var user1 = new User { Id = new Guid("7b8ceac1-9fb1-4e15-af4b-890b1f0c3ebf"), Password = "123", Username = "User123" };
            var user2 = new User { Id = new Guid("5875412f-e8b8-493e-bd58-5df35083342c"), Password = "456", Username = "User456" };
            var user4 = new User { Id = new Guid("cef70a7a-3349-4368-85ed-66b8c274fad1"), Password = "p6NY0hg", Username = "mjenkins0" };
            var user5 = new User { Id = new Guid("71d8e924-7c58-4424-9e1b-b14eefa76abc"), Password = "5w7JhI42GLC", Username = "amartin1" };
            var user6 = new User { Id = new Guid("a051d1ca-a3c5-45d4-be60-5bc5256ce83e"), Password = "3NypkQZSe", Username = "vallen2" };
            var user7 = new User { Id = new Guid("2b3b4d72-1c15-40e0-a05a-012b724950c3"), Password = "MsNDnRy1", Username = "mblack3" };
            var user8 = new User { Id = new Guid("2550f510-e5c9-45a4-90a0-c286e4bcd948"), Password = "8dpEdKRn", Username = "schapman4" };
            var user9 = new User { Id = new Guid("874c0bc3-6d9b-4dfa-b42c-8403fe1b281d"), Password = "7s7G9nai", Username = "gdiaz5" };
            var user10 = new User { Id = new Guid("16c6e264-0091-45f6-b9fd-02716d8d62dd"), Password = "3h7Vnh9rUpCl", Username = "cwheeler6" };
            var user11 = new User { Id = new Guid("0d1a6711-e9eb-418e-adda-47a62a7900c9"), Password = "g8KhtQpk", Username = "bparker7" };

            _users = new List<User> { user1, user2, _user, user4, user5, user6, user7, user8, user9, user10, user11 };
        }

        #endregion

        [TestMethod]
        public void GetAllOnUserService()
        {
            //arrange
            var mockUserRepository = new Mock<IUserRepository>();

            mockUserRepository.Setup(x => x.GetAll()).Returns(_users);

            UserService userService = new UserService(mockUserRepository.Object);

            //act
            List<User> retUsers = userService.GetAll().ToList();

            //assert
            CollectionAssert.AreEqual(_users, retUsers);

            userService.Dispose();
        }

        [TestMethod]
        public void GetOnUserService()
        {
            //arrange
            var mockUserRepository = new Mock<IUserRepository>();
            Guid id = _user.Id;

            mockUserRepository.Setup(x => x.Get(id)).Returns(_user);

            UserService userService = new UserService(mockUserRepository.Object);

            //act
            User retUser = userService.Get(id);

            //assert
            Assert.AreEqual(_user, retUser);

            userService.Dispose();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidPasswordException))]
        public void AuthenticateWithInvalidPasswordOnUserService()
        {
            //arrange
            var mockUserRepository = new Mock<IUserRepository>();

            mockUserRepository.Setup(x => x.GetAll()).Returns(_users);

            UserService userService = new UserService(mockUserRepository.Object);

            //act
            User retUser = userService.Authenticate(_user.Username, _user.Password + "WRONG");

            //assert - expect exception

            userService.Dispose();
        }

        [TestMethod]
        public void AuthenticateValidPasswordOnUserService()
        {
            //arrange
            var mockUserRepository = new Mock<IUserRepository>();

            mockUserRepository.Setup(x => x.GetAll()).Returns(_users);

            UserService userService = new UserService(mockUserRepository.Object);

            //act
            User retUser = userService.Authenticate(_user.Username, _user.Password);

            //assert
            Assert.AreEqual(_user, retUser);

            userService.Dispose();
        }

        [TestMethod]
        [ExpectedException(typeof(ObjectNotFoundException))]
        public void AuthenticateWithNoExistentUserOnUserService()
        {
            //arrange
            var mockUserRepository = new Mock<IUserRepository>();

            mockUserRepository.Setup(x => x.GetAll()).Returns(_users);

            UserService userService = new UserService(mockUserRepository.Object);

            //act
            User retUser = userService.Authenticate(_user.Username + "DOESNTEXIST", _user.Password);

            //assert - expect exception

            userService.Dispose();
        }

        [TestMethod]
        [ExpectedException(typeof(ObjectAlreadyExistException))]
        public void CreateWithExistingUserOnUserService()
        {
            //arrange
            var mockUserRepository = new Mock<IUserRepository>();

            mockUserRepository.Setup(x => x.GetAll()).Returns(_users);

            UserService userService = new UserService(mockUserRepository.Object);

            //act
            Guid id = userService.Create(_user);

            //assert - expect exception

            userService.Dispose();
        }

        [TestMethod]
        public void CreateOnUserService()
        {
            //arrange
            var mockUserRepository = new Mock<IUserRepository>();
            User userToCreate = new User
                {
                    Id = new Guid("0b21d4b6-eb42-456b-9828-a90cb604bceb"),
                    Password = "7BbfOOoMJCf",
                    Username = "igardner8"
                };

            mockUserRepository.Setup(x => x.GetAll()).Returns(_users);

            UserService userService = new UserService(mockUserRepository.Object);

            //act
            Guid id = userService.Create(userToCreate);

            //assert
            mockUserRepository.Verify(y => y.Create(It.IsAny<User>()));
            Assert.IsNotNull(id);

            userService.Dispose();
        }

        [TestMethod]
        [ExpectedException(typeof(ObjectAlreadyExistException))]
        public void UpdateToExistingUsernameOnUserService()
        {
            //arrange
            var mockUserRepository = new Mock<IUserRepository>();

            mockUserRepository.Setup(x => x.GetAll()).Returns(_users);

            UserService userService = new UserService(mockUserRepository.Object);

            //set username to that of another user
            _user.Username = _users[0].Username;

            //act
            userService.Update(_user);

            //assert - expected exception

            userService.Dispose();            
        }

        [TestMethod]
        public void UpdateOnUserService()
        {
            //arrange
            var mockUserRepository = new Mock<IUserRepository>();

            mockUserRepository.Setup(x => x.GetAll()).Returns(_users);

            UserService userService = new UserService(mockUserRepository.Object);

            //set username to that of another user
            _user.Username = _user.Username + "WITHUPDATE";

            //act
            userService.Update(_user);

            //assert - expected exception
            mockUserRepository.Verify(y => y.Update(It.IsAny<User>()));

            userService.Dispose();
        }

        [TestMethod]
        public void DeleteOnUserService()
        {
            //arrange
            var mockUserRepository = new Mock<IUserRepository>();

            mockUserRepository.Setup(x => x.GetAll()).Returns(_users);

            UserService userService = new UserService(mockUserRepository.Object);

            //act
            userService.Delete(_user.Id);

            //assert - expected exception
            mockUserRepository.Verify(y => y.Delete(It.IsAny<Guid>()));

            userService.Dispose();
        }

    }
}
