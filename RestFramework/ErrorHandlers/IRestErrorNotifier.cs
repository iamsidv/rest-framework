using RestAPIModule.API.Base;
using UnityEngine;
namespace RestAPIModule.API.ErrorHandling
{
    public abstract class IRestErrorNotifier : MonoBehaviour
    {
        /// <summary>
        /// This is used to deal with error codes such a 404 not found errors.
        /// </summary>
        /// <param name="errorCode"> Return the error code </param>
        /// <param name="error">Explains the message related to the error code</param>
        public abstract void NotifyError(int errorCode, string error, IServerAPI serverAPI);

        /// <summary>
        /// Triggered when the rest api response failes. Generates messages during
        /// TimeOut, Connection Error and Failed.
        /// </summary>
        /// <param name="failureCode"> it is generally an integer.</param>
        /// <param name="message"></param>
        public abstract void NotifyFailed(int failureCode, string message, IServerAPI serverAPI);
    }
}