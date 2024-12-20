import { Route, Routes } from "react-router-dom";
import {Orders} from "./components/Orders";
import {CreateOrder} from "./components/CreateOrder";
import  AuthorizedRoute from "./components/auth/AuthorizedRoute";
import Login from "./components/auth/Login";
import Register from "./components/auth/Register";

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
          path="orders"
          element={
            <AuthorizedRoute loggedInUser={loggedInUser}>
              <Orders />
            </AuthorizedRoute>
          }
        />
        <Route
          path="orders/create"
          element={
            <AuthorizedRoute loggedInUser={loggedInUser}>
              <CreateOrder />
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
    </Routes>
  );
}
