using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phonebook.Data.Context
{
    public class Configuration
    {
        public string FilePaths { get; set; }
        public string UsersFileName { get; set; }
        public string ContactsFileName { get; set; }
        public string ContactNumbersFileName { get; set; }
    }
}
