

export const getAllEmployees = () => {
    return fetch("http://localhost:5168/api/employee", {
      method: "GET",
    }).then((res) => {
      if (!res.ok) {
        throw new Error(`Error fetching employees: ${res.statusText}`);
      }
      return res.json();
    });
  };
  