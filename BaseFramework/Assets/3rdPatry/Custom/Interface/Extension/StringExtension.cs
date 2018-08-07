using UnityEngine;
using System.IO;
using System.Collections;
using System.Text;
/// <summary>
/// 字符串的处理
/// </summary>
public static class StringExtension
{
    /// <summary>
    /// 字符串是否为null或空
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static bool IsNullOrEmpty(this string str)
    {
        return string.IsNullOrEmpty(str);
    }

    /// <summary>
    /// 将字符串转换为int
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static int ToInt(this string str)
    {
        int result = 0;
        if (!int.TryParse(str, out result))
        {
            Debug.LogError("Can't Convert \"" + str + "\" to Int !");
        }
        return result;
    }

    public static T ConvertTo<T>(this string str)
    {
        T result = default(T);
        try
        {
            result = (T)System.Convert.ChangeType(str, typeof(T));
        }
        catch (System.Exception e)
        {
            Debug.LogError("Can't Convert \"" + str + "\" to " + typeof(T) + " !" + "\r\n" + e);
        }
        return result;
    }
    /// <summary>
    /// 将字符串转换为flaot
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static float ToFloat(this string str)
    {
        float result = 0;
        if (!float.TryParse(str, out result))
        {
            Debug.LogError("Can't Convert \"" + str + "\" to Float !");
        }
        return result;
    }

    public static byte[] ToByte(this string str)
    {
        return Encoding.UTF8.GetBytes(str);
    }

    /// <summary>
    /// 将字符串转换为enum
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="enumName"></param>
    /// <param name="isError">转换失败是否会报错</param>
    /// <returns>正确返回枚举，错误返回Null</returns>
    public static T ToEnum<T>(this string enumName, bool isError = true)
    {
        try
        {
            return (T)System.Enum.Parse(typeof(T), enumName);
        }
        catch
        {
            if (isError)
                Debug.LogError("Can't Find (" + enumName + ") in enum " + typeof(T) + "!");
        }
        return default(T);
    }

    /// <summary>
    /// 移除路径，得到文件名
    /// </summary>
    /// <param name="pathName">路径或Url,如E:/abc.jpg</param>
    /// <returns></returns>
    public static string GetFileName(this string pathName)
    {
        return Path.GetFileName(pathName);
    }

    /// <summary>
    /// 移除路径和扩展名，得到文件名
    /// </summary>
    /// <param name="pathName">路径或Url,如E:/abc.jpg</param>
    /// <returns>如abc</returns>
    public static string GetFileNameWithoutExtension(this string pathName)
    {
        return Path.GetFileNameWithoutExtension(pathName);
    }

    /// <summary>
    /// 获取后缀名，如.jpg
    /// </summary>
    /// <param name="pathName"></param>
    /// <returns></returns>
    public static string GetExtension(this string pathName)
    {
        return Path.GetExtension(pathName);
    }

    public static bool IsNull(this object target)
    {
        return target == null;
    }
}