export const getOrders = async () => {
  try {
    console.log("Fetching orders from /api/order...");
    const response = await fetch("http://localhost:5168/api/order");
    
    if (!response.ok) {
      console.error("Failed to fetch orders:", response.status, response.statusText);
      throw new Error("Failed to fetch orders");
    }

    const data = await response.json();
    console.log("Fetched orders successfully:", data);
    return data;
  } catch (error) {
    console.error("Error occurred while fetching orders:", error);
    throw error; // Re-throw error for the caller to handle
  }
};

export const getOrderByDate = async (orderDate) => {
  try {
    console.log("Fetch URL:", `/api/order/by-date?date=${orderDate}`);
    const response = await fetch (`http://localhost:5168/api/order/by-date?date=${orderDate}`);
    if(!response.ok){
      throw new Error ("Failed to fetch orders");
    } else {
      return await response.json();
    } 
  } catch (error) {
    console.error("error fetching orders by date", error);
  }
}

  
  export const getOrderById = async (orderId) => {
    const response = await fetch(`/api/orders/${orderId}`);
    if (!response.ok) throw new Error(`Failed to fetch order with ID: ${orderId}`);
    return response.json();
  };
  
  export const createOrder = async (orderData) => {
    try {
      const response = await fetch("http://localhost:5168/api/order", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(orderData),
      });
      if (!response.ok) {
        const error = await response.json();
        throw new Error(error.message || "Failed to create order");
      }
      return response.json(); // Return created order details (or success message)
    } catch (error) {
      console.error("Error creating order:", error);
      throw error;
    }
  };
  
  
  export const updateOrder = async (orderId, updatedData) => {
    const response = await fetch(`/api/orders/${orderId}`, {
      method: "PUT",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(updatedData),
    });
    if (!response.ok) throw new Error("Failed to update order");
    return response.json();
  };
  
  export const deleteOrder = async (orderId) => {
    const response = await fetch(`/api/orders/${orderId}`, {
      method: "DELETE",
    });
    if (!response.ok) throw new Error(`Failed to delete order with ID: ${orderId}`);
  };
  

  export const updatePizza = async (orderId, pizzaId, updatedPizzaDto) => {
const apiUrl = `http://localhost:5168/api/order/${orderId}/pizzas/${pizzaId}`;

try {
  console.log(`Updating pizza at ${apiUrl} with data`, updatedPizzaDto);

  const response = await fetch(apiUrl, {
    method: "PUT",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(updatedPizzaDto),
  });

  if (!response.ok) {
    console.error("Failed to update pizza:", response.status, response.statusText);
    throw new Error("Failed to update pizza");
  }

  const updatedOrder = await response.json();
  console.log("Pizza updated successfully:", updatedOrder);

  return updatedOrder; // Return the updated order
} catch (error) {
  console.error("Error in updatePizza:", error);
  throw error;



}
  }