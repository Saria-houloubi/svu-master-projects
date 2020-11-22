import Lookup from './Controllers/LookupController';
import Entity from './Controllers/EntityController';
import Auth from './Controllers/AuthController';
import {client} from './Controllers/BaseController';

const setHeaderAuthToken=(token)=>{
    client.defaults.headers.common['Authorization'] = `Bearer ${token}`;
}

export  {
    Lookup,
    Entity,
    Auth,
    setHeaderAuthToken
}