import { useEffect, useState } from "react";
import TimeAgo from 'timeago-react';
import { Badge, Breadcrumb, Button, Card, Col, Container, Dropdown, Form, Row, Spinner } from "react-bootstrap";

import { useAuth } from "../components/AuthProvider";
import { Link } from "react-router-dom";
import { IssueStatusBadge } from "../components/IssueStatusBadge";

export default function IssuesPage(props) {

    let [loading, setLoading] = useState(true);
    let [search, setSearch] = useState("is:open")
    let [page, setPage] = useState(1);
    let [issues, setIssues] = useState([]);

    const { user, token } = useAuth();

    useEffect(() => loadIssues(), []);

    let loadIssues = () => {

        let api_url = new URL(process.env.REACT_APP_API_URL);
        api_url.pathname = "/api/Issue/";

        api_url.searchParams.append("page", 1)
        api_url.searchParams.append("pageSize", 10)
        api_url.searchParams.append("search", "asd")

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
                                setIssues(res)
                                setLoading(false)
                                break;
                        };
                    });
            })
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
        <Container className="mt-3 mb-5 h-100">
            <Row>
                <Col sm>
                    <Breadcrumb>
                        <Breadcrumb.Item><Link to={"/"}>Home</Link></Breadcrumb.Item>
                        <Breadcrumb.Item active>
                            Issues
                        </Breadcrumb.Item>
                    </Breadcrumb>
                </Col>
            </Row>
            <Row>
                <Col sm={9}>
                    <h2 className="medium">
                        Issues
                        <small className="text-muted"> Global</small>
                    </h2>
                    <p>Issues são ambientes de desenvolvimento de projetos, que com base em um conjunto de pessoas com conhecimentos e habilidades diferentes, fornece os recursos necessários para discussão, desenvolvimento e roteirização de uma determinada solução.</p>
                </Col>
                <Col sm className="d-flex flex-row-reverse ">
                </Col>
            </Row>

            <Row>
                <Col>
                    <Row className="mt-2">
                        <Col>
                            <Form.Group className="d-flex">
                                <Form.Control type="text" placeholder="Buscar Issue" value={search} onChange={(e) => setSearch(e.target.value)} />
                                <Button variant="outline-secondary" mr="2">Buscar</Button>
                            </Form.Group>
                        </Col>
                        <Col sm={4} className="d-flex flex-row-reverse">
                            <Link to={"create"}><Button variant="success">Nova Issue</Button></Link>
                        </Col>
                    </Row>
                </Col>
            </Row>

            <Row>
                <Col>
                    <Card className="mt-3">
                        <Card.Body>
                            {issues.map((issue, index) => {


                                return (
                                    <div key={index}>

                                        <Row className="mt-4">
                                            <Col>
                                                <Link to={`${issue.id}`}>

                                                    <h6><IssueStatusBadge type={issue.status}/> {issue.code.toUpperCase()} • {issue.title}</h6>
                                                </Link>

                                            </Col>
                                            <Col sm="auto">
                                                <Badge bg="dark">{issue.topic.name}</Badge>
                                            </Col>
                                        </Row>

                                        <Row className="mt-1">
                                            <Col>
                                                <p>Aberto por <a href="#">{issue.author.firstName} {issue.author.lastName}</a></p>
                                            </Col>
                                            <Col sm="auto">
                                                <p>Published <TimeAgo
                                                    datetime={issue.createdAt}
                                                    locale='pt_BR'
                                                /></p>
                                            </Col>
                                        </Row>
                                        <hr />
                                    </div>
                                );
                            })}
                        </Card.Body>
                    </Card>
                </Col>
            </Row>
        </Container>
    )
}