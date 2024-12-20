import { useState } from "react";
import { NavLink as RRNavLink } from "react-router-dom";
import {
  Button,
  Collapse,
  Nav,
  NavLink,
  NavItem,
  Navbar,
  NavbarBrand,
  NavbarToggler,
} from "reactstrap";
import { logout } from "../manager/authManager";

/* eslint-disable react/prop-types */
export default function NavBar({ loggedInUser, setLoggedInUser }) {
  const [open, setOpen] = useState(false);

  const toggleNavbar = () => setOpen(!open);

  const handleLogout = () => {
    logout().then(() => {
      setLoggedInUser(null);
      localStorage.removeItem("loggedInUser");
      setOpen(false);
    });
  };

  return (
    <Navbar color="light" light expand="lg">
      <NavbarBrand tag={RRNavLink} to="/">
        Shepherd Pie
      </NavbarBrand>
      {loggedInUser ? (
        <>
          <NavbarToggler onClick={toggleNavbar} />
          <Collapse isOpen={open} navbar>
            <Nav navbar>
              <NavItem>
                <NavLink tag={RRNavLink} to="/orders">
                  Orders
                </NavLink>
              </NavItem>
              <NavItem>
                <NavLink tag={RRNavLink} to="/orders/create">
                  Create Order
                </NavLink>
              </NavItem>
            </Nav>
          </Collapse>
          <Button color="primary" onClick={handleLogout}>
            Logout
          </Button>
        </>
      ) : (
        <Nav navbar>
          <NavItem>
            <NavLink tag={RRNavLink} to="/login">
              Login
            </NavLink>
          </NavItem>
        </Nav>
      )}
    </Navbar>
  );
}
