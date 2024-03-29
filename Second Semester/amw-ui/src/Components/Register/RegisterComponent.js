import React from 'react';
import { InputGroup,Form,Col, Button, Collapse } from 'react-bootstrap';
import {Entity} from '../../Api';
class RegisterCompnonet extends React.Component{

    constructor(props){
        super(props);

        this.state = {
            showRegisterInfo : false,
            registerFor : this.props.registerFor
        }

    }
    toggleLRegisterInfo=()=>{
        this.setState({
            showRegisterInfo : !this.state.showRegisterInfo
        })
    }
    handelOnRegisterSubmit= (e)=>{
        e.preventDefault();

        Entity.registerInfo(this.props.registerFor, this.props.registerFormInputs.reduce((acc,curr)=>{
            console.log('acc',acc);
            console.log('curr',curr);
            return Object.assign({},acc,{
               [curr.name] : this.state[curr.name] 
            })
        },0))
        .then(data=>{
            if(data.status === 201){
                this.props.showAlert(`Almost there ${data.data.fullName}, try to login with the register login and password`,'success');
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
                <p>Do not have an account yet! <Button variant='outline-primary' size='sm' onClick={this.toggleLRegisterInfo} > Register Now!</Button></p>
                <Collapse in={this.state.showRegisterInfo}>
                    <Form >
                        {
                            this.props.registerFormInputs.map(item=>
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


export default RegisterCompnonet;