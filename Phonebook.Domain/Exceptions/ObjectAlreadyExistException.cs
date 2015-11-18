using System;

namespace Phonebook.Domain.Exceptions
{
    public class ObjectAlreadyExistException : Exception
    {
        public ObjectAlreadyExistException(string objectName) : base(objectName + " already exists")
        {

        }

        public ObjectAlreadyExistException(string objectName, string uniquePropertyName)
        {
            
        }
    }
}
