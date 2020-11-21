import React from  'react';
import Navbar from 'react-bootstrap/Navbar';
import Nav from 'react-bootstrap/Nav';
import NavBar from 'react-bootstrap/Navbar'; 
import {  ToggleButtonGroup, ToggleButton, Button } from 'react-bootstrap';

class CustomNavbarCompnent extends React.Component{

    getNonAuthorizedLinks(){
        if(this.props.isLogedIn)
            return;

        return (
            this.props.links.nonAuthorizedLinks.map(item=>{
                return <Nav.Link key={item.id} href={item.link}>{item.title}</Nav.Link>;
            })
        );
    };

    getEndNavBar(){
        if(this.props.isLogedIn){
            return(<Button variant="outline-warning" onClick={this.props.onLogout}>Logout</Button>)
        }
        return (
            <ToggleButtonGroup type='radio' name='account_type' defaultValue={this.props.account.default.type} onChange={this.props.onAccoutTypeChange} >
                            {
                                this.props.account.types.map(item=>
                                    <ToggleButton
                                        key={item.id}
                                        value = {item.type}
                                        variant='light'
                                        >
                                        {item.type}
                                    </ToggleButton>
                                )
                            }
            </ToggleButtonGroup>
        )
    }

    render(){
        return (
                <NavBar bg="dark" expand="lg" variant='dark'>
                    <NavBar.Brand href="#home">Amw</NavBar.Brand>
                    <NavBar.Toggle aria-controls="basic-navbar-nav"/>
                    <Navbar.Collapse id="basic-navbar-nav">
                        <Nav  className="mr-auto">
                        {
                            this.props.links.all.map(item=>{
                                return <Nav.Link key={item.id} href={item.link}>{item.title}</Nav.Link>;
                            })
                        }
                        {
                            this.getNonAuthorizedLinks()
                        }
                        </Nav>
                    </Navbar.Collapse>
                    <Navbar.Collapse className='justify-content-end'>
                        {
                            this.getEndNavBar()
                        }
                    </Navbar.Collapse>
                </NavBar>
        );
    }
}

export default CustomNavbarCompnent;