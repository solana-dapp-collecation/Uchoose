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
                            <Form className="d-flex">
                                <FormControl
                                    type="search"
                                    placeholder="Search items, collections and accounts"
                                    className="me-4"
                                    aria-label="Search"
                                    style={{width: '30vw', marginLeft: '-10vw'}}
                                    // size="lg"
                                />
                                <Button variant="outline-success">Search</Button>
                            </Form>
                        </Nav>
                        {/*TODO. Place authentication here when redux-saga setup finished*/}
                        {/*<Nav>*/}
                        {/*    <Nav.Link href="#deets">Marketplace</Nav.Link>*/}
                        {/*    <Nav.Link eventKey={2} href="#memes">*/}
                        {/*        Help*/}
                        {/*    </Nav.Link>*/}
                        {/*    <Navbar.Text>*/}
                        {/*        Connect Wallet*/}
                        {/*    </Navbar.Text>*/}
                        {/*</Nav>*/}
                    </Navbar.Collapse>
                </Container>
            </Navbar>
        </header>
    );
}
