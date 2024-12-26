import { useState, useEffect } from "react";
import { useParams, useLocation, useNavigate} from "react-router-dom";
import { getAllEmployees } from "../manager/EmployeeManager";
import { getAllUsers } from "../manager/userManager";
import { getAllCondiments } from "../manager/condimentManager";
import { updatePizza } from "../manager/orderManager";


export const UpdatePizza = () => {
  const navigate = useNavigate();
  const { orderId, pizzaId } = useParams();
  const location = useLocation();
  const [pizza, setPizza] = useState(location.state?.pizza || null);
  console.log("what is the pizza param", pizza);
  const [updatedPizza, setUpdatedPizza] = useState(
    location.state?.pizza || null
  );
  console.log("what is value updatedPizza", updatedPizza);
  const [employees, setEmployees] = useState([]);
  const [users, setUsers] = useState([]);
  const [condiments, setCondiments] = useState([]);
  const [pizzaSizes] = useState([
    { size: "Large", basePrice: 15.0 },
    { size: "Medium", basePrice: 12.0 },
    { size: "Small", basePrice: 10.0 },
  ]);

  // const [currentPizza, setCurrentPizza] = useState({
  //   size: "",
  //   basePrice: 0,
  //   selectedCondiments: [],
  // });

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
        console.error("error fetching data", error);
      }
    };
    fetchData();
  }, []);

  const addCondimentToPizza = (e, condiment) => {
    const checked = e.target.checked;
  
    setUpdatedPizza((pizza) => {
      // If checked, add the condiment
      if (checked) {
        return {
          ...pizza,
          pizzaCondiments: [...pizza.pizzaCondiments, condiment],
        };
      } 
      // If unchecked, remove the condiment
      return {
        ...pizza,
        pizzaCondiments: pizza.pizzaCondiments.filter(
          (c) => c.condimentId !== condiment.condimentId
        ),
      };
    });
  };
  
  const updatePizzaSize = (e) => {
    const newSize = e.target.value;
    const newBasePrice = pizzaSizes.find(size => size.size === newSize)?.basePrice || 0;

    setUpdatedPizza((pizza) => {
      return {
        ...pizza,
        size: e.target.value,
        basePrice: newBasePrice,
      }
    })
  }

  const submitUpdatedPizza = async () => {
    const mappedCondiments = updatedPizza.pizzaCondiments.map(
      (condiment) => condiment.condimentId
    );

    const updatedPizzaDto = {
      pizzaId: updatedPizza.pizzaId,
      size: updatedPizza.size,
      condimentIds: mappedCondiments,
    };

    console.log("Submitting updated pizza:", updatedPizzaDto);
    const updatedOrder = await updatePizza(
      orderId,
      pizza.pizzaId,
      updatedPizzaDto
    );

    console.log("Pizza updated successfully:", updatedOrder);

    navigate("/orders", {
      state: {
        orderIdForUpdate: orderId,
        pizzaIdForUpdate: pizzaId
      }
    })
  };



  return (
    <>
      <h2> Select Condiments </h2>
      <div>
        {condiments.map((condiment, index) => (
          <label key={index} style={{ display: "block", margin: "5px 0" }}>
            <input
              type="checkbox"
              value={condiment.condimentId}
              onChange={(e) => addCondimentToPizza(e, condiment)}
              checked={updatedPizza.pizzaCondiments.some(
                (pc) => pc.condimentId === condiment.condimentId
              )}
            />
            Condiment Name And Price: {condiment.condimentName} -{" "}
            {condiment.cost.toFixed(2)};
          </label>
        ))}
        {/* <button onClick={submitPizzaToOrder} > Submit to Add Pizza to Order </button> */}
      </div>

      <h2> Update Pizza Size </h2>
      <select value={pizza.size} onChange={(e) => updatePizzaSize(e)}>
        <option> Select Pizza Size </option>
        {pizzaSizes.map((pizza, index) => (
          <option key={index} value={pizza.size}>
            Pizza Info: {pizza.size} - Pizza Cost:{pizza.basePrice.toFixed(2)}
          </option>
        ))}
      </select>

      <h2> Current Pizza Values</h2>
      <div>
        <p>
          <strong>Pizza ID:</strong> {pizza?.pizzaId}
        </p>
        <p>
          <strong>Size:</strong> {pizza?.size}
        </p>
        <p>
          <strong>Price:</strong> ${pizza.basePrice}
        </p>
        <p>
          {pizza.pizzaCondiments.map((eachPC, index) => (
            <p key={index}>
              <strong> CondimentId </strong> {eachPC.condimentId}
              <strong> CondimentType </strong> {eachPC.condimentType}
              <strong> Condiment Name </strong> {eachPC.condimentName}
            </p>
          ))}
        </p>
      </div>

      <h2> Updated Pizza Values</h2>
      <div>
        <p>
          <strong>Pizza ID:</strong> {updatedPizza?.pizzaId}
        </p>
        <p>
          <strong>Size:</strong> {updatedPizza?.size}
        </p>
        <p>
          <strong>Price:</strong> ${updatedPizza.basePrice}
        </p>
        <p>
          {updatedPizza.pizzaCondiments.map((eachPC, index) => (
            <p key={index}>
              <strong> CondimentId </strong> {eachPC.condimentId}
              <strong> CondimentType </strong> {eachPC.condimentType}
              <strong> Condiment Name </strong> {eachPC.condimentName}
            </p>
          ))}
        </p>

        <button onClick={submitUpdatedPizza} style={{ marginTop: "20px" }}>
          Submit Updated Pizza
        </button>
      </div>
    </>
  )
}
