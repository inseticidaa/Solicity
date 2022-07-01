import { Container, Navbar, Nav, NavDropdown } from "react-bootstrap";
import { Link, Navigate, Outlet } from "react-router-dom";
import { useAuth } from "./AuthProvider";

export const ProtectedLayout = () => {
  const { user, logout } = useAuth();

  if (!user) {
    return <Navigate to="/" />;
  }

  return (
    <div className="h-100">
      <Navbar collapseOnSelect expand="lg" bg="dark" variant="dark">
        <Container>
          <Navbar.Brand><Link to={"/"}>Solicity</Link></Navbar.Brand>
          <Navbar.Toggle aria-controls="responsive-navbar-nav" />
          <Navbar.Collapse id="responsive-navbar-nav">
            <Nav className="me-auto">
              <Link to={"/issues"}><Nav.Link href="#features">issues</Nav.Link></Link>
              <Nav.Link href="#pricing">Pricing</Nav.Link>

            </Nav>
            <Nav>
              <NavDropdown title={user.firstName} id="collasible-nav-dropdown">
                <NavDropdown.Item href="#action/3.1">Action</NavDropdown.Item>
                <NavDropdown.Item href="#action/3.2">Another action</NavDropdown.Item>
                <NavDropdown.Item href="#action/3.3">Something</NavDropdown.Item>
                <NavDropdown.Divider />
                <NavDropdown.Item onClick={() => logout()}>Sair</NavDropdown.Item>
              </NavDropdown>
            </Nav>
          </Navbar.Collapse>
        </Container>
      </Navbar>
      <Outlet />
    </div>
  )
};