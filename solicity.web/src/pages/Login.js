import { useState } from "react";
import { Row, Col, Form, Button, Container, Card, Spinner } from "react-bootstrap";
import { useNavigate } from "react-router-dom";
import { ToastContainer, toast } from "react-toastify";
import { useAuth } from "../components/AuthProvider";


export default function LoginPage(props) {

    const { login, user, token } = useAuth();
    const navigate = useNavigate();

    let [loading, setLoading] = useState(false);
    let [email, setEmail] = useState("");
    let [emailIsInvalid, setEmailIsInvalid] = useState(false);
    let [password, setPassword] = useState("");

    let siginSubmit = () => {
        toast.dismiss()
        toast.clearWaitingQueue();
        setLoading(true);

        if (IsEmail()) {
            setEmailIsInvalid(false);
        }
        else {
            setEmailIsInvalid(true);
            return setLoading(false);
        }

        login(email, password, err => {
            if (err) {
                setLoading(false);

                toast.error(err, {
                    position: toast.POSITION.BOTTOM_CENTER,
                    autoClose: 15000
                });

                return;
            }

            navigate("/");
        });

    }

    let IsEmail = () => {
        var emailChecker = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/;
        return emailChecker.test(email);
    }

    return (
        <Container className="d-flex justify-content-center mt-5">
            <Row>
                <Col>
                    <h1 className="mb-4 text-center">Solicity</h1>
                    <Card style={{ width: '22rem' }}>
                        <Card.Body>
                            <Card.Title>Login</Card.Title>
                            <Form>
                                <Form.Group className="mb-3" controlId="formBasicEmail">
                                    <Form.Label>Email address</Form.Label>
                                    <Form.Control type="email" placeholder="Enter email" disabled={loading} value={email} onChange={(e) => setEmail(e.target.value)} isInvalid={emailIsInvalid} />
                                    <Form.Text className="text-muted">
                                        We'll never share your email with anyone else.
                                    </Form.Text>
                                </Form.Group>

                                <Form.Group className="mb-3" controlId="formBasicPassword">
                                    <Form.Label>Password</Form.Label>
                                    <Form.Control type="password" placeholder="Password" disabled={loading} value={password} onChange={(e) => setPassword(e.target.value)} />
                                </Form.Group>
                                <Form.Group className="mb-3" controlId="formBasicCheckbox">
                                    <Form.Check type="checkbox" label="Continuar conectado" disabled={true} />
                                </Form.Group>
                                <Button variant="primary" onClick={siginSubmit} disabled={loading}>
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
                                    Sign In
                                </Button>
                            </Form>
                        </Card.Body>
                    </Card>
                </Col>
            </Row>
            <Row>
                <Col>
                    <ToastContainer limit={2}/>
                </Col>
            </Row>
        </Container>
    )
}