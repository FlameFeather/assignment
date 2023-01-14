using assignment.Models;
using assignment.Servers;
using assignment.ViewsModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;
using System;
using System.Collections.Generic;
using gaowork;

namespace assignment.Controllers
{
	[ApiController]
	[Route("Repass")]
	[AllowAnonymous]
	public class RepassController : Controller
	{
		[HttpPost("manager/check")]
		public async Task<ActionResult<object>> managercheck(Model<RepassModel> repass)
		{
			try
			{
				var context = ContextMaker.getContext();
				var managers = from manager in context.Managers
							   where manager.Mid == repass.data.id && manager.Mpwd == repass.data.pwd
							   select manager;
				if (managers.Count() == 1) return new { flag = true };
				else return new { flag = false };
			}
			catch { return new { flag = false }; }
		}
		[HttpPost("manager/update")]
		public async Task<ActionResult<object>> managerupdate(Model<RepassModel> repass)
		{
			try
			{
				var context = ContextMaker.getContext();
				var managers = from manager in context.Managers
							   where manager.Mid == repass.data.id
							   select manager;
				managers.FirstOrDefault().Mpwd = repass.data.pwd;
				await context.SaveChangesAsync();
				return new { flag = true };
			}
			catch { return new { flag = false }; }
		}

		[HttpPost("user/check")]
		public async Task<ActionResult<object>> usercheck(Model<RepassModel> repass)
		{
			try
			{
				var context = ContextMaker.getContext();
				var users = from user in context.Users
							   where user.Uid == repass.data.id && user.Upwd == repass.data.pwd
							   select user;
				if (users.Count() == 1) return new { flag = true };
				else return new { flag = false };
			}
			catch { return new { flag = false }; }
		}
		[HttpPost("user/update")]
		public async Task<ActionResult<object>> userupdate(Model<RepassModel> repass)
		{
			try
			{
				var context = ContextMaker.getContext();
				var users = from user in context.Users
							   where user.Uid== repass.data.id
							   select user;
				users.FirstOrDefault().Upwd = repass.data.pwd;
				await context.SaveChangesAsync();
				return new { flag = true };
			}
			catch { return new { flag = false }; }
		}
	}
}
