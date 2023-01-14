using System.Collections.Generic;
using System.Linq;

namespace gaowork
{
	public class Paging
	{
		public int currentPage { get; set; }
		public int pageSize { get; set; }
		public string searchWord { get; set; }
		static public IEnumerable<object> atPage(IEnumerable<object> list,int pageSize,int currentPage)
		{
			var page = list.SkipLast(list.Count() - pageSize * currentPage).Skip(pageSize * (currentPage - 1));
			return page;
		}
	}
}
