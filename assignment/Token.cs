using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace 大作业_高
{
	public class Token
	{
		private string _token;
		private readonly IConfiguration _configuration;
		public Token(string uid ,string role, IConfiguration configuration)
		{
			_configuration = configuration;
			var claims = new List<Claim>{
				new Claim(JwtRegisteredClaimNames.Sub, uid),
				new Claim(JwtRegisteredClaimNames.Typ, role)
			};

			var tokenConfigSection = _configuration.GetSection("Security:Token");
			var key = new SymmetricSecurityKey(
				Encoding.UTF8.GetBytes(tokenConfigSection["Key"])
			);
			var signCredential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
			var jwtToken = new JwtSecurityToken(
				issuer: tokenConfigSection["Issuer"],
				audience: tokenConfigSection["Audience"],
				claims: claims,
				expires: DateTime.Now.AddMinutes(100000),
				signingCredentials: signCredential
			);

			_token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
		}
		public string getToken() { return _token; }
		static public string getId(string token)
		{
			List<Claim> x = (List<Claim>)new JwtSecurityTokenHandler().ReadJwtToken(token).Claims;
			return x[0].Value;
		}
		static public string getRole(string token)
		{
			List<Claim> x = (List<Claim>)new JwtSecurityTokenHandler().ReadJwtToken(token).Claims;
			return x[1].Value;
		}
	}
}
