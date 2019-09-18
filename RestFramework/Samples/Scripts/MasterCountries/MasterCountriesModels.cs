using RestAPIModule.SharedModels;

namespace RestAPIModule.API.MasterData.Models
{
    [System.Serializable]
    public class ServerCountryInfo
    {
        public int master_country_id;
        public string title;
    }

    [System.Serializable]
    public class MasterCountryResponse : ResponseMessage
    {
        public ServerCountryInfo[] country;
    }
}

namespace RestAPIModule.API
{
    public partial class ServerConstants
    {

    }
}