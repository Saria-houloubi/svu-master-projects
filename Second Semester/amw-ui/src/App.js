import React from 'react';
import {navigationBarLinks,cookieDefaults, registerFrom,account} from './Sitemap';
import Cookies from 'universal-cookie';
import Login from './Components/Login/LoginComponent';
import Navbar from './Components/Navigation/CustomNavbarCompnent';
import UserInformation from './Components/User/UserInformationComponent';
import CompanyInformation from './Components/Company/CompanyInformationComponent';
import { Alert, Container } from 'react-bootstrap';

class App extends React.Component{

    
    constructor (props){
        super(props)
        
        this.state = {
            isLogedIn : false,
            viewType : 'candidate',
            alertMessage: '',
            alertType :'',
            showHeaderAlert :false,
            userInfo : {}
        }
    };

    componentDidMount(){
        const cookies = new Cookies();
        var cookieValue = cookies.get(cookieDefaults.userInfo);
        if(cookieValue){
            this.setState(
                {
                    isLogedIn : true,
                    userInfo : cookieValue
                })
        }
        else{
            // cookies.set(cookieName,JSON.stringify(
            //     {
            //         fullName:'saria',
            //         tel: '000000',
            //         educationLevel : 'leve',
            //         creationDate : new Date(),
            //         login : 'login',
            //         diplomas:[{
            //             id : 1,
            //             title : 'diploma 1',
            //             creationDate : new Date()
            //         },
            //         {
            //             id : 1,
            //             title : 'diploma 2',
            //             creationDate : new Date()
            //         }]
            //     }
            // ));
        }
    }

    
    handelAccountTypeChange = (type)=> {
        this.setState({
            viewType : type
        })
    }

    handelShowTopAlert = (message,type) =>{
        this.setState({
            showHeaderAlert : true,
            alertMessage : message,
            alertType: type
        })

        setTimeout(() => {
            this.setState({
                showHeaderAlert : false,
                headerAlert : '',
                headerAlertType: ''
            })
    
        }, 3000);
    }

    handelSuccessLogin = (userInfo) => {
        const cookies = new Cookies();
        cookies.set(cookieDefaults.token,userInfo.extra['token']);
        cookies.set(cookieDefaults.userInfo,userInfo);
     
        this.setState(
            {
                isLogedIn : true,
                userInfo : userInfo
            })
    }

    handelSuccessLogout = () => {
        const cookies = new Cookies();
        cookies.remove(cookieDefaults.token);
        cookies.remove(cookieDefaults.userInfo);
        this.setState(
            {
                isLogedIn : false,
                userInfo : {}
            })
    }
    
    getAccountInformation= ()=>{
        if(this.state.isLogedIn){
            switch(this.state.viewType){
                case 'candidate':
                    return(<UserInformation user={this.state.userInfo} />)
                case 'company':
                    return(<CompanyInformation user={this.state.userInfo}/>)
                default:
                    return;
            }   
        }else{
            return(
                <Login withRegister={true} registerFromInputs={registerFrom[this.state.viewType]} showAlert={this.handelShowTopAlert} onSuccessLogin={this.handelSuccessLogin}/>
            )
        }
    }

    getTopAlert= ()=>{
        if(this.state.showHeaderAlert)
            return (<Alert className="m-3" variant={this.state.alertType}>{this.state.alertMessage}</Alert>);
        return;
    }
    render(){
        return (
            <div>
                <Navbar links={navigationBarLinks} isLogedIn={this.state.isLogedIn} account={account} onLogout={this.handelSuccessLogout} onAccoutTypeChange={this.handelAccountTypeChange}/>
                {
                    this.getTopAlert()
                }
                <Container className="mt-3">
                    {
                        this.getAccountInformation()
                    }
                </Container>
            </div>
        );
    };
}

export default App;