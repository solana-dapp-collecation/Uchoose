import React from 'react';
import {Divider, Steps} from "antd";

const {Step} = Steps;

const Roadmap = () => (
    <>
        <Divider orientation="center"><b>Roadmap. Move roadmap to separate component</b></Divider>
        <div style={{width: '400px', margin: '0 auto'}}>
            <Steps direction="vertical" current={1}>
                <Step title="Finished" description="MVP."/>
                <Step title="In Progress" description="Add NFT Card view."/>
                <Step title="Waiting" description="Add ability to create dynamic NFT collections."/>
            </Steps>
        </div>
        </>
);

export default Roadmap;
