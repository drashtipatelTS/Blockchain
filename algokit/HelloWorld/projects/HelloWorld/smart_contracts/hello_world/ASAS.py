from algosdk import account, mnemonic, encoding, algod, transaction
from algosdk.future import transaction # type: ignore
from algosdk.v2client import algod
import json
import logging


# Algorand API configuration
algod_token = "a" *64 
algod_address = "https://testnet-api.algonode.cloud"
algod_client = algod.AlgodClient(algod_token, algod_address)

logging.info(f"algo client:- {algod_client}")

# Replace the below values with your mnemonic and Algorand address
creator_mnemonic = "banner enlist wide have awake rail resource antique arch tonight pilot abuse file metal canvas beyond antique apart giant once slight ice beef able uncle"
creator_address = "N3WGSFVJRZ6UNRPCXUZGRQOTVQOLRLPKZILVMNWO7OYBUQM2DZBVHZEAUY"

# Recover the private key from the mnemonic
creator_private_key = mnemonic.to_private_key(creator_mnemonic)

# Function to create an ASA
def create_asset(asset_name, asset_unit_name, creator_private_key):
    params = algod_client.suggested_params()
    txn = transaction.AssetConfigTxn(
        sender=creator_address,
        sp=params,
        total=1000000,  # Total supply of your asset
        decimals=0,     # Decimals for your asset
        default_frozen=False,
        unit_name=asset_unit_name,
        asset_name=asset_name,
        manager=creator_address,
        reserve=creator_address,
        freeze=creator_address,
        clawback=creator_address,
        strict_empty_address_check=False
    )
    signed_txn = txn.sign(creator_private_key)
    tx_id = algod_client.send_transaction(signed_txn)
    logging.info(f"tx id :- {tx_id}")
    return tx_id

# Function to opt-in to an ASA
'''def opt_in_to_asset(asset_id, opt_in_private_key):
    params = algod_client.suggested_params()
    txn = transaction.AssetTransferTxn(
        sender=opt_in_private_key,
        sp=params,
        receiver=opt_in_private_key,
        amt=0,
        index=asset_id
    )
    signed_txn = txn.sign(opt_in_private_key)
    tx_id = algod_client.send_transaction(signed_txn)
    print("--------------------------Opt-in transaction ID:", tx_id)
    return tx_id'''

# Create an asset
asset_name = "DrashtiAsset"
asset_unit_name = "DP"
asset_id = create_asset(asset_name, asset_unit_name, creator_private_key)
# print("----------------------------Asset Id:"+asset_id)

# Opt-in to the asset using a different account
'''opt_in_mnemonic = "banner enlist wide have awake rail resource antique arch tonight pilot abuse file metal canvas beyond antique apart giant once slight ice beef able uncle"
opt_in_private_key = mnemonic.to_private_key(opt_in_mnemonic)
opt_in_to_asset(asset_id, opt_in_private_key)'''
