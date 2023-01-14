using assignment.Models;
using assignment.Servers;
using assignment.ViewsModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;
using System;

namespace assignment.Controllers
{
	[ApiController]
	[Route("userManageGood")]
	[AllowAnonymous]
	public class UserManageGoodController : Controller
	{
		[HttpPost("repaid")]
		public async Task<ActionResult<object>> repaid(Model<string> model)
		{
			try
			{
				var goodcontext = ContextMaker.getContext();
				var goods = from good in goodcontext.Goods
							where good.Gid == model.data
							select good;
				var context = ContextMaker.getContext();
				Record record = new Record();
				record.Gid = goods.FirstOrDefault().Gid;
				record.Uid = goods.FirstOrDefault().Uid;
				record.BeginTime = goods.FirstOrDefault().BeginTime;
				record.EndTime = DateTime.Now.ToString();
				record.State = 2;
				await context.Records.AddAsync(record);
				await context.SaveChangesAsync();
				goods.FirstOrDefault().Uid = null;
				goods.FirstOrDefault().BeginTime = null;
				goods.FirstOrDefault().KeepTime = 0;
				goods.FirstOrDefault().Gstate = 1;
				await goodcontext.SaveChangesAsync();
				return new { flag = true };
			}
			catch { return new { flag = false }; }
		}
		[HttpPost("lose")]
		public async Task<ActionResult<object>> lose(Model<string> model)
		{
			try
			{
				var goodcontext = ContextMaker.getContext();
				var goods = from good in goodcontext.Goods
							where good.Gid == model.data
							select good;
				var context = ContextMaker.getContext();
				Record record = new Record();
				record.Gid = goods.FirstOrDefault().Gid;
				record.Uid = goods.FirstOrDefault().Uid;
				record.BeginTime = goods.FirstOrDefault().BeginTime;
				record.EndTime = DateTime.Now.ToString();
				record.State = 3;
				await context.Records.AddAsync(record);
				await context.SaveChangesAsync();
				goods.FirstOrDefault().Uid = null;
				goods.FirstOrDefault().BeginTime = null;
				goods.FirstOrDefault().KeepTime = -1;
				goods.FirstOrDefault().Gstate = 3;
				await goodcontext.SaveChangesAsync();
				return new { flag = true };
			}
			catch { return new { flag = false }; }
		}
	}
}
