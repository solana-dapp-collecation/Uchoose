import React, {StrictMode} from 'react';
import ReactDOM from 'react-dom';
import Wallet from './phantom-connector/Wallet';
import "antd/dist/antd.css";
// @ts-ignore
import Layout from '../components/Layout';
import 'bootstrap/dist/css/bootstrap.min.css';
import "bootstrap-icons/font/bootstrap-icons.css";

// Use require instead of import, and order matters
require('@solana/wallet-adapter-react-ui/styles.css');
require('./assets/styles/index.css');


ReactDOM.render(
    <StrictMode>
        <Wallet/>
    </StrictMode>,
    document.getElementById('root')
);
