import React from 'react';
import { InputGroup,Form,Col, Button, Collapse } from 'react-bootstrap';

class RegisterCompnonet extends React.Component{

    constructor(props){
        super(props);

        this.state = {
            showRegisterInfo : false
        }
    }
    toggleLRegisterInfo=()=>{
        this.setState({
            showRegisterInfo : !this.state.showRegisterInfo
        })
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
                                            <Form.Control placeholder={item.placeHolder} type={item.type} id={`inlineFormInput_${item.label}`} required={item.required}/>
                                        </InputGroup>
                                    </Col>
                                </Form.Row>
                            )
                        }
    
                        <Button type='submit' block >Register</Button>
                    </Form>
                </Collapse>
            </div>
        )
    }
}


export default RegisterCompnonet;