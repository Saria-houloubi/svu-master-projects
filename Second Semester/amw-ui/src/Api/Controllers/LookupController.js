import {client, onErrorRequest} from './BaseController'

const apiEndPoint = 'lookup';


//
//Gets all the education levels for user to choose from
//
const getEducationLevels = () => 
     client.get(`${apiEndPoint}/EducationLevels`)
    .then(res => {return res.data})
    .catch(err=>onErrorRequest(err))

export default{
    getEducationLevels
}