using RestAPIModule.SharedModels;

namespace RestAPIModule.API.Authentication.Models
{
    [System.Serializable]
    public class LoginResponse : ResponseMessage
    {
        public int user_id;
        public string user_name;
        public string access_token;
        public bool new_user;
    }
}

namespace RestAPIModule.API
{
    public partial class ServerConstants
    {
        public enum LoginType
        {
            FB_LOGIN = 1,
            GUEST = 2,
            EMAIL = 3,
        }
    }
}
