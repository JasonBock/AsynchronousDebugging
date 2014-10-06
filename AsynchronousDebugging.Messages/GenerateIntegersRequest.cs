using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsynchronousDebugging.Messages
{
	public class GenerateIntegersRequest
	{
		public string apiKey { get; set; }
		public int n { get; set; }
		public int min { get; set; }
		public int max { get; set; }
	}
}
