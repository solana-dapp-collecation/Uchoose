import {useCeramic} from "use-ceramic";
import type {NextPage} from 'next'
import Head from 'next/head'
import styles from '../styles/Home.module.css'
import React, {useEffect, useState} from "react";
import topBarStyles from '../styles/top-bar.module.scss';
import "react-multi-carousel/lib/styles.css";
import CustomCarouselWithCards from "../components/carousel-component/customCarouselComponent";
import {Button} from "antd";
import {CollectionStreamSchema, CollectionsStreamSchema} from '../components/constants';
import { createDefinition, publishSchema } from '@ceramicstudio/idx-tools';

import { Divider } from 'antd';
import { Steps } from 'antd';

const { Step } = Steps;

const Home: NextPage = () => {

    const ceramic = useCeramic();
    const [isAuthenticated, setAuthenticated] = useState(ceramic.isAuthenticated);
    const [isInProgress, setProgress] = useState(false);

    useEffect(() => {
        const subscription = ceramic.isAuthenticated$.subscribe(
            (isAuthenticated) => {
                setAuthenticated(isAuthenticated);

            }
        );

        return () => {
            subscription.unsubscribe();
        };
    });

    const handleLogin = async () => {
        setProgress(true);
        try {
            const authProvider = await ceramic.connect();
            let didToken = await ceramic.authenticate(authProvider);
            let tokenFromStorage = localStorage.getItem('didToken');
            if (!tokenFromStorage) {
                localStorage.setItem('didToken', didToken.id);
            }
            console.log(didToken);
        } catch (e) {
            console.error(e);
        } finally {
            setProgress(false);
        }
    };

    const renderButton = () => {
        if (isInProgress) {
            return (
                <>
                    <button disabled={true}>Connecting...</button>
                </>
            );
        } else {
            return (
                <>
                    <button onClick={handleLogin}>Connect Wallet</button>
                </>
            );
        }
    };

    const redirectToLink = () => {
        console.log('123');
    }

    const createTestSchema = async () => {
        console.log('123');
        console.log('going to create schema');
        // Publish the two schemas
        console.log('before publish');
        const [collectionStreamSchema, collectionsStreamSchema] = await Promise.all([
            publishSchema(ceramic.client, { content: CollectionsStreamSchema }),
            publishSchema(ceramic.client, { content: CollectionStreamSchema }),
        ])
        console.log('after publish');

        console.log('before definition');

        // Create the definition using the created schema ID
        const collectionsStreamDefinition = await createDefinition(ceramic.client, {
            name: 'CollectionsStream',
            description: 'Stream with all collections StreamID',
            schema: collectionsStreamSchema.commitId.toUrl(),
        })
        console.log('after definition');

        console.log('Looks like that something happened');
    }

    return (
        <div className={styles.container}>
            <Head>
                <title>Uch∞se</title>
                <meta name="description" content="Generated by create next app"/>
                <link rel="icon" href="/main-logo-2.png"/>
            </Head>

            {/*Top bar*/}
            <div className={topBarStyles.topBarContainer}>
                <div className={styles.logo}
                     style={{display: 'inline-block', verticalAlign: 'middle', marginRight: '10px'}}>
                    <img src="/main-logo-2.png" alt="Uch∞se" style={{width: '100px'}}/>
                </div>
                <div style={{display: 'inline-block', verticalAlign: 'middle', marginTop: '30px'}}>
                    {renderButton()}
                </div>
            </div>
            {/*Main body*/}
            <main className={styles.main}>

                <div className={styles.grid} style={{marginTop: '0px'}}>
                    <div className={`${styles.card} ${!isAuthenticated ? styles.cardDisabled : ''}`}>
                        <h2>Manage collections &rarr;</h2>
                        <p>Use to create your collection. Place orders</p>
                        <Button disabled={!isAuthenticated} href="/manage-collections" type="primary">Manage</Button>
                    </div>
                    <div className={styles.card}>
                        <h2>View &rarr;</h2>
                        <p>View existing collections</p>
                        <Button href="/views" type="primary">View</Button>
                    </div>
                    <div className={`${styles.card} ${!isAuthenticated ? styles.cardDisabled : ''}`}>
                        <h2>Transactions &rarr;</h2>
                        <p>See transactions history</p>
                        <Button disabled={!isAuthenticated} type="primary">Transactions</Button>
                    </div>
                    <div className={`${styles.card} ${!isAuthenticated ? styles.cardDisabled : ''}`}>
                        <h2>Logs/Statistics &rarr;</h2>
                        <p>See logs and statistics</p>
                        <Button disabled={!isAuthenticated} type="primary">Logs</Button>
                    </div>
                </div>
                <div style={{marginTop: '1vh'}}>
                    <CustomCarouselWithCards/>
                </div>

                <Divider orientation="left"><b>Roadmap</b></Divider>
                <div>
                    <Steps direction="vertical" current={1}>
                        <Step title="Finished" description="Create MVP on the Hackathon." />
                        <Step title="In Progress" description="Add NFT Card view." />
                        <Step title="Waiting" description="Add ability to create dynamic NFT collections." />
                    </Steps>
                </div>
                
                <Divider orientation="left"><b>For testing (dev) - delete later</b></Divider>
                <Button onClick={()=>createTestSchema()}>Auth</Button>
            </main>

            <footer>
                <a
                    href="#"
                    target="_blank"
                    rel="noopener noreferrer"
                >
                    Made by{' '}
                    <span className={styles.logo}>
                        <img src="/main-logo.png" alt="LifeLoopTeam" style={{width: '120px', marginTop: '-15px'}}/>
                    </span>
                </a>
            </footer>
        </div>
    )
}

export default Home
