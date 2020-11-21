import {client, onErrorRequest} from './BaseController'

const apiEndPoint = 'candidate';

//
//Gets all the education levels for user to choose from
//
const postLoginCandidate = (login,password) => 
     client.post(`${apiEndPoint}/Auth`,{
        login,
        password
     })
    .then(res => {return res.data})
    .catch(err=>onErrorRequest(err))


export default {
    postLoginCandidate
}