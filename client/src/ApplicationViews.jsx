import { Route, Routes } from "react-router-dom";
import {Orders} from "./components/Orders";
import {CreateOrder} from "./components/CreateOrder";
import  AuthorizedRoute from "./components/auth/AuthorizedRoute";
import Login from "./components/auth/Login";
import Register from "./components/auth/Register";
import {UpdateOrder} from "./components/UpdateOrder";
import {UpdatePizza} from "./components/UpdatePizza";

/* eslint-disable react/prop-types */
export default function ApplicationViews({ loggedInUser, setLoggedInUser }) {
  return (
    <Routes>
      <Route path="/">
        <Route
          index
          element={
            <AuthorizedRoute loggedInUser={loggedInUser}>
              <Orders />
            </AuthorizedRoute>
          }
        />
        <Route
          path="login"
          element={<Login setLoggedInUser={setLoggedInUser} />}
        />
        <Route
          path="register"
          element={<Register setLoggedInUser={setLoggedInUser} />}
        />
      </Route>
      <Route path="*" element={<p>Whoops, nothing here...</p>} />

      <Route path="orders">
          <Route
          index
          element={
            <AuthorizedRoute loggedInUser={loggedInUser}>
              <Orders />
            </AuthorizedRoute>
          }
          />
          <Route
          path=":orderId/:pizzaId/updatePizza"
          element={
            <AuthorizedRoute loggedInUser ={loggedInUser}>
              <UpdatePizza />
            </AuthorizedRoute>
          }
          />

        <Route
          path="create"
          element={
            <AuthorizedRoute loggedInUser={loggedInUser}>
              <CreateOrder />
            </AuthorizedRoute>
          }
        />
        <Route
        path=":orderId/update"
        element= {
          <AuthorizedRoute loggedInUser={loggedInUser}>
            <UpdateOrder />
          </AuthorizedRoute>
        }
        />
      </Route>
    </Routes>
  );
}
