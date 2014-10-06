using System.Collections.Generic;
using System.Numerics;
using System.Threading.Tasks;

namespace AsynchronousDebugging.UnitTests
{
	public sealed class CollatzConsumer
	{
		public static async Task<CollatzConsumer> Create(ICollatzProducer producer, BigInteger value)
		{
			var consumer = new CollatzConsumer();
			consumer.Sequence = await producer.ProduceAsync(value);
			return consumer;
		}

		private CollatzConsumer()
			: base() { }

		public IReadOnlyList<BigInteger> Sequence { get; private set; }
	}
}
