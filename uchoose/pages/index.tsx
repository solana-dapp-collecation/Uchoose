import {useCeramic} from "use-ceramic";
import type {NextPage} from 'next'
import Head from 'next/head'
import styles from '../styles/Home.module.css'
import React, {useEffect, useState} from "react";
import topBarStyles from '../styles/top-bar.module.scss';
import "react-multi-carousel/lib/styles.css";
import CustomCarouselWithCards from "../components/carousel-component/CustomCarouselComponent";
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
import {getPartOfIdToShow, getBase64} from "../utils/utils";
import TagsCloudComponent from "../components/tags-cloud-component/TagsCloudComponent";
import Roadmap from "../components/roadmap-component/Roadmap";
import {Container, Button} from "react-bootstrap";


const DB_STORAGE_NAME: string = 'main_db';
const HEADER_LOGO = '/main-logo-2.png';


// Load image
// https://stackoverflow.com/questions/13938686/can-i-load-a-local-file-into-an-html-canvas-element

export const DBConfig = {
    name: DB_STORAGE_NAME,
    version: 1,
    objectStoresMeta: [
        {
            store: 'people',
            storeConfig: {keyPath: 'id', autoIncrement: true},
            storeSchema: [
                {name: 'name', keypath: 'name', options: {unique: false}},
                {name: 'email', keypath: 'email', options: {unique: false}}
            ]
        }
    ]
};

// initDB(DBConfig);

