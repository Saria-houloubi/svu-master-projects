import React from 'react';
import Register from '../Register/RegisterComponent';
import { Form,Button } from 'react-bootstrap';
import {Candidate} from '../../Api';

class LoginComponent extends React.Component{


    constructor(props){
        super(props);


        this.state = {
            withRegister : false,
            login : '',
            password : ''
        }
    }

    componentDidMount(){
        this.setState({
            withRegister :this.props.withRegister
        })
    }

    getRegister(){
        if(this.state.withRegister){
            return(<Register registerFormInputs={this.props.registerFromInputs}/>)
        }else{
            return;
        }
    }

    handelInputChange=(event)=>{
        var name = event.target.name;
        var value = event.target.value;
        
        this.setState({
            [name] : value
        });
    }

    handelLoginSubmit=(event)=>{
        
        event.preventDefault();
        Candidate.postLoginCandidate(this.state.login,this.state.password).then(data=>{
            if(data.status === 200){
                this.props.onSuccessLogin(data.data)
            }else{
                var errors = data.errors.map(item=>item.error).reduce((acc,curr) => {
                    return `${acc}, ${curr}`;
                });

                this.props.showAlert(errors,'danger');
            }
        }).catch(err=>console.log(err));
    }
    render(){
        return (
            <div>
                <h1>Already have an account login!</h1>
                <Form>
                    <Form.Group>
                        <Form.Label>
                            Login username/email
                        </Form.Label>
                        <Form.Control type="text" name="login" placeholder="username/email" onChange={this.handelInputChange} required/>
                    </Form.Group>
                    <Form.Group>
                        <Form.Label>
                            Password
                        </Form.Label>
                        <Form.Control type="password" name="password" placeholder="password" onChange={this.handelInputChange} required/>
                    </Form.Group>
                  
                    <Button variant='primary' type='submit' block onClick={this.handelLoginSubmit}>
                        Login
                    </Button>
                </Form>
                <hr/>
                <div className='mt-3' >
                    {
                        this.getRegister()   
                    }
                </div>
            </div>
        )
    }
}

export default LoginComponent;