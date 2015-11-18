using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Phonebook.Data.JsonUtilities;
using Phonebook.Domain.Model;

namespace Phonebook.Data.Context
{
    public class PhonebookEntities : IDisposable
    {
        public string FilePath;
        public IList<User> Users { get; set; }

        public PhonebookEntities(string folderPath, string usersFilePath)
        {
            string fullUserFilePath;

            if (!Directory.Exists(folderPath))
            {
                throw new DirectoryNotFoundException(folderPath + " not found");
            }

            if (folderPath.EndsWith("\\"))
            {
                fullUserFilePath = folderPath + usersFilePath;
            }
            else
            {
                fullUserFilePath = folderPath + "\\" + usersFilePath;
            }

            FilePath = fullUserFilePath;

            LoadUsersFile();
        }

        //Json.NET
        private void LoadUsersFile()
        {
            if (File.Exists(FilePath))
            {
                try
                {
                    Users = JsonSerialization.ReadFromJsonFile<List<User>>(FilePath) ?? new List<User>();
                }
                catch (Exception)
                {
                    throw new Exception("There was a problem reading from " + FilePath);
                }
            }
            else
            {
                throw new DirectoryNotFoundException(FilePath + " not found");
            }
        }

        private void SaveUsersFile()
        {
            if (File.Exists(FilePath))
            {
                try
                {
                    JsonSerialization.WriteToJsonFile<List<User>>(FilePath, Users.ToList());
                }
                catch (Exception)
                {
                    throw new Exception("There was a problem writing to " + FilePath);
                }
            }
            else
            {
                throw new DirectoryNotFoundException(FilePath + " not found");
            }
        }

        public void Dispose()
        {
            SaveUsersFile();
            Users = null;
        }
    }
}
