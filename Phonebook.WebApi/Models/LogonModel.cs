using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Phonebook.WebApi.Models
{
	public class LogonModel
	{
		public string Username { get; set; }
		public string Password { get; set; }
	}
}