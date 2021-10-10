import React from "react";
import navStyles from "../styles/NavigationBar.module.css";
import {Button, Container, Form, FormControl, Nav, Navbar, NavDropdown} from "react-bootstrap";
import {HEADER_LOGO} from "../constants/constants";

export function NavigationBar(props: React.PropsWithChildren<{}>) {

    return (
        <header id="header" className="header fixed-top">
            <Navbar bg="light" expand="lg" sticky="top">
                <Container>
                    <Navbar.Brand href="/">
                        <img
                            src={HEADER_LOGO}
                            width="100"
                            height="30"
                            className="d-inline-block align-top"
                            alt="Uch∞se"
                        /></Navbar.Brand>
                    <Navbar.Toggle aria-controls="responsive-navbar-nav"/>
                    <Navbar.Collapse id="responsive-navbar-nav">
                        <Nav className="mx-lg-auto">
                            <b style={{width: '20vw', marginLeft: '-5vw'}}>Schema creation support utility</b>
                        </Nav>
                    </Navbar.Collapse>
                </Container>
            </Navbar>
        </header>
    );
}
