#Testnet
import logging
import algokit_utils
from algosdk.v2client.algod import AlgodClient
from algosdk.v2client.indexer import IndexerClient
from algosdk import mnemonic, account

logger = logging.getLogger(__name__)

# Define the TestNet endpoints and API key
algod_address = "https://testnet-api.algonode.cloud"
indexer_address = "https://testnet-idx.algonode.cloud"
api_key = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"  

headers = {
    "X-API-Key": api_key,
}

# Initialize the clients
algod_client = AlgodClient(algod_token="", algod_address=algod_address, headers=headers)
indexer_client = IndexerClient(indexer_token="", indexer_address=indexer_address, headers=headers)

# Ensure you have a funded TestNet account as the deployer
deployer_mnemonic = "banner enlist wide have awake rail resource antique arch tonight pilot abuse file metal canvas beyond antique apart giant once slight ice beef able uncle"  # Replace with your TestNet account mnemonic

# Get the private key and address from the mnemonic
deployer_private_key = mnemonic.to_private_key(deployer_mnemonic)
print("-------------------------------Deployer Private Key: "+deployer_private_key)
deployer_address = account.address_from_private_key(deployer_private_key)
print("-------------------------------Deployer Address: "+deployer_address)

# Create an algokit_utils.Account object
deployer = algokit_utils.Account(private_key=deployer_private_key, address=deployer_address)

# Define deployment behaviour based on supplied app spec
def deploy(
    algod_client: AlgodClient,
    indexer_client: IndexerClient,
    app_spec: algokit_utils.ApplicationSpecification,
    deployer: algokit_utils.Account,
) -> None:
    from smart_contracts.artifacts.hello_world.client import (
        HelloWorldClient,
    )

    app_client = HelloWorldClient(
        algod_client,
        creator=deployer,
        indexer_client=indexer_client,
    )

    app_client.deploy(
        on_schema_break=algokit_utils.OnSchemaBreak.AppendApp,
        on_update=algokit_utils.OnUpdate.AppendApp,
    )
    name = "world"
    response = app_client.hello(name=name)
    logger.info(
        f"Called hello on {app_spec.contract.name} ({app_client.app_id}) "
        f"with name={name}, received: {response.return_value}"
    )

    a = 300
    b = 100
    response = app_client.addition(a=a, b=b)
    logger.info(
        f"Called Addition on {app_spec.contract.name} ({app_client.app_id}) "
        f"with Values ={a,b}, received: {response.return_value}"
    )

# Function to fetch transactions for an address
'''def fetch_transactions(address: str):
    response = indexer_client.search_transactions_by_address(address)
    return response.get('transactions', [])

# Fetch transactions for the deployer address
transactions = fetch_transactions(deployer_address)
for tx in transactions:
    print("--------------------------------Transaction:")
    print(tx)'''