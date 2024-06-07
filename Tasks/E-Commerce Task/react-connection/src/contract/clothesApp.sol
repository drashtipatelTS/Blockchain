// SPDX-License-Identifier: GPL-3.0

pragma solidity >=0.8.2 <0.9.0;

contract clothingApp {

    mapping (uint => bool) public existedid;
    
    struct cloths{
        uint id;
        string name;
        uint price;
        uint quantity;
        uint total;
    }

    cloths[] public clt;

    event clothsData(uint id,string name,uint price,uint quantity,uint total);

    function addClothes(uint _id,string memory _name,uint _price, uint _quantity) public{
        require(!existedid[_id],"Id already exists");
        clt.push(cloths(_id,_name,_price,_quantity,_quantity*_price));
        existedid[_id] = true;  
        emit clothsData(_id,_name,_price,_quantity,_quantity*_price);    
    }
    function getClothes() public view returns(cloths[] memory){
        cloths[] memory newclt = new cloths[](clt.length);
        for (uint i=0; i < clt.length; i++)
        {
            newclt[i]=clt[i];
        }
        return newclt;
    }

}