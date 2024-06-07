// SPDX-License-Identifier: GPL-3.0

pragma solidity >=0.8.2 <0.9.0;
import "@openzeppelin/contracts/token/ERC721/ERC721.sol";
import "@openzeppelin/contracts/token/ERC721/extensions/ERC721URIStorage.sol";
import "@openzeppelin/contracts/token/ERC721/IERC721Receiver.sol";
import "@openzeppelin/contracts/utils/Counters.sol";
import "@openzeppelin/contracts/access/Ownable.sol";

contract NFTPausable is ERC721URIStorage, Ownable{
    using  Counters for Counters.Counter;
    Counters.Counter private _tokenIds;
    
    mapping (string => uint8) hashes;
    uint public _currentTime;
    bool private _paused;

    constructor() ERC721("NFTPausable","NFTP") Ownable(msg.sender){}

    function awardItem(address recipient,string calldata hash,string calldata metadata)public whenNotPaused returns (uint){
            require(hashes[hash]!=1);
            hashes[hash] = 1;
            _tokenIds.increment();
            uint newItemId = _tokenIds.current();
            _currentTime = block.timestamp;
            _mint(recipient, newItemId);
            _setTokenURI(newItemId, metadata);
            return newItemId;
        }

    function pause() external onlyOwner {
        _paused = true;
    }

    function unpause() external onlyOwner {
        _paused = false;
    }

    function isPaused() public view returns (bool) {
        return _paused;
    }

    modifier whenNotPaused() {
        require(!_paused, "Contract is paused");
        _;
    }
}