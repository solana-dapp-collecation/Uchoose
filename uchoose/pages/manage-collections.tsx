import { Layout } from "../components/layout";
import { useCeramic } from "use-ceramic";
import { CreateStream } from "../components/mint/create-stream";
import { TileDocument } from "@ceramicnetwork/stream-tile";
import { MintNft } from "../components/mint/mint-nft";
import { useState } from "react";
import { ChangeController } from "../components/mint/change-controller";


const NoteSchema = {
  $schema: 'http://json-schema.org/draft-07/schema#',
  title: 'Note',
  type: 'object',
  properties: {
    date: {
      type: 'string',
      format: 'date-time',
      title: 'date',
      maxLength: 30,
    },
    text: {
      type: 'string',
      title: 'text',
      maxLength: 4000,
    },
  },
}

const NotesListSchema = {
  $schema: 'http://json-schema.org/draft-07/schema#',
  title: 'NotesList',
  type: 'object',
  properties: {
    notes: {
      type: 'array',
      title: 'notes',
      items: {
        type: 'object',
        title: 'NoteItem',
        properties: {
          id: {
            $ref: '#/definitions/CeramicStreamId',
          },
          title: {
            type: 'string',
            title: 'title',
            maxLength: 100,
          },
        },
      },
    },
  },
  definitions: {
    CeramicStreamId: {
      type: 'string',
      pattern: '^ceramic://.+(\\\\?version=.+)?',
      maxLength: 150,
    },
  },
}

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
      <div>
        <img src="/manage_page.png"/>
      </div>
      {/*   <CreateStream onCreate={handleCreateStream} />
       {renderMint()}
       {renderChangeController()} */}
     </Layout>
  );
}
