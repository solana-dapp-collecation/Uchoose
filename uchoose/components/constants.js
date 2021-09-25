export const NoteSchema = {
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

export const NotesListSchema = {
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
