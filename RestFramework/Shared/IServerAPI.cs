using System;
using System.Collections.Generic;
using BestHTTP;
using RestAPIModule.SharedModels;
using UnityEngine;

namespace RestAPIModule.API.Base
{
    /// <summary>
    /// Standard interface for encapsulating the api request.
    /// </summary>
    public interface IServerAPI
    {
        string MethodName { get; set; }
        HTTPRequest OriginalRequest { get; }
        List<RestAPIEntry> Entries { get; set; }
        void Init(params object[] requestParams);
    }

    public abstract class BaseServerRequest : IServerAPI
    {
        #region private members
        private List<RestAPIEntry> _entries;
        private string _methodName;
        private readonly HTTPRequest _originalRequest;
        #endregion

        #region public members
        public List<RestAPIEntry> Entries { get => _entries; set => _entries = value; }
        public string MethodName { get => _methodName; set => _methodName = value; }
        public HTTPRequest OriginalRequest => _originalRequest;
        #endregion

        protected abstract void AddRequestParams(params object[] requestParams);

        public BaseServerRequest(string methodName)
        {
            Entries = new List<RestAPIEntry>();
            this.MethodName = methodName;
            _originalRequest = new HTTPRequest(new Uri(ServerSettings.CurrentUrlSettings.RestUri + MethodName));
        }

        /// <summary>
        /// Gets called after the creation of object externally.
        /// </summary>
        /// <param name="requestParams"></param>
        public void Init(params object[] requestParams)
        {
            AddRequestParams(requestParams);
            AddCommonParams();
            MarshallFieldsInRequest();
        }

        /// <summary>
        /// Only the commom parameters for every api call gets added.
        /// </summary>
        private void AddCommonParams()
        {
            //This has to be first entry!
            Entries.Insert(ServerVariables.applicationKey, ServerVariables.paramAppKey, 0);

            if (ServerSettings.IsLoggedIn)
            {   //Second entry for better URL building.
                Entries.Insert(ServerVariables.kUserID, ServerSettings.staticPlayer.UserId, 1);
                Entries.Insert(ServerVariables.kAccessToken, ServerSettings.staticPlayer.AccessToken, 2);
            }
        }

        /// <summary>
        /// This adds the extra parameters in the request.
        /// </summary>
        private void MarshallFieldsInRequest()
        {
            for (int i = 0; i < Entries.Count; i++)
            {
                _originalRequest.AddField(Entries[i].key, Entries[i].value);
            }
        }

    }

    /// <summary>
    /// Should be used when there no additional parameters for the requests.
    /// </summary>
    public class BasicRequest : BaseServerRequest
    {
        public BasicRequest(string methodName) : base(methodName)
        {
        }

        protected override void AddRequestParams(params object[] requestParams)
        {
            //No parameters have to be added over here.
        }
    }
}
