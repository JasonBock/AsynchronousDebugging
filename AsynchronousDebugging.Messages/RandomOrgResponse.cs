
namespace AsynchronousDebugging.Messages
{
	public class RandomOrgResponse<T>
	{
		public string jsonrpc { get; set; }
		public RandomOrgError error { get; set; }
		public T result { get; set; }
		public int? id { get; set; }
	}
}
