import {WalletAdapterNetwork, WalletError} from '@solana/wallet-adapter-base';
import {ConnectionProvider, WalletProvider} from '@solana/wallet-adapter-react';
import {WalletModalProvider} from '@solana/wallet-adapter-react-ui';
import {
    getLedgerWallet,
    getPhantomWallet,
    getSlopeWallet,
    getSolflareWallet,
    getSolletWallet,
    getSolletExtensionWallet,
    getTorusWallet,
} from '@solana/wallet-adapter-wallets';
import {clusterApiUrl} from '@solana/web3.js';
import React, {FC, useCallback, useMemo} from 'react';
import toast, {Toaster} from 'react-hot-toast';
import Notification from '../Notification';
import NavigationBar from "../NavigationBar";
import MainComponent from "../MainConponent";

import {CeramicProvider, CeramicService, Networks} from "use-ceramic";
import {Web3Provider, Web3Service} from "../use-web3";
import WalletConnectProvider from "@walletconnect/web3-provider";
import {EthereumAuthProvider} from "@ceramicnetwork/blockchain-utils-linking";
import "antd/dist/antd.css";
// @ts-ignore
import Layout from '../components/Layout';
import 'bootstrap/dist/css/bootstrap.min.css';
import "bootstrap-icons/font/bootstrap-icons.css";


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


// @ts-ignore
ceramicService.connect = async () => {
    await web3Service.connect();
    const provider = web3Service.provider;
    const web3 = web3Service.web3;
    const accounts = await web3.eth.getAccounts();
    return new EthereumAuthProvider(provider, accounts[0]);
};


const Wallet: FC = () => {
    const network = WalletAdapterNetwork.Devnet;
    const endpoint = useMemo(() => clusterApiUrl(network), [network]);

    // @solana/wallet-adapter-wallets imports all the adapters but supports tree shaking --
    // Only the wallets you want to support will be compiled into your application
    const wallets = useMemo(
        () => [
            getPhantomWallet(),
            getSlopeWallet(),
            getSolflareWallet(),
            getTorusWallet({
                options: {clientId: 'Get a client ID @ https://developer.tor.us'},
            }),
            getLedgerWallet(),
            getSolletWallet({network}),
            getSolletExtensionWallet({network}),
        ],
        [network]
    );

    const onError = useCallback(
        (error: WalletError) =>
            toast.custom(
                <Notification
                    message={error.message ? `${error.name}: ${error.message}` : error.name}
                    variant="error"
                />
            ),
        []
    );

    return (
        <Web3Provider service={web3Service}>
            <CeramicProvider service={ceramicService}>
                <ConnectionProvider endpoint={endpoint}>
                    <WalletProvider wallets={wallets} onError={onError} autoConnect>
                        <WalletModalProvider>
                            <NavigationBar/>
                            {/*<Navigation/>*/}
                            <MainComponent/>
                        </WalletModalProvider>
                        <Toaster position="bottom-left" reverseOrder={false}/>
                    </WalletProvider>
                </ConnectionProvider>
            </CeramicProvider>
        </Web3Provider>
    );
};

export default Wallet;
