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

  
  export const getOrderById = async (orderId) => {
    const response = await fetch(`/api/orders/${orderId}`);
    if (!response.ok) throw new Error(`Failed to fetch order with ID: ${orderId}`);
    return response.json();
  };
  
  export const createOrder = async (orderData) => {
    const response = await fetch("/api/orders", {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(orderData),
    });
    if (!response.ok) throw new Error("Failed to create order");
    return response.json();
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
  