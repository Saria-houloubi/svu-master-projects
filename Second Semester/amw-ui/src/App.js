import React from 'react';
import {navigationBarLinks,cookieDefaults, registerFrom,account} from './Sitemap';
import Cookies from 'universal-cookie';
import Login from './Components/Login/LoginComponent';
import Navbar from './Components/Navigation/CustomNavbarCompnent';
import UserInformation from './Components/User/UserInformationComponent';
import CompanyInformation from './Components/Company/CompanyInformationComponent';
import { Alert, Container } from 'react-bootstrap';
import { Entity,setHeaderAuthToken } from './Api';

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
        var cookieValue = cookies.get(cookieDefaults.token);

        if(cookieValue){

            this.setState(
                {
                    isLogedIn : true,
                    viewType : cookieValue.type
                });

            //Setup the authentication
            setHeaderAuthToken(cookieValue.token);
            //Get current informatoin information
            Entity.getInfo(cookieValue.type).then(res=>{
                console.log(res)
                if(res && res.status === 200 && res.count === 1){
                    this.setState(
                        {
                            userInfo : res.data
                        })
                }
                else if(res){
                    this.handelSuccessLogout();
                }
            })
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
        cookies.set(cookieDefaults.token,{
            token : userInfo.extra['token'],
            type :this.state.viewType
        });
     
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
        setHeaderAuthToken('');

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
                <Login withRegister={true} registerFromInputs={registerFrom[this.state.viewType]} loginType={this.state.viewType} showAlert={this.handelShowTopAlert} onSuccessLogin={this.handelSuccessLogin}/>
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