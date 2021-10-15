import React from 'react';
import {Divider, Steps} from "antd";

const {Step} = Steps;

const Roadmap = () => (
    <>
        <Divider orientation="center"><b>Roadmap</b></Divider>
        <div style={{width: '400px', margin: '0 auto'}}>
            <Steps direction="vertical" current={1}>
                <Step title="In progress" description="MVP with Phantom wallet."/>
                <Step title="To do" description="Add NFT Card view."/>
                <Step title="To do" description="Add ability to create dynamic NFT collections."/>
            </Steps>
        </div>
        </>
);

export default Roadmap;
