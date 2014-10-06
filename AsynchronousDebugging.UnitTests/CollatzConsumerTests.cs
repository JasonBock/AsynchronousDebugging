using System;
using System.Collections.Generic;
using System.Numerics;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace AsynchronousDebugging.UnitTests
{
	[TestClass]
	public sealed class CollatzConsumerTests
	{
		[TestMethod]
		public async Task Consume()
		{
			var value = new BigInteger(4);
			var expectedSequence = new List<BigInteger>
				{
					new BigInteger(4), new BigInteger(2), new BigInteger(1)
				}.AsReadOnly();

			var producer = new Mock<ICollatzProducer>(MockBehavior.Strict);
			producer.Setup(_ => _.ProduceAsync(value))
				.Returns(Task.FromResult<IReadOnlyList<BigInteger>>(expectedSequence));

			var consumer = await CollatzConsumer.Create(producer.Object, value);

			Assert.AreEqual(expectedSequence.Count, consumer.Sequence.Count);

			for (var i = 0; i < expectedSequence.Count; i++)
			{
				Assert.AreEqual(expectedSequence[i], consumer.Sequence[i]);
			}

			producer.VerifyAll();
		}

		[TestMethod]
		[ExpectedException(typeof(NotSupportedException))]
		public async Task ConsumeWithError()
		{
			var value = new BigInteger(4);
			var producer = new Mock<ICollatzProducer>(MockBehavior.Strict);
			producer.Setup(_ => _.ProduceAsync(value))
				.Returns(Task.Run<IReadOnlyList<BigInteger>>(
					new Func<IReadOnlyList<BigInteger>>(() => { throw new NotSupportedException(); })));

			await CollatzConsumer.Create(producer.Object, value);
		}
	}
}
