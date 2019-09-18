using System;
using RestAPIModule.API.Base;
using RestAPIModule.API.MasterData.Models;

namespace RestAPIModule.API.MasterData.Countries
{
    public static class MasterCountriesAPI
	{
		public static void GetMasterCountries(Action<MasterCountryResponse> success = null, Action<int, string> failure = null)
		{
            BasicRequest request = new BasicRequest(ServerVariables.apiMasterCountry);
            request.Init();
            RestAPIController.PostSendRequest(request, success, failure);
        }
	}
}