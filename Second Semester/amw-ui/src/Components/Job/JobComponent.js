import React from 'react'
import {Card } from 'react-bootstrap'

class JobComponent extends React.Component{

    render(){

        var job = this.props.job;
        return(
        <Card style={{ width: '18rem' }}>
            <Card.Body>
            <Card.Title>{job.title}</Card.Title>
            <Card.Subtitle className="mb-2 text-muted">{job.salery}</Card.Subtitle>
            <Card.Text>
                Required Education Level : {job.educationLevel}<br/>
                Required Experience Years : {job.experienceYears}
            </Card.Text>
            <Card.Link href="#">Card Link</Card.Link>
            <Card.Link href="#">Another Link</Card.Link>
        </Card.Body>
        </Card>
        )
    }
}

export default JobComponent;