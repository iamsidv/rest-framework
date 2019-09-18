using UnityEngine;

[CreateAssetMenu(fileName = "UrlSettings", menuName = "JuegoTools/CreateUrlSettings", order = 1)]
public class UrlSettings : ScriptableObject
{
    private const string RestParams = "/rest.php?methodName=";
    private const string SocketParams = "/socket.io/";

    [SerializeField] private string restUri = null;
    [SerializeField] private string socketUri = null;
    [SerializeField] private int connectTimeout = 30;
    [SerializeField] private int timeOut = 30;
    [SerializeField] private string editorDeviceIdentifier = "EditorDeviceId";
    [SerializeField] private bool showDebugUrl = false;

    public string RestUri => restUri.Trim() + RestParams;
    public string SocketUri => socketUri.Trim() + SocketParams;
    public int ConnectTimeout => connectTimeout;
    public int TimeOut => timeOut;
    public bool UrlDebuging => showDebugUrl;
    public string EditorDeviceIdentifier => editorDeviceIdentifier;
}