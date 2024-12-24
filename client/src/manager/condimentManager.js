const _apiUrl = "http://localhost:5168/api/condiment";

export const getAllCondiments = async () => {
  try {
    console.log("Fetching condiments from", _apiUrl);
    const response = await fetch(_apiUrl, {
      method: "GET",
    });

    if (!response.ok) {
      console.error("Failed to fetch condiments:", response.status, response.statusText);
      throw new Error("Failed to fetch condiments");
    }

    const data = await response.json();
    console.log("Fetched condiments successfully:", data);
    return data;
  } catch (error) {
    console.error("Error occurred while fetching condiments:", error);
    throw error; // Re-throw error for the caller to handle
  }
};
