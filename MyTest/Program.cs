using System;
using System.Collections.Generic;
using System.Linq;
using Phonebook.Data.Context;
using Phonebook.Data.Repositories;
using Phonebook.Domain.Model;

namespace MyTest
{
    public class Program
    {
        static void Main(string[] args)
        {
            //PhonebookEntities phonebookEntities = new PhonebookEntities(Properties.Settings.Default.FilePaths, Properties.Settings.Default.UserFile);
            //Console.WriteLine();

            //UserRepository userRepository = new UserRepository();

            //userRepository.Create(new User {Password = "password", Username = "Tej.Sidhu"});
            //userRepository.Create(new User { Password = "password2", Username = "John.Doe" });

            //userRepository.Dispose();

            UserRepository userRepository = new UserRepository();

            User userToUpdate = userRepository.Get(users[0].Id);

            userToUpdate.Username = "Test.Test";

            userRepository.Update(userToUpdate);

            userRepository.Create(new User { Password = "password3", Username = "Sue.Smith" });

            List<User> users = userRepository.GetAll().ToList();

            foreach (var user in users)
            {
                Console.WriteLine("Username: " + user.Username + ", Password: " + user.Password);
            }

            userRepository.Dispose();

            Console.ReadLine();
        }
    }
}
