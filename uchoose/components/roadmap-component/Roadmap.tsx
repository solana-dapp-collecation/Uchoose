import React from 'react';
import {Divider, Steps} from "antd";

const {Step} = Steps;

const Roadmap = (props: any) => (
    <>
        <Divider orientation="left"><b>Roadmap. Move roadmap to separate component</b></Divider>
        <div>
            <Steps direction="vertical" current={1}>
                <Step title="Finished" description="MVP."/>
                <Step title="In Progress" description="Add NFT Card view."/>
                <Step title="Waiting" description="Add ability to create dynamic NFT collections."/>
            </Steps>
        </div>
        </>
);

export default Roadmap;
