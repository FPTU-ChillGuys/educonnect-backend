using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduConnect.Application.DTOs.Requests
{
	public class RefreshTokenRequest
	{
		public required string UserId { get; set; }
		public required string RefreshToken { get; set; }
	}
}
