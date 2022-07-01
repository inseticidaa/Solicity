import { useEffect, useState } from "react";
import { Breadcrumb, Card, Col, Container, Dropdown, Nav, Row, Toast, Button, Table, Badge, FormControl, Spinner } from "react-bootstrap";
import { useParams } from "react-router-dom";
import TimeAgo from "timeago-react";
import { useNavigate } from "react-router-dom";
import { useAuth } from "../components/AuthProvider";
import CommentViewer from "../components/CommentViewer";
import CommentEditor from "../components/CommentEditor";
import { EditorState, convertToRaw } from 'draft-js';
import { IssueStatusBadge } from "../components/IssueStatusBadge";

export default function IssuePage(props) {

    const { user, token } = useAuth();
    let { issueId } = useParams();
    let navigate = useNavigate();

    let [loading, setLoading] = useState(true);
    let [sending, setSending] = useState(false);
    let [issue, setIssue] = useState(null);

    const [editorState, setEditorState] = useState(
        () => EditorState.createEmpty(),
    );

    useEffect(() => {
        loadIssue();
    }, []);

    let loadIssue = (callback) => {

        let api_url = new URL(process.env.REACT_APP_API_URL);
        api_url.pathname = `/api/Issue/${issueId}`;

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
                            case 404:
                                console.log(res)
                                break;
                            case 200:
                                console.log(res)
                                setIssue(res)
                                setLoading(false);
                                if (callback) {
                                    callback();
                                }
                                break;
                        };
                    });
            })
    }

    let handlerAddComment = () => {

        setSending(true);

        let api_url = new URL(process.env.REACT_APP_API_URL);
        api_url.pathname = `/api/Issue/${issueId}/AddComment`;

        console.log(`/api/Issue/${issueId}/AddComment`)

        fetch(
            api_url,
            {
                method: 'POST',
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + token
                },
                body: JSON.stringify(JSON.stringify(convertToRaw(editorState.getCurrentContent())))
            })
            .then((response, err) => {

                if (err) {
                    setSending(false);
                    return;
                }

                response.json()
                    .then(res => {
                        switch (response.status) {
                            case 400:
                            case 401:
                                console.log(res)
                                setSending(false);
                                return;
                            case 200:
                            case 201:
                                loadIssue(() => {
                                    setSending(false);
                                    setEditorState(() => EditorState.createEmpty())
                                });
                                break;
                        };
                    });
            });
    }


    if (loading) {
        return (
            <Container fluid className="mt-5 d-flex justify-content-center">
                <Row className="mt-5">
                    <Col className="mt-5">
                        <Spinner animation="border" role="status">
                            <span className="visually-hidden">Loading...</span>
                        </Spinner>
                    </Col>
                </Row>
            </Container>
        )
    }

    return (
        <Container className="mt-3 mb-5">
            <Row>
                <Col sm>
                    <Breadcrumb>
                        <Breadcrumb.Item onClick={() => navigate("/")}>Home</Breadcrumb.Item>
                        <Breadcrumb.Item onClick={() => navigate("/issues")}>
                            Issues
                        </Breadcrumb.Item>
                        <Breadcrumb.Item active>
                            {issue.code.toUpperCase()}
                        </Breadcrumb.Item>
                    </Breadcrumb>
                </Col>
            </Row>
            <Row>
                <Col sm={9}>
                    <h2 className="medium">
                        {issue.title}
                        <small className="text-muted"> #{issue.code.toUpperCase()}</small>
                    </h2>
                </Col>
                <Col sm className="d-flex flex-row-reverse ">
                    <Dropdown className="align-self-center">
                        <Dropdown.Toggle variant="success" id="dropdown-basic">
                            Actions
                        </Dropdown.Toggle>

                        <Dropdown.Menu>
                            <Dropdown.Item href="#/action-1">Submeter para aprovacao</Dropdown.Item>
                            <Dropdown.Item href="#/action-1">Cancelar</Dropdown.Item>
                            <Dropdown.Item href="#/action-1">Merge</Dropdown.Item>
                        </Dropdown.Menu>
                    </Dropdown>
                </Col>
            </Row>
            <Row>
                <Col sm={10}>
                    <span className="lead fw-normal">
                        <IssueStatusBadge type={issue.status} /> Issue aberta por {issue.author.firstName} {issue.author.lastName} a <TimeAgo
                            datetime={issue.createdAt}
                            locale='pt_BR'
                        />
                    </span>
                </Col>
            </Row>
            <Row>
                <Col sm>
                    <hr className="mt-3 mb-4" />
                </Col>
            </Row>
            <Row>
                <Col>
                    {
                        issue.comments.map((data, index) => {

                            return <div className="mt-3"><CommentViewer value={data} key={index} /></div>
                        }).reverse()
                    }

                    <Card className="w-100 mt-3">
                        <Card.Body className="p-3">
                            <CommentEditor editorState={editorState} setEditorState={setEditorState} readOnly={sending} />
                            <div className="d-flex flex-row-reverse mt-3">
                                <Button variant="success" size="sm" onClick={handlerAddComment}>
                                    {
                                        sending && <Spinner
                                            as="span"
                                            animation="border"
                                            size="sm"
                                            role="status"
                                            aria-hidden="true"
                                            className="mr-2"
                                        />
                                    }
                                    Commentar
                                </Button>
                            </div>
                        </Card.Body>
                    </Card>
                </Col>
                <Col sm={4}>
                    <Card className="w-100 mt-3">
                        <Card.Body className="p-4">
                            <h5 className="card-title">Dados da Issue</h5>
                            <Row>
                                <Table hover size="sm">
                                    <tbody>
                                        <tr>
                                            <td><strong>Topico</strong></td>
                                            <td>{issue.topic.name}</td>
                                        </tr>
                                        <tr>
                                            <td><strong>Solicitante</strong></td>
                                            <td>{issue.author.firstName} {issue.author.lastName}</td>
                                        </tr>
                                    </tbody>
                                </Table>
                            </Row>
                        </Card.Body>
                    </Card>
                </Col>

            </Row>

        </Container>
    )
}