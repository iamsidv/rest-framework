using System;
using System.Collections.Generic;
using BestHTTP;
using RestAPIModule.API.Base;
using RestAPIModule.SharedModels;
using UnityEngine;

public class RestAPIController
{
    #region APICalls

    /// <summary>
    /// Sends the GET request
    /// </summary>
    /// <param name="apimethod">Apimethod.</param>
    /// <param name="entries">Entries.</param>
    /// <param name="success">Success.</param>
    /// <param name="failure">Failure.</param>
    private static void SendRequestGET(string apimethod, List<RestAPIEntry> entries, Action<string> success = null, Action failure = null)
    {
        BuildCommonParams(apimethod, ref entries);
        HTTPRequest req = new HTTPRequest(new Uri(ServerSettings.CurrentUrlSettings.RestUri + apimethod),
                              HTTPMethods.Get, ((originalRequest, response) =>
                              {
                                  if (response.StatusCode == 200)
                                  {
                                      if (success != null)
                                          success.Invoke(response.DataAsText);
                                  }
                                  else
                                  {
                                      if (failure != null)
                                          failure.Invoke();
                                  }
                              }));

        for (int i = 0; i < entries.Count; i++)
        {
            req.AddField(entries[i].key, entries[i].value);
        }

        req.ConnectTimeout = TimeSpan.FromSeconds(30);
        req.Timeout = TimeSpan.FromSeconds(30);
        Debug.Log(req.CurrentUri);
        req.Send();
    }

    /// <summary>
    /// Sends the POST request.
    /// </summary>
    /// <param name="apimethod">Apimethod.</param>
    /// <param name="entries">Entries.</param>
    /// <param name="success">Success.</param>
    /// <param name="failure">Failure.</param>
    private static void SendRequestPOST(string apimethod, List<RestAPIEntry> entries, Action<string> success = null, Action<int, string> failure = null)
    {
        try
        {
            BuildCommonParams(apimethod, ref entries);
            HTTPRequest req = new HTTPRequest(new Uri(ServerSettings.CurrentUrlSettings.RestUri + apimethod),
                                  HTTPMethods.Post, ((originalRequest, response) =>
                                  {
                                      switch (originalRequest.State)
                                      {
                                          case HTTPRequestStates.Finished:

                                              if (response.StatusCode == 200)
                                              {
                                                  if (success != null)
                                                  {
                                                      success.Invoke(response.DataAsText);
                                                  }
                                              }
                                              else
                                                  ServerSettings.ErrorNotifier?.NotifyError(response.StatusCode, response.Message, null);
                                              break;
                                          case HTTPRequestStates.Error:
                                          case HTTPRequestStates.TimedOut:
                                          case HTTPRequestStates.ConnectionTimedOut:
                                              ServerSettings.ErrorNotifier?.NotifyFailed((int)originalRequest.State, response.Message, null);
                                              break;
                                          default:
                                              break;
                                      }
                                  }));

            for (int i = 0; i < entries.Count; i++)
            {
                req.AddField(entries[i].key, entries[i].value);
            }
            PrintDebugUrl(entries, apimethod);
            req.ConnectTimeout = TimeSpan.FromSeconds(ServerSettings.CurrentUrlSettings.ConnectTimeout);
            req.Timeout = TimeSpan.FromSeconds(ServerSettings.CurrentUrlSettings.TimeOut);
            req.Send();
            //
        }
        catch (Exception ex)
        {
            Debug.LogError("Caught at - " + apimethod);
            Debug.LogError("Exception is" + ex);
        }

    }

    /// <summary>
    /// Sends the POST request.
    /// </summary>
    /// <param name="apimethod">Apimethod.</param>
    /// <param name="entries">Entries.</param>
    /// <param name="success">Success.</param>
    /// <param name="failure">Failure.</param>
    /// <typeparam name="T">The 1st type parameter.</typeparam>
    private static void SendRequestPOST<T>(string apimethod, List<RestAPIEntry> entries, Action<T> success = null, Action<int, string> failure = null) where T : ResponseMessage
    {
        BuildCommonParams(apimethod, ref entries);

        HTTPRequest req = new HTTPRequest(new Uri(ServerSettings.CurrentUrlSettings.RestUri + apimethod),
                              HTTPMethods.Post, ((originalRequest, response) =>
                              {
                                  var fileredResponse = ((response == null) ? "Response is null" : response.DataAsText);
                                  Debug.Log("Response: " + fileredResponse);
                                  switch (originalRequest.State)
                                  {
                                      case HTTPRequestStates.Finished:
                                          if (response.StatusCode == 200)
                                              GetResponse<T>(response.DataAsText, success, failure);
                                          else
                                              ServerSettings.ErrorNotifier?.NotifyError(response.StatusCode, response.Message, null);
                                          break;
                                      case HTTPRequestStates.Error:
                                      case HTTPRequestStates.TimedOut:
                                      case HTTPRequestStates.ConnectionTimedOut:
                                          ServerSettings.ErrorNotifier?.NotifyFailed((int)originalRequest.State, fileredResponse, null);
                                          break;
                                      default:
                                          break;
                                  }
                              }));

        for (int i = 0; i < entries.Count; i++)
        {
            req.AddField(entries[i].key, entries[i].value);
        }
        //
        PrintDebugUrl(entries, apimethod);
        req.ConnectTimeout = TimeSpan.FromSeconds(ServerSettings.CurrentUrlSettings.ConnectTimeout);
        req.Timeout = TimeSpan.FromSeconds(ServerSettings.CurrentUrlSettings.TimeOut);
        //
        req.Send();
    }

