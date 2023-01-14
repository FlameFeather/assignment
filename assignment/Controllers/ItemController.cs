using assignment.Models;
using assignment.ViewsModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using gaowork;
using assignment.Servers;

namespace assignment.Controllers
{
	[ApiController]
	[Route("item")]
	[AllowAnonymous]
	//[Authorize]
	public class ItemController : Controller
	{
		private public_goodContext _goodContext = new public_goodContext();
		[HttpPost("get")]
		public async Task<ActionResult<object>> get(Model<object> model)
		{
			try
			{
				var res = from item in _goodContext.Items
						  where item.Iname.Contains(model.page.searchWord)
						  select item;
				IEnumerable<object> items = Paging.atPage(res.ToList(), model.page.pageSize, model.page.currentPage);
				return new { flag = true, data = items, total = res.Count() };
			}
			catch { return new { flag = false }; }
		}
		[HttpPost("add")]
		public async Task<ActionResult<object>> add(Model<AddItemModel> addItem)
		{
			try
			{
				Item item = new Item();
				item.Itemid = addItem.data.itemId;
				item.Iname = addItem.data.itemName;
				item.Itime = int.Parse(addItem.data.itemTime);
				await _goodContext.Items.AddAsync(item);
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
				var res = from item in _goodContext.Items
						  where item.Itemid == id.data
						  select item;
				_goodContext.Remove(res.FirstOrDefault());
				await _goodContext.SaveChangesAsync();
				return new { flag = IntegrityConstraints.examination() };
			}
			catch { return new { flag = false }; }
		}

		[HttpPost("update")]
		public async Task<ActionResult<object>> update(Model<AddItemModel> updateItem)
		{
			try
			{
				var res = from item in _goodContext.Items
						  where item.Itemid == updateItem.data.itemId
						  select item;
				res.FirstOrDefault().Iname= updateItem.data.itemName;
				res.FirstOrDefault().Itemid = updateItem.data.itemTime;
				await _goodContext.SaveChangesAsync();
				return new { flag = IntegrityConstraints.examination() };
			}
			catch { return new { flag = false }; }
		}
	}
}
