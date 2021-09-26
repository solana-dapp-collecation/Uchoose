import {useCeramic} from "use-ceramic";
import type {NextPage} from 'next'
import Head from 'next/head'
import styles from '../styles/Home.module.css'
import React, {useEffect, useState} from "react";
import topBarStyles from '../styles/top-bar.module.scss';
import "react-multi-carousel/lib/styles.css";
import CustomCarouselWithCards from "../components/carousel-component/customCarouselComponent";
import {Button} from "antd";
import {CollectionStreamSchema, CollectionsStreamSchema} from '../components/schemas';
import {DID_TOKEN_KEY} from '../components/constants';
import {createDefinition, publishSchema} from '@ceramicstudio/idx-tools';

import {Divider, Input} from 'antd';
import {Steps} from 'antd';

const {Step} = Steps;
const {Search} = Input;

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
            console.log(authProvider);
            let didToken = await ceramic.authenticate(authProvider);
            let tokenFromStorage = localStorage.getItem(DID_TOKEN_KEY);
            if (!tokenFromStorage) {
                localStorage.setItem(DID_TOKEN_KEY, didToken.id);
            }
            console.log(didToken);
        } catch (e) {
            console.error(e);
        } finally {
            setProgress(false);
        }
    };

    const getPartOfIdToShow = (): string => {
        let didToken = localStorage.getItem(DID_TOKEN_KEY);
        if (!didToken) {
            didToken = 'Authenticated';
        } else {
            try {
                didToken = didToken.slice(0, 15);
                didToken += '...';
            } catch {
            }
        }
        return didToken;
    }

    const renderButton = () => {
        if (isInProgress) {
            return (
                <>
                    <button disabled={true}>Connecting...</button>
                </>
            );
        } else if (isAuthenticated) {
            return (
                <>
                    <button onClick={handleLogin}><b>{getPartOfIdToShow()}</b></button>
                </>
            );
        } else {
            return (
                <>
                    <button onClick={handleLogin}><b>Connect Wallet</b></button>
                </>
            )
        }
    };

    const redirectToLink = () => {
        console.log('redirect to link');
    }

    const createTestSchema = async () => {
        try {
            console.log('going to create schema');
            // Publish the two schemas
            console.log('before publish');

            // TODO - единожды при первом запуске сохранять, так как схемы надо создавать только один раз
            const [collectionStreamSchema, collectionsStreamSchema] = await Promise.all([
                publishSchema(ceramic.client, {content: CollectionsStreamSchema, name: 'CollectionsStreamSchema'}),
                publishSchema(ceramic.client, {content: CollectionStreamSchema, name: 'CollectionStreamSchema'}),
            ])
            console.log('%c --- after schema publish ---', 'background-color: red');
            console.log(collectionsStreamSchema);
            console.log('%c ---', 'background-color: red');
            console.log('before definition');

            // Create the definition using the created schema ID
            const collectionsStreamDefinition = await createDefinition(ceramic.client, {
                name: 'CollectionsStream',
                description: 'Stream with all collections StreamID',
                schema: collectionsStreamSchema.commitId.toUrl(),
            })
            console.log('%c --- after definition ---', 'background-color: red');
            console.log(collectionsStreamDefinition);
            console.log('%c ---', 'background-color: red');
            console.log('after definition');

            console.log('Looks like that something happened');

            // Write config to JSON file
            const config = {
                definitions: {
                    collections: collectionsStreamDefinition.id.toString(),
                },
                schemas: {
                    collection: collectionStreamSchema.commitId.toUrl(),
                    collections: collectionsStreamSchema.commitId.toUrl(),
                },
            }

            console.log(JSON.stringify(config));

            // TODO - разобраться с сохранением этих данных, чтобы были доступны всем глобально
            // запись в файл не работает
            // await writeFile('./configs/config.json', JSON.stringify(config))
            // console.log('Looks like all saved!');
        } catch (e) {
            console.error(e);
        }
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
                <div className={topBarStyles.mainSearchBar}>
                    <Search placeholder="Search items, collections and accounts" enterButton/>
                </div>
                <div>
                    <CustomCarouselWithCards/>
                </div>

                <Divider orientation="left"/>

                <div className={styles.grid} style={{marginTop: '0px'}}>
                    <div className={`${styles.card} ${!isAuthenticated ? styles.cardDisabled : ''}`}>
                        <h2>Manage collections &rarr;</h2>
                        <p>Use to create your dynamic collection with setting its properties.</p>
                        <br/>
                        <Button disabled={!isAuthenticated} href="/manage-collections" type="primary">Manage</Button>
                    </div>
                    {/*<div className={styles.card}>
                        <h2>View &rarr;</h2>
                        <p>View existing collections</p>
                        <Button href="/views" type="primary">View</Button>
                    </div>*/}
                    <div className={`${styles.card} ${!isAuthenticated ? styles.cardDisabled : ''}`}>
                        <h2>Transactions &rarr;</h2>
                        <p>See transactions history for all each collection.</p>
                        <br/>
                        <Button disabled={!isAuthenticated} type="primary">Transactions</Button>
                    </div>
                    {/*<div className={`${styles.card} ${!isAuthenticated ? styles.cardDisabled : ''}`}>
                        <h2>Logs/Statistics &rarr;</h2>
                        <p>See logs and statistics</p>
                        <Button disabled={!isAuthenticated} type="primary">Logs</Button>
                    </div>*/}
                </div>

                <Divider orientation="left"><b>Roadmap</b></Divider>
                <div>
                    <Steps direction="vertical" current={1}>
                        <Step title="Finished" description="Create MVP on the Hackathon."/>
                        <Step title="In Progress" description="Add NFT Card view."/>
                        <Step title="Waiting" description="Add ability to create dynamic NFT collections."/>
                    </Steps>
                </div>

                <Divider orientation="left"><b>For testing (dev) - delete later</b></Divider>
                <Button onClick={() => createTestSchema()}>Test Saving Schemas</Button>
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
