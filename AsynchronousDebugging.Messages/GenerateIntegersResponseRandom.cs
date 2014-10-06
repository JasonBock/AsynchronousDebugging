using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsynchronousDebugging.Messages
{
	public class GenerateIntegersResponseRandom
	{
		public int[] data { get; set; }
		public string completionTime { get; set; }
	}
}
