
const navigationBarLinks = 
{
    all : [
        {
            id :1,
            link: '/#Jobs',
            title :'Jobs'
        }
    ],
    nonAuthorizedLinks : [
       
    ]
};

const cookieDefaults = {
    userInfo : '.Amw.user',
    token : '.Amw.token'
}

const registerFrom = {
    candidate : [
        {
            order : 1,
            label :'Full Name',
            srOnly : 'Full name',
            placeHolder : 'Full Name',
            required : true,
            type : 'text'
        },
        {
            order : 2,
            label :'Login',
            srOnly : 'Login',
            placeHolder : 'Login',
            required : true,
            type : 'text'
        },
        {
            order : 3,
            label :'Password',
            srOnly : 'Password',
            placeHolder : 'Password',
            required : true,
            type : 'password'
        },
        {
            order : 4,
            label :'Education level',
            srOnly : 'Education level',
            placeHolder : 'Education level',
            type : 'text'
        },
        {
            order : 5,
            label :'Experince',
            srOnly : 'Experince',
            placeHolder : 'Experince',
            type : 'number'
        },
        {
            order : 6,
            label :'Tel',
            srOnly : 'Tel',
            placeHolder : 'Tel',
            type : 'text'
        }
    ],
    company : [
        {
            order : 1,
            label :'Name',
            srOnly : 'Name',
            placeHolder : 'Name',
            required : true,
            type : 'text'
        },
        {
            order : 2,
            label :'Login',
            srOnly : 'Login',
            placeHolder : 'Login',
            required : true,
            type : 'text'
        },
        {
            order : 3,
            label :'Password',
            srOnly : 'Password',
            placeHolder : 'Password',
            required : true,
            type : 'password'
        },
        {
            order : 4,
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