import Axios from   'axios';
import Config from '../../Config/index';

const client = Axios.create(Config.axios);

const onErrorRequest = (error)=>{
    console.log(error);
}

export  {
    client,
    onErrorRequest
}