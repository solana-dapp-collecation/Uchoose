import {useCeramic} from "use-ceramic";
import type {NextPage} from 'next'
import Head from 'next/head'
import styles from '../styles/Home.module.css'
import React, {useEffect, useState} from "react";
import topBarStyles from '../styles/top-bar.module.scss';
import "react-multi-carousel/lib/styles.css";
import CustomCarouselWithCards from "../components/carousel-component/customCarouselComponent";
import {Button, Form, Modal} from "antd";
import {CollectionStreamSchema, CollectionsStreamSchema} from '../components/schemas';
import {DEFINITION_OF_SCHEMA_1, DID_TOKEN_KEY} from '../components/constants';
import {createDefinition, publishSchema} from '@ceramicstudio/idx-tools';
import {IDX} from '@ceramicstudio/idx';

import {Divider, Input} from 'antd';
import {Steps} from 'antd';

const {Step} = Steps;
const {Search} = Input;

import {Upload, message} from 'antd';
import {LoadingOutlined, PlusOutlined, UploadOutlined} from '@ant-design/icons';
import {TileDocument} from "@ceramicnetwork/stream-tile";
import client from "@hapi/wreck";

function getBase64(img: any, callback: any) {
    const reader = new FileReader();
    reader.addEventListener('load', () => callback(reader.result));
    reader.readAsDataURL(img);
}

function beforeUpload(file: any) {
    const isJpgOrPng = file.type === 'image/jpeg' || file.type === 'image/png';
    if (!isJpgOrPng) {
        message.error('You can only upload JPG/PNG file!');
    }
    const isLt2M = file.size / 1024 / 1024 < 2;
    if (!isLt2M) {
        message.error('Image must smaller than 2MB!');
    }
    return isJpgOrPng && isLt2M;
}

const Home: NextPage = () => {

    const ceramic = useCeramic();
    const [isAuthenticated, setAuthenticated] = useState(ceramic.isAuthenticated);
    const [isInProgress, setProgress] = useState(false);
    const [isModalVisible, setIsModalVisible] = useState(false);
    const [collectionName, setCollectionName] = useState('');
    const [qtyOfNFTs, setQtyOfNFTs] = useState('');
    const [imgSizeWidth, setImgWidth] = useState('');
    const [imgSizeHeight, setImgHeight] = useState('');

    // TODO. Handle this one properly. Prepare for collections of images
    const [imageData, setImageData] = useState('');

    const props = {
        name: 'file',
        action: '...',
        headers: {
            authorization: 'authorization-text',
        },
        onChange(info: any) {
            if (info.file.status !== 'uploading') {
                console.log(info.file, info.fileList);
            }
            if (info.file.status === 'done') {
                message.success(`${info.file.name} file uploaded successfully`);
            } else if (info.file.status === 'error') {
                message.error(`${info.file.name} file upload failed.`);
            }
        },
    };

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

    const showModal = () => {
        setIsModalVisible(true);
    };

    /**
     * Here we creating streams for specific collection and collections (it's StreamIDs) via IDX
     *
     */
    const handleOk = async () => {
        // add method that gathers data from form and put them into API to create record in ceramic
        // TODO. Create correct stream
        // const authProvider = await ceramic.connect();
        // let didToken = await ceramic.authenticate(authProvider);
        let schemaDef = localStorage.getItem(DEFINITION_OF_SCHEMA_1);
        // @ts-ignore
        const idx = new IDX({ceramic: ceramic.client, aliases: schemaDef});


        // TODO. All data have to be passed from form, populated by creator/user
        let streamSchemaCollection1 = {
            "collectionName": "Collection1",
                "quantityOfNft": 10,
                "nftPartIds": [ "1", "2" ],
                "nftPictureWidth": 256,
                "nftPictureHeight": 256,
                "nftPicture": "0x99..asdasd",
                "newCollectionProperties": null,
                "coCreator": null
        }
        // Here we are passing stream id of stream collection
        let resultCollection1 = await idx.set('Collection1', streamSchemaCollection1);
        console.log(resultCollection1);
        let streamSchemaCollections = {
            collectionsStreamIDs: [resultCollection1.cid]
        }
        let result = await idx.set('Collections', streamSchemaCollections);
        console.log(result);
        // Load the existing notes
        // const notesList = await idx.get<{ notes: Array<NoteItem> }>('notes')
        // const tile = await TileDocument.create(ceramic.client, {
        //     name: name,
        //     description: description,
        //     image: cid,
        // });
        // setStreamId(tile.id.toString());
        setIsModalVisible(false);
    };

    const handleCancel = () => {
        setIsModalVisible(false);
    };

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
            // запись в файл не работает
            // await writeFile('./configs/config.json', JSON.stringify(config))
            // console.log('Looks like all saved!');
        } catch (e) {
            console.error(e);
        }
    }

    function handleCollectionNameInput(e: any) {
        // editing.hours = e.target.value;
        // setEditing(editing);
        setCollectionName(e.target.value);
        console.log(e.target.value);
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

                <Divider orientation="left"><b>Create collection</b></Divider>
                <Button type="primary" onClick={showModal}>
                    Create Collection
                </Button>
                <Modal title="Creating collection" visible={isModalVisible} okText={'Create'} onOk={handleOk}
                       onCancel={handleCancel}>
                    <Form
                        name="basic"
                        labelCol={{span: 8}}
                        wrapperCol={{span: 16}}
                        initialValues={{remember: true}}
                        //onFinish={onFinish}
                        //onFinishFailed={onFinishFailed}
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
                            <Input type="number"/>
                        </Form.Item>

                        <Form.Item
                            label="NFT width px"
                            name="nftWidth"
                            rules={[{required: true, message: 'Please fill in NFT width'}]}
                        >
                            <Input type="number" min={64}/>
                        </Form.Item>
                        <Form.Item
                            label="NFT height px"
                            name="nftHeight"
                            rules={[{required: true, message: 'Please fill in NFT height'}]}
                        >
                            <Input type="number" min={64}/>
                        </Form.Item>

                        {/* TODO - добавить больше элементов*/}

                        <Form.Item
                            label="Layers/Images"
                            name="layers_images"
                            rules={[{required: true, message: 'Please upload layers/images!'}]}
                        >
                            <Upload {...props}>
                                <Button icon={<UploadOutlined/>}>Click to Upload</Button>
                            </Upload>
                        </Form.Item>

                        {/*<Form.Item wrapperCol={{offset: 8, span: 16}}>*/}
                        {/*    <Button type="primary" htmlType="submit">*/}
                        {/*        Create*/}
                        {/*    </Button>*/}
                        {/*</Form.Item>*/}
                    </Form>
                </Modal>
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
