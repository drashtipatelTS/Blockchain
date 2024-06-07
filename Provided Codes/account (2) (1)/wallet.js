//private key - 0x4a8fba6399694c6b95558e141eaddd72194f0d6db2495b71445c4c452f5da319
//account - 0x29415ECf92d6D52FB8F5179c00cf9Ffb5E5870Cd
//0x4979E646B063125ACC8fF2f8d344d747FBeE2fE7  -   0x628c881a082a0887f45833ef176cd51986abcfdbde2cca8e116adf1abc29a5ca

const {ethers, JsonRpcProvider} = require('ethers');

//const node = 'https://thrumming-solitary-patron.ethereum-sepolia.discover.quiknode.pro/466a2a728481d5162ba231e0b6b9b5ef191825e8/';
const node = 'https://multi-cool-sponge.matic-amoy.quiknode.pro/18f893fa05496edfc6729928848044add7ffb4cb/'; 

const provider = new JsonRpcProvider(node);

const privateKey = '9499190c557914c76cd567fca500f094b31d11dc9bb775d9d175d1b74c462249';
const receiver = '0x0A1a25d47bF97b6B1d757203e7Cf513bBc44C435';

const wallet = new ethers.Wallet(privateKey,provider);
const amountTosend = '0.05';

const tx ={
    to: receiver,
    value: ethers.parseEther(amountTosend)
}

async function main() {

    //  
    
   /* const wallet = ethers.Wallet.createRandom();
    console.log(wallet.address);
    console.log(wallet.mnemonic);
    console.log(wallet.privateKey);
    console.log(wallet.signingKey);*/
    
    const balance = await provider.getBalance('0xbeD73Caa46F4b25726c1130a4C1311f7A4419981');
    const balance1 = await provider.getBalance('0x0A1a25d47bF97b6B1d757203e7Cf513bBc44C435');
    console.log(ethers.formatEther(balance) + " " + ethers.formatEther(balance1));

    wallet.sendTransaction(tx).then(txobj => console.log('txHash: ' , txobj.hash));

    console.log(ethers.formatEther(balance) + " " + ethers.formatEther(balance1));
    
    }

    

main();

