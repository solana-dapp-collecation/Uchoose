import React, {FC} from "react";
// import {useWallet} from "@solana/wallet-adapter-react";
// import {WalletDisconnectButton, WalletMultiButton} from "@solana/wallet-adapter-react-ui";
import "./assets/styles/Home.module.css";
import {Footer} from "./Footer";
import Main from "./Main";

const MainComponent: FC = () => {
    // const { wallet } = useWallet();

    return (
        <div className="container">
            {/*<main className="main">*/}
            {/*</main>*/}
            <Main/>
            <Footer/>
        </div>
    );
};

export default MainComponent;
