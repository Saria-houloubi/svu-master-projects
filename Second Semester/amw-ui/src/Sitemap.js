
const navigationBarLinks = 
{
    all : [
        // {
        //     id :1,
        //     link: '/#Jobs',
        //     title :'Jobs'
        // }
    ],
    nonAuthorizedLinks : [
       
    ]
};

const cookieDefaults = {
    token : '.Amw.token'
}

const registerFrom = {
    candidate : [
        {
            order : 1,
            name : 'fullName',
            label :'Full Name',
            srOnly : 'Full name',
            placeHolder : 'Full Name',
            value :'name',
            required : true,
            type : 'text'
        },
        {
            order : 2,
            name : 'login',
            label :'Login',
            srOnly : 'Login',
            placeHolder : 'Login',
            value :'login',
            required : true,
            type : 'text'
        },
        {
            order : 3,
            name : 'password',
            label :'Password',
            srOnly : 'Password',
            placeHolder : 'Password',
            value :'password',
            required : true,
            type : 'password'
        },
        {
            order : 4,
            name : 'educationLevel',
            label :'Education level',
            srOnly : 'Education level',
            value :'levle',
            placeHolder : 'Education level',
            type : 'text'
        },
        {
            order : 5,
            name : 'experince',
            label :'Experince',
            srOnly : 'Experince',
            value :1,
            placeHolder : 'Experince',
            type : 'number'
        },
        {
            order : 6,
            name : 'tel',
            label :'Tel',
            srOnly : 'Tel',
            value :1,
            placeHolder : 'Tel',
            type : 'text'
        }
    ],
    company : [
        {
            order : 1,
            name : 'name',
            label :'Name',
            srOnly : 'Name',
            placeHolder : 'Name',
            required : true,
            type : 'text'
        },
        {
            order : 2,
            name : 'login',
            label :'Login',
            srOnly : 'Login',
            placeHolder : 'Login',
            required : true,
            type : 'text'
        },
        {
            order : 3,
            name : 'password',
            label :'Password',
            srOnly : 'Password',
            placeHolder : 'Password',
            required : true,
            type : 'password'
        },
        {
            order : 4,
            name : 'tel',
            label :'Tel',
            srOnly : 'Tel',
            placeHolder : 'Tel',
            type : 'text'
        }
    ]

};

const account = 
{
    default :  {
        id : 1,
        type: 'candidate',
    },
    types : [
        {
            id : 1,
            type: 'candidate',
        },
        {
            id : 2,
            type: 'company',
        },
    ]
}
export  {
    navigationBarLinks,
    cookieDefaults,
    registerFrom,
    account
}