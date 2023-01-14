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
	[Route("UserAudit")]
	[AllowAnonymous]
	public class UserAuditController : Controller
	{
		[HttpPost("get")]
		public async Task<ActionResult<object>> get(Model<string> model)
		{
			try
			{
				var context = ContextMaker.getContext();
				var tempusers = from user in context.TempUsers
							select user;
				IEnumerable<object> page = Paging.atPage(tempusers.ToList(), model.page.pageSize, model.page.currentPage);
				return new { flag = true, data = page, total = tempusers.Count() };
			}
			catch { return new { flag = false }; }
		}
		[HttpPost("remove")]
		public async Task<ActionResult<object>> remove(Model<string> model)
		{
			try
			{
				var context = ContextMaker.getContext();
				var tempusers = from tempuser in context.TempUsers
							where tempuser.TempUid == model.data
							select tempuser;
				context.Remove(tempusers.FirstOrDefault());
				await context.SaveChangesAsync();
				return new { flag = true };
			}
			catch { return new { flag = false }; }
		}
		[HttpPost("audit")]
		public async Task<ActionResult<object>> remove(Model<TempUser> model)
		{
			try
			{
				var context = ContextMaker.getContext();
				var tempusers = from tempuser in context.TempUsers
								where tempuser.TempUid == model.data.TempUid
								select tempuser;
				context.Remove(tempusers.FirstOrDefault());
				await context.SaveChangesAsync();
				User user = new User();
				user.Uid=model.data.TempUid;
				user.Upwd = model.data.TempUpwd;
				user.Udate = model.data.TempDate;
				user.Ucontext = model.data.TempContext;
				user.Gender = model.data.TempGender;
				await context.Users.AddAsync(user);
				await context.SaveChangesAsync();
				return new { flag = true };
			}
			catch { return new { flag = false }; }
		}
	}
}
