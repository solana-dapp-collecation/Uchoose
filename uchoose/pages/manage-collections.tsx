import {Layout} from "../components/layout";
import {useCeramic} from "use-ceramic";
import {CreateStream} from "../components/mint/create-stream";
import {TileDocument} from "@ceramicnetwork/stream-tile";
import {MintNft} from "../components/mint/mint-nft";
import {useState} from "react";
import {ChangeController} from "../components/mint/change-controller";

import {Form, Input, Checkbox, Button} from 'antd';

import React from 'react';
import {Upload} from 'antd';
// import ImgCrop from 'antd-img-crop';

{/* https://ant.design/components/upload/ */
}
{/*const UploadedProperties = () => {
  const [fileList, setFileList] = useState([
    {
      uid: '-1',
      name: 'face-with-tongue.png',
      status: 'done',
      url: './public/layers/face-with-tongue.png',
    },
  ]);

  const onChange = ({ fileList: newFileList }) => {
    setFileList(newFileList);
  };

  const onPreview = async file => {
    let src = file.url;
    if (!src) {
      src = await new Promise(resolve => {
        const reader = new FileReader();
        reader.readAsDataURL(file.originFileObj);
        reader.onload = () => resolve(reader.result);
      });
    }
    const image = new Image();
    image.src = src;
    const imgWindow = window.open(src);
    imgWindow.document.write(image.outerHTML);
  };

  return (
    <ImgCrop rotate>
      <Upload
        action="https://www.mocky.io/v2/5cc8019d300000980a055e76"
        listType="picture-card"
        fileList={fileList}
        onChange={onChange}
        onPreview={onPreview}
      >
        {fileList.length < 5 && '+ Upload'}
      </Upload>
    </ImgCrop>
  );
};*/
}

export default function ManageCollectionPage() {
    const [tile, setTile] = useState<TileDocument | undefined>(undefined);
    const [token, setToken] = useState<{ contract: string; tokenId: string } | undefined>(undefined);

    const handleMint = (contract: string, tokenId: string) => {
        setToken({
            contract,
            tokenId,
        });
    };

    const handleCreateStream = (tile: TileDocument) => {
        setTile(tile);
    };

    const renderMint = () => {
        if (tile) {
            return <MintNft tile={tile} onDone={handleMint}/>;
        } else {
            return <></>;
        }
    };

    const renderChangeController = () => {
        if (token && tile) {
            return <ChangeController tile={tile} token={token}/>;
        } else {
            return <></>;
        }
    };

    return (

        <Layout>
            {/*<div>
        <img src="/manage_page.png"/>
      </div>*/}

            {/*   <CreateStream onCreate={handleCreateStream} />
       {renderMint()}
       {renderChangeController()} */}

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
                    <Input style={{width: "450px"}}/>
                </Form.Item>

                <Form.Item
                    label="Quantity of Nft"
                    name="quantityOfNft"
                    rules={[{required: true, message: 'Please input Quantity of Nft!'}]}
                >
                    <Input type="number" style={{width: "450px"}}/>
                </Form.Item>

                {/* TODO - добавить больше элементов*/}

                <Form.Item
                    label="Parameters"
                    name="parameters"
                    rules={[{required: true, message: 'Please input Parameters!'}]}
                >
                    {/* TODO - разобраться, как подключить и добавить*/}
                    {/*<UploadedProperties/>*/}
                </Form.Item>

                <Form.Item wrapperCol={{offset: 8, span: 16}}>
                    <Button type="primary" htmlType="submit">
                        Create
                    </Button>
                </Form.Item>
            </Form>
        </Layout>
    );
}
