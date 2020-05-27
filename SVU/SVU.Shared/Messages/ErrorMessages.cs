namespace SVU.Shared.Messages
{
    /// <summary>
    /// holds the error messages that are returned by the server
    /// </summary>
    public static class ErrorMessages
    {
        public const string SomthingWorngHappend = "Ops! there seems to be somthing wrong while doing some operations";
        public const string InvaildData = "The data that was sent either is invaild or not well formated";
        public const string InvaildLoginAttempt = "Unable to authenticate with the sent username and password";
        public const string JavaScriptNotAllowed = "javascript is not allowed";
        public const string WrongCaptcha = "The provided captcha is invalid";


        public static string EmailIsUsed(string email) => $"Email {email} is already in use";
        public static string UsernameIsUsed(string username) => $"Username {username} is already in use";



    }
}
