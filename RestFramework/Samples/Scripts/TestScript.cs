using RestAPIModule.API;
using RestAPIModule.API.Authentication;
using RestAPIModule.API.Authentication.Models;
using RestAPIModule.API.MasterData.Countries;
using RestAPIModule.API.MasterData.Models;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AuthenticationAPI.UserLogin("Blob", ServerConstants.LoginType.GUEST, OnLoginSuccess, OnLoginFailure);
    }

    private void OnLoginFailure(int arg1, string arg2)
    {

    }

    private void OnLoginSuccess(LoginResponse obj)
    {
        Debug.Log(obj == null);
        Debug.Log("Is logged in " + ServerSettings.IsLoggedIn);
    }

    public void LoadMasterCountryData()
    {
        MasterCountriesAPI.GetMasterCountries(OnCountriesFetched);
    }

    private void OnCountriesFetched(MasterCountryResponse obj)
    {
        Debug.Log("Country Data Fetched");
    }
}