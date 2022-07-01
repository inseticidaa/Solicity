import React, { Component, useRef } from 'react';
import Editor, { composeDecorators } from '@draft-js-plugins/editor';
import { Card, Container } from 'react-bootstrap';

import createImagePlugin from '@draft-js-plugins/image';
import createToolbarPlugin from '@draft-js-plugins/static-toolbar';
import createAlignmentPlugin from '@draft-js-plugins/alignment';
import createFocusPlugin from '@draft-js-plugins/focus';
import createResizeablePlugin from '@draft-js-plugins/resizeable';
import createBlockDndPlugin from '@draft-js-plugins/drag-n-drop';
import { convertFromRaw, EditorState } from 'draft-js';


import 'draft-js/dist/Draft.css';
import '@draft-js-plugins/inline-toolbar/lib/plugin.css';
import '@draft-js-plugins/image/lib/plugin.css';
import '@draft-js-plugins/static-toolbar/lib/plugin.css';
import TimeAgo from 'timeago-react';

// Plugins
const staticToolbarPlugin = createToolbarPlugin();
const focusPlugin = createFocusPlugin();
const resizeablePlugin = createResizeablePlugin();
const blockDndPlugin = createBlockDndPlugin();
const alignmentPlugin = createAlignmentPlugin();

const { Toolbar } = staticToolbarPlugin;
const { AlignmentTool } = alignmentPlugin;

const decorator = composeDecorators(
    resizeablePlugin.decorator,
    alignmentPlugin.decorator,
    focusPlugin.decorator,
    blockDndPlugin.decorator
);

const imagePlugin = createImagePlugin({ decorator });


const plugins = [
    staticToolbarPlugin,
    blockDndPlugin,
    focusPlugin,
    alignmentPlugin,
    resizeablePlugin,
    imagePlugin
];


export default class CommentViewer extends Component {

    state = {
        editorState: EditorState.createWithContent(convertFromRaw(JSON.parse(this.props.value.comment))),
    };

    focus = () => {
        this.editor.focus();
    };

    onChange = (editorState) => {
        this.setState({
            editorState,
        });
    };

    render() {
        return (
            <Container fluid className="p-0">
                <Card>
                    <Card.Header>
                        <a href='#'>{this.props.value.author.firstName}</a>{" "}
                        <span className="text-muted"> enviou a <TimeAgo
                            datetime={this.props.value.createdAt}
                            locale='pt_BR'
                        /></span>
                    </Card.Header>
                    <Card.Body className="p-4">
                        <Editor
                            readOnly={true}
                            editorState={this.state.editorState}
                            onChange={this.onChange}
                            plugins={plugins}
                            ref={(element) => {
                                this.editor = element;
                            }}
                        />
                    </Card.Body>
                </Card>
            </Container>
        );
    }
}