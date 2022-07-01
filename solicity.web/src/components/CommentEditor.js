import React, { Component, useRef } from 'react';
import Editor, { composeDecorators } from '@draft-js-plugins/editor';
import { Card, Container } from 'react-bootstrap';

import createImagePlugin from '@draft-js-plugins/image';
import createToolbarPlugin from '@draft-js-plugins/static-toolbar';
import { readFile } from '@draft-js-plugins/drag-n-drop-upload';
import createAlignmentPlugin from '@draft-js-plugins/alignment';
import createFocusPlugin from '@draft-js-plugins/focus';
import createResizeablePlugin from '@draft-js-plugins/resizeable';
import createBlockDndPlugin from '@draft-js-plugins/drag-n-drop';
import createDragNDropUploadPlugin from '@draft-js-plugins/drag-n-drop-upload';

import 'draft-js/dist/Draft.css';
import '@draft-js-plugins/inline-toolbar/lib/plugin.css';
import '@draft-js-plugins/image/lib/plugin.css';
import '@draft-js-plugins/static-toolbar/lib/plugin.css';

function mockUpload(data, success, failed, progress) {
    function doProgress(percent) {
        progress(percent || 1);
        if (percent === 100) {
            // Start reading the file
            Promise.all(data.files.map(readFile)).then((files) =>
                success(files, { retainSrc: true })
            );
        } else {
            setTimeout(doProgress, 250, (percent || 0) + 10);
        }
    }

    doProgress();
}

// Plugins
const focusPlugin = createFocusPlugin();
const resizeablePlugin = createResizeablePlugin();
const blockDndPlugin = createBlockDndPlugin();
const alignmentPlugin = createAlignmentPlugin();

const { AlignmentTool } = alignmentPlugin;

const decorator = composeDecorators(
    resizeablePlugin.decorator,
    alignmentPlugin.decorator,
    focusPlugin.decorator,
    blockDndPlugin.decorator
);

const imagePlugin = createImagePlugin({ decorator });
const staticToolbarPlugin = createToolbarPlugin({ decorator });

const { Toolbar } = staticToolbarPlugin;



const dragNDropFileUploadPlugin = createDragNDropUploadPlugin({
    handleUpload: mockUpload,
    addImage: imagePlugin.addImage,
});

const plugins = [
    staticToolbarPlugin,
    dragNDropFileUploadPlugin,
    blockDndPlugin,
    focusPlugin,
    alignmentPlugin,
    resizeablePlugin,
    imagePlugin
];


export default class CommentEditor extends Component {

    focus = () => {
        this.editor.focus();
    };

    render() {
        return (
            <Container fluid className="p-0">
                <div onClick={this.focus}>
                    <Card>
                        <Card.Body>
                            <Editor
                                readOnly={this.props.readOnly ? this.props.readOnly : false }
                                placeholder='Escreva aqui...'
                                editorState={this.props.editorState}
                                onChange={this.props.setEditorState}
                                plugins={plugins}
                                ref={(element) => {
                                    this.editor = element;
                                }}
                            />
                        </Card.Body>
                        <Card.Footer>
                            <Toolbar />
                        </Card.Footer>
                    </Card>
                </div>
            </Container>
        );
    }
}