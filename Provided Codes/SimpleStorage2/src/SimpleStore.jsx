import React, { useEffect, useState } from "react";
import { ethers } from "ethers";
import SimpleStorage_abi from "./contracts/SimpleStorage_abi.json";

const SimpleStorage = () => {
  let contractAddress = "0x68d556d7E19F7A0F00098df9BB710244fde7630A";

  const [errorMessage, setErrorMessage] = useState(null);
  const [defaultAccount, setDefaultAccount] = useState(null);
  const [connButtonText, setConnButtonText] = useState("Connect Wallet");

  const [currentContractVal, setCurrentContractVal] = useState(null);

  const [provider, setProvider] = useState(null);
  const [signer, setSigner] = useState(null);
  const [contract, setContract] = useState(null);

  const [id, setId] = useState("");
  const [name, setName] = useState("");
  const [price, setPrice] = useState("");
  const [quantity, setQuantity] = useState("");

  const connectWalletHandler = () => {
    if (window.ethereum && window.ethereum.isMetaMask) {
      window.ethereum
        .request({ method: "eth_requestAccounts" })
        .then((result) => {
          accountChangedHandler(result[0]);
          setConnButtonText("Wallet Connected");
        })
        .catch((error) => {
          setErrorMessage(error.message);
        });
    } else {
      console.log("Need to install MetaMask");
      setErrorMessage("Please install MetaMask browser extension to interact");
    }
  };

  const accountChangedHandler = (newAccount) => {
    setDefaultAccount(newAccount);
    updateEthers();
  };

  const chainChangedHandler = () => {
    window.location.reload();
  };


  useEffect(() => {
    

    if (window.ethereum) {
      window.ethereum.on("accountsChanged", accountChangedHandler);
      window.ethereum.on("chainChanged", chainChangedHandler);
    }

    return () => {
      // Cleanup: Unsubscribe event listeners when component unmounts
      if (window.ethereum) {
        window.ethereum.removeListener("accountsChanged", accountChangedHandler);
        window.ethereum.removeListener("chainChanged", chainChangedHandler);
      }
    };
  },[]); 

  const updateEthers = () => {
    let tempProvider = new ethers.providers.Web3Provider(window.ethereum);
    setProvider(tempProvider);

    let tempSigner = tempProvider.getSigner();
    setSigner(tempSigner);

    let tempContract = new ethers.Contract(
      contractAddress,
      SimpleStorage_abi,
      tempSigner
    );
    setContract(tempContract);
  };

  // const setHandler = (event) => {
  //   event.preventDefault();
  //   console.log("sending " + event.target.setNum.value + " to the contract");
  //   contract.set(event.target.setNum.value);
  // };
  const setHandler = (event) => {
    event.preventDefault();
    // var id = event.target.id.value;
    // var name=event.target.name.value;
    // var price=event.target.price.value;
    // var quantity=event.target.quantity.value;
    const total = price * quantity;
    contract.addClothes(id,name,price,quantity);
    console.log(id,name,price,quantity);
  };

  const getCurrentVal = async () => {
     let val = await contract.getClothes();
     setCurrentContractVal(val.toString());
    console.log("Current Value:" + val);
  };

  return (
    <div>
      <h4> {"Get/Set Contract interaction"} </h4>
      <button onClick={connectWalletHandler}>{connButtonText}</button>
      <div>
        <h3>Address: {defaultAccount}</h3>
      </div>
      <form onSubmit={setHandler}>
        {/* <input id="setNum" type="text" />
        <button type={"submit"}> Set Value </button> */}
         <input id="id" type="text" placeholder="ID" value={id} onChange={(e) => setId(e.target.value)}/><br/>
            <input id="name" type="text" placeholder="Name" value={name} onChange={(e) => setName(e.target.value) }/><br/>
            <input id="price" type="text" placeholder="Price" value={price} onChange={(e) => setPrice(e.target.value) }/><br/>
            <input id="quantity" type="text" placeholder="Quantity" value={quantity} onChange={(e) => setQuantity(e.target.value) } /><br/>
            <input id="quantity" readOnly type="text" placeholder="Total" value={price*quantity}/><br/>
            <button type={"submit"}> Add Clothes </button>
      </form>
      <div>
        <button onClick={getCurrentVal} style={{ marginTop: "5em" }}>
          {" "}
          Get Value{" "}
        </button>
      </div>
      {currentContractVal}
      {errorMessage}
    </div>
  );
};

export default SimpleStorage;
