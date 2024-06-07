using Algorand;
using Algorand.Algod;
using Algorand.Algod.Model;
using Algorand.Algod.Model.Transactions;
using Algorand.KMD;
using AlgorandTest.Proxies;
using AlgoStudio.Clients;
using Proxies;
using System.Text;

namespace AlgorandTest
{
    internal class Program
    {
        private static DefaultApi algodApiInstance;
        private static Algorand.KMD.Api kmdApi;
        private const string walletName = "unencrypted-default-wallet";
        internal static Account creator, user, user2;

        static async Task Main(string[] args)
        {
            //The Accounts we'll be needing


            //make a connection to the Algod node
            SetUpAlgodConnection();

            //Set up accounts based on mnemonics, and create a connection to Algod
            await SetUpAccounts();



            //Deploy smart contracts to the network:
            ulong? removeDupsApp = await DeployRemoveDupsApp(creator);
            Console.WriteLine("removeDupsApp: "+ removeDupsApp.Value.ToString());

            ulong? helloworld = await DeployHelloWorldApp(creator);
            Console.WriteLine("helloworld: " + helloworld.Value.ToString());

            //Instantiate proxies to the contracts

            RemoveDuplicateBytesProxy removeDuplicateBytesProxy = new RemoveDuplicateBytesProxy(algodApiInstance, removeDupsApp.Value);
            HelloWorldProxy helloWorldProxy = new HelloWorldProxy(algodApiInstance, helloworld.Value);


            //demonstrate using a contract to remove character sequences from a string
            string redundantString = "AaaBbb";
            var removeDuplicateResult = await removeDuplicateBytesProxy.Dedup(creator, null, Encoding.UTF8.GetBytes(redundantString), "Remove dups");
            string removeDuplicateResultString = Encoding.UTF8.GetString(removeDuplicateResult);

            //var str = "";
            var Result = await helloWorldProxy.Helloworld(creator,2000,"",null);
            Console.WriteLine(Result);

            var sumRes = await helloWorldProxy.Addition(creator, 1000, 10, 20, "", null);
            Console.WriteLine("Addition of 10 and 20 is: "+sumRes);

            Console.WriteLine($"The result of de-duplicating sequenced characters from {redundantString} is {removeDuplicateResultString}");
            Console.WriteLine();
        }

        private static async Task<ulong?> DeployHelloWorldApp(Account creator)
        {
            var helloworld = new HelloWorld.HelloWorld();
            var helloWorldApp = await helloworld.Deploy(creator, algodApiInstance);
            return helloWorldApp;
        }

        private static async Task<ulong?> DeployRemoveDupsApp(Account creator)
        {
            //deploy the RemoveDuplicateBytes contract: this contract removes sequences from strings
            var removeDups = new RemoveDuplicateBytes.RemoveDuplicateBytes();
            var removeDupsApp = await removeDups.Deploy(creator, algodApiInstance);
            return removeDupsApp;
        }

        private static void SetUpAlgodConnection()
        {
            //A standard sandbox connection
            var httpClient = HttpClientConfigurator.ConfigureHttpClient(@"http://localhost:4001", "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");
            algodApiInstance = new DefaultApi(httpClient);

            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("X-KMD-API-Token", "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");
            kmdApi = new Api(client);
            kmdApi.BaseUrl = @"http://localhost:4002";


        }

        private static async Task SetUpAccounts()
        {
            var accounts = await getDefaultWallet();

            //get accounts based on the above private keys using the .NET SDK
            creator = accounts[0];
            user = accounts[1];
            user2 = accounts[2];
        }

        private static async Task<List<Account>> getDefaultWallet()
        {

            string handle = await getWalletHandleToken();
            var accs = await kmdApi.ListKeysInWalletAsync(new Algorand.KMD.ListKeysRequest() { Wallet_handle_token = handle });
            if (accs.Addresses.Count < 3) throw new Exception("Sandbox should offer minimum of 3 demo accounts.");

            List<Account> accounts = new List<Account>();
            foreach (var a in accs.Addresses)
            {
                var resp = await kmdApi.ExportKeyAsync(new ExportKeyRequest() { Address = a, Wallet_handle_token = handle, Wallet_password = "" });
                Account account = new Account(resp.Private_key);
                accounts.Add(account);
            }
            return accounts;

        }

        private static async Task<string> getWalletHandleToken()
        {
            var wallets = await kmdApi.ListWalletsAsync(null);
            var wallet = wallets.Wallets.Where(w => w.Name == walletName).FirstOrDefault();
            var handle = await kmdApi.InitWalletHandleTokenAsync(new InitWalletHandleTokenRequest() { Wallet_id = wallet.Id, Wallet_password = "" });
            return handle.Wallet_handle_token;
        }
    }
}