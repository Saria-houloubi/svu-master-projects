import React from 'react';
import { InputGroup,Form,Col, Button, Collapse } from 'react-bootstrap';
import {Entity} from '../../Api';
class JobFormCompnonet extends React.Component{

    constructor(props){
        super(props);

        this.state = {
            showFromPart : false,
        }

    }
    toggleFromPart=()=>{
        this.setState({
            showFromPart : !this.state.showFromPart
        })
    }
    handelOnRegisterSubmit= (e)=>{
        e.preventDefault();

        Entity.registerInfo('job', this.props.formInputs.reduce((acc,curr)=>{
            return Object.assign({},acc,{
               [curr.name] : this.state[curr.name] 
            })
        },0))
        .then(data=>{
            if(data.status === 201){
                this.props.showAlert(`A new job has been added`,'success');
                this.props.onSuccessCreate(data.data);
            }else{
                var errors = data.errors.map(item=>item.error).reduce((acc,curr) => {
                    return `${acc}, ${curr}`;
                });

                this.props.showAlert(errors,'danger');
            }
        }).catch(err=>console.log(err));
        

    }

    handelInputChange=(event)=>{
        var name = event.target.name;
        var value = event.target.value;
        
        this.setState({
            [name] : value
        });
    }

    render(){
        return(
            <div>
                <p>Need a new hero <Button variant='outline-primary' size='sm' onClick={this.toggleFromPart} > Spread the word!</Button></p>
                <Collapse in={this.state.showFromPart}>
                    <Form >
                        {
                            this.props.formInputs.map(item=>
                                <Form.Row className='mb-3' key={item.order}>
                                    <Col >
                                        <Form.Label htmlFor={`inlineFormInput_${item.label}`} srOnly>
                                            {item.srOnly}
                                        </Form.Label>
                                        <InputGroup>
                                            <InputGroup.Prepend>
                                                <InputGroup.Text>
                                                    {item.label}
                                                </InputGroup.Text>
                                            </InputGroup.Prepend>
                                            <Form.Control onChange={this.handelInputChange} name={item.name} placeholder={item.placeHolder} type={item.type} id={`inlineFormInput_${item.label}`} required={item.required}/>
                                        </InputGroup>
                                    </Col>
                                </Form.Row>
                            )
                        }
    
                        <Button type='submit' block onClick={this.handelOnRegisterSubmit}>Register</Button>
                    </Form>
                </Collapse>
            </div>
        )
    }
}


export default JobFormCompnonet;