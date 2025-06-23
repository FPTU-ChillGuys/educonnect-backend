using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduConnect.Application.DTOs.Responses.AuthResponses
{
	public class TokenResponse
	{
		public required string AccessToken { get; set; }
		public required string RefreshToken { get; set; }
	}
}
