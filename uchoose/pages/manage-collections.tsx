import { Layout } from "../components/layout";
import { useCeramic } from "use-ceramic";
import { CreateStream } from "../components/mint/create-stream";
import { TileDocument } from "@ceramicnetwork/stream-tile";
import { MintNft } from "../components/mint/mint-nft";
import { useState } from "react";
import { ChangeController } from "../components/mint/change-controller";

import { Form, Input, Checkbox, Button } from 'antd';

export default function MintPage() {
  const [tile, setTile] = useState<TileDocument | undefined>(undefined);
  const [token, setToken] = useState<
    { contract: string; tokenId: string } | undefined
  >(undefined);

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
      return <MintNft tile={tile} onDone={handleMint} />;
    } else {
      return <></>;
    }
  };

  const renderChangeController = () => {
    if (token && tile) {
      return <ChangeController tile={tile} token={token} />;
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
      labelCol={{ span: 8 }}
      wrapperCol={{ span: 16 }}
      initialValues={{ remember: true }}
      //onFinish={onFinish}
      //onFinishFailed={onFinishFailed}
      autoComplete="off"
    >
      <Form.Item
        label="Collection Name"
        name="collectionName"
        rules={[{ required: true, message: 'Please input Collection Name!' }]}
        >
          <Input  style={{width: "250px"}} />
        </Form.Item>

        <Form.Item
          label="Quantity of Nft"
          name="quantityOfNft"
          rules={[{ required: true, message: 'Please input Quantity of Nft!' }]}
        >
          <Input type="number" style={{width: "250px"}} />
        </Form.Item>

        {/* TODO - добавить больше элементов*/}

        <Form.Item name="mintFirstNft" valuePropName="checked" wrapperCol={{ offset: 8, span: 16 }}>
          <Checkbox>Mint the first NFT</Checkbox>
        </Form.Item>

        <Form.Item wrapperCol={{ offset: 8, span: 16 }}>
          <Button type="primary" htmlType="submit">
            Create
          </Button>
        </Form.Item>
      </Form>
     </Layout>
  );
}