using System.Collections.Generic;
using System.Numerics;
using System.Threading.Tasks;

namespace AsynchronousDebugging.UnitTests
{
	public interface ICollatzProducer
	{
		Task<IReadOnlyList<BigInteger>> ProduceAsync(BigInteger value);
	}
}
