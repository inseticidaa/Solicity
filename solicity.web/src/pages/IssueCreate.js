import React, { Component, useEffect, useMemo, useRef, useState } from "react";
import { Alert, Button, Card, Col, Container, Form, Row, Spinner } from "react-bootstrap";
import { useAuth } from "../components/AuthProvider";
import CommentEditor from "../components/CommentEditor";
import { useNavigate } from "react-router-dom";

import { EditorState, convertToRaw } from 'draft-js';

export default function IssueCreatePage(props) {

    let { user, token } = useAuth();
    let navigate = useNavigate();

    let [loading, setLoading] = useState(false);
    let [alert, setAlert] = useState(null);
    let [issueTitle, setIssueTitle] = useState("");
    let [topic, setTopic] = useState("");
    let [topicList, setTopicList] = useState([]);

    const [editorState, setEditorState] = React.useState(
        () => EditorState.createEmpty(),
    );

    useEffect(() => {

        let api_url = new URL(process.env.REACT_APP_API_URL);
        api_url.pathname = "/api/Topic";

        api_url.searchParams.append("page", 1)
        api_url.searchParams.append("pageSize", 10)

        fetch(
            api_url,
            {
                method: 'GET',
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + token
                },
            })
            .then((response, err) => {

                if (err) {
                    //return callback(err)
                }

                console.log(response);
                console.log(err);

                response.json()
                    .then(res => {
                        switch (response.status) {
                            case 400:
                            case 401:
                                //callback("Unauthorized Sign In")
                                console.log(res)
                                break;
                            case 200:
                                console.log(res)
                                setTopicList(res)
                                break;
                        };
                    });
            })

    }, []);


    let handlerSubmit = () => {

        setLoading(true);

        if (issueTitle === "") {
            setLoading(false);
            setAlert({ message: "Voce precisa dar um Título para Issue!", type: "warning" })
            return;
        }

        if (topic === "-1") {
            setLoading(false);
            setAlert({ message: "Voce precisa selecionar um Tópico para sua Issue!", type: "warning" })
            return;
        }

        let api_url = new URL(process.env.REACT_APP_API_URL);
        api_url.pathname = "/api/Issue";

        fetch(
            api_url,
            {
                method: 'POST',
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + token
                },
                body: JSON.stringify({

                    topicId: topic,
                    title: issueTitle,
                    comment: JSON.stringify(convertToRaw(editorState.getCurrentContent()))

                })
            })
            .then((response, err) => {

                if (err) {
                    setLoading(false);
                    setAlert({ message: err, type: "danger" })
                    return;
                }

                response.json()
                    .then(res => {
                        switch (response.status) {
                            case 400:
                            case 401:
                                setLoading(false);
                                setAlert({ message: res.title, type: "danger" })
                                console.log(res)
                                return;
                            case 200:
                            case 201:
                                console.log(res)
                                navigate(`/issues/${res.id}`)
                                setLoading(false);
                                break;
                        };
                    });
            });
    }

    return (
        <Container>
            <Row className="justify-content-md-center mt-5">
                <Col md={7}>
                    {
                        alert &&
                        <Alert variant={alert.type}>
                            <Alert.Heading>{alert.title}</Alert.Heading>
                            {alert.message}
                        </Alert>
                    }
                </Col>
            </Row>
            <Row className="justify-content-md-center mt-2">
                <Col md={7}>
                    <h2>Nova issue</h2>
                    <hr className="mt-4 mb-4" />
                    <Form>
                        <Form.Group className="mb-3">
                            <Form.Label>Título da Issue *</Form.Label>
                            <Form.Control type="text" as="input" value={issueTitle} onChange={(e) => setIssueTitle(e.target.value)} />
                            <Form.Text className="text-muted">
                                Dica: Coloque um título que seja de fácil identificação
                            </Form.Text>
                        </Form.Group>
                        <Form.Group className="mb-3">
                            <Form.Label>Tópico</Form.Label>
                            <Form.Select disabled={topicList.length <= 1} value={topic} onChange={(e) => { setTopic(e.target.value) }}>
                                <option value={-1}>Selecione um Tópico</option>
                                {
                                    topicList.map((topic, index) => {
                                        return <option key={index} value={`${topic.id}`}>{topic.name}</option>
                                    })
                                }
                            </Form.Select>
                            <Form.Text className="text-muted mt-2">
                                {
                                    topicList.map((item) => {
                                        if (item.id == topic) {
                                            return (
                                                <Alert variant="info" className="mt-3">
                                                    <Alert.Heading>{item.name}</Alert.Heading>     
                                                    <hr />
                                                    <p>
                                                        {item.description}
                                                    </p>
                                                </Alert>
                                            )
                                        }
                                    })
                                }
                            </Form.Text>
                        </Form.Group>
                        <Form.Group className="mt-2">
                            <Form.Label>Proposta</Form.Label>
                            <CommentEditor editorState={editorState} setEditorState={setEditorState} />
                            <Form.Text className="text-muted">
                                Quanto mais documentado sua Issue mais rápido ela poder ser resolvida!
                            </Form.Text>
                        </Form.Group>
                        <Button variant="success" className="mt-3" size="md" onClick={handlerSubmit} disabled={loading}>
                            {
                                loading && <Spinner
                                    as="span"
                                    animation="border"
                                    size="sm"
                                    role="status"
                                    aria-hidden="true"
                                    className="mr-2"
                                />
                            }
                            Abrir Issue
                        </Button>
                    </Form>
                </Col>
            </Row>
        </Container>
    )
}