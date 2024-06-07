const {ethers, JsonRpcProvider} = require('ethers');

//const node = 'https://crimson-special-grass.discover.quiknode.pro/de3f51844add6881b678aa315fb96c2e65a09d37/'; 
const node = 'https://multi-cool-sponge.matic-amoy.quiknode.pro/18f893fa05496edfc6729928848044add7ffb4cb/'; 

const provider = new JsonRpcProvider(node);

async function main() {
    
    //const balance = await provider.getBalance('0x95222290DD7278Aa3Ddd389Cc1E1d165CC4BAfe5');
    const balance = await provider.getBalance('0xbeD73Caa46F4b25726c1130a4C1311f7A4419981');
    //const balance = await provider.getBalance('vitalik.eth');

    console.log(ethers.formatEther(balance));
    }

main();

