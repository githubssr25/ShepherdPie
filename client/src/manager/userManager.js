

export const getAllUsers = () => {
    return fetch("http://localhost:5168/api/user", {
      method: "GET",
    }).then((res) => {
      if (!res.ok) {
        throw new Error(`Error fetching users: ${res.statusText}`);
      }
      return res.json();
    });
  };
  