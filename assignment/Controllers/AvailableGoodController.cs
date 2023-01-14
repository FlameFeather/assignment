using assignment.Models;
using assignment.ViewsModels;
using gaowork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using assignment.Servers;
using 大作业_高;
using System;

namespace assignment.Controllers
{
	[ApiController]
	[Route("availablegood")]
	[AllowAnonymous]
	public class AvailableGoodController : Controller
	{
		private public_goodContext _goodContext = new public_goodContext();
		[HttpPost("get")]
		public async Task<ActionResult<object>> get(Model<string> model)
		{
			try
			{
				var res = from good in _goodContext.Goods
						  where good.Gid.Contains(model.page.searchWord) && good.Itemid == model.data && good.Gstate == 1
						  select good;
				IEnumerable<object> goods = Paging.atPage(res.ToList(), model.page.pageSize, model.page.currentPage);
				return new { flag = true, data = goods, total = res.Count() };
			}
			catch { return new { flag = false }; }
		}

		[HttpPost("borrow")]
		public async Task<ActionResult<object>> borrow(Model<string> model, [FromHeader]string token)
		{
			try
			{
				var res = from good in _goodContext.Goods
						  where good.Gid==model.data
						  select good;
				res.FirstOrDefault().Gstate = 2;
				res.FirstOrDefault().Uid = Token.getId(token);
				res.FirstOrDefault().BeginTime = DateTime.Now.ToString();
				await _goodContext.SaveChangesAsync();
				IntegrityConstraints.examination();
				return new { flag = true };
			}
			catch { return new { flag = false }; }
		}
	}
}
