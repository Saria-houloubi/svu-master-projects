import React from 'react';
import {Card, Container, Row } from 'react-bootstrap';
import Job from './JobComponent';

class JobListComponent extends React.Component{


    getRowsElements = (array,chunkSize) => {
        return [].concat.apply([],
          array.map(function(elem, i) {
            return i % chunkSize ? [] : [array.slice(i, i + chunkSize)];
          })
        );
      }

    render(){
        return(
        <Container>
            {
                this.getRowsElements().map(jobs=>
                    <Row className="justify-content-md-center"> 
                            {jobs.map(job =>
                                <Col>
                                    <Job job={job}/>
                                </Col>
                            )}
                    </Row>
                )
            }
        </Container>
        )
    }
}

export default JobListComponent;