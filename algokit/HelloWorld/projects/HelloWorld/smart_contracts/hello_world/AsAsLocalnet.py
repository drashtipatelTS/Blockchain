from algosdk import account, mnemonic, encoding, algod, transaction
import json
import logging

# Algorand API configuration for LocalNet
algod_address = "http://localhost:8980"  # Replace port if your node is running on a different port
algod_token = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"  # Replace with your localnet token if needed
algod_client = algod.AlgodClient(algod_token, algod_address)

logging.info(f"algo client:- {algod_client}")
    
# Function to create an ASA
def create_asset(asset_name, asset_unit_name):
    # Create a new account
    creator_private_key, creator_address = "4IPITRVNW4IHO7ZUGFCIS45DJESC6XEDJ5HSQPBH73NT4HJNSNVK4LBXEM"

    params = algod_client.suggested_params()
    txn = transaction.AssetConfigTxn(
        sender=creator_address,
        sp=params,
        total=1000,  # Total supply of your asset
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

# Create an asset
asset_name = "DrashtiAsset"
asset_unit_name = "DP"
asset_id = create_asset(asset_name, asset_unit_name)

logging.info(f"Asset created with ID: {asset_id}")
