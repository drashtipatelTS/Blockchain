const {ethers, JsonRpcProvider} = require('ethers');

//const node = 'https://crimson-special-grass.discover.quiknode.pro/de3f51844add6881b678aa315fb96c2e65a09d37/'; 
const node = 'https://multi-cool-sponge.matic-amoy.quiknode.pro/18f893fa05496edfc6729928848044add7ffb4cb/';

const provider = new JsonRpcProvider(node);

async function main() {
    const blcokNumber = await provider.getBlockNumber();
    console.log(blcokNumber);

    const block = await provider.getBlock(blcokNumber);
    
    //console.log(block);
    console.log(block.hash);
    console.log(block.miner);
    console.log(block.timestamp);
    }

main();

