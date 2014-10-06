
namespace AsynchronousDebugging.Messages
{
	public class RandomOrgRequest<T>
	{
		public string jsonrpc { get; set; }
		public string method { get; set; }
		public T @params { get; set; }
		public int id { get; set; }
	}
}
