import React from   'react';
import Alert from 'react-bootstrap/Alert';
import ListGroup from 'react-bootstrap/ListGroup';
import Button from 'react-bootstrap/Button';
import Collapse from 'react-bootstrap/Collapse';

class CompanyInformationComponents extends React.Component{
    
    constructor(props){
        super(props);

        this.state = {
            showAccountInfo : false,
        }
    }

    toggleLAccountInfoListGroup=()=>{
        this.setState({
            showAccountInfo : !this.state.showAccountInfo
        })
    }
    render(){

        const user = this.props.user;
        return(
            <Alert variant='info'>
                <Alert.Heading>
                    Welcome back <b>{user.name}</b> Lets find your next hero!
                    <div className="d-flex justify-content-end">
                        <Button variant="outline-info" onClick={this.toggleLAccountInfoListGroup}>
                            My Info
                        </Button>
                    </div>
                </Alert.Heading>
                <hr/>
                <Collapse in={this.state.showAccountInfo}>
                    <ListGroup variant='flush'>
                        <ListGroup.Item disabled={false} variant='info'><b>Name:</b> {user.name}</ListGroup.Item>
                        <ListGroup.Item variant='info'><b>Login:</b> {user.login}</ListGroup.Item>
                        <ListGroup.Item variant='info'><b>Tel:</b> {user.tel}</ListGroup.Item>
                        <ListGroup.Item variant='info'><b>Joind Date:</b> {user.creationDate}</ListGroup.Item>
                    </ListGroup>
                </Collapse>
            </Alert>
        )
    }
}

export default CompanyInformationComponents;
