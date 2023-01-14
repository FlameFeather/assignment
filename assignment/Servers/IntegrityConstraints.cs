using assignment.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace assignment.Servers
{
	public class IntegrityConstraints
	{
		static private public_goodContext _goodContext = new public_goodContext();
		static private public_goodContext _Context = new public_goodContext();
		public static bool examination()
		{
			try
			{
				var goods = from good in _goodContext.Goods
							select good;
				goods.ToList();
				foreach (var good in goods)
				{
					var res = from item in _Context.Items
							  where item.Itemid == good.Itemid
							  select item;
					if (res.Count() == 0)
					{
						_Context.Remove(good);
						_Context.SaveChanges();
					}
				}
				var items = from item in _goodContext.Items
							select item;
				items.ToList();
				foreach (var item in items)
				{
					var AvailableQuantity = from good in _Context.Goods
											where good.Itemid == item.Itemid && good.Gstate == 1
											select good;
					item.AvailableQuantity = AvailableQuantity.Count();
					var BorrowedQuantity = from good in _Context.Goods
										   where good.Itemid == item.Itemid && good.Gstate == 2
										   select good;
					item.BorrowedQuantity = BorrowedQuantity.Count();
					var UnavailableQuantity = from good in _Context.Goods
											  where good.Itemid == item.Itemid && good.Gstate == 3
											  select good;
					item.UnavailableQuantity = UnavailableQuantity.Count();
				}
				_goodContext.SaveChanges();

				return true;
			}
			catch { return false; }
		}
		public void examinationOnGoodTime(object state)
		{
			public_goodContext _GoodContext = ContextMaker.getContext();
			var goods = from good in _GoodContext.Goods
						select good;
			foreach (var good in goods)
			{
				if (good.Gstate != 2) continue;
				public_goodContext _itemContext = ContextMaker.getContext();
				var items = from item in _itemContext.Items
							where item.Itemid == good.Itemid
							select new { time = item.Itime };
				int time = items.FirstOrDefault().time;
				if ((DateTime.Now - DateTime.Parse(good.BeginTime)).TotalMinutes < time)
				{
					good.KeepTime = time - (DateTime.Now - DateTime.Parse(good.BeginTime)).TotalMinutes;
				}
				else
				{
					Record record = new Record();
					record.Gid = good.Gid;
					record.Uid = good.Uid;
					record.BeginTime = good.BeginTime;
					record.EndTime = DateTime.Now.ToString();
					record.State = 3;
					var re= ContextMaker.getContext();
					re.Records.Add(record);
					re.SaveChanges();
					good.Gstate = 3; 
					good.KeepTime = -1;
				}
			}
			_GoodContext.SaveChanges();
			examination();
		}
	}
}
