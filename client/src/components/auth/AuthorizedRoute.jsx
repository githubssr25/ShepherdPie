import { Navigate } from "react-router-dom";

/* eslint-disable react/prop-types */
const AuthorizedRoute = ({ loggedInUser, children }) => {
  if (!loggedInUser) {
    // Redirect to login page if the user is not logged in
    return <Navigate to="/login" replace />;
  }

  // Render children if the user is logged in
  return children;
};

export default AuthorizedRoute;
