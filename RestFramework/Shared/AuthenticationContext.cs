namespace RestAPIModule.Context
{
    public sealed class AuthenticationContext
    {
        public int UserId { get; private set; }
        public string UserName { get; private set; }
        public string AccessToken { get; private set; }
        public bool IsLoggedIn { get; private set; }

        public AuthenticationContext() { }

        internal void Init(int user_id, string access_token)
        {
            this.UserId = user_id;
            this.AccessToken = access_token;
            this.IsLoggedIn = true;
        }

        internal void ClearInfo()
        {
            this.UserId = -1;
            this.UserName = null;
            this.AccessToken = null;
            this.IsLoggedIn = false;
        }
    }
}