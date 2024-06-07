const {ethers, JsonRpcProvider} = require('ethers');

//const node = 'https://icy-virulent-diamond.matic-testnet.discover.quiknode.pro/31f37d091c1d4e85dc4c9d9cba66b1274a03d3c3/'; 
const node = 'https://multi-cool-sponge.matic-amoy.quiknode.pro/18f893fa05496edfc6729928848044add7ffb4cb/'; 

//const node = 'wss://20.58.166.39/8545';
const provider = new JsonRpcProvider(node);

const tokenAdress = '0x502124Ff39D51968708Cd42C57B3d6aEcD9DEeE1'; //shiba enu 0x95aD61b0a150d79219dCF64E1E6Cc01f0B64C4cE
//const tokenAddress = '0xa0b86991c6218b36c1d19d4a2e9eb0ce3606eb48'; //USDC
const tokenAbi = 
	// {
	// 	"inputs": [],
	// 	"stateMutability": "nonpayable",
	// 	"type": "constructor"
	// },
	// {
	// 	"anonymous": false,
	// 	"inputs": [
	// 		{
	// 			"indexed": false,
	// 			"internalType": "address",
	// 			"name": "owner",
	// 			"type": "address"
	// 		},
	// 		{
	// 			"components": [
	// 				{
	// 					"internalType": "uint256",
	// 					"name": "age",
	// 					"type": "uint256"
	// 				},
	// 				{
	// 					"internalType": "string",
	// 					"name": "name",
	// 					"type": "string"
	// 				}
	// 			],
	// 			"indexed": false,
	// 			"internalType": "struct Events.Person",
	// 			"name": "p2",
	// 			"type": "tuple"
	// 		},
	// 		{
	// 			"indexed": false,
	// 			"internalType": "uint256",
	// 			"name": "timestamp",
	// 			"type": "uint256"
	// 		}
	// 	],
	// 	"name": "newPerson",
	// 	"type": "event"
	// },
	// {
	// 	"inputs": [
	// 		{
	// 			"internalType": "uint256",
	// 			"name": "_age",
	// 			"type": "uint256"
	// 		},
	// 		{
	// 			"internalType": "string",
	// 			"name": "_name",
	// 			"type": "string"
	// 		}
	// 	],
	// 	"name": "setPerson",
	// 	"outputs": [],
	// 	"stateMutability": "nonpayable",
	// 	"type": "function"
	// },
	// {
	// 	"inputs": [],
	// 	"name": "getPerson",
	// 	"outputs": [
	// 		{
	// 			"components": [
	// 				{
	// 					"internalType": "uint256",
	// 					"name": "age",
	// 					"type": "uint256"
	// 				},
	// 				{
	// 					"internalType": "string",
	// 					"name": "name",
	// 					"type": "string"
	// 				}
	// 			],
	// 			"internalType": "struct Events.Person",
	// 			"name": "",
	// 			"type": "tuple"
	// 		}
	// 	],
	// 	"stateMutability": "view",
	// 	"type": "function"
	// }
	[
		{
			"inputs": [
				{
					"internalType": "uint256",
					"name": "_id",
					"type": "uint256"
				},
				{
					"internalType": "string",
					"name": "_name",
					"type": "string"
				},
				{
					"internalType": "uint256",
					"name": "_price",
					"type": "uint256"
				},
				{
					"internalType": "uint256",
					"name": "_quantity",
					"type": "uint256"
				}
			],
			"name": "addClothes",
			"outputs": [],
			"stateMutability": "nonpayable",
			"type": "function"
		},
		{
			"anonymous": false,
			"inputs": [
				{
					"indexed": true,
					"internalType": "address",
					"name": "sender",
					"type": "address"
				},
				{
					"components": [
						{
							"internalType": "uint256",
							"name": "id",
							"type": "uint256"
						},
						{
							"internalType": "string",
							"name": "name",
							"type": "string"
						},
						{
							"internalType": "uint256",
							"name": "price",
							"type": "uint256"
						},
						{
							"internalType": "uint256",
							"name": "quantity",
							"type": "uint256"
						},
						{
							"internalType": "uint256",
							"name": "total",
							"type": "uint256"
						}
					],
					"indexed": false,
					"internalType": "struct clothingApp.cloths[]",
					"name": "data",
					"type": "tuple[]"
				}
			],
			"name": "clothsData",
			"type": "event"
		},
		{
			"inputs": [],
			"name": "getClothes",
			"outputs": [
				{
					"components": [
						{
							"internalType": "uint256",
							"name": "id",
							"type": "uint256"
						},
						{
							"internalType": "string",
							"name": "name",
							"type": "string"
						},
						{
							"internalType": "uint256",
							"name": "price",
							"type": "uint256"
						},
						{
							"internalType": "uint256",
							"name": "quantity",
							"type": "uint256"
						},
						{
							"internalType": "uint256",
							"name": "total",
							"type": "uint256"
						}
					],
					"internalType": "struct clothingApp.cloths[]",
					"name": "",
					"type": "tuple[]"
				}
			],
			"stateMutability": "nonpayable",
			"type": "function"
		},
		{
			"inputs": [
				{
					"internalType": "uint256",
					"name": "",
					"type": "uint256"
				}
			],
			"name": "clt",
			"outputs": [
				{
					"internalType": "uint256",
					"name": "id",
					"type": "uint256"
				},
				{
					"internalType": "string",
					"name": "name",
					"type": "string"
				},
				{
					"internalType": "uint256",
					"name": "price",
					"type": "uint256"
				},
				{
					"internalType": "uint256",
					"name": "quantity",
					"type": "uint256"
				},
				{
					"internalType": "uint256",
					"name": "total",
					"type": "uint256"
				}
			],
			"stateMutability": "view",
			"type": "function"
		},
		{
			"inputs": [
				{
					"internalType": "uint256",
					"name": "",
					"type": "uint256"
				}
			],
			"name": "existedid",
			"outputs": [
				{
					"internalType": "bool",
					"name": "",
					"type": "bool"
				}
			],
			"stateMutability": "view",
			"type": "function"
		}
	];
const contract = new ethers.Contract(tokenAdress, tokenAbi, provider);

async function main() {

	try {
        const cloths = await contract.getClothes();
        console.log('Clothes:', cloths);
    } catch (error) {
        console.error('Error:', error);
    }
    
    // const cloths = await contract.getClothes();
	// console.log('Result:', cloths);
   
    // contract.on("clothsData",(from,to,data) => {
    
    //     console.log(from,to,data);
    
    // })
    
    }

main();

