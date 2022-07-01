import { Badge } from "react-bootstrap";

export const IssueStatusBadge = ({ type }) => {

    switch (type) {
        case 1:
            return <Badge bg="primary">Open</Badge>
        case 2:
            return <Badge bg="secondary">Cancelled</Badge>
        case 3:
            return <Badge bg="warning">Merged</Badge>
        case 4:
            return <Badge bg="primary">Planning</Badge>
        case 5:
            return <Badge bg="info">In Review</Badge>
        case 6:
            return <Badge bg="danger">Rejected</Badge>
        case 7:
            return <Badge bg="success">Approved</Badge>
        case 8:
            return <Badge bg="primary">Done</Badge>
        default:
            return <Badge />;
    }
};