const Home: NextPage = ({articles}: any) => {

    // console.log(articles);

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

    const showModal = () => {
        setIsModalVisible(true);
    };

    const imageUpload = (e: any) => {
        const file = e.target.files[0];
        console.log(file);
        getBase64(file).then(base64 => {
            localStorage["fileBase64"] = base64;
            console.log("file stored", base64);
        });
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

            // TODO - до этого 
            let streamSchemaCollection1 = {
                collectionName: collectionName,
                quantityOfNft: parseInt(qtyOfNFTs),
                // TODO - тут будут идентификаторы частей, из которых состоит NFT
                nftPartIds: ["1", "2"],
                nftPictureWidth: parseInt(imgSizeWidth),
                nftPictureHeight: parseInt(imgSizeHeight),
                // Place file here
                nftPicture: '', //"0x99..asdasd", // - изначально до создания первой NFT тут пусто
                newCollectionProperties: {},
                coCreator: ''
            }

            // здесь создаём стрим для коллекции
            let resultCollection1 = await TileDocument.create(ceramic.client, streamSchemaCollection1,
                {
                    controllers: [ceramic.did.id],
                    schema: aliases.schemas.collection
                });

            console.log(resultCollection1);
            console.log('Создан стрим для первой коллекции: ' + resultCollection1.id.toString());
            message.success('Создан стрим для первой коллекции: ' + resultCollection1.id.toString(), 3);

            // здесь помещаем StreamID созданного стрима для первой коллекции в стрим для всех коллекций
            let streamSchemaCollections = {
                collectionsStreamIDs: [resultCollection1.id.toString()]
            }

            let resultCollections = await TileDocument.create(ceramic.client, streamSchemaCollections,
                {
                    controllers: [ceramic.did.id],
                    schema: aliases.schemas.collections
                });

            console.log(resultCollections);
            console.log('Создан стрим со всеми коллекциями: ' + resultCollections.id.toString());
            message.success('Создан стрим со всеми коллекциями: ' + resultCollections.id.toString(), 3);
        } catch (ex) {
            message.error('Can\'t create stream. See logs');
            console.log('%c --- Can\'t create stream ---', 'background-color: red');
            console.log(ex);
        }
        setIsModalVisible(false);
    };

    const handleCancel = () => {
        setIsModalVisible(false);
    };

    const saveIntoDb = () => {
        const openRequest = indexedDB.open(DB_STORAGE_NAME, 1);
        openRequest.onsuccess = function () {
            let db = openRequest.result;
            db.onversionchange = function () {
                db.close();
                alert("База данных устарела, пожалуста, перезагрузите страницу.")
            };
            // ...база данных доступна как объект db...
        };
        console.log(JSON.stringify(openRequest));
    }

    const createTestSchema = async () => {
        try {
            console.log('going to create schema');
            // Publish the two schemas
            console.log('before publish');

            // TODO - единожды при первом запуске сохранять, так как схемы надо создавать только один раз
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

            // TODO - разобраться с сохранением этих данных, чтобы были доступны всем глобально
            message.info('Schemas were created. Temporary solution. Should be initiate only once for specific network', 5);
            message.info(JSON.stringify(config), 5);
            message.info('Now frontend will send request to backend to retrieve some data', 5);
        } catch (e) {
            console.error(e);
        }
    }

    function handleCollectionNameInput(e: any) {
        setCollectionName(e.target.value);
    }

    function handleQuantityOfNFTsInput(e: any) {
        setQtyOfNFTs(e.target.value);
    }

    function handleImgWidthInput(e: any) {
        setImgWidth(e.target.value);
    }

    function handleImgHeightInput(e: any) {
        setImgHeight(e.target.value);
    }

    return (
        <Container className="p-3" style={{marginTop: '100px'}}>
            <>
                {/*TODO. Tob bar have to be moved into separated component*/}
                {/*Top bar*/}
                <div className={topBarStyles.topBarContainer}>
                    <div style={{display: 'inline-block', verticalAlign: 'middle', marginTop: '30px'}}>
                        {renderButton()}
                    </div>
                </div>
                {/*Main body*/}
                <div>
                    <div>
                        {/*TODO. Fix this one. Causing unexpected warnings*/}
                        <CustomCarouselWithCards/>
                    </div>

                    <Divider orientation="left"/>

                    <div className={styles.grid} style={{marginTop: '0px'}}>
                        <div className={`${styles.card} ${!isAuthenticated ? styles.cardDisabled : ''}`}>
                            <h2>Manage collections &rarr;</h2>
                            <p>Use to create your dynamic collection with setting its properties.</p>
                            <br/>
                            <Button disabled={!isAuthenticated} href="/manage-collections"
                                    variant="primary">Manage</Button>
                        </div>
                        <div className={`${styles.card} ${!isAuthenticated ? styles.cardDisabled : ''}`}>
                            <h2>Transactions &rarr;</h2>
                            <p>See transactions history for all each collection.</p>
                            <br/>
                            <Button disabled={!isAuthenticated} variant="primary">Transactions</Button>
                        </div>
                    </div>

                    <Roadmap/>

                    <Divider orientation="left"><b>For testing (dev) - delete later</b></Divider>
                    <Button onClick={() => createTestSchema()}>Test Saving Schemas. No sense to press. Already created
                        during auth</Button>
                    <Button onClick={() => saveIntoDb()}>Store to db. To be deleted. Just a stub</Button>

                    <Divider orientation="left"><b>Create collection</b></Divider>
                    <Button variant="primary" onClick={showModal}>
                        Create Collection
                    </Button>
                    <Modal title="Creating collection" visible={isModalVisible} okText={'Create'}
                           onOk={handleCreateNewNFTCollection}
                           onCancel={handleCancel}
                           width={800}
                    >
                        <Form
                            name="basic"
                            labelCol={{span: 8}}
                            wrapperCol={{span: 16}}
                            initialValues={{remember: true}}
                            autoComplete="off"
                        >
                            <Form.Item
                                label="Collection Name"
                                name="collectionName"
                                rules={[{required: true, message: 'Please input Collection Name!'}]}
                            >
                                <Input onChange={handleCollectionNameInput}/>
                            </Form.Item>
                            <Form.Item
                                label="Quantity of Nft"
                                name="quantityOfNft"
                                rules={[{required: true, message: 'Please input Quantity of Nft!'}]}
                            >
                                <Input type="number" onChange={handleQuantityOfNFTsInput}/>
                            </Form.Item>
                            <Form.Item
                                label="NFT width px"
                                name="nftWidth"
                                rules={[{required: true, message: 'Please fill in NFT width'}]}
                            >
                                <Input type="number" min={64} onChange={handleImgWidthInput}/>
                            </Form.Item>
                            <Form.Item
                                label="NFT height px"
                                name="nftHeight"
                                rules={[{required: true, message: 'Please fill in NFT height'}]}
                            >
                                <Input type="number" min={64} onChange={handleImgHeightInput}/>
                            </Form.Item>
                            <input
                                type="file"
                                id="imageFile"
                                name='imageFile'
                                onChange={imageUpload}/>
                        </Form>
                        <TagsCloudComponent width={600} height={500}/>
                    </Modal>
                    <TagsCloudComponent width={1000} height={800}/>
                </div>
            </>
        </Container>
    );
}

export default Home

export const getStaticProps = async () => {
    // TODO. Add here logic that retrieves publicly available images from server. Change endpoint to images retriever
    const res = await fetch(`https://jsonplaceholder.typicode.com/posts?_limit=6`);
    const articles = await res.json();
    return {
        props: {
            articles
        }
    }
}


// /**
//  * For work with mongo. Just for test
//  * @param context
//  */
// export async function getServerSideProps(context: any) {
//     // @ts-ignore
//     const client = await clientPromise
//
//     // client.db() will be the default database passed in the MONGODB_URI
//     // You can change the database by calling the client.db() function and specifying a database like:
//     // const db = client.db("myDatabase");
//     // Then you can execute queries against your database like so:
//     // db.find({}) or any of the MongoDB Node Driver commands
//
//     const isConnected = await client.isConnected()
//
//     return {
//         props: {isConnected},
//     }
// }

