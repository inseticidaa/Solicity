import { Col, Container, Row } from "react-bootstrap";
import { useAuth } from "../components/AuthProvider";

export default function HomePage(props) {

    const { user, token } = useAuth();

    console.log(user)
    console.log(token)

    return (
        <Container>
            <Row>
                <Col>
                    asd
                </Col>
            </Row>
        </Container>
    )
}