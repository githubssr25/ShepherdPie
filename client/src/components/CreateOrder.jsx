import { useState, useEffect } from "react";
import { getAllEmployees } from "../manager/EmployeeManager";
import { getAllUsers } from "../manager/userManager";
import { getAllCondiments } from "../manager/condimentManager";
import { createOrder } from "../manager/orderManager";


export const CreateOrder = () => {
  const [employees, setEmployees] = useState([]);
  const [users, setUsers] = useState([]);
  const [condiments, setCondiments] = useState([]);
  const [pizzaSizes] = useState([
    { size: "Large", basePrice: 15.0 },
    { size: "Medium", basePrice: 12.0 },
    { size: "Small", basePrice: 10.0 },
  ]);

  const [enteredTip, setEnteredTip] = useState(0);

  const [totalPizzas, setTotalPizzas] = useState([]);
  const [currentPizza, setCurrentPizza] = useState({
    size: "",
    basePrice: 0,
    selectedCondiments: [],
  });

  const [chosenUser, setChosenUser] = useState(null);
  const [chosenOrderTaker, setChosenOrderTaker] = useState(null);
  const [chosenDeliveryPerson, setChosenDeliveryPerson] = useState(null);


  useEffect(() => {
    const fetchData = async () => {
      try {
        console.log("Fetching employees, users, and condiments...");
        const fetchedEmployees = await getAllEmployees();
        const fetchedUsers = await getAllUsers();
        const fetchedCondiments = await getAllCondiments();

        setEmployees(fetchedEmployees);
        setUsers(fetchedUsers);
        setCondiments(fetchedCondiments);

        console.log("Fetched employees:", fetchedEmployees);
        console.log("Fetched users:", fetchedUsers);
        console.log("Fetched condiments:", fetchedCondiments);
      } catch (error) {
        console.error("Error fetching data:", error);
      }
    };

    fetchData();
  }, []);

  const updatePizza = (event) => {
    const {value} = event.target;

  const ourPizza = pizzaSizes.find((eachPizza) => eachPizza.size == value);

    setCurrentPizza((pizza) => ({
      ...pizza,
      size: value,
      basePrice : ourPizza ? ourPizza.basePrice : 0,
    }))
  }

  const addCondimentToPizza = (e, condiment) => {
    const isChecked = e.target.checked;

    setCurrentPizza((prevPizza) => {
      const updatedCondiments = isChecked ? 
      [...prevPizza.selectedCondiments, condiment]
      : prevPizza.selectedCondiments.filter((c) => c.condimentId != condiment.condimentId);

      return {
        ...prevPizza,
        selectedCondiments: updatedCondiments
      }
    })
  }

  const submitPizzaToOrder = () => {
    setTotalPizzas((prevTotal) => [...prevTotal, currentPizza]);
    setCurrentPizza({
      size: "",
      basePrice: 0,
      selectedCondiments: [],
    });
  }

  const removePizza = (e) => {
    const isChecked = e.target.checked;
    const indexToRemove = parseInt(e.target.value, 10); // Get the index from the checkbox value
  
    if (isChecked) {
      setTotalPizzas((prevTotal) =>
        prevTotal.filter((pizza, index) => index !== indexToRemove) // Use index to filter out the pizza
      );
    }
  };

  const submitOrder = async () => {
    if (!chosenUser || !chosenOrderTaker || !chosenDeliveryPerson) {
      alert("Please select a user, order taker, and delivery person before submitting.");
      return;
    }
  
    // Calculate the delivery fee ($5 per pizza)
    const deliveryFee = totalPizzas.length * 5;
    const tip = parseFloat(enteredTip || 0);
  
    // Calculate the total amount (sum of pizza base prices, condiments, tip, and delivery fee)
    const totalAmount = totalPizzas.reduce((sum, pizza) => {
      const totalCost = pizza.basePrice;

      const toppingPrice = pizza.selectedCondiments.reduce((sum, eachTopping) => {
        return sum + eachTopping.cost;
      }, 0);
      return sum + totalCost + toppingPrice;
    }, 0);
    
    const finalTotalAmount = totalAmount + deliveryFee + tip;


  
    // Create the order DTO
    const orderDto = {
      userId: chosenUser,
      orderTakerEmployeeId: chosenOrderTaker,
      deliveryPersonEmployeeId: chosenDeliveryPerson,
      totalAmount: finalTotalAmount,
      tipLeftCustomer: parseFloat(enteredTip || 0),
      deliveryFee: deliveryFee,
      orderDate: new Date().toISOString(),
      pizzas: totalPizzas.map((pizza) => ({
        size: pizza.size,
        basePrice: pizza.basePrice,
        condiments: pizza.selectedCondiments.map((condiment) => ({
          condimentId: condiment.condimentId,
      })),
        })),
    };
// TOMORROW AS OF 608 12-23 NEED TO NOW MAKE BACKEND METHOD FOR CREATE ORDER AND SUBMIT IT AND THEN 
// O NBACKEND WE ALSO NEED TO CREATE PIZZA THATWE JUST MADE THEN AND ONLY THEN CAN WE ACTUALLY SUBMIT THE ORDER AND ALL THE NEEDED STUFF IN JOIN TABLES 
// AND WHAT NOT 

try {
  console.log("Submitting order:", orderDto);
  const createdOrder = await createOrder(orderDto); // Call `createOrder`
  console.log("Order created successfully:", createdOrder);
  alert("Order submitted successfully!");
} catch (error) {
  console.error("Error submitting order:", error);
  alert("Failed to submit order. Please try again.");
}

  }
  

    return (
      <div style={{ padding: "20px", fontFamily: "Arial, sans-serif", maxWidth: "800px", margin: "auto" }}>
      <h1 style={{ textAlign: "center" }}>Create Order</h1>
      <p> Please Note there will be a $5 surcharge per pizza when calculating delivery</p>
        <p>This is where orders will be created.</p>
        <div>
          <label htmlFor="input-number"> Enter The Tip You Want to Leave</label>
          <input
          type="number"
          id="input-number"
          value={enteredTip}
          onChange={(e) => setEnteredTip(e.target.value)}
          />
        </div>

        <h2>Select User</h2>
      <select
        value={chosenUser || ""}
        onChange={(e) => setChosenUser(e.target.value)}
        style={{ padding: "10px", width: "100%", marginBottom: "20px" }}
      >
        <option value="">Select a User</option>
        {users.map((user) => (
          <option key={user.id} value={user.id}>
            {user.firstName} {user.lastName}
          </option>
        ))}
      </select>

      <h2>Select Employees</h2>
      <div>
        <h3>Order Taker</h3>
        <select
          value={chosenOrderTaker || ""}
          onChange={(e) => setChosenOrderTaker(e.target.value)}
          style={{ padding: "10px", width: "100%", marginBottom: "20px" }}
        >
          <option value="">Select an Order Taker</option>
          {employees.map((employee) => (
            <option key={employee.employeeId} value={employee.employeeId}>
              {employee.name}
            </option>
          ))}
        </select>

        <h3>Delivery Person</h3>
        <select
          value={chosenDeliveryPerson || ""}
          onChange={(e) => setChosenDeliveryPerson(e.target.value)}
          style={{ padding: "10px", width: "100%", marginBottom: "20px" }}
        >
          <option value="">Select a Delivery Person</option>
          {employees.map((employee) => (
            <option key={employee.employeeId} value={employee.employeeId}>
              {employee.name}
            </option>
          ))}
        </select>
      </div>


        <h2> Add Pizza </h2>
        <select 
        value = {currentPizza.size}
        onChange={(e) => updatePizza(e)}
        >
          <option > Select Pizza Size </option>
          {pizzaSizes.map((pizza, index) => (
            <option key={index} value={pizza.size} >
              Pizza Info: {pizza.size} - Pizza Cost:{pizza.basePrice.toFixed(2)}
            </option>
          )) }
        </select>

        <h2> Select Condiments </h2>
          <div>
            {condiments.map((condiment, index) => (
               <label key={index} style={{ display: "block", margin: "5px 0" }}>
                <input 
                type="checkbox"
                value={condiment.condimentId}
                onChange={(e) => addCondimentToPizza(e, condiment)}
                />
                Condiment Name And Price: {condiment.condimentName} - {condiment.cost.toFixed(2)};
              </label>
            )) }
            <button onClick={submitPizzaToOrder} > Submit to Add Pizza to Order </button>
          </div>

        <h2> Current Pizza Order </h2>
        <div>
          {totalPizzas && totalPizzas.length > 0 ? (
            totalPizzas.map((eachPizza, index) => (
              <div key={index}>
              <input
              type="checkbox"
              value={index}
              onChange={(e) => removePizza(e, eachPizza)}
              id={`pizza-checkbox-${index}`}
              />
              <label htmlFor={`pizza-checkbox-${index}`}>
              <div>
              <p>Pizza Size: {eachPizza.size}</p>
              <p>Base Price: ${eachPizza.basePrice.toFixed(2)}</p>
              <p>Selected Condiments: {eachPizza.selectedCondiments.map(condiment => condiment.condimentName).join(", ") || "None"}</p>
            <p> Check The Box To Remove the Pizza From The Order </p>
            </div>
            </label>
          </div>
            ))
            ) : (
              <p> No Pizzas Are Currently Saved To The Order </p>
            )}
        </div>
        <button onClick={submitOrder} style={{ marginTop: "20px", padding: "10px 20px", fontSize: "16px" }}>
      Submit Order
    </button>
      </div>
    );
  }
  