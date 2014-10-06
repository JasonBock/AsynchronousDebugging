using System;
using System.Numerics;
using System.Threading;
using System.Web.Http;
using AsynchronousDebugging.Collatz;
using AsynchronousDebugging.Messages;
using Spackle.Extensions;

namespace AsynchronousDebugging.CollatzOnServer.Controllers
{
	public sealed class CollatzController
		: ApiController
	{
		// POST api/collatz
		public CollatzGetResponseMessage Post([FromBody]CollatzGetRequestMessage request)
		{
			var response = new CollatzGetResponseMessage
			{
				Value = request.Value
			};

			BigInteger value = BigInteger.Zero;

			if (BigInteger.TryParse(request.Value, out value))
			{
				response.Time = new Action(
					() =>
					{
						if (request.PauseTime > 0)
						{
							Thread.Sleep(request.PauseTime);
						}
						response.Iterations = CollatzConjecture.Collatz(value);
					}).Time();
			}

			return response;
		}
	}
}
