using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduConnect.Application.DTOs.Users
{
	public class Register
	{
		[Required(ErrorMessage = "Please input username!")]
		public string? Username { get; set; }

		[Required(ErrorMessage = "Please input email!")]
		[EmailAddress(ErrorMessage = "Invalid email address!")]
		public string Email { get; set; } = string.Empty;

		[Required(ErrorMessage = "Please input password!")]
		public string Password { get; set; } = string.Empty;
	}
}
