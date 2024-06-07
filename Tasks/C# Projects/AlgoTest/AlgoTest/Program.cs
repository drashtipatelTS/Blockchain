using System;
using System.Net.Http;
using System.Threading.Tasks;
using Algorand.V2;

namespace HelloWorldAlgorand
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // Define the Algorand API endpoint and token
            string ALGOD_API_ADDR = "https://testnet-api.algonode.cloud";
            string ALGOD_API_TOKEN = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"; // Replace with your actual API key if using a service that requires it

            // Setup Algorand client with a timeout
            var httpClient = new HttpClient
            {
                BaseAddress = new Uri(ALGOD_API_ADDR),
                Timeout = TimeSpan.FromSeconds(30) // Increase timeout duration
            };
            if (!string.IsNullOrEmpty(ALGOD_API_TOKEN))
            {
                httpClient.DefaultRequestHeaders.Add("X-API-Key", ALGOD_API_TOKEN);
            }

            var algodApiInstance = new AlgodApi(httpClient);

            try
            {
                // Fetch the node status
                var nodeStatus = await algodApiInstance.GetStatusAsync();

                // Print node status
                Console.WriteLine("Hello, Algorand!");
                Console.WriteLine("Node Status:");
                Console.WriteLine($"Last Round: {nodeStatus.LastRound}");
                Console.WriteLine($"Time Since Last Round: {nodeStatus.TimeSinceLastRound}");
                Console.WriteLine($"CatchupTime: {nodeStatus.CatchupTime}");
                Console.WriteLine($"Last Consensus Version: {nodeStatus.LastConsensusVersion}");
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception when calling AlgodApi.GetStatus: " + e.Message);
            }
        }
    }

    // Helper class to configure HttpClient
    public class AlgodApi
    {
        private readonly HttpClient _httpClient;

        public AlgodApi(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<NodeStatus> GetStatusAsync()
        {
            HttpResponseMessage response = await _httpClient.GetAsync("/v2/status");
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            return Newtonsoft.Json.JsonConvert.DeserializeObject<NodeStatus>(responseBody);
        }
    }

    public class NodeStatus
    {
        public ulong LastRound { get; set; }
        public ulong TimeSinceLastRound { get; set; }
        public ulong CatchupTime { get; set; }
        public string LastConsensusVersion { get; set; }
    }
}
