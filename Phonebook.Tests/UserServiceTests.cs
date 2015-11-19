using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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
            _user = new User
            {
                Id = Guid.NewGuid(),
                Password = "789",
                Username = "User789",
                PhoneBook = new List<Contact>
                {
                    new Contact
                    {
                        Title = "Dr",
                        Email = "ForenameC.SurnameC@Testsite.com",
                        Forename = "ForenameC",
                        Surname = "SurnameC",
                        ContactNumbers = new List<ContactNumber>
                        {
                            new ContactNumber {Description = "Home", TelephoneNumber = "01789123456"},
                            new ContactNumber {Description = "Mobile", TelephoneNumber = "07894567890"},
                            new ContactNumber {Description = "Work", TelephoneNumber = "02071234565"}
                        }
                    },
                    new Contact
                    {
                        Title = "Mrs",
                        Email = "ForenameD.SurnameD@Testsite.com",
                        Forename = "ForenameD",
                        Surname = "SurnameD",
                        ContactNumbers = new List<ContactNumber>
                        {
                            new ContactNumber {Description = "Home", TelephoneNumber = "01456856985"},
                            new ContactNumber {Description = "Mobile", TelephoneNumber = "07191213456"}
                        }
                    }
                }
            };



            _users = new List<User>
            {
                new User
                {
                    Id = Guid.NewGuid(),
                    Password = "123",
                    Username = "User123",
                    PhoneBook = new List<Contact>
                    {
                        new Contact
                        {
                            Title = "Mr",
                            Email = "User123@Testsite.com",
                            Forename = "User",
                            Surname = "123",
                            ContactNumbers = new List<ContactNumber>
                            {
                                new ContactNumber {Description = "Home", TelephoneNumber = "01234567890"},
                                new ContactNumber {Description = "Mobile", TelephoneNumber = "07234567890"}
                            }
                        }
                    }
                },

                new User {Id = Guid.NewGuid(), Password = "456", Username = "User456"},

                _user,

                new User
                {
                    Id = new Guid("cef70a7a-3349-4368-85ed-66b8c274fad1"),
                    Password = "p6NY0hg",
                    Username = "mjenkins0",
                    PhoneBook = new List<Contact>
                    {
                        new Contact
                        {
                            Title = "Dr",
                            Email = "treyes0@goo.gl",
                            Forename = "Theresa",
                            Surname = "Reyes",
                            ContactNumbers = new List<ContactNumber>
                            {
                                new ContactNumber {Description = "Home", TelephoneNumber = "223-(114)194-5298"},
                                new ContactNumber {Description = "Work", TelephoneNumber = "51-(814)657-3017"},
                                new ContactNumber {Description = "Other", TelephoneNumber = "82-(930)566-8463"},
                                new ContactNumber {Description = "Mobile", TelephoneNumber = "86-(747)144-0051"}
                            }
                        },
                        new Contact
                        {
                            Title = "Honorable",
                            Email = "pwagner1@ed.gov",
                            Forename = "Pamela",
                            Surname = "Wagner",
                            ContactNumbers = new List<ContactNumber>
                            {
                                new ContactNumber {Description = "Home", TelephoneNumber = "63-(682)494-7599"},
                                new ContactNumber {Description = "Mobile", TelephoneNumber = "63-(803)354-8303"},
                                new ContactNumber {Description = "Mobile2", TelephoneNumber = "86-(308)286-4513"},
                                new ContactNumber {Description = "Work2", TelephoneNumber = "975-(830)275-5384"}
                            }
                        },
                        new Contact {
                            Title = "Mrs", 
                            Email = "stucker2@tuttocitta.it", 
                            Forename = "Steve", 
                            Surname = "Tucker", 
                            ContactNumbers = new List<ContactNumber> 
                            {
                                new ContactNumber { Description = "Mobile", TelephoneNumber = "265-(687)741-1095"},
                                new ContactNumber { Description = "Home", TelephoneNumber = "48-(665)626-5285"},
                                new ContactNumber { Description = "Home2", TelephoneNumber = "48-(614)442-9642"}
                            }
                        }   
                    }
                },

                new User {
                    Id = new Guid("71d8e924-7c58-4424-9e1b-b14eefa76abc"), 
                    Password = "5w7JhI42GLC", 
                    Username = "amartin1", 
                    PhoneBook = new List<Contact> {
                        new Contact {
                            Title = "Honorable", 
                            Email = "sbaker3@noaa.gov", 
                            Forename = "Sean", 
                            Surname = "Baker", 
                            ContactNumbers = new List<ContactNumber> 
                            {
                                new ContactNumber { Description = "Mobile", TelephoneNumber = "57-(125)288-1758"},
                                new ContactNumber { Description = "Other", TelephoneNumber = "7-(139)817-5652"}
                            }
                            },
                        new Contact {
                            Title = "Mrs", 
                            Email = "mtucker4@yale.edu", 
                            Forename = "Melissa", 
                            Surname = "Tucker", 
                            ContactNumbers = new List<ContactNumber> 
                            {
                                new ContactNumber { Description = "Other", TelephoneNumber = "52-(670)627-2609"},
                                new ContactNumber { Description = "Home", TelephoneNumber = "63-(657)857-7791"}
                            }
                            },
                        new Contact {
                            Title = "Honorable", 
                            Email = "sgraham5@bbc.co.uk", 
                            Forename = "Shirley", 
                            Surname = "Graham", 
                            ContactNumbers = new List<ContactNumber> 
                            {
                                new ContactNumber { Description = "Other", TelephoneNumber = "86-(774)243-0204"}
                            }
                        },
                        new Contact {
                            Title = "Rev", 
                            Email = "ahunt6@narod.ru", 
                            Forename = "Ashley", 
                            Surname = "Hunt", 
                            ContactNumbers = new List<ContactNumber> 
                            {
                                new ContactNumber { Description = "Other", TelephoneNumber = "48-(710)469-4667"},
                                new ContactNumber { Description = "Mobile2", TelephoneNumber = "62-(534)338-5255"}
                            }
                        }
                    }
                },
                new User {
                    Id = new Guid("a051d1ca-a3c5-45d4-be60-5bc5256ce83e"), 
                    Password = "3NypkQZSe", 
                    Username = "vallen2", 
                    PhoneBook = new List<Contact> 
                    {
                        new Contact {
                            Title = "Rev", 
                            Email = "wriley7@dagondesign.com", 
                            Forename = "Walter", 
                            Surname = "Riley", 
                            ContactNumbers = new List<ContactNumber> 
                            {
                                new ContactNumber { Description = "Mobile2", TelephoneNumber = "46-(325)136-3955"},
                                new ContactNumber { Description = "Work", TelephoneNumber = "94-(910)407-0813"}
                            }
                        }
                    }
                },
                new User {
                    Id = new Guid("2b3b4d72-1c15-40e0-a05a-012b724950c3"), 
                    Password = "MsNDnRy1", 
                    Username = "mblack3", 
                    PhoneBook = new List<Contact> 
                    {
                        new Contact {
                            Title = "Rev", 
                            Email = "jdunn8@ycombinator.com", 
                            Forename = "Jessica", 
                            Surname = "Dunn", 
                            ContactNumbers = new List<ContactNumber> 
                            {
                                new ContactNumber { Description = "Work", TelephoneNumber = "62-(245)962-1428"},
                                new ContactNumber { Description = "Home", TelephoneNumber = "7-(751)565-1842"},
                                new ContactNumber { Description = "Other", TelephoneNumber = "62-(734)980-1000"}
                            }
                        },
                    
                        new Contact {
                            Title = "Honorable", 
                            Email = "sgrant9@liveinternet.ru", 
                            Forename = "Shirley", 
                            Surname = "Grant", 
                            ContactNumbers = new List<ContactNumber> 
                            {
                                new ContactNumber { Description = "Home2", TelephoneNumber = "33-(289)301-0876"},
                                new ContactNumber { Description = "Mobile", TelephoneNumber = "968-(454)704-5622"}
                            }
                        },

                        new Contact {
                            Title = "Rev", 
                            Email = "nwilsona@zimbio.com", 
                            Forename = "Norma", 
                            Surname = "Wilson", 
                            ContactNumbers = new List<ContactNumber> 
                            {
                                new ContactNumber { Description = "Other", TelephoneNumber = "1-(378)311-0349"},
                                new ContactNumber { Description = "Work", TelephoneNumber = "86-(581)151-8667"},
                                new ContactNumber { Description = "Mobile", TelephoneNumber = "86-(287)749-7955"}
                            }
                        },

                        new Contact {
                            Title = "Mrs", 
                            Email = "hrobertsonb@vistaprint.com", 
                            Forename = "Helen", 
                            Surname = "Robertson", 
                            ContactNumbers = new List<ContactNumber> 
                            {
                                new ContactNumber { Description = "Mobile", TelephoneNumber = "55-(898)392-2144"},
                                new ContactNumber { Description = "Work", TelephoneNumber = "690-(350)327-5620"}
                            }
                        },
                        new Contact {
                            Title = "Rev", 
                            Email = "erussellc@about.com", 
                            Forename = "Edward", 
                            Surname = "Russell", 
                            ContactNumbers = new List<ContactNumber> 
                            {
                                new ContactNumber { Description = "Mobile", TelephoneNumber = "62-(563)310-0428"}
                            }
                        },
                        new Contact {
                            Title = "Mrs", 
                            Email = "jlongd@t.co", 
                            Forename = "Janet", 
                            Surname = "Long", 
                            ContactNumbers = new List<ContactNumber> 
                            {
                                new ContactNumber { Description = "Mobile", TelephoneNumber = "7-(842)169-2249"},
                                new ContactNumber { Description = "Mobile2", TelephoneNumber = "30-(259)939-6668"},
                                new ContactNumber { Description = "Home", TelephoneNumber = "380-(545)404-6597"}
                            }
                        }
                    }
                },

                new User {
                    Id = new Guid("2550f510-e5c9-45a4-90a0-c286e4bcd948"), 
                    Password = "8dpEdKRn", 
                    Username = "schapman4", 
                    PhoneBook = new List<Contact> 
                    {
                        new Contact {
                            Title = "Mr", 
                            Email = "bpaynee@mapquest.com", 
                            Forename = "Betty", 
                            Surname = "Payne", 
                            ContactNumbers = new List<ContactNumber> 
                            {
                                new ContactNumber { Description = "Mobile2", TelephoneNumber = "31-(660)756-2250"},
                                new ContactNumber { Description = "Home", TelephoneNumber = "33-(463)506-4425"}
                            }
                        },
                        new Contact {
                            Title = "Ms", 
                            Email = "mgreenef@harvard.edu", 
                            Forename = "Martha", 
                            Surname = "Greene", 
                            ContactNumbers = new List<ContactNumber> 
                            {
                                new ContactNumber { Description = "Home", TelephoneNumber = "63-(354)249-0763"},
                                new ContactNumber { Description = "Mobile", TelephoneNumber = "261-(682)156-0600"},
                                new ContactNumber { Description = "Home2", TelephoneNumber = "351-(465)251-8330"},
                                new ContactNumber { Description = "Work2", TelephoneNumber = "86-(739)256-9594"},
                                new ContactNumber { Description = "Mobile2", TelephoneNumber = "7-(751)319-6218"},
                                new ContactNumber { Description = "Other", TelephoneNumber = "62-(345)673-4441"}
                            }
                        },
                        new Contact {
                            Title = "Honorable", 
                            Email = "acampbellg@intel.com", 
                            Forename = "Anthony", 
                            Surname = "Campbell", 
                            ContactNumbers = new List<ContactNumber> 
                            {
                                new ContactNumber { Description = "Work", TelephoneNumber = "53-(604)112-0424"},
                                new ContactNumber { Description = "Other", TelephoneNumber = "86-(656)425-9353"}
                            }
                        },
                        new Contact {
                            Title = "Rev", 
                            Email = "jhunterh@yolasite.com", 
                            Forename = "Jean", 
                            Surname = "Hunter", 
                            ContactNumbers = new List<ContactNumber> 
                            {
                                new ContactNumber { Description = "Work", TelephoneNumber = "1-(198)210-8272"},
                                new ContactNumber { Description = "Work2", TelephoneNumber = "46-(903)828-2457"},
                                new ContactNumber { Description = "Other", TelephoneNumber = "258-(508)395-2565"},
                                new ContactNumber { Description = "Home", TelephoneNumber = "63-(341)144-3466"}
                            }
                        },
                        new Contact {
                            Title = "Dr", 
                            Email = "jgonzalezi@cpanel.net", 
                            Forename = "Joyce", 
                            Surname = "Gonzalez", 
                            ContactNumbers = new List<ContactNumber> 
                            {
                                new ContactNumber { Description = "Work2", TelephoneNumber = "62-(995)365-2156"}
                            }
                        },
                        new Contact {
                            Title = "Ms", 
                            Email = "rricej@tumblr.com", 
                            Forename = "Randy", 
                            Surname = "Rice", 
                            ContactNumbers = new List<ContactNumber> 
                            {
                                new ContactNumber { Description = "Work2", TelephoneNumber = "1-(411)149-6535"},
                                new ContactNumber { Description = "Home", TelephoneNumber = "7-(881)814-8584"},
                                new ContactNumber { Description = "Mobile2", TelephoneNumber = "86-(734)901-7131"}
                            }
                        },
                        new Contact {
                            Title = "Mr", 
                            Email = "mhendersonk@clickbank.net", 
                            Forename = "Martin", 
                            Surname = "Henderson", 
                            ContactNumbers = new List<ContactNumber> 
                            {
                                new ContactNumber { Description = "Work2", TelephoneNumber = "57-(404)565-5252"},
                                new ContactNumber { Description = "Mobile2", TelephoneNumber = "54-(762)869-2805"},
                                new ContactNumber { Description = "Other", TelephoneNumber = "46-(167)234-0525"},
                                new ContactNumber { Description = "Mobile", TelephoneNumber = "63-(646)676-2472"},
                                new ContactNumber { Description = "Work", TelephoneNumber = "81-(489)457-1125"}
                            }
                        },
                        new Contact {
                            Title = "Ms", 
                            Email = "pfosterl@ebay.com", 
                            Forename = "Paul", 
                            Surname = "Foster", 
                            ContactNumbers = new List<ContactNumber> 
                            {
                                new ContactNumber { Description = "Work2", TelephoneNumber = "84-(541)806-4332"},
                                new ContactNumber { Description = "Home", TelephoneNumber = "86-(811)526-6501"}
                            }
                        },
                        new Contact {
                            Title = "Mr", 
                            Email = "mharperm@cnn.com", 
                            Forename = "Margaret", 
                            Surname = "Harper", 
                            ContactNumbers = new List<ContactNumber> 
                            {
                                new ContactNumber { Description = "Work2", TelephoneNumber = "66-(938)148-7912"},
                                new ContactNumber { Description = "Home2", TelephoneNumber = "63-(579)175-9055"},
                                new ContactNumber { Description = "Mobile2", TelephoneNumber = "63-(343)386-6303"}
                            }
                        },
                        new Contact {
                            Title = "Dr", 
                            Email = "jmedinan@disqus.com", 
                            Forename = "Joshua", 
                            Surname = "Medina", 
                            ContactNumbers = new List<ContactNumber> 
                            {
                                new ContactNumber { Description = "Home2", TelephoneNumber = "371-(735)800-6642"},
                                new ContactNumber { Description = "Mobile", TelephoneNumber = "261-(663)663-8303"}
                            }
                        }
                    }
                },

                new User {
                    Id = new Guid("874c0bc3-6d9b-4dfa-b42c-8403fe1b281d"), 
                    Password = "7s7G9nai", 
                    Username = "gdiaz5", 
                    PhoneBook = new List<Contact> 
                    {
                        new Contact {
                            Title = "Dr", 
                            Email = "hlopezo@wsj.com", 
                            Forename = "Howard", 
                            Surname = "Lopez", 
                            ContactNumbers = new List<ContactNumber> 
                            {
                                new ContactNumber { Description = "Home2", TelephoneNumber = "46-(687)459-0160"},
                                new ContactNumber { Description = "Other", TelephoneNumber = "358-(642)255-7286"}
                            }
                        },
                        new Contact {
                            Title = "Dr", 
                            Email = "kwhitep@nps.gov", 
                            Forename = "Kenneth", 
                            Surname = "White", 
                            ContactNumbers = new List<ContactNumber> 
                            {
                                new ContactNumber { Description = "Other", TelephoneNumber = "1-(729)832-6967"},
                                new ContactNumber { Description = "Mobile2", TelephoneNumber = "385-(860)482-1867"},
                                new ContactNumber { Description = "Work2", TelephoneNumber = "234-(663)810-9887"},
                                new ContactNumber { Description = "Mobile", TelephoneNumber = "55-(743)282-5907"}
                            }
                        },
                        new Contact {
                            Title = "Dr", 
                            Email = "jlewisq@cbslocal.com", 
                            Forename = "Jeremy", 
                            Surname = "Lewis", 
                            ContactNumbers = new List<ContactNumber> 
                            {
                                new ContactNumber { Description = "Work2", TelephoneNumber = "46-(358)893-6975"},
                                new ContactNumber { Description = "Home", TelephoneNumber = "258-(211)363-7457"}
                            }
                        }
                    }
                },

                new User {
                    Id = new Guid("16c6e264-0091-45f6-b9fd-02716d8d62dd"), 
                    Password = "3h7Vnh9rUpCl", 
                    Username = "cwheeler6", 
                    PhoneBook = new List<Contact> 
                    {
                        new Contact {
                            Title = "Rev", 
                            Email = "jbennettr@dmoz.org", 
                            Forename = "Jane", 
                            Surname = "Bennett", 
                            ContactNumbers = new List<ContactNumber> 
                            {
                                new ContactNumber { Description = "Home", TelephoneNumber = "265-(744)578-6260"},
                                new ContactNumber { Description = "Mobile", TelephoneNumber = "81-(798)694-9013"},
                                new ContactNumber { Description = "Other", TelephoneNumber = "33-(701)793-9111"},
                                new ContactNumber { Description = "Work", TelephoneNumber = "86-(252)205-7575"}
                            }
                        },
                        new Contact {
                            Title = "Ms", 
                            Email = "rrichardss@upenn.edu", 
                            Forename = "Robin", 
                            Surname = "Richards", 
                            ContactNumbers = new List<ContactNumber> 
                            {
                                new ContactNumber { Description = "Mobile", TelephoneNumber = "62-(686)313-3595"},
                                new ContactNumber { Description = "Mobile2", TelephoneNumber = "27-(226)985-5369"}
                            }
                        },
                        new Contact {
                            Title = "Ms", 
                            Email = "fthompsont@pen.io", 
                            Forename = "Frances", 
                            Surname = "Thompson", 
                            ContactNumbers = new List<ContactNumber> 
                            {
                                new ContactNumber { Description = "Home", TelephoneNumber = "967-(222)511-2827"},
                                new ContactNumber { Description = "Home2", TelephoneNumber = "996-(677)154-8701"},
                                new ContactNumber { Description = "Mobile2", TelephoneNumber = "62-(227)427-6989"},
                                new ContactNumber { Description = "Other", TelephoneNumber = "63-(761)970-5238"},
                                new ContactNumber { Description = "Work2", TelephoneNumber = "1-(714)922-2248"}
                            }
                        },
                        new Contact {
                            Title = "Honorable", 
                            Email = "mmyersu@wired.com", 
                            Forename = "Michael", 
                            Surname = "Myers", 
                            ContactNumbers = new List<ContactNumber> 
                            {
                                new ContactNumber { Description = "Home2", TelephoneNumber = "976-(693)690-0541"},
                                new ContactNumber { Description = "Mobile", TelephoneNumber = "53-(622)239-3459"},
                                new ContactNumber { Description = "Work2", TelephoneNumber = "351-(783)239-2490"}
                            }
                        }
                    }
                },
                new User {
                    Id = new Guid("0d1a6711-e9eb-418e-adda-47a62a7900c9"), 
                    Password = "g8KhtQpk", 
                    Username = "bparker7", 
                    PhoneBook = new List<Contact> 
                    {
                        new Contact {
                            Title = "Rev", 
                            Email = "smorrisv@arizona.edu", 
                            Forename = "Stephen", 
                            Surname = "Morris", 
                            ContactNumbers = new List<ContactNumber> 
                            {
                                new ContactNumber { Description = "Mobile", TelephoneNumber = "62-(504)502-7437"},
                                new ContactNumber { Description = "Home", TelephoneNumber = "886-(206)925-2299"},
                                new ContactNumber { Description = "Work", TelephoneNumber = "52-(361)173-7820"},
                                new ContactNumber { Description = "Home2", TelephoneNumber = "7-(579)225-0318"},
                                new ContactNumber { Description = "Work2", TelephoneNumber = "373-(412)567-5419"}
                            }
                        },
                        new Contact {
                            Title = "Mr", 
                            Email = "kmartinezw@weebly.com", 
                            Forename = "Kathryn", 
                            Surname = "Martinez", 
                            ContactNumbers = new List<ContactNumber> 
                            {
                                new ContactNumber { Description = "Home2", TelephoneNumber = "352-(819)901-6407"},
                                new ContactNumber { Description = "Mobile", TelephoneNumber = "33-(610)808-0139"},
                                new ContactNumber { Description = "Other", TelephoneNumber = "1-(312)651-1344"}
                            }
                        },
                        new Contact {
                            Title = "Mr", 
                            Email = "wryanx@flickr.com", 
                            Forename = "Walter", 
                            Surname = "Ryan", 
                            ContactNumbers = new List<ContactNumber> 
                            {
                                new ContactNumber { Description = "Home2", TelephoneNumber = "86-(441)551-8335"},
                                new ContactNumber { Description = "Work2", TelephoneNumber = "964-(638)916-8780"},
                                new ContactNumber { Description = "Mobile", TelephoneNumber = "48-(311)854-6190"}
                            }
                        }
                    }
                }
            };
        }

        #endregion

        [TestMethod]
        public void GetAllOnUserServices()
        {
            //arrange
            var mockUserRepository = new Mock<IRepository<User>>();

            mockUserRepository.Setup(x => x.GetAll()).Returns(_users);

            UserService userService = new UserService(mockUserRepository.Object);

            //act
            List<User> retUsers = userService.GetAll().ToList();

            //assert
            CollectionAssert.AreEqual(_users, retUsers);
        }

        [TestMethod]
        public void GetOnUserServices()
        {
            //arrange
            var mockUserRepository = new Mock<IRepository<User>>();
            Guid id = _user.Id;

            mockUserRepository.Setup(x => x.Get(id)).Returns(_user);

            UserService userService = new UserService(mockUserRepository.Object);

            //act
            User retUser = userService.Get(id);

            //assert
            Assert.AreEqual(_user, retUser);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidPasswordException))]
        public void AuthenticateWithInvalidPasswordUserServices()
        {
            //arrange
            var mockUserRepository = new Mock<IRepository<User>>();

            mockUserRepository.Setup(x => x.GetAll()).Returns(_users);

            UserService userService = new UserService(mockUserRepository.Object);

            //act
            User retUser = userService.Authenticate(_user.Username, _user.Password + "WRONG");

            //assert - expect exception
        }

        [TestMethod]
        public void AuthenticateValidPasswordUserServices()
        {
            //arrange
            var mockUserRepository = new Mock<IRepository<User>>();

            mockUserRepository.Setup(x => x.GetAll()).Returns(_users);

            UserService userService = new UserService(mockUserRepository.Object);

            //act
            User retUser = userService.Authenticate(_user.Username, _user.Password);

            //assert
            Assert.AreEqual(_user, retUser);
        }

        [TestMethod]
        [ExpectedException(typeof(ObjectNotFoundException))]
        public void AuthenticateWithNoExistentUserUserServices()
        {
            //arrange
            var mockUserRepository = new Mock<IRepository<User>>();

            mockUserRepository.Setup(x => x.GetAll()).Returns(_users);

            UserService userService = new UserService(mockUserRepository.Object);

            //act
            User retUser = userService.Authenticate(_user.Username + "DOESNTEXIST", _user.Password);

            //assert - expect exception
        }

        [TestMethod]
        [ExpectedException(typeof(ObjectAlreadyExistException))]
        public void CreateWithExistingUserUserServices()
        {
            //arrange
            var mockUserRepository = new Mock<IRepository<User>>();

            mockUserRepository.Setup(x => x.GetAll()).Returns(_users);

            UserService userService = new UserService(mockUserRepository.Object);

            //act
            Guid id = userService.Create(_user);

            //assert - expect exception
        }

        //[TestMethod]
        //public void CreateUserServices()
        //{
        //    //arrange
        //    var mockUserRepository = new Mock<IRepository<User>>();
        //    User userToCreate = new User
        //        {
        //            Id = new Guid("0b21d4b6-eb42-456b-9828-a90cb604bceb"),
        //            Password = "7BbfOOoMJCf",
        //            Username = "igardner8",
        //            PhoneBook = new List<Contact>
        //                {
        //                    new Contact
        //                    {
        //                        Title = "Mr",
        //                        Email = "sperezy@nyu.edu",
        //                        Forename = "Susan",
        //                        Surname = "Perez",
        //                        ContactNumbers = new List<ContactNumber>
        //                        {
        //                            new ContactNumber {Description = "Mobile", TelephoneNumber = "86-(719)546-0680"},
        //                            new ContactNumber {Description = "Mobile2", TelephoneNumber = "387-(833)766-7041"}
        //                        }
        //                    },
        //                    new Contact
        //                    {
        //                        Title = "Mr",
        //                        Email = "lmcdonaldz@dedecms.com",
        //                        Forename = "Louis",
        //                        Surname = "Mcdonald",
        //                        ContactNumbers = new List<ContactNumber>
        //                        {
        //                            new ContactNumber {Description = "Mobile2", TelephoneNumber = "62-(113)771-6674"}
        //                        }
        //                    }
        //                }
        //        };

        //    mockUserRepository.Setup(x => x.GetAll()).Returns(_users);

        //    UserService userService = new UserService(mockUserRepository.Object);

        //    //act
        //    Guid id = userService.Create(_user);

        //    //assert
        //}
    }
}
