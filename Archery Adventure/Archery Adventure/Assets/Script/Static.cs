using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class Static
{
	public static float HEIGHT_PLAYER = 0.8f;
	public static float ENEMY_MAX_HEIGHT_AIM = 7f;
	public static float ENEMY_MIN_HEIGHT_AIM = 2f;
	public static float ENEMY_MAX_POWER = 40f;

    public static string strPathSavedData
    {
        get
        {
#if UNITY_IOS
            return System.IO.Path.Combine(Application.persistentDataPath, "da/aa/");
#else
            return Application.persistentDataPath + "/../da/aa/";
#endif
        }
    }

    public static string strPathSavedRData
    {
        get
        {
#if UNITY_IOS
            return System.IO.Path.Combine(Application.persistentDataPath, "da/00/");
#else
            return Application.persistentDataPath + "/../da/00/";
#endif
        }
    }

    public static string GetDeviceId()
    {
#if UNITY_IOS && !UNITY_EDITOR
        string uuid = UUIDiOS.GetKeyChainValue("uuid");
        if(uuid == "null")
        {
			uuid = SystemInfo.deviceUniqueIdentifier;
            UUIDiOS.SaveKeyChainValue("uuid", uuid);
        } else {
			Debug.Log("uuid lấy từ Keychain: " + uuid);
        }

		return uuid;
#else
        return SystemInfo.deviceUniqueIdentifier;
#endif
    }

    private static System.Random rng = new System.Random();
    public static int[] CoinValues = new int[] { 1250, 250, 50, 10 };
    public static bool isUnlockAll = false;

    public static float MaxX { get { return Camera.main.orthographicSize * Camera.main.aspect; } }

    public static float MinX { get { return -Camera.main.orthographicSize * Camera.main.aspect; } }

    public static float MaxY { get { return Camera.main.orthographicSize; } }

    public static float MinY { get { return -Camera.main.orthographicSize; } }

    public static float BottomEdge { get { return MinY - 0.3f; } }

    public static float TopEdge { get { return MaxY + 0.3f; } }

    public static float WaveDelay { get { return 2f; } }

    public static float RandomAttackCoolDown { get { return UnityEngine.Random.Range(40, 60) / 10f; } }

    public static float RandomDiveCoolDown { get { return UnityEngine.Random.Range(20, 40) / 10f; } }
    /*
    public static float waveAttackCoolDown { get { return 3f; } }
    public static float waveDiveCoolDown { get { return 4f; } }
    */
    public static float enemyAttackCoolDown { get { return 3f; } }

    public static float RadianToDegree(float radian)
    {
        return radian * 180 / Mathf.PI;
    }

    internal static object VectorByRotateAngle(float angle, object normalized)
    {
        throw new NotImplementedException();
    }

    public static float DegreeToRadian(float degree)
    {
        return degree * Mathf.PI / 180;
    }

    public static string Number2String(int number, bool isCaps)
    {
        char c = (char)((isCaps ? 65 : 97) + (number - 1));
        return c.ToString();
    }

    public static float DifficultStatCalculate(float z, float lower, float higher, float statLower, float statHigher)
    {
        return (z - lower) / (higher - lower) * (statHigher - statLower) + statLower;
    }

    public static long CurrentTimeInMilliSec()
    {
        return (long)(DateTime.Now - new DateTime(1970, 1, 1)).TotalMilliseconds;
    }

    public static Vector3 PointDistanceFromPoint(float distance, Vector3 direction, Vector3 fromPoint)
    {
        Vector3 finalDirection = direction.normalized * distance;
        Vector3 targetPosition = fromPoint + finalDirection;

        return targetPosition;
    }

    public static Vector2 VectorByRotateAngle(float angle, Vector2 v) // Unity quay theo chiều ngược kim đồng hồ
    {
        float cos = Mathf.Cos(DegreeToRadian(angle));
        float sin = Mathf.Sin(DegreeToRadian(angle));

        return new Vector2(v.x * cos - v.y * sin, v.x * sin + v.y * cos);
    }

    public static float AngleDir(Vector2 A, Vector2 B) // Check B bên phải hay trái A ( > 0: bên phải )
    {
        return -A.x * B.y + A.y * B.x;
    }

    public static float FallSpeed { get { return 2.5f; } }

    public static void Shuffle<T>(this IList<T> list)
    {
        for (var i = 0; i < list.Count; i++)
            list.Swap(i, rng.Next(i, list.Count));
    }

    public static void Swap<T>(this IList<T> list, int i, int j)
    {
        T temp = list[i];
        list[i] = list[j];
        list[j] = temp;
    }

    public static void ResetTransform(this Transform trans)
    {
        trans.position = Vector3.zero;
        trans.localRotation = Quaternion.identity;
        trans.localScale = new Vector3(1, 1, 1);
    }

    public static float DurationFromSpeed(Vector2 a, Vector2 b, float speed)
    {
        return Vector2.Distance(a, b) / speed;
    }

    public static Vector3 RandomPointFromObject(Transform objTransform, float range, int maxAngle = 360)
    {
        Vector2 originDirection = VectorByRotateAngle(-maxAngle / 2, Vector2.up);
        int randomDegree = UnityEngine.Random.Range(0, maxAngle);
        Vector2 randomDirection = VectorByRotateAngle(randomDegree, originDirection).normalized;
        return PointDistanceFromPoint(range, randomDirection, objTransform.position);
    }

    public static void LookAtDirection2D(this Transform trans, Vector3 targetPosition)
    {
        Vector3 dir = targetPosition - trans.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        trans.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    public static void LookAtDirection2DPlus90(this Transform trans, Vector3 targetPosition)
    {
        Vector3 dir = targetPosition - trans.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        trans.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);
    }

    public static void LookAtDirection2DMinus90(this Transform trans, Vector3 targetPosition)
    {
        Vector3 dir = targetPosition - trans.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        trans.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
    }

    public static void LookAtTarget(this Transform trans, Vector3 targetPosition)
    {
        Vector3 dir = targetPosition - trans.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
        trans.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    public static Color Parse(string hexstring)
    {
        if (hexstring.StartsWith("#"))
        {
            hexstring = hexstring.Substring(1);
        }

        if (hexstring.StartsWith("0x"))
        {
            hexstring = hexstring.Substring(2);
        }

        if (hexstring.Length != 6 && hexstring.Length != 8)
        {
            throw new Exception(string.Format("{0} is not a valid color string.", hexstring));
        }

        byte r = byte.Parse(hexstring.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
        byte g = byte.Parse(hexstring.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
        byte b = byte.Parse(hexstring.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);

        if (hexstring.Length == 8)
        {
            byte a = byte.Parse(hexstring.Substring(6, 2), System.Globalization.NumberStyles.HexNumber);
            return new Color32(r, g, b, a);
        }
        return new Color32(r, g, b, 1);
    }

    public static int GetStarLevelRequirement(int level)
    {
        level = level / 8 * 8;
        int a = 12;
        int starRequirement = 0;
        if (level < 24)
            starRequirement = 0;
        else
            starRequirement = 2 * (level - (a + 1));
        //else
        //    starRequirement = 3 * (level - (a + 1));

        return starRequirement;
    }

//    #region Simple Json Extension
//
//    public static string GetNodeStringValue(this JSONNode jsonNode, string key, string defaultValue = "")
//    {
//        return jsonNode[key] ? jsonNode[key].Value : defaultValue;
//    }
//
//    public static int GetNodeIntValue(this JSONNode jsonNode, string key, int defaultValue = 0)
//    {
//        return jsonNode[key] ? jsonNode[key].AsInt : defaultValue;
//    }
//
//    public static float GetNodeFloatValue(this JSONNode jsonNode, string key, float defaultValue = 0f)
//    {
//        return jsonNode[key] ? jsonNode[key].AsFloat : defaultValue;
//    }
//
//    public static bool GetNodeBoolValue(this JSONNode jsonNode, string key, bool defaultValue = true)
//    {
//        return jsonNode[key] ? jsonNode[key].AsBool : defaultValue;
//    }
//
//    public static bool NodeHasKey(this JSONNode jsonNode, string key)
//    {
//        return jsonNode[key] != null;
//    }
//
//    #endregion
//
//    public static int CurrentTimeInSecond
//    {
//        get { return (int)(DateTime.Now - new DateTime(1970, 1, 1)).TotalSeconds; }
//    }
//
//    public static long CurrentTimeInMilisec
//    {
//        get { return (long)(DateTime.Now - new DateTime(1970, 1, 1)).TotalMilliseconds; }
//    }
//
//    public static string Encrypt(this string str)
//    {
//        byte[] bytes = System.Text.Encoding.UTF8.GetBytes(str);
//        int randomInt = rng.Next(3, 10);
//        JSONArray arr = new JSONArray();
//        arr.Add(randomInt);
//        for (int i = 0; i < bytes.Length; ++i)
//        {
//            if (i % randomInt == 0)
//            {
//                arr.Add(rng.Next(1, 100));
//            }
//            arr.Add((int)bytes[i]);
//        }
//        str = arr.ToString();
//        str = str.Replace("[", "").Replace("]", "");
//        return str;
//    }
//
//    public static string Decrypt(this string str)
//    {
//        if (!str.Contains("{"))
//        {
//            str = "[" + str + "]";
//            JSONArray arr = JSON.Parse(str).AsArray;
//            int ran = arr[0];
//            int c = 1;
//            List<byte> bytes = new List<byte>();
//            for (int i = 1; i < arr.Count; ++i)
//            {
//                if (i == c)
//                {
//                    c += ran + 1;
//                }
//                else
//                {
//                    bytes.Add((byte)arr[i]);
//                }
//            }
//            str = System.Text.Encoding.UTF8.GetString(bytes.ToArray());
//        }
//        return str;
//    }
    
    public static string FormatNumberShorter(int m)
    {
        string original = "" + m;
        string tmp = "";
        if (original.Length > 9)
        {
            tmp = original.Substring(0, original.Length - 9) + "." + original.Substring(original.Length - 9, 1) + "B";
        }
        else if (original.Length > 6)
        {
            tmp = original.Substring(0, original.Length - 6) + "." + original.Substring(original.Length - 6, 1) + "M";
        }
        else if (original.Length > 3)
        {
            tmp = original.Substring(0, original.Length - 3) + "." + original.Substring(original.Length - 3, 1) + "K";
        }
        else
        {
            tmp = original;
        }
        if (tmp.Contains("."))
        {
            string end = tmp.Substring(tmp.Length - 2, 1);
            while (end.CompareTo("0") == 0)
            {
                tmp = tmp.Substring(0, tmp.Length - 2) + tmp.Substring(tmp.Length - 1, 1);
                end = tmp.Substring(tmp.Length - 2, 1);
            }
            if (end.CompareTo(".") == 0)
            {
                tmp = tmp.Substring(0, tmp.Length - 2) + tmp.Substring(tmp.Length - 1, 1);
            }
        }
        return tmp;
    }

    public static string FormatNumberAddChar(int m, string wedge, int max = 999999999)
    {
        if (m > max)
        {
            return FormatNumberShorter(m);
        }

        string original = "" + m;
        string tmp = "";
        if (original.Length > 3)
        {
            while (original.Length > 3)
            {
                string sub = original.Substring(original.Length - 3, 3);
                original = original.Substring(0, original.Length - 3);

                tmp = sub + ((tmp.Length > 0) ? (wedge + tmp) : tmp);
            }
            tmp = original + wedge + tmp;
        }
        else
        {
            tmp = original;
        }
        return tmp;
    }

    public static string FormatTime(int timeRemain)
    {
        string timeStr = "";
        int d = timeRemain / (24 * 60 * 60);
        int h = (timeRemain - d * 24 * 60 * 60) / (60 * 60);
        int m = (timeRemain - d * 24 * 60 * 60 - h * 60 * 60) / 60;
        int s = timeRemain - d * 24 * 60 * 60 - h * 60 * 60 - m * 60;
        if (d > 0)
        {
            if (h > 0)
            {
                timeStr = d + "d " + h + "h";
            }
            else
            {
                timeStr = d + "d ";
            }
        }
        else if (h > 0)
        {
            if (m > 0)
            {
                timeStr = h + "h " + m + "m";
            }
            else
            {
                timeStr = h + "h";
            }
        }
        else if (m > 0)
        {
            if (s > 0)
            {
                timeStr = m + "m " + s + "s";
            }
            else
            {
                timeStr = m + "m";
            }
        }
        else
        {
            timeStr = s + "s";
        }
        return timeStr;
    }

    public static string FormatTimeLong(int timeRemain)
    {
        string timeStr = "";
        int d = timeRemain / (24 * 60 * 60);
        int h = (timeRemain - d * 24 * 60 * 60) / (60 * 60);
        int m = (timeRemain - d * 24 * 60 * 60 - h * 60 * 60) / 60;
        int s = timeRemain - d * 24 * 60 * 60 - h * 60 * 60 - m * 60;

        if (d > 0)
        {
            timeStr += (d + "d ");
        }
        if (h > 0)
        {
            timeStr += (h + "h ");
        }
        if (m > 0)
        {
            timeStr += (m + "m ");
        }
        if (s > 0)
        {
            timeStr += (s + "s");
        }
        return timeStr;
    }
  
}
