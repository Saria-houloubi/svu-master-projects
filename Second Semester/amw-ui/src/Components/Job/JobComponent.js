import React from 'react'
import {Card } from 'react-bootstrap'

class JobComponent extends React.Component{

    render(){

        var job = this.props.job;
        return(
        <Card style={{ width: '18rem' }} key={job.id}>
            <Card.Body>
            <Card.Title>{job.title} </Card.Title>
            <Card.Subtitle className="mb-2 text-muted">{job.salery} $</Card.Subtitle>
            <Card.Text>
                <b>Education Level:</b> {job.educationLevel}<br/>
                <b>Experience Years:</b> {job.experienceYears}
            </Card.Text>
        </Card.Body>
        </Card>
        )
    }
}

export default JobComponent;