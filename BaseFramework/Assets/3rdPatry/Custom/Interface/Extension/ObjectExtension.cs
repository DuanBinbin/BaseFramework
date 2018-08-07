using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
public static class ObjectExtension
{
    /// <summary>
    /// 类型转换
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="str"></param>
    /// <returns></returns>
    public static T ConvertTo<T>(this object str)
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

}
