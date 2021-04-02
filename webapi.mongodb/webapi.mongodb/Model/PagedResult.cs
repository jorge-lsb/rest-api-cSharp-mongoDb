using System.Collections.Generic;

namespace webapi.mongodb.Model
{
	public class PagedResult<T> where T : class
	{
		public IEnumerable<T> List { get; set; }
		public long TotalResults { get; set; }
		public int PageIndex { get; set; }
		public int PageSize { get; set; }
	}
}
