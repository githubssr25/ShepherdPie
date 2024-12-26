import { useState, useEffect } from "react";
import {getOrders, getOrderByDate, getOrderById} from '../manager/orderManager'
import { useLocation } from "react-router-dom";
import Calendar from "react-calendar";
import { Link } from "react-router-dom";

export const Orders = () => {
  const location = useLocation();
  const [orders, setOrders] = useState([]);
  const [selectedDate, setSelectedDate] = useState(new Date()); // Default to today's date
  const { orderIdForUpdate, pizzaIdForUpdate } = location.state || {};


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


  useEffect(() => {
    const fetchUpdatedOrders = async () => {
      if(!orderIdForUpdate){
        const allOrders = await getOrders();
        setOrders(allOrders);
      } else {
        const updatedOrder = await getOrderById(orderIdForUpdate);
        setOrders((prevOrder) => {
         return prevOrder.map((order) => {
           if(order.orderId === updatedOrder.orderId) {
            return updatedOrder;
           } 
           return order; //dont need an else for this 
          })
        });
      }};

    fetchUpdatedOrders();
  }, [orderIdForUpdate, pizzaIdForUpdate]);

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
                <h1> Pizza Info </h1>
                {order.pizzas.map((pizza, index) => (
                  <div key ={index}>
              <p> <strong> Pizza Number {index} </strong></p>
              <p><strong>pizza ID:</strong> {pizza.pizzaId}</p>
              <p><strong>Base Price:</strong> {pizza.basePrice}</p>
              <p><strong>Size:</strong> ${pizza.size}</p>
              <Link
              to={`/orders/${order.orderId}/${pizza.pizzaId}/updatePizza`}

              state={{ pizza, order }}
              >
                Edit Pizza
              </Link>
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
//12-24 next step is use navigate we have to get the updated pizza bkac ot orders so orders actually displays and has updated pizza value 