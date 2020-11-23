import {client, onErrorRequest} from './BaseController'

const apiEndPoint = 'lookup';


//
//Gets lookup data based on a type
//
const getLookupData = (type) => 
     client.get(`${apiEndPoint}/${type}`)
    .then(res => {return res.data})
    .catch(err=>onErrorRequest(err))

export default{
    getLookupData
}