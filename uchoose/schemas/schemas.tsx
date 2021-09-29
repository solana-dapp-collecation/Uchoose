export const CollectionsStreamSchema = {
    $schema: 'http://json-schema.org/draft-07/schema#',
    title: 'CollectionsStream',
    type: 'object',
    properties: {
        collectionsStreamIDs: { // список StreamID всех созданных Creator'ом коллекций
            type: 'array',
            items: { type: 'string' },
        }
    },
    'required': [
        'collectionsStreamIDs'
    ]
}

export const CollectionStreamSchema = {
    $schema: 'http://json-schema.org/draft-07/schema#',
    title: 'CollectionStream',
    type: 'object',
    properties: {
        collectionName: {
            type: 'string'
        },
        quantityOfNft: { // кол-во NFT в коллекции (меняться не будет)
            type: 'integer'
        },
        nftPartIds: { // части-слои для новой NFT (только их id) - меняться будет, для каждой NFT свой набор частей
            type: 'array',
            uniqueItems: true,
            items: {
                type: 'string'
            }
        },
        nftPictureWidth: { // ширина NFT (меняться не будет)
            type: 'integer'
        },
        nftPictureHeight: { // высота NFT (меняться не будет)
            type: 'integer'
        },
        nftPicture: { // сама NFT в виде массива байт (меняться будет для каждой NFT), либо адрес Etherium-контракта
            type: 'string'
        },
        coCreator: { // тут будет идентификатор Buyer'а, который купил предыдущую NFT и задал параметры текущей NFT
            type: 'string'
        },
        newCollectionProperties: { // параметры новой коллекции (во всех NFT, кроме последней, тут будет значение null) 
            type: 'object',
            properties: {
              collectionName: {
                type: 'string'
              },
              numberOfParts: {
                type: 'integer'
              }
            }
        }
    }
}