using System;
using RestAPIModule.API.Authentication.Models;
using RestAPIModule.API.Base;

namespace RestAPIModule.API.Authentication
{
    public static class AuthenticationAPI
    {
        public static void UserLogin(string username, ServerConstants.LoginType loginType, Action<LoginResponse> success = null, Action<int, string> failure = null)
        {
            AuthenticationRequest authenticationRequest = new AuthenticationRequest(ServerVariables.apiUserLogin);
            authenticationRequest.Init((int)loginType, username, ServerSettings.DeviceUniqueIdentifier);
            RestAPIController.PostSendRequest<LoginResponse>(authenticationRequest, (loginResponse) =>
            {
                InitiateLoginSuccess(loginResponse);

                if (success != null)
                    success.Invoke(loginResponse);
            }, failure);
        }

        private static void InitiateLoginSuccess(LoginResponse responseObj)
        {
            ServerSettings.staticPlayer.Init(responseObj.user_id, responseObj.access_token);
        }
    }

    public class AuthenticationRequest : BasicRequest
    {
        public AuthenticationRequest(string methodName) : base(methodName)
        {
        }

        protected override void AddRequestParams(params object[] requestParams)
        {
            Entries.Add(ServerVariables.kType, requestParams[0]);
            Entries.Add(ServerVariables.kName, requestParams[1]);
            Entries.Add(ServerVariables.kDeviceToken, requestParams[2]);
        }
    }
}