using Algorand;
using Algorand.Algod;
using Algorand.Algod.Model;
using Algorand.Algod.Model.Transactions;
using Algorand.KMD;
using AlgorandTestnet.Proxies;
using AlgoStudio.Clients;
using Proxies;
using System.Text;

namespace AlgorandTestnet
{
    internal class Program
    {
        private static DefaultApi algodApiInstance;
        private static Algorand.KMD.Api kmdApi;
        internal static Account creator, user, user2;

        static async Task Main(string[] args)
        {
            //The Accounts we'll be needing

            try { 


            //make a connection to the Algod node
            SetUpAlgodConnection();

            //Set up accounts based on mnemonics, and create a connection to Algod
            await SetUpAccounts();

            ulong? appId = await DeployHelloWorldApp(creator);
            Console.WriteLine(appId.Value);

            HelloWorldProxy helloWorldProxy = new HelloWorldProxy(algodApiInstance, appId.Value);

            int firstValue, secondValue;

            Console.WriteLine("Enter the first value:");
            firstValue = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter the second value:");
            secondValue = Convert.ToInt32(Console.ReadLine());

            var result = await helloWorldProxy.Addition(creator, 1000, firstValue, secondValue, "", null);
            Console.WriteLine("Addition : " + result);
            }
            catch (Exception e) {
                Console.WriteLine(e.ToString());
                Console.WriteLine(e.Message);
            }
        }

     
        private static void SetUpAlgodConnection()
        {
            //A standard sandbox connection
            var httpClient = HttpClientConfigurator.ConfigureHttpClient(@"https://testnet-idx.algonode.cloud", "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");
            algodApiInstance = new DefaultApi(httpClient);

            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("X-KMD-API-Token", "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");
            kmdApi = new Api(client);
            kmdApi.BaseUrl = @"https://testnet-idx.algonode.cloud";
        }

        private static async Task SetUpAccounts()
        {
            //var accounts = await getDefaultWallet();

            //get accounts based on the above private keys using the .NET SDK
           /* creator = accounts[0];
            user = accounts[1];
            user2 = accounts[2];*/

            string creatorMnemonic = "banner enlist wide have awake rail resource antique arch tonight pilot abuse file metal canvas beyond antique apart giant once slight ice beef able uncle";
            creator = new Account(creatorMnemonic);
        }
        private static async Task<ulong?> DeployHelloWorldApp(Account creator)
        {
            var helloworld = new HelloWorld.HelloWorld();
            var helloWorldApp = await helloworld.Deploy(creator, algodApiInstance);
            return helloWorldApp;
        }
    }
}