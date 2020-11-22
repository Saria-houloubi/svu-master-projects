import {client, onErrorRequest} from './BaseController'

//
//Gets the info based on the sent type
//
const getInfo = (type) =>
    client.get(type)
    .then(res=>res.data)
    .catch(err=>onErrorRequest(err))



//
//Sends a register request to the set type
//
const registerInfo= (type,info)=>
    client.post(type,info)
    .then(res=>res.data)
    .catch(err=>onErrorRequest(err))


export default {
    getInfo,
    registerInfo
}