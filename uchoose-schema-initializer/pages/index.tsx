import {useCeramic} from "use-ceramic";
import type {NextPage} from 'next'
import Head from 'next/head'
import styles from '../styles/Home.module.css'
import React, {useEffect, useState} from "react";
import topBarStyles from '../styles/top-bar.module.css';
import "react-multi-carousel/lib/styles.css";
import {Form, Modal} from "antd";
import {CollectionStreamSchema, CollectionsStreamSchema} from '../schemas/schemas';
import {DEFINITION_OF_SCHEMA_1, DID_TOKEN_KEY} from '../constants/constants';
import {createDefinition, publishSchema} from '@ceramicstudio/idx-tools';
import {IDX} from '@ceramicstudio/idx';

import {Divider, Input} from 'antd';

const {Search} = Input;

import {Upload, message} from 'antd';
import {LoadingOutlined, PlusOutlined, UploadOutlined} from '@ant-design/icons';
import {TileDocument} from "@ceramicnetwork/stream-tile";
import client from "@hapi/wreck";
import {Container, Button} from "react-bootstrap";


const HEADER_LOGO = '/main-logo-2.png';


const Home: NextPage = () => {

    const ceramic = useCeramic();
    const [isAuthenticated, setAuthenticated] = useState(ceramic.isAuthenticated);
    const [isInProgress, setProgress] = useState(false);
    const [isModalVisible, setIsModalVisible] = useState(false);
    const [collectionName, setCollectionName] = useState('');
    const [qtyOfNFTs, setQtyOfNFTs] = useState('');
    const [imgSizeWidth, setImgWidth] = useState('');
    const [imgSizeHeight, setImgHeight] = useState('');

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

    // TODO. Create custom auth form. For now auth form is controlled by ceramic library
    const handleLogin = async () => {
        setProgress(true);
        try {
            const authProvider = await ceramic.connect();
            let didToken = await ceramic.authenticate(authProvider);
            let tokenFromStorage = localStorage.getItem(DID_TOKEN_KEY);
            if (!tokenFromStorage) {
                localStorage.setItem(DID_TOKEN_KEY, didToken.id);
            }
            message.success(`We got did token. ${JSON.stringify(didToken)}`, 5);
            await createTestSchema();
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
        } else if (isAuthenticated) {
            let didToken = localStorage.getItem(DID_TOKEN_KEY);
            return (
                <>
                    <button onClick={handleLogin}><b>{didToken}</b></button>
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

    /**
     * Here we creating streams for specific collection and collections (it's StreamIDs) via IDX
     *
     */
    const handleCreateNewNFTCollection = async () => {
        try {
            let schemaDef = localStorage.getItem(DEFINITION_OF_SCHEMA_1);
            // @ts-ignore
            const idx = new IDX({ceramic: ceramic.client, aliases: schemaDef});
            let aliases = JSON.parse(schemaDef as string);

            // TODO. All data have to be passed from form, populated by creator/user
            // let streamSchemaCollection1 = {
            //     "collectionName": "Collection1",
            //         "quantityOfNft": 10,
            //         "nftPartIds": [ "1", "2" ],
            //         "nftPictureWidth": 256,
            //         "nftPictureHeight": 256,
            //         "nftPicture": "0x99..asdasd",
            //         "newCollectionProperties": null,
            //         "coCreator": null
            // }

            // TODO - ???? ?????????? 
            let streamSchemaCollection1 = {
                collectionName: collectionName,
                quantityOfNft: parseInt(qtyOfNFTs),
                // TODO - ?????? ?????????? ???????????????????????????? ????????????, ???? ?????????????? ?????????????? NFT
                nftPartIds: ["1", "2"],
                nftPictureWidth: parseInt(imgSizeWidth),
                nftPictureHeight: parseInt(imgSizeHeight),
                // Place file here
                nftPicture: '', //"0x99..asdasd", // - ???????????????????? ???? ???????????????? ???????????? NFT ?????? ??????????
                newCollectionProperties: {},
                coCreator: ''
            }

            // ?????????? ?????????????? ?????????? ?????? ??????????????????
            let resultCollection1 = await TileDocument.create(ceramic.client, streamSchemaCollection1,
                {
                    controllers: [ceramic.did.id],
                    schema: aliases.schemas.collection
                });

            console.log(resultCollection1);
            console.log('???????????? ?????????? ?????? ???????????? ??????????????????: ' + resultCollection1.id.toString());
            message.success('???????????? ?????????? ?????? ???????????? ??????????????????: ' + resultCollection1.id.toString(), 3);

            // ?????????? ???????????????? StreamID ???????????????????? ???????????? ?????? ???????????? ?????????????????? ?? ?????????? ?????? ???????? ??????????????????
            let streamSchemaCollections = {
                collectionsStreamIDs: [resultCollection1.id.toString()]
            }

            let resultCollections = await TileDocument.create(ceramic.client, streamSchemaCollections,
                {
                    controllers: [ceramic.did.id],
                    schema: aliases.schemas.collections
                });

            console.log(resultCollections);
            console.log('???????????? ?????????? ???? ?????????? ??????????????????????: ' + resultCollections.id.toString());
            message.success('???????????? ?????????? ???? ?????????? ??????????????????????: ' + resultCollections.id.toString(), 3);
        } catch (ex) {
            message.error('Can\'t create stream. See logs');
            console.log('%c --- Can\'t create stream ---', 'background-color: red');
            console.log(ex);
        }
        setIsModalVisible(false);
    };

    const createTestSchema = async () => {
        try {
            console.log('going to create schema');
            // Publish the two schemas
            console.log('before publish');

            // TODO - ???????????????? ?????? ???????????? ?????????????? ??????????????????, ?????? ?????? ?????????? ???????? ?????????????????? ???????????? ???????? ??????
            const [collectionsStreamSchema, collectionStreamSchema] = await Promise.all([
                publishSchema(ceramic.client, {content: CollectionsStreamSchema, name: 'CollectionsStreamSchema'}),
                publishSchema(ceramic.client, {content: CollectionStreamSchema, name: 'CollectionStreamSchema'}),
            ])
            console.log('%c --- after schema publish ---', 'background-color: red');
            console.log(collectionsStreamSchema);
            console.log(collectionStreamSchema);
            console.log('%c ---', 'background-color: red');
            console.log('before definition');

            // Create the definition using the created schema ID
            const collectionStreamDefinition = await createDefinition(ceramic.client, {
                name: 'CollectionStream',
                description: 'Stream with all NFT of concrete collection',
                schema: collectionStreamSchema.commitId.toUrl(),
            });
            const collectionsStreamDefinition = await createDefinition(ceramic.client, {
                name: 'CollectionsStream',
                description: 'Stream with all collections StreamID',
                schema: collectionsStreamSchema.commitId.toUrl(),
            });
            console.log('%c --- after definition ---', 'background-color: red');
            console.log(collectionsStreamDefinition);
            console.log(collectionStreamDefinition);
            console.log('%c ---', 'background-color: red');
            console.log('after definition');

            console.log('Looks like that something happened');

            // Write config to JSON file
            const config = {
                definitions: {
                    collection: collectionStreamDefinition.id.toString(),
                    collections: collectionsStreamDefinition.id.toString(),
                },
                schemas: {
                    collection: collectionStreamSchema.commitId.toUrl(),
                    collections: collectionsStreamSchema.commitId.toUrl(),
                },
            }

            console.log(JSON.stringify(config));
            localStorage.setItem(DEFINITION_OF_SCHEMA_1, JSON.stringify(config));

            // TODO - ?????????????????????? ?? ?????????????????????? ???????? ????????????, ?????????? ???????? ???????????????? ???????? ??????????????????
            message.info('Schemas were created. Temporary solution. Should be initiate only once for specific network', 5);
            message.info(JSON.stringify(config), 5);
            message.info('Now frontend will send request to backend to retrieve some data', 5);
        } catch (e) {
            console.error(e);
        }
    }

    return (
        <Container className="p-3" style={{marginTop: '100px'}}>
            <>
                {/*Top bar*/}
                <div className={topBarStyles.topBarContainer}>
                    <div style={{display: 'inline-block', verticalAlign: 'middle', marginTop: '30px'}}>
                        {/*HERE WE GOING TO CREATE SCHEMA RIGHT AFTER AUTHENTICATION*/}
                        {renderButton()}
                    </div>
                </div>
                {/*Main body*/}
                <div>
                    <Divider orientation="center"><b>To initialize schema in network manually</b></Divider>
                    <div style={{textAlign: 'center'}}>
                        <Button className="me-4" onClick={() => createTestSchema()}>Test Saving Schemas. No sense to
                            use it. Already created during authentication. Just press auth button</Button>
                    </div>
                </div>
            </>
        </Container>
    );
}

export default Home;
