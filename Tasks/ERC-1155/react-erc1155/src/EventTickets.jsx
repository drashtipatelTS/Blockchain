import React, { useState, useEffect } from "react";
import ticketAbi from "./contracts/ticketAbi.json";
const ethers = require("ethers");

// const node = 'https://multi-cool-sponge.matic-amoy.quiknode.pro/18f893fa05496edfc6729928848044add7ffb4cb/';
const CONTRACT_ADDR = "0xA60A1eFf0Ff451b32d60B4BE2e25a0f48c0e7Abf";
// const provider = new ethers.providers.JsonRpcProvider(node);
const provider = new ethers.providers.Web3Provider(window.ethereum);
const contract = new ethers.Contract(CONTRACT_ADDR, ticketAbi, provider);
const signer = provider.getSigner();
const contractWithSigner = new ethers.Contract(
  CONTRACT_ADDR,
  ticketAbi,
  signer
);

const TicketingApp = () => {
  const [errorMessage, setErrorMessage] = useState(null);
  const [defaultAccount, setDefaultAccount] = useState(null);
  const [connButtonText, setConnButtonText] = useState("Connect Wallet");

  //   const [provider, setProvider] = useState(null);
  //   const [contract, setContract] = useState(null);
  const [eventId, setEventId] = useState("");
  //   const [ticketAmount, setTicketAmount] = useState(0);
  const [message, setMessage] = useState("");
  const [eventName, setEventName] = useState("");
  const [date, setDate] = useState("");
  const [venue, setVenue] = useState("");
  const [price, setPrice] = useState("");
  const [to, setTo = useState] = useState("");
  // const [data, setData] = useState('');
  const [buyerName, setbuyerName] = useState("");
  const [quantity, setQuantity] = useState("");
  const [events, setEvents] = useState([]);
  const [buyerDetails, setBuyerDetails] = useState([]);
  // const[buyer,setBuyer] = useState('');

  // const [totalPrice, setTotalPrice = useState]=useState('');
  // const [discountThreshold, setDiscountThreshold] = useState('');
  // const [discountPercentage, setDiscountPercentage] = useState('');

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
          console.log(errorMessage);
        });
    } else {
      console.log("Need to install MetaMask");
      setErrorMessage("Please install MetaMask browser extension to interact");
    }
  };
  const accountChangedHandler = (newAccount) => {
    setDefaultAccount(newAccount);
    //  updateEthers();
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
        window.ethereum.removeListener(
          "accountsChanged",
          accountChangedHandler
        );
        window.ethereum.removeListener("chainChanged", chainChangedHandler);
      }
    };
  }, []);

  const createEvent = async () => {
    try {
      if (!defaultAccount) {
        throw new Error("Please connect your wallet first");
      }
      // const provider = new ethers.providers.JsonRpcProvider(node);
      await contractWithSigner.createEvent(
        to,
        eventId,
        eventName,
        date,
        venue,
        price
      );
      contract.on("NewEvent", (to, eventId, eventName, date, venue, price) => {
        console.log("Event created successfully!");
        console.log("Event Details:");
        console.log("Event ID:", eventId);
        console.log("Event Name:", eventName);
        console.log("Date:", date);
        console.log("Venue:", venue);
        console.log("price:", price.toString());
        console.log("To:", to);
        // console.log('Data:', data);
      });
      console.log("Event created successfully!");
    } catch (error) {
      console.error("Error creating event:", error);
    }
  };
  const getCurrentVal = async () => {
    let val = await contract.getAllEvents();
    setEvents(val);
    console.log("Current Value:" + val);
  };
  const buyEvent = async () => {
    try {
      contract.on("EventBought", (eventId, buyerName, quantity, totalPrice) => {
        console.log(`Tickets purchased by ${buyerName}`);
        console.log(`Event:${eventId}`);
        console.log(`quantity:${quantity}`);
        console.log(`price:${totalPrice}`);
      });
      await contractWithSigner.buyEvent(eventId, buyerName, quantity);
      //   console.log(totalPrice);
      setMessage("Tickets purchased successfully!");
    } catch (error) {
      console.error("Error buying tickets:", error);
      setMessage("Error buying tickets.");
    }
  };

  const getBuyerById = async () => {
    try {
      if (!defaultAccount) {
        throw new Error("Please connect your wallet first");
      }
      await contractWithSigner.getBuyer(eventId);
      contract.on("BuyerDetails", (eventId, buyerName, quantity, totalPrice) => {
        console.log("Event ID:", eventId.toString());
        console.log("Buyername:", buyerName);
        console.log("quantity:", quantity.toString());
        console.log("TotalPrice:", totalPrice.toString());
        setBuyerDetails([{ eventId, buyerName, quantity, totalPrice }]);
      });
    } catch (error) {
      console.error("Error while getting event by id:", error);
    }
  };
  //   const transferTickets = async () => {
  //     try {
  //         contract.on("TicketsTransferred", (from, to, eventId, amount) => {
  //             setMessage(`Tickets transferred from ${from} to ${to} for event ${eventId}: ${amount} tickets`);
  //         });
  //         await contractWithSigner.transferEventTicket(fromAddress, toAddress, eventId, amount);
  //         setMessage("Transfer initiated successfully!");
  //     } catch (error) {
  //         console.error("Error transferring tickets:", error);
  //         setMessage("Error transferring tickets.");
  //     }
  // };
  // const withdrawBalance = async () => {
  //     try {
  //         contract.on("Withdrawal", (owner, amount) => {
  //             setMessage(`Withdrawal event detected! Owner: ${owner}, Amount: ${amount}`);
  //         });
  //         await contractWithSigner.withdraw();
  //         setMessage("Withdrawal successful!");
  //     } catch (error) {
  //         console.error("Error withdrawing balance:", error);
  //         setMessage("Error withdrawing balance.");
  //     }
  // };

  return (
    <div>
        <div>
        <button onClick={connectWalletHandler}>{connButtonText}</button>
      </div>
      <div>
        <h3>Address: {defaultAccount}</h3>
      </div>
      <h1>Create Event</h1>
      <label>
        To Address:
        <input type="text" value={to} onChange={(e) => setTo(e.target.value)} />
      </label>
      <label></label>
      <label>
        Event ID:
        <input
          type="text"
          value={eventId}
          onChange={(e) => setEventId(e.target.value)}
        />
      </label>
      <label>
        Event Name:
        <input
          type="text"
          value={eventName}
          onChange={(e) => setEventName(e.target.value)}
        />
      </label>
      <label>
        Date:
        <input
          type="text"
          value={date}
          onChange={(e) => setDate(e.target.value)}
        />
      </label>
      <label>
        Venue:
        <input
          type="text"
          value={venue}
          onChange={(e) => setVenue(e.target.value)}
        />
      </label>
      <label>
        Price:
        <input
          type="text"
          value={price}
          onChange={(e) => setPrice(e.target.value)}
        />
      </label>
      {/* <label>
            Data:
                <input type="text" value={data} onChange={(e) => setData(e.target.value)} />
            </label> */}
      <button onClick={createEvent}>Create Event</button>
      <h1>Buy a Ticket</h1>
      <div>{message}</div>
      <label>
        Event ID:
        <input
          type="number"
          value={eventId}
          onChange={(e) => setEventId(e.target.value)}
        />
      </label>
      <label>
        Buyer Name:
        <input
          type="text"
          value={buyerName}
          onChange={(e) => setbuyerName(e.target.value)}
        />
      </label>
      <label>
        Quantity:
        <input
          type="number"
          value={quantity}
          onChange={(e) => setQuantity(e.target.value)}
        />
      </label>
      {/* <label>
        Total Price:
        <input
          type="number"
          value={totalPrice}
          onChange={(e) => setTotalPrice(e.target.value)}
        />
      </label> */}
      <button onClick={buyEvent}>Buy Tickets</button>
      {/* <h1>Ticket Transfer From App</h1>
            <label>
                From Address:
                <input type="text" value={fromAddress} onChange={(e) => setFromAddress(e.target.value)} />
            </label>
            <label>
                To Address:
                <input type="text" value={toAddress} onChange={(e) => setToAddress(e.target.value)} />
            </label>
            <label>
                Event ID:
                <input type="number" value={eventId} onChange={(e) => setEventId(e.target.value)} />
            </label>
            <label>
                Amount:
                <input type="number" value={amount} onChange={(e) => setAmount(e.target.value)} />
            </label>
            <button onClick={transferTickets}>Transfer Tickets</button>

            <div>
            <h1>Withdrawal from Ticketing App</h1>
            <button onClick={withdrawBalance}>Withdraw Balance</button>
            <div>{message}</div>
            <div>Default Account: {defaultAccount}</div>
        </div> */}

      <div>
        <div className="border rounded mx-5 my-3 table-responsive">
          <i>
            <h2 className="my-3">Get Event</h2>
          </i>
          <button onClick={getCurrentVal} className="btn btn-primary">
            Get All Events
          </button>
          <table
            className="table table-striped mx-5 my-3"
            style={{ border: "1px solid black" }}
          >
            <thead>
              <tr>
                <th>Organizer</th>
                <th>Event ID</th>
                <th>Event Name</th>
                <th>Date</th>
                <th>venue</th>
                <th>Ticket Price</th>
              </tr>
            </thead>
            <tbody>
              {events.map((event, index) => (
                <tr key={index}>
                  <td>{event.organizer.toString()}</td>
                  <td>{event.eventId.toString()}</td>
                  <td>{event.eventName}</td>
                  <td>{event.date}</td>
                  <td>{event.venue}</td>
                  <td>{event.price.toString()}</td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      </div>
      <h1>Get Buyer Details of Specific Event</h1>
      <label>
        Event ID:
        <input
          type="text"
          value={eventId}
          onChange={(e) => setEventId(e.target.value)}
        />
      </label>
      <button onClick={getBuyerById}>Get Buyer Details</button>
      <h2>Buyer Details</h2>
      <ul>
        {buyerDetails.map((buyer, index) => (
          <li key={index}>
            <strong>Event ID:</strong> {buyer.eventId.toString()} <br />
            <strong>Buyer Name:</strong> {buyer.buyerName} <br />
            <strong>Quantity:</strong> {buyer.quantity.toString()} <br />
            <strong>Total Price:</strong> {buyer.totalPrice.toString()}
          </li>
        ))}
      </ul>
    </div>
  );
};

export default TicketingApp;
