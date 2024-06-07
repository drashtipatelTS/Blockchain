using Algorand;
using Algorand.Algod;
using Algorand.Algod.Model;
using Algorand.KMD;
using AlgorandTest.Proxies;
using AlgoStudio.Clients;
using Proxies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorandTest
{
    public class Testnet
    {
        private static DefaultApi algodApiInstance;
        private static Algorand.KMD.Api kmdApi;
        internal static Account creator, user, user2;

        public async Task Run()
        {
            // The Accounts we'll be needing
            SetUpAlgodConnection();

            // Set up accounts based on mnemonics, and create a connection to Algod
            SetUpAccountsAsync();

            ulong? appId = await DeployHelloWorldApp(creator);
            Console.WriteLine(appId.Value);

            HelloWorldProxy helloWorldProxy = new HelloWorldProxy(algodApiInstance, appId.Value);

            int firstValue, secondValue;

            Console.WriteLine("Enter the first value:");
            firstValue = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter the second value:");
            secondValue = Convert.ToInt32(Console.ReadLine());

            var result = await helloWorldProxy.Addition(creator, 1000,firstValue, secondValue,"",null);
            Console.WriteLine("Addition : "+result);
        }

        private static async Task<ulong?> DeployHelloWorldApp(Account creator)
        {
            var helloworld = new HelloWorld.HelloWorld();
            var helloWorldApp = await helloworld.Deploy(creator, algodApiInstance);
            return helloWorldApp;
        }

        private static async Task<ulong?> DeployRemoveDupsApp(Account creator)
        {
            // Deploy the RemoveDuplicateBytes contract: this contract removes sequences from strings
            var removeDups = new RemoveDuplicateBytes.RemoveDuplicateBytes();
            var removeDupsApp = await removeDups.Deploy(creator, algodApiInstance);
            return removeDupsApp;
        }

        private static void SetUpAlgodConnection()
        {

            //Connect to Algorand Testnet
            var httpClient = HttpClientConfigurator.ConfigureHttpClient(@"https://testnet-idx.algonode.cloud", "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");
            algodApiInstance = new DefaultApi(httpClient);

            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("X-KMD-API-Token", "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");
            kmdApi = new Api(client);
            kmdApi.BaseUrl = @"https://testnet-idx.algonode.cloud";
        }

        private static async Task SetUpAccountsAsync()
        {

            // Replace these mnemonics with your testnet account mnemonics
            string creatorMnemonic = "banner enlist wide have awake rail resource antique arch tonight pilot abuse file metal canvas beyond antique apart giant once slight ice beef able uncle";
           // string userMnemonic = "oak degree whale sugar happy exhibit crazy sense stable pizza scan pizza cake treat suit process spider miss clutch much minor alone slot fancy about cause";

            creator = new Account(creatorMnemonic);
            //user = new Account(userMnemonic);
        }

    }
}