    /// <summary>
    /// Sends the POST request to the server.
    /// </summary>
    public static void PostSendRequest<T>(IServerAPI apiReq, Action<T> success = null, Action<int, string> failure = null) where T : ResponseMessage
    {
        HTTPRequest req = apiReq.OriginalRequest;
        req.MethodType = HTTPMethods.Post;
        req.Callback = (originalRequest, response) =>
        {
            var fileredResponse = ((response == null) ? "Response is null" : response.DataAsText);
            Debug.Log("Response: " + fileredResponse);
            switch (originalRequest.State)
            {
                case HTTPRequestStates.Finished:
                    if (response.StatusCode == 200)
                    {
                        GetResponse<T>(response.DataAsText, success, failure);
                    }
                    else
                        ServerSettings.ErrorNotifier?.NotifyError(response.StatusCode, response.Message, apiReq);
                    break;
                case HTTPRequestStates.Error:
                case HTTPRequestStates.TimedOut:
                case HTTPRequestStates.ConnectionTimedOut:
                    ServerSettings.ErrorNotifier?.NotifyFailed((int)originalRequest.State, fileredResponse, apiReq);
                    break;
                default:
                    break;
            }
        };

        //
        PrintDebugUrl(apiReq.Entries, apiReq.MethodName);
        req.ConnectTimeout = TimeSpan.FromSeconds(ServerSettings.CurrentUrlSettings.ConnectTimeout);
        req.Timeout = TimeSpan.FromSeconds(ServerSettings.CurrentUrlSettings.TimeOut);
        //
        req.Send();
    }

    private static void BuildCommonParams(string apimethod, ref List<RestAPIEntry> entries)
    {
        //This has to be first entry!
        entries.Insert(ServerVariables.applicationKey, ServerVariables.paramAppKey, 0);

        if (ServerSettings.IsLoggedIn)
        {   //Second entry for better URL building.
            entries.Insert(ServerVariables.kUserID, ServerSettings.staticPlayer.UserId, 1);
            entries.Insert(ServerVariables.kAccessToken, ServerSettings.staticPlayer.AccessToken, 2);
        }
    }

    /// <summary>
    /// Gets the response and parses it as per the Type entered.
    /// </summary>
    /// <param name="jsonData">Json data in string format.</param>
    /// <param name="success">Success Callback.</param>
    /// <param name="failure">Failure Callback.</param>
    /// <typeparam name="T">The 1st type parameter.</typeparam>
    private static void GetResponse<T>(string jsonData, Action<T> success = null, Action<int, string> failure = null) where T : ResponseMessage
    {
        ServerResponse<T> _response = JsonUtility.FromJson<ServerResponse<T>>(jsonData);
        if (_response.responseCode == ServerVariables.RESPONSE_SUCCESS)
        {
            if (success != null)
            {
                success.Invoke(_response.responseMsg);
            }
        }
        else if (_response.responseCode == ServerVariables.RESPONSE_INVALID_TOKEN)
        {
            if (failure != null)
            {
                Debug.Log("Callng from here...invalid access token");
                failure.Invoke(_response.responseCode, "Duplicate login");
            }

        }
        else
        {
            if (failure != null)
            {
                failure.Invoke(_response.responseCode, _response.responseInfo);
            }
        }
    }
    #endregion

    #region Debugging
    private static void PrintDebugUrl(List<RestAPIEntry> entries, string apimethod)
    {
        if (ServerSettings.CurrentUrlSettings.UrlDebuging)
        {
            string url = ServerSettings.CurrentUrlSettings.RestUri + apimethod;
            for (int i = 0; i < entries.Count; i++)
            {
                url += "&" + entries[i].key + "=" + entries[i].value;
            }
            Debug.Log("<color=green>" + url + "</color>");
        }
    }
    #endregion
}