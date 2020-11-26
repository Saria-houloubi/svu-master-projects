import React from 'react';
import { Container, Row,Col,Form } from 'react-bootstrap';
import Job from './JobComponent';
import {Entity,Lookup} from '../../Api';

class JobListComponent extends React.Component{


    constructor(props){
        super(props);


        this.state ={
            jobList :[],
            filter:{},
            companies:[],
            educationLevels:[],
            sort:{},
            order: {}
        }
    }

    componentDidMount(){
        this.getJobs();
        this.getEductionLevels();
        this.getCompanies();
    }

    getJobs=()=>{
        Entity.getByFilter('job',this.state.filter)
        .then(data=>{
            if (data && data.count > 0){
                this.setState({
                    jobList: data.data
                });
            } else {
                this.setState({
                    jobList: []
                });
            }
        })
    }

    getRowsElements = (array,chunkSize) => {
        if(array){
            return [].concat.apply([],
            array.map(function(elem, i) {
                return i % chunkSize ? [] : [array.slice(i, i + chunkSize)];
             })
            );
        }
      }
    
    getEductionLevels=()=>{
        Lookup.getLookupData('educationLevels')
        .then(data=>{
            if(data.count > 0){
                this.setState({
                    educationLevels : data.data
                })
            }
        })
    }

    getCompanies=()=>{
        Entity.getByFilter('company',{})
        .then(data=>{
            if(data && data.count>0){
                this.setState({
                    companies : data.data
                })
            }
        })
    }

    handelFormChange=(event)=>{
        var name = event.target.name;
        var value = event.target.value;
        var stateprop = event.target.attributes.stateprop.value;
        
        this.setState({
            [stateprop] : {
                ...this.state[stateprop],
                [name]: value
            }
        },()=>this.getJobs())
    }
    handelSortTypeChange = (event)=>{
        this.setState({
            order : {
                ...this.state.order,
                type : event.target.defaultValue
            },
            jobList : this.state.jobList.sort(this.handelSort)

        })
    }

    handelSortChange = (event)=>{
        this.setState({
            order : {
                ...this.state.order,
                by : event.target.value
            },
            jobList : this.state.jobList.sort(this.handelSort)
        })

    }
    handelSort= (a,b)=>{
        if(this.state.order.by && this.state.order.type){
            if(this.state.order.by === 'asc')
                return a[this.state.order.type] > b[this.state.order.type]
            else
                return a[this.state.order.type] < b[this.state.order.type]
        }
        return true;
    }


    render(){
        return(
            
        <Container className="mt-3">
            <Container className="border p-5">
                <Row >
                    <Col md="auto">
                        <Form.Label>Company</Form.Label>
                    </Col>
                    <Col>
                        <Form.Control
                            name="company"
                            stateprop='filter'
                            value={this.state.filter.company}
                            onChange={this.handelFormChange}
                        />
                    </Col>
                    <Col  md="auto">
                        <Form.Control  as='select'
                            stateprop='filter'
                            onChange={this.handelFormChange}
                            name ="company"
                        >
                            {
                            this.state.companies.map(item=>{
                                return(<option value={item.name} key={item.id}>{item.name}</option>)
                            })
                            }
                        </Form.Control>
                    </Col>
                </Row>

                <Row className='mt-3'>
                    <Col md="auto">
                        <Form.Label>Title</Form.Label>
                    </Col>
                    <Col>
                        <Form.Control
                            stateprop='filter'
                            name="title"
                            value={this.state.filter.title}
                            onChange={this.handelFormChange}
                        />
                    </Col>
                </Row>
                
                <Row className='mt-3'>
                    <Col md="auto">
                        <Form.Label>Education Level</Form.Label>
                    </Col>
                    <Col>
                        <Form.Control
                            stateprop='filter'
                            name="educationLevel"
                            value={this.state.filter.educationLevel}
                            onChange={this.handelFormChange}
                        />
                    </Col>
                    <Col  md="auto">
                        <Form.Control  as='select'
                            onChange={this.handelFormChange}
                            stateprop='filter'
                            name ="educationLevel"
                        >
                            {
                            this.state.educationLevels.map(item=>{
                                return(<option value={item.level} key={item.id}>{item.level}</option>)
                            })
                            }
                        </Form.Control>
                    </Col>
                </Row>
                
                <Row className='mt-3' >
                    <Col md="auto">
                        <Form.Label>Experince</Form.Label>
                    </Col>
                    <Col>
                        <Form.Control
                            onChange={this.handelFormChange}
                            name="experienceYears"
                            stateprop='filter'
                            type='number'
                        />
                    </Col>
                </Row>
                <Row className='mt-3' >
                    <Col md="auto">
                        <Form.Label>Salery</Form.Label>
                    </Col>
                    <Col>
                        <Form.Control
                            onChange={this.handelFormChange}
                            name="salery"
                            stateprop='filter'
                            type='number'
                        />
                    </Col>
                </Row>
            </Container>
            <Container className="border mt-2 p-2">
                <Row>
                    <Col md='auto'>
                       <Form.Label>Sort by</Form.Label>
                    </Col>
                    <Col>
                        <Form.Control name='sortby' as='select' stateprop='sort' onChange={this.handelSortChange}>
                            <option value='educationLevel'>Education Level</option>
                            <option value='experienceYears'>ExperienceYears</option>
                        </Form.Control>
                    </Col>
                    <Col md='auto'>
                        <Form.Group controlId="formBasicCheckbox" onChange={this.handelSortTypeChange}>
                            <Form.Check inline label='ASC' type='radio' name='sorttype'  stateprop='sort' value='asc' />
                            <Form.Check inline label='DESC' type='radio' name='sorttype' stateprop='sort' value='desc' />
                        </Form.Group>
                    </Col>
                  
                </Row>
            </Container>
            {
                this.getRowsElements(this.state.jobList,3).map((jobs,idx)=>
                    <Row className="justify-content-md-center m-3" key={idx}> 
                            {jobs.sort((a,b)=>{
                                 if (a.educationLevel < b.educationLevel) {
                                    return -1;
                                }
                                if (b.educationLevel > a.educationLevel) {
                                    return 1;
                                }
                                return 0;
                            }).map(job =>
                                <Col key={job.id}  className='m-2'>
                                    <Job  job={job}/>
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