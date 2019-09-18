using RestAPIModule.API.ErrorHandling;
using RestAPIModule.Context;
using UnityEngine;

public static class ServerSettings
{
    private static UrlSettings mUrlSettings;
    private const string devServerUrl = "dev-server-url";

    // TODO : Find a better way to implement this!
    internal static readonly AuthenticationContext staticPlayer = new AuthenticationContext();
    internal static bool IsLoggedIn { get => (bool)staticPlayer?.IsLoggedIn; }
    private static IRestErrorNotifier mErrorNotifier;

    static ServerSettings()
    {
        
    }

    /// <summary>
    /// The current url settings reference that is being used.
    /// </summary>
    internal static UrlSettings CurrentUrlSettings
    {
        get
        {
            if (mUrlSettings == null)
            {
                UrlSettings[] urlSettings = Resources.LoadAll<UrlSettings>(devServerUrl);
                mUrlSettings = urlSettings[0];
            }
            return mUrlSettings;
        }
    }

    internal static IRestErrorNotifier ErrorNotifier
    {
        get
        {
            if(mErrorNotifier==null)
            {
                mErrorNotifier = Object.FindObjectOfType<IRestErrorNotifier>();
            }

            return mErrorNotifier;
        }
    }

    /// <summary>
    /// Get the unique device identifier from the android and iOS.
    /// </summary>
    internal static string DeviceUniqueIdentifier
    {
        get
        {
            var deviceId = mUrlSettings.EditorDeviceIdentifier;
#if UNITY_ANDROID && !UNITY_EDITOR
                AndroidJavaClass up = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");
                AndroidJavaObject currentActivity = up.GetStatic<AndroidJavaObject> ("currentActivity");
                AndroidJavaObject contentResolver = currentActivity.Call<AndroidJavaObject> ("getContentResolver");
                AndroidJavaClass secure = new AndroidJavaClass ("android.provider.Settings$Secure");
                deviceId = secure.CallStatic<string> ("getString", contentResolver, "android_id");
#elif !UNITY_EDITOR
            deviceId = SystemInfo.deviceUniqueIdentifier;
#endif
            return deviceId;
        }
    }

    /// <summary>
    /// Use this method during logging out from the session.
    /// </summary>
    internal static void ClearSession()
    {
        staticPlayer?.ClearInfo();
    }
}