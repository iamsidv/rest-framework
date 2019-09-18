using System;
using System.Collections.Generic;
using UnityEngine;
using RestAPIModule;
using UnityEngine.EventSystems;
using RestAPIModule.SharedModels;
using RestAPIModule.API;

public static class ServerExtensions
{
    public static RestAPIEntry SetValue(this string _key, object _value)
    {
        return new RestAPIEntry(_key, _value);
    }

    public static void Add(this List<RestAPIEntry> list, string _key, object _value)
    {
        list.Add(new RestAPIEntry(_key, _value));
    }

    public static void Insert(this List<RestAPIEntry> list, string _key, object _value, int index)
    {
        list.Insert(index, new RestAPIEntry(_key, _value));
    }

    public static float RoundOff(this float mValue)
    {
        return Mathf.Round(mValue * 10f) / 10f;
    }

    public static Vector3 RoundOff(this Vector3 thisVec)
    {
        float newX = Mathf.Round(thisVec.x * 10f) / 10f;
        float newY = Mathf.Round(thisVec.y * 10f) / 10f;
        float newZ = Mathf.Round(thisVec.z * 10f) / 10f;

        return new Vector3(newX, newY, newZ);
    }



    public static string ToShortString(this string inputString)
    {
        int validCharacters = 20;
        //int maxDots = 2;

        if (inputString.Length > validCharacters)
        {
            // inputString = inputString.Remove(10) + "..";
            inputString = inputString.Remove(validCharacters) + "..";

        }

        return inputString;
    }

   

    public static int ToInt(this float num)
    {
        return (int)num;
    }

    public static int ToInt(this BestHTTP.HTTPRequestStates num)
    {
        return (int)num;
    }

    public static int ToInt(this Enum enums)
    {
        return Convert.ToInt32(enums);
    }

    public static BestHTTP.HTTPRequestStates ToHttpRequestState(this int num)
    {
        return (BestHTTP.HTTPRequestStates)num;
    }

    public static string ToBase64String(this Texture2D texture)
    {
        return Convert.ToBase64String(texture.EncodeToPNG());
    }

    public static Texture2D ToTexture2D(this string image64string)
    {
        byte[] imageByteArray = Convert.FromBase64String(image64string);
        Texture2D texture = new Texture2D(0, 0);
        texture.LoadImage(imageByteArray);
        return texture;
    }

    public static Texture2D FromBytesToTex2D(this byte[] imageByteArray)
    {
        Texture2D texture = new Texture2D(0, 0, TextureFormat.ARGB32, false, true);
        texture.LoadImage(imageByteArray);
        return texture;
    }

    public static bool ToBoolean(this int num)
    {
        return Convert.ToBoolean(num);
    }

    public static ServerConstants.LoginType ToLoginType(this int num)
    {
        return (ServerConstants.LoginType)num;
    }

    public static ServerVariables.Gender ToGender(this int num)
    {
        return (ServerVariables.Gender)num;
    }

    public static string ToDesiredDigitialUnit(this long bytes)
    {
        float count = Mathf.Floor(Mathf.Log10(bytes) + 1);

        if (count < 3)
        {
            return (bytes.ToString("F1") + "Bytes");
        }
        else if (count >= 3 && count < 7)
        {
            return ((bytes / Mathf.Pow(2, 10)).ToString("F1") + "KB");
        }
        else if (count >= 7 && count < 9)
        {
            return ((bytes / Mathf.Pow(2, 20)).ToString("F1") + "MB");
        }
        else
        {
            return ((bytes / Mathf.Pow(2, 30)).ToString("F1") + "GB");
        }

    }

    public static float GetDecimalPart(this float num)
    {
        float finalVal = (float)(num - (int)num);
        return finalVal;
    }

    public static string ToDDHHMMSS(this float timer)
    {
        int timeInSeconds = (int)(timer % 60);
        int timeInMinutes = (int)((timer / 60) % 60);
        int timeInHours = (int)((timer / 3600) % 24);
        string seconds = timeInSeconds < 10 ? "0" + timeInSeconds : timeInSeconds.ToString();
        string minutes = timeInMinutes < 10 ? "0" + timeInMinutes : timeInMinutes.ToString();
        string hours = timeInMinutes < 10 ? "0" + timeInHours : timeInHours.ToString();
        string days = timeInHours < 10 ? "0" + timeInHours : timeInHours.ToString();
        return string.Format("{0}:{1}:{2}:{3}", new object[] { days, hours, minutes, seconds });
    }

    public static T GetValue<T>(this Dictionary<string, object> mdict, string key)
    {
        object _value;
        if (mdict.TryGetValue(key, out _value))
        {
            if (_value != null && _value.GetType() == typeof(T))
                return (T)_value;

            return default(T);
        }
        return default(T);
    }

   
    public static bool IsPointerOverUIObject(this EventSystem eventSystem)
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(eventSystem);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
}
