
namespace AsynchronousDebugging.Messages
{
	public class RandomOrgError
	{
		public int code { get; set; }
		public string message { get; set; }
		public object data { get; set; }
	}
}
