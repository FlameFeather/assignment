using assignment.Models;
using assignment.ViewsModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Threading.Tasks;
using 大作业_高;

namespace assignment.Controllers
{
	[ApiController]
	[Route("account")]
	[AllowAnonymous]
	//[Authorize]
	public class AccountController : Controller
	{
		private public_goodContext _goodContext = new public_goodContext();
		private IConfiguration _configuration;
		public AccountController(IConfiguration configuration) { _configuration = configuration; }

		[HttpPost("userLogin")]
		public ActionResult<object> userLogin(LoginModel login)
		{
			try
			{
				var res = from user in _goodContext.Users
						  where user.Uid == login.UserName && user.Upwd == login.Password
						  select user;
				if (res.Count() == 1)
				{
					return new
					{
						flag = true,
						token = new Token(login.UserName, "user", _configuration).getToken()
					};
				}
				else { return new { flag = false }; }
			}
			catch { return new { flag = false }; }
		}

		[HttpPost("managerLogin")]
		public ActionResult<object> managerLogin(LoginModel login)
		{
			try
			{
				var res = from manager in _goodContext.Managers
						  where manager.Mid == login.UserName && manager.Mpwd == login.Password
						  select manager;
				if (res.Count() == 1)
				{
					return new
					{
						flag = true,
						token = new Token(login.UserName, "manager", _configuration).getToken()
					};
				}
				else { return new { flag = false }; }
			}
			catch { return new { flag = false }; }
		}

		[HttpPost("userRegister")]
		public async Task<ActionResult<object>> userRegister(UserRegisterModel userRegister)
		{
			try
			{
				var res1 = from user in _goodContext.Users
						   where user.Uid == userRegister.Id
						   select user;
				var res2 = from user in _goodContext.TempUsers
						   where user.TempUid == userRegister.Id
						   select user;
				if (res2.Count() + res1.Count() == 0)
				{
					TempUser tempUser = new TempUser();
					tempUser.TempUid = userRegister.Id;
					tempUser.TempUpwd = userRegister.pwd;
					tempUser.TempDate = userRegister.date;
					tempUser.TempGender = userRegister.gender;
					tempUser.TempContext = userRegister.context;
					await _goodContext.TempUsers.AddAsync(tempUser);
					await _goodContext.SaveChangesAsync();
					return new
					{
						flag = true,
						//token = new Token(userRegister.Id, "user", _configuration).getToken()
					};
				}
				else { return new { flag = false }; }
			}
			catch { return new { flag = false }; }
		}
	}
}
