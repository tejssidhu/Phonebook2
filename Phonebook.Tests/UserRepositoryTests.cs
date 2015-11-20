using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Phonebook.Data.Repositories;
using Phonebook.Domain.Model;

namespace Phonebook.Tests
{
    [TestClass]
    public class UserRepositoryTests
    {
        [TestMethod]
        public void TestGetAllOnUserRepository()
        {
            //Arrange
            UserRepository userRepository = new UserRepository();

            User userToCreate = new User
                {
                    Id = new Guid("0b21d4b6-eb42-456b-9828-a90cb604bceb"),
                    Password = "7BbfOOoMJCf",
                    Username = "igardner8",
                    PhoneBook = new List<Contact>
                    {
                        new Contact
                        {
                            Title = "Mr",
                            Email = "sperezy@nyu.edu",
                            Forename = "Susan",
                            Surname = "Perez",
                            ContactNumbers = new List<ContactNumber>
                            {
                                new ContactNumber {Description = "Mobile", TelephoneNumber = "86-(719)546-0680"},
                                new ContactNumber {Description = "Mobile2", TelephoneNumber = "387-(833)766-7041"}
                            }
                        },
                        new Contact
                        {
                            Title = "Mr",
                            Email = "lmcdonaldz@dedecms.com",
                            Forename = "Louis",
                            Surname = "Mcdonald",
                            ContactNumbers = new List<ContactNumber>
                            {
                                new ContactNumber {Description = "Mobile2", TelephoneNumber = "62-(113)771-6674"}
                            }
                        }
                    }
                };

            //Act
            Guid id = userRepository.Create(userToCreate);

            //Assert
            Assert.IsNotNull(id);
        }
    }

}
