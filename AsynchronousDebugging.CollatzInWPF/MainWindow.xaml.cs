using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using AsynchronousDebugging.Collatz;
using AsynchronousDebugging.Messages;
using Spackle;
using Spackle.Extensions;

namespace AsynchronousDebugging.CollatzInWPF
{
	public partial class MainWindow
		: Window
	{
		public MainWindow()
		{
			this.InitializeComponent();
			this.Value.Text = CollatzConjecture.NumberThatWillCauseStackOverflowInReleaseUnderDebugger;
		}

		private BigInteger? GetValue()
		{
			var value = BigInteger.Zero;

			return (BigInteger.TryParse(this.Value.Text, out value) && value > 0) ?
				value : null as BigInteger?;
		}

		private static string GetLastSequenceValues(TextWriter writer, int numberOfValues)
		{
			var results = writer.ToString().Split(
				new[] { Environment.NewLine }, StringSplitOptions.None);
			return string.Join(Environment.NewLine, results,
				results.Length - numberOfValues, numberOfValues);
		}

		private void OnRunSynchronouslyClick(object sender, RoutedEventArgs e)
		{
			using (Cursors.Wait.Bind(() => this.Cursor))
			{
				this.Results.Text = string.Empty;

				BigInteger? value = null;

				if ((value = this.GetValue()) != null)
				{
					using (var writer = new StringWriter())
					{
						CollatzConjecture.Collatz(value.Value, writer);
						this.Results.Text = MainWindow.GetLastSequenceValues(writer, 5);
					}
				}
			}
		}

		private async void OnRunAsynchronouslyClick(object sender, RoutedEventArgs e)
		{
			using (Cursors.Wait.Bind(() => this.Cursor))
			{
				this.Results.Text = string.Empty;

				BigInteger? value = null;

				if ((value = this.GetValue()) != null)
				{
					using (var writer = new StringWriter())
					{
						await CollatzConjecture.CollatzAsync(value.Value, writer);
						this.Results.Text = MainWindow.GetLastSequenceValues(writer, 5);
					}
				}
			}
		}

		private void OnRunAsynchronouslyWithVoidClick(object sender, RoutedEventArgs e)
		{
			using (Cursors.Wait.Bind(() => this.Cursor))
			{
				this.Results.Text = string.Empty;

				BigInteger? value = null;

				if ((value = this.GetValue()) != null)
				{
					using (var stream = File.Create("output.txt"))
					{
						CollatzConjecture.CollatzReturningVoidAsync(value.Value, stream);
						this.Results.Text = stream.Length.ToString();
					}
				}
			}
		}

		private async void OnRunAsynchronouslyUsingFileClick(object sender, RoutedEventArgs e)
		{
			using (Cursors.Wait.Bind(() => this.Cursor))
			{
				this.Results.Text = string.Empty;

				BigInteger? value = null;

				if ((value = this.GetValue()) != null)
				{
					using (var stream = File.Create("output.txt"))
					{
						await CollatzConjecture.CollatzAsync(value.Value, stream);
						this.Results.Text = stream.Length.ToString();
					}
				}
			}
		}

		private async void OnRunManyRandomCollatzOnServerClick(object sender, RoutedEventArgs e)
		{
			using (Cursors.Wait.Bind(() => this.Cursor))
			{
				this.Results.Text = string.Empty;
				var iterations = 0;

				if (int.TryParse(this.Iterations.Text, out iterations))
				{
					var pauseTime = 0;
					int.TryParse(this.PauseTime.Text, out pauseTime);

					var random = new SecureRandom();

					using (var writer = new StringWriter())
					{
						var responses = new List<Task<CollatzGetResponseMessage>>();

						for (var i = 0; i < iterations; i++)
						{
							var value = random.GetBigInteger((ulong)random.Next(50, 200));
							responses.Add(WebApiCalls.GetResponse(value, pauseTime));
						}

						await Task.WhenAll(responses);

						foreach (var response in responses)
						{
							var result = response.Result;
							writer.WriteLine(string.Format("Number: {0}, Iterations: {1}, Time: {2}",
								result.Value, result.Iterations, result.Time));
						}

						this.Results.Text = writer.ToString();
					}
				}
			}
		}

		private async void OnRunRandomOrgCollatzOnServerClick(object sender, RoutedEventArgs e)
		{
			using (Cursors.Wait.Bind(() => this.Cursor))
			{
				this.Results.Text = string.Empty;
				var pauseTime = 0;
				int.TryParse(this.PauseTime.Text, out pauseTime);

				var value = await WebApiCalls.GetRandomNumberFromWeb();
				var response = await WebApiCalls.GetResponse(value, pauseTime);
				this.Results.Text = string.Format("Number: {0}, Iterations: {1}, Time: {2}",
					response.Value, response.Iterations, response.Time);
			}
		}
	}
}
