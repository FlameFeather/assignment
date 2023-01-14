using assignment.Servers;
using assignment.ViewsModels;
using gaowork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace assignment.Controllers
{
	[ApiController]
	[Route("history")]
	[AllowAnonymous]
	public class HistoryController : Controller
	{
		[HttpPost("get")]
		public async Task<ActionResult<object>> get(Model<HistoryModel> model)
		{
			try
			{
				var _recordsContext = ContextMaker.getContext();
				var records = from record in _recordsContext.Records
							  join good in _recordsContext.Goods on record.Gid equals good.Gid
							  join item in _recordsContext.Items on good.Itemid equals item.Itemid
							  where (model.data.uid != "" ? record.BeginTime.Contains(model.page.searchWord) : record.Uid.Contains(model.page.searchWord))
							  && (model.data.itemid == "" ? true : good.Itemid == model.data.itemid)
							  && (model.data.uid != "" ? record.Uid == model.data.uid : true)
							  select new
							  {
								  rid = record.Rid,
								  gid = record.Gid,
								  iname = item.Iname,
								  uid = record.Uid,
								  beginTime = record.BeginTime,
								  keepTime=good.KeepTime,
								  endTime = record.EndTime,
								  state = record.State,
							  };
				var _goodContext = ContextMaker.getContext();
				var goods = from good in _goodContext.Goods
							join item in _goodContext.Items on good.Itemid equals item.Itemid
							where good.Gstate == 2
							&& (model.data.uid == "" ? good.Uid.Contains(model.page.searchWord) : good.Gid.Contains(model.page.searchWord))
							&& (model.data.itemid == "" ? true : good.Itemid == model.data.itemid)
							&& (model.data.uid != "" ? good.Uid == model.data.uid : true)
							select new
							{
								rid = 0,
								gid = good.Gid,
								iname = item.Iname,
								uid = good.Uid,
								beginTime = good.BeginTime,
								keepTime = good.KeepTime,
								endTime = "0",
								state = 1,
							};
				if (model.data.part == "0")
				{
					IEnumerable<object> good1 = goods.ToList();
					IEnumerable<object> record1 = records.ToList();
					good1 = good1.Concat(record1);
					IEnumerable<object> page = Paging.atPage(good1, model.page.pageSize, model.page.currentPage);
					return new { flag = true, data = page, total = good1.Count() };
				}
				else if(model.data.part == "1")
				{
					IEnumerable<object> page = Paging.atPage(goods.ToList(), model.page.pageSize, model.page.currentPage);
					return new { flag = true, data = page, total = goods.Count() };
				}
				else
				{
					IEnumerable<object> page = Paging.atPage(records.ToList(), model.page.pageSize, model.page.currentPage);
					return new { flag = true, data = page, total = records.Count() };
				}
			}
			catch { return new { flag = false }; }
		}
	}
}
