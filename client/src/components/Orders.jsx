import { useState, useEffect } from "react";
import {getOrders, getOrderByDate} from '../manager/orderManager'
import Calendar from "react-calendar";
import { Link } from "react-router-dom";

export const Orders = () => {
  const [orders, setOrders] = useState([]);
  const [selectedDate, setSelectedDate] = useState(new Date()); // Default to today's date

  useEffect(() => {
    const fetchOrders = async () => {
      try {
        // const formattedDate = selectedDate.toISOString().split("T")[0]; // Format date for backend
        const data = await getOrders();
        setOrders(data);
      } catch (error) {
        console.error("Error fetching orders:", error);
      }
    };

    fetchOrders();
  }, [selectedDate]);

  const handleDateChange = (date) => {
    setSelectedDate(date);
  };

  const clickFunction = async () => {
    const data = await getOrders();
    setOrders(data);
    console.log("did clickFunction do anything if so what is data", data);
  }

const getOrdersByDate = async (selectedDate) => {
    const formattedDate = selectedDate.toISOString().split("T")[0]; // Format date for backend
    console.log("Formatted Date Sent to Backend:", formattedDate);
    const ourResponse = await getOrderByDate(formattedDate);
    console.log("what is our response when we do getOrdersByDate", ourResponse);
    setOrders(ourResponse);
}

  return (
    <div style={{ padding: "20px", fontFamily: "Arial, sans-serif", maxWidth: "800px", margin: "auto" }}>
      <h1 style={{ textAlign: "center", marginBottom: "20px" }}>View Orders</h1>

      {/* Calendar Section */}
      <div>
      <div style={{ textAlign: "center", marginBottom: "30px" }}>
        <Calendar
          onChange={handleDateChange}
          value={selectedDate}
        />
      </div>
      <div>
        <button onClick = {() => getOrdersByDate(selectedDate)}> Click This To Get Orders On Selected Date </button>
      </div>
      </div>

      {/* Orders Section */}
      <div style={{ borderTop: "1px solid #ddd", paddingTop: "20px" }}>
        {orders.length > 0 ? (
          orders.map((order) => (
            <div
              key={order.orderId}
              style={{
                border: "1px solid #ddd",
                borderRadius: "8px",
                padding: "15px",
                marginBottom: "15px",
                backgroundColor: "#f9f9f9",
                boxShadow: "0 2px 4px rgba(0, 0, 0, 0.1)",
              }}
            >
              <p><strong>Order ID:</strong> {order.orderId}</p>
              <p><strong>Order Date:</strong> {order.orderDate}</p>
              <p><strong>Total Amount:</strong> ${order.totalAmount}</p>
              <p><strong>Order Status:</strong> {order.orderStatus}</p>
              <p><strong>Delivery Name:</strong> {order.deliveryPerson.name}</p>
              <div>
                {order.pizzas.map((pizza, index) => (
                  <div key ={index}>
              <p><strong>pizza ID:</strong> {pizza.pizzaId}</p>
              <p><strong>Base Price:</strong> {pizza.basePrice}</p>
              <p><strong>Size:</strong> ${pizza.size}</p>
                  </div>
                ))
                }
              </div>
              <Link
              to={`/orders/${order.orderId}/update`}
              state={{ order }}
              >
                Edit Order 
              </Link>
            </div>
          ))
        ) : (
          <p style={{ textAlign: "center", color: "#888", fontSize: "16px" }}>No orders available for the selected date.</p>
        )}
      </div>
      <button onClick={clickFunction}> click this to get orders </button>
    </div>
  );
};
