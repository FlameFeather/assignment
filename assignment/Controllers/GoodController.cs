using assignment.Models;
using assignment.ViewsModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;
using gaowork;
using System.Collections.Generic;
using assignment.Servers;

namespace assignment.Controllers
{
	[ApiController]
	[Route("good")]
	[AllowAnonymous]
	public class GoodController : Controller
	{
		private public_goodContext _goodContext = new public_goodContext();
		[HttpPost("get")]
		public async Task<ActionResult<object>> get(Model<string> model)
		{
			try
			{
				var res = from good in _goodContext.Goods
						  where good.Gid.Contains(model.page.searchWord) && good.Itemid == model.data
						  select good;
				IEnumerable<object> goods = Paging.atPage(res.ToList(), model.page.pageSize, model.page.currentPage);
				return new { flag = true, data = goods, total = res.Count() };
			}
			catch { return new { flag = false }; }
		}
		[HttpPost("add")]
		public async Task<ActionResult<object>> add(Model<AddGoodModel> addGood)
		{
			try
			{
				Good good = new Good();
				good.Itemid = addGood.data.itemid;
				good.Gid = addGood.data.gid;
				good.Gcontext = addGood.data.gcontext;
				await _goodContext.Goods.AddAsync(good);
				await _goodContext.SaveChangesAsync();
				return new { flag = IntegrityConstraints.examination() };
			}
			catch { return new { flag = false }; }
		}
		[HttpPost("delete")]
		public async Task<ActionResult<object>> delete(Model<string> id)
		{
			try
			{
				var res = from good in _goodContext.Goods
						  where good.Gid == id.data
						  select good;
				_goodContext.Remove(res.FirstOrDefault());
				await _goodContext.SaveChangesAsync();
				return new { flag = IntegrityConstraints.examination() };
			}
			catch { return new { flag = false }; }
		}
		[HttpPost("update")]
		public async Task<ActionResult<object>> update(Model<AddGoodModel> updateGood)
		{
			try
			{
				var res = from good in _goodContext.Goods
						  where good.Gid == updateGood.data.gid
						  select good;
				res.FirstOrDefault().Itemid = updateGood.data.itemid;
				res.FirstOrDefault().Gcontext = updateGood.data.gcontext;
				await _goodContext.SaveChangesAsync();
				return new { flag = IntegrityConstraints.examination() };
			}
			catch { return new { flag = false }; }
		}
	}
}
