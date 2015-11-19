using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Phonebook.Data.JsonUtilities;
using Phonebook.Domain.Model;

namespace Phonebook.Data.Context
{
    public class PhonebookContext : IDisposable
    {
        private readonly string _filePath;
        public IList<User> Users { get; set; }

        public PhonebookContext(string folderPath, string usersFilePath)
        {
            if (!Directory.Exists(folderPath))
            {
                throw new DirectoryNotFoundException(folderPath + " not found");
            }

            if (folderPath.EndsWith("\\"))
            {
                _filePath = folderPath + usersFilePath;
            }
            else
            {
                _filePath = folderPath + "\\" + usersFilePath;
            }

            LoadUsersFile();
        }

        //Json.NET
        private void LoadUsersFile()
        {
            if (File.Exists(_filePath))
            {
                try
                {
                    Users = JsonSerialization.ReadFromJsonFile<List<User>>(_filePath) ?? new List<User>();
                }
                catch (Exception)
                {
                    throw new Exception("There was a problem reading from " + _filePath);
                }
            }
            else
            {
                throw new DirectoryNotFoundException(_filePath + " not found");
            }
        }

        private void SaveUsersFile()
        {
            if (File.Exists(_filePath))
            {
                try
                {
                    JsonSerialization.WriteToJsonFile<List<User>>(_filePath, Users.ToList());
                }
                catch (Exception)
                {
                    throw new Exception("There was a problem writing to " + _filePath);
                }
            }
            else
            {
                throw new DirectoryNotFoundException(_filePath + " not found");
            }
        }

        public void SaveChanges()
        {
            SaveUsersFile();
        }

        public void Dispose()
        {
            SaveUsersFile();
            Users = null;
        }
    }
}
