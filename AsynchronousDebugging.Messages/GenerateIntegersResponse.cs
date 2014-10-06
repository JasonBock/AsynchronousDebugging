
namespace AsynchronousDebugging.Messages
{
	public class GenerateIntegersResponse
	{
		public GenerateIntegersResponseRandom random { get; set; }
		public int bitsUsed { get; set; }
		public int bitsLeft { get; set; }
		public int requestsLeft { get; set; }
		public int advisoryDelay { get; set; }
	}
}
