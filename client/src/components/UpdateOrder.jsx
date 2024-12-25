import { useParams, useLocation, useNavigate } from "react-router-dom";
import { useState, useEffect } from "react";
import { getOrderById, updateOrder } from "../manager/orderManager";
import { getAllCondiments } from "../manager/condimentManager";
import { getAllEmployees } from "../manager/EmployeeManager";

export const UpdateOrder = () => {
  console.log("UpdateOrder component loaded");

  const { orderId } = useParams();
  const location = useLocation();
  const [order, setOrder] = useState(location.state?.order || null); // Order data
  const [updatedOrder, setUpdatedOrder] = useState(
    location.state?.order || null
  ); // Order data
  const [employees, setEmployees] = useState([]);
  const [condiments, setCondiments] = useState([]);
  const [chosenDriver, setChosenDriver] = useState({});
  const [addedPizza, setAddedPizza] = useState({});
//   console.log("whatis value of order", order);

  const [pizzaSizes] = useState([
    { size: "Large", basePrice: 15.0 },
    { size: "Medium", basePrice: 12.0 },
    { size: "Small", basePrice: 10.0 },
  ]);

  const [currentPizza, setCurrentPizza] = useState({
    size: "",
    basePrice: 0,
    selectedCondiments: [],
  });

  useEffect(() => {
    (async () => {
      if (!order) {
        setOrder(order); // Use the passed order directly if available
      }
      const fetchedCondiments = await getAllCondiments();
      const fetchedEmployees = await getAllEmployees();
      setCondiments(fetchedCondiments);
      setEmployees(fetchedEmployees);
    })();
  }, [order]);

  const removePizza = (pizza, event) => {
    const isChecked = event.target.checked;
    const pizzaCost = pizza.basePrice + pizza.selectedCondiments.reduce((sum, condiment) => sum + condiment.cost, 0);
    setUpdatedOrder((prevOrder) => {
      if (isChecked) {
        return {
          ...prevOrder,
          totalAmount: prevOrder.totalAmount - pizzaCost,
          pizzas: prevOrder.pizzas.filter((p) => p.pizzaId != pizza.pizzaId),
        };
      } else {
        return {
          ...prevOrder,
          totalAmount: prevOrder.totalAmount + pizzaCost,
          pizzas: [...prevOrder.pizzas, pizza],
        };
      }
    });
  };

  const updateChosenDriver = (e) => {
    const ourEmployeeId = parseInt(e.target.value, 10); // Extract the employeeId from the event
    const selectedEmployee = employees.find(
      (employee) => employee.employeeId === ourEmployeeId
    );

    if (selectedEmployee) {
      setChosenDriver(selectedEmployee.employeeId); // Update the chosenDriver state
      setUpdatedOrder((prevOrder) => ({
        ...prevOrder,
        deliveryPerson: selectedEmployee, // Set the selected employee object
      }));
    }
  };

  //remember to add $5 tip when adding new pizza to order
  const addPizza = (event) => {
    const { value } = event.target;
    const ourPizza = pizzaSizes.find((eachPizza) => eachPizza.size == value);

    setCurrentPizza((pizza) => ({
      ...pizza,
      size: value,
      basePrice: ourPizza ? ourPizza.basePrice : 0,
    }));

    setUpdatedOrder((order) => ({
      ...order,
      pizzas: [...order.pizzas, ourPizza],
    }));
  };

  const addCondimentToPizza = (e, condiment) => {
    const isChecked = e.target.checked;

    setCurrentPizza((prevPizza) => {
      const updatedCondiments = isChecked
        ? [...prevPizza.selectedCondiments, condiment]
        : prevPizza.selectedCondiments.filter(
            (c) => c.condimentId != condiment.condimentId
          );
      return {
        ...prevPizza,
        selectedCondiments: updatedCondiments,
      };
    });
  };

  const submitPizzaToOrder = () => {
    setUpdatedOrder((order) => ({
      ...order,
      pizzas: [...order.pizzas, currentPizza],
    }));

    setCurrentPizza({
      size: "",
      basePrice: 0,
      selectedCondiments: [],
    });
  };

  return (
    <div
      style={{
        padding: "20px",
        fontFamily: "Arial, sans-serif",
        maxWidth: "800px",
        margin: "auto",
      }}
    >
      <h1>Update Order</h1>

      <h2>Order Details</h2>
      <p>
        <strong>Order ID:</strong> {order?.orderId}
      </p>
      <p>
        <strong>Order Status:</strong> {order?.orderStatus}
      </p>
      <p>
        <strong>Total Amount:</strong> ${order.totalAmount}
      </p>
      <p>
        <strong>Order Status:</strong> {order.orderStatus}
      </p>
      <p>
        <strong>Deliverer Name:</strong> {order.deliveryPerson.name}
      </p>
      <p>
        {" "}
        <strong>
          {" "}
          You Can Change the Deliverer By Selecting From Options Below{" "}
        </strong>{" "}
      </p>

      <select
        value={chosenDriver || ""}
        onChange={(e) => updateChosenDriver(e)}
        style={{ padding: "10px", width: "100%", marginBottom: "20px" }}
      >
        <option value="">Select a Delivery Person</option>
        {employees.map((employee) => (
          <option key={employee.employeeId} value={employee.employeeId}>
            {employee.name}
          </option>
        ))}
      </select>

      <div>
        {order.pizzas.map((pizza, index) => (
          <div key={index}>
            <input
              type="checkbox"
              value={index}
              onChange={(e) => removePizza(pizza, e)}
              id={`pizza-${index}`}
            />
            <p> Check The Box Next To This Pizza To Remove It From Order </p>
            <p>
              <strong>pizza ID:</strong> {pizza.pizzaId}
            </p>
            <p>
              <strong>Base Price:</strong> {pizza.basePrice}
            </p>
            <p>
              <strong>Size:</strong> ${pizza.size}
            </p>
          </div>
        ))}
      </div>

      <h2> Add New Pizza To Order</h2>
      <select value={currentPizza.size} onChange={(e) => addPizza(e)}>
        <option> Select Pizza Size and Price</option>
        {pizzaSizes.map((pizza, index) => (
          <option key={index} value={pizza.size}>
            Pizza Info: {pizza.size} - Pizza Cost:{pizza.basePrice.toFixed(2)}
          </option>
        ))}
      </select>
      <h2> Select Condiments </h2>
      <div>
        {condiments.map((condiment, index) => (
          <label key={index} style={{ display: "block", margin: "5px 0" }}>
            <input
              type="checkbox"
              value={condiment.condimentId}
              onChange={(e) => addCondimentToPizza(e, condiment)}
              checked={currentPizza.selectedCondiments.some((c) => {
                // console.log(
                //   `Checking if condiment ${condiment.condimentId} is in selectedCondiments`,
                //   c.condimentId === condiment.condimentId
                // );
                return c.condimentId === condiment.condimentId;
              })}
            />
            Condiment Name And Price: {condiment.condimentName} -{" "}
            {condiment.cost.toFixed(2)};
          </label>
        ))}
        <button onClick={submitPizzaToOrder}>
          {" "}
          Submit to Add Pizza to Order{" "}
        </button>
      </div>

      {/* Updated Order Details */}
      <h2>Updated Order Details</h2>
      <p>
        <strong>Order ID:</strong> {updatedOrder?.orderId}
      </p>
      <p>
        <strong>Order Status:</strong> {updatedOrder?.orderStatus}
      </p>
      <p>
        <strong>Total Amount:</strong> ${updatedOrder?.totalAmount}
      </p>
      <p>
        <strong>Delivery Person:</strong>{" "}
        {updatedOrder?.deliveryPerson?.name || "Not Assigned"}
      </p>
      <div>
        <h3>Updated Pizzas</h3>
        {updatedOrder?.pizzas?.map((pizza, index) => (
          <div key={index}>
            <p>
              <strong>Pizza ID:</strong> {pizza.pizzaId || "New"}
            </p>
            <p>
              <strong>Size:</strong> {pizza.size}
            </p>
            <p>
              <strong>Base Price:</strong> ${pizza.basePrice}
            </p>
            <p>
              <strong>Condiments:</strong>{" "}
              {pizza.selectedCondiments
                ?.map((c) => c.condimentName)
                .join(", ") || "None"}
            </p>
          </div>
        ))}
      </div>
    </div>
  );
}