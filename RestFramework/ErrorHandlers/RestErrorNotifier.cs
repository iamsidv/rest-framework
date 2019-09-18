using RestAPIModule.API.Base;
using UnityEngine;
namespace RestAPIModule.API.ErrorHandling
{
    public class RestErrorNotifier : IRestErrorNotifier
    {
        /// <summary>
        /// This is used to deal with error codes such a 404 not found errors.
        /// </summary>
        public override void NotifyError(int errorCode, string error, IServerAPI serverAPI)
        {
            Debug.Log("Notify Error, code : " + errorCode + " message " + error);
        }

        /// <summary>
        /// Triggered when the rest api response failes. Generates messages during
        /// TimeOut, Connection Error and Failed.
        /// </summary>
        public override void NotifyFailed(int failureCode, string message, IServerAPI serverAPI)
        {
            Debug.Log("Notify Failed, code : " + failureCode + " message " + message
                + " Methodname " + serverAPI.MethodName);
        }
    }
}