using assignment.Models;
using assignment.Servers;
using assignment.ViewsModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using gaowork;

namespace assignment.Controllers
{
	[ApiController]
	[Route("ManageUser")]
	[AllowAnonymous]
	public class ManageUserController : Controller
	{
		[HttpPost("get")]
		public async Task<ActionResult<object>> get(Model<string> model)
		{
			try
			{
				var context = ContextMaker.getContext();
				var users = from user in context.Users
							select user;
				IEnumerable<object> page = Paging.atPage(users.ToList(), model.page.pageSize, model.page.currentPage);
				return new { flag = true, data = page, total = users.Count() };
			}
			catch { return new { flag = false }; }
		}
		[HttpPost("update")]
		public async Task<ActionResult<object>> update(Model<User> model)
		{
			try
			{
				var context = ContextMaker.getContext();
				var users = from user in context.Users
							where user.Uid == model.data.Uid
							select user;
				users.FirstOrDefault().Upwd = model.data.Upwd;
				users.FirstOrDefault().Udate = model.data.Udate;
				users.FirstOrDefault().Ucontext = model.data.Ucontext;
				users.FirstOrDefault().Gender = model.data.Gender;
				await context.SaveChangesAsync();
				return new { flag = true };
			}
			catch { return new { flag = false }; }
		}
		[HttpPost("remove")]
		public async Task<ActionResult<object>> remove(Model<string> model)
		{
			try
			{
				var context = ContextMaker.getContext();
				var users = from user in context.Users
							where user.Uid == model.data
							select user;
				context.Remove(users.FirstOrDefault());
				await context.SaveChangesAsync();
				return new { flag = true };
			}
			catch { return new { flag = false }; }
		}
	}
}
