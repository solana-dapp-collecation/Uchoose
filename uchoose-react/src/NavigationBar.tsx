import {Button, Container, Form, FormControl, Nav, Navbar, NavDropdown} from "react-bootstrap";
import {HEADER_LOGO} from "./constants/constants";
import {useWallet} from '@solana/wallet-adapter-react';
import {WalletDisconnectButton, WalletMultiButton} from '@solana/wallet-adapter-react-ui';
import React, {FC} from 'react';
import main_logo from './assets/images/logo.svg';
import main_logo_png from './assets/images/logo.png';
import logo_title from './assets/images/logo_title.svg';

const NavigationBar: FC = () => {
    const {wallet} = useWallet();

    return (
        <header id="header" className="header fixed-top">
            <Navbar bg="light" expand="lg" sticky="top">
                <Container>
                    <Navbar.Brand href="/">
                        <img
                            src={main_logo_png}
                            height="30"
                            className="d-inline-block align-top"
                            alt="Uch∞se"
                        />
                        <img
                            src={logo_title}
                            height="30"
                            className="d-inline-block align-top"
                            alt="Uch∞se"
                            style={{marginLeft: '20px'}}
                        />
                    </Navbar.Brand>
                    <Navbar.Toggle aria-controls="responsive-navbar-nav"/>
                    <Navbar.Collapse id="responsive-navbar-nav">
                        <Nav className="mx-lg-auto">
                            <Form className="d-flex">
                                <FormControl
                                    type="search"
                                    placeholder="Search items, collections and accounts"
                                    className="me-4"
                                    aria-label="Search"
                                    style={{width: '45vw', marginLeft: '5vw'}}
                                    // size="lg"
                                />
                                <Button variant="outline-success">Search</Button>
                            </Form>
                        </Nav>
                    </Navbar.Collapse>
                    <Navbar.Collapse className="justify-content-end">
                        <div>
                            <WalletMultiButton/>
                            {wallet && <WalletDisconnectButton/>}
                        </div>
                    </Navbar.Collapse>
                </Container>
            </Navbar>
        </header>
    );
}

export default NavigationBar;
