import "../styles/globals.css";
import type {AppProps} from "next/app";
import React from "react";
import {CeramicProvider, CeramicService, Networks} from "use-ceramic";
import {Web3Provider, Web3Service} from "../components/use-web3";
import WalletConnectProvider from "@walletconnect/web3-provider";
import {EthereumAuthProvider} from "@ceramicnetwork/blockchain-utils-linking";
import "antd/dist/antd.css";
// @ts-ignore
import Layout from '../components/Layout';
import 'bootstrap/dist/css/bootstrap.min.css';
import "bootstrap-icons/font/bootstrap-icons.css";
import dynamic from 'next/dynamic';

const web3Service = new Web3Service({
    network: "rinkeby",
    cacheProvider: false,
    providerOptions: {
        injected: {
            package: null,
        },
        walletconnect: {
            package: WalletConnectProvider,
            options: {
                infuraId: "b407db983da44def8a68e3fdb6bea776",
            },
        },
    },
});

const ceramicService = new CeramicService(
    Networks.DEV_UNSTABLE,
    // 'http://localhost:7007'
    "https://ceramic-dev.3boxlabs.com"
);

const WalletConnectionProvider = dynamic(() => import('../components/phantom-connector/WalletConnectionProvider'), {
    ssr: false,
});


// @ts-ignore
ceramicService.connect = async () => {
    await web3Service.connect();
    const provider = web3Service.provider;
    const web3 = web3Service.web3;
    const accounts = await web3.eth.getAccounts();
    return new EthereumAuthProvider(provider, accounts[0]);
};

function MyApp({Component, pageProps}: AppProps) {
    return (
        <WalletConnectionProvider>
            <Web3Provider service={web3Service}>
                <CeramicProvider service={ceramicService}>
                    <Layout>
                        <Component {...pageProps} />
                    </Layout>
                </CeramicProvider>
            </Web3Provider>
        </WalletConnectionProvider>
    );
}

export default MyApp;
