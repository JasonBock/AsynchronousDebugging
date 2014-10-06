using System.Configuration;
using System.Net.Http;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using AsynchronousDebugging.Messages;
using Newtonsoft.Json;

namespace AsynchronousDebugging.CollatzInWPF
{
	public static class WebApiCalls
	{
		public static async Task<CollatzGetResponseMessage> GetResponse(BigInteger value, int pauseTime)
		{
			var request = new CollatzGetRequestMessage
			{
				Value = value.ToString(),
				PauseTime = pauseTime
			};
			var message = JsonConvert.SerializeObject(request, Formatting.Indented);
			var content = new StringContent(message, Encoding.Unicode, "application/json");
			var postResponse = await new HttpClient().PostAsync("http://localhost:3514/api/collatz", content);
			return JsonConvert.DeserializeObject<CollatzGetResponseMessage>(
				await postResponse.Content.ReadAsStringAsync());
		}

		public static async Task<BigInteger> GetRandomNumberFromWeb()
		{
			var key = ConfigurationManager.AppSettings["RandomOrgKey"];
			var request = new RandomOrgRequest<GenerateIntegersRequest>
			{
				id = 0,
				jsonrpc = "2.0",
				method = "generateIntegers",
				@params = new GenerateIntegersRequest
				{
					apiKey = key,
					n = 1,
					min = 1,
					max = 100000000
				}
			};

			var content = new StringContent(JsonConvert.SerializeObject(request, Formatting.Indented),
				Encoding.Default, "application/json-rpc");
			var client = new HttpClient();
			var postResponse = client.PostAsync("https://api.random.org/json-rpc/1/invoke", content);

			var awaitedPostResponse = await postResponse;
			var response = JsonConvert.DeserializeObject<RandomOrgResponse<GenerateIntegersResponse>>(
				await awaitedPostResponse.Content.ReadAsStringAsync());

			return response.result.random.data[0];
		}
	}
}
