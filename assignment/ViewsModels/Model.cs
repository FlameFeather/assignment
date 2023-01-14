using gaowork;

namespace assignment.ViewsModels
{
	public class Model<T>
	{
		public T data { get; set; }
		public Paging page { get; set; }
	}
}
