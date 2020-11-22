import {client, onErrorRequest} from './BaseController'

//
//Tries to authenticate a type which in our case coudl be company or candidate
//
const postLogin = (type,login,password) => 
     client.post(`${type}/Auth`,{
        login,
        password
     })
    .then(res => {return res.data})
    .catch(err=>onErrorRequest(err))


export default {
    postLogin,
}