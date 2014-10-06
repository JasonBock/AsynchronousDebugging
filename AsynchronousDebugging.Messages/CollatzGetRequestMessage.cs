namespace AsynchronousDebugging.Messages
{
	public sealed class CollatzGetRequestMessage
	{
		public int PauseTime { get; set; }
		public string Value { get; set; }
	}
}
