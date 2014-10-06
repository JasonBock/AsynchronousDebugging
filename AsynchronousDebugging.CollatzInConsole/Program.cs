using System;
using System.Diagnostics;
using System.IO;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace AsynchronousDebugging.CollatzInConsole
{
	class Program
	{
		static void Main(string[] args)
		{
			Program.GetCallingMethod();

			if (args != null && args.Length == 1)
			{
				BigInteger value = BigInteger.Zero;

				if (BigInteger.TryParse(args[0], out value))
				{
					Program.Collatz(value, Console.Out);
				}
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static void GetCallingMethod()
		{
			var frame = new StackFrame(1);
			Console.Out.WriteLine(frame.GetMethod().Name);
		}

		public static void Collatz(BigInteger value, TextWriter writer)
		{
			writer.WriteLine(value.ToString());

			if (value > 1)
			{
				var nextValue = value % 2 == 0 ?
					value / 2 : (3 * value) + 1;

				Program.Collatz(nextValue, writer);
			}
		}
	}
}
