using System;

namespace AsynchronousDebugging.Messages
{
	public sealed class CollatzGetResponseMessage
	{
		public string Value { get; set; }
		public int Iterations { get; set; }
		public TimeSpan Time { get; set; }
	}
}
