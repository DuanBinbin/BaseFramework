using UnityEngine;
using System.Collections.Generic;
using System;
/// <summary>
/// Delegate 的扩展方法,当委托不为空时执行  (就算为空也可以调用扩展）
/// </summary>
public static class ActionExtension
{
    /// <summary>
    /// 执行委托
    /// </summary>
    /// <param name="function"></param>
    public static void Excute(this Action function)
    {
        if (function != null)
            function();
    }

    /// <summary>
    /// 执行一次然后清空
    /// </summary>
    /// <param name="function"></param>
    public static void ExcuteOnce(this Action function)
    {
        if (function != null)
            function();

        function = null;
    }

    /// <summary>
    /// 执行委托
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="function"></param>
    /// <param name="data"></param>
    public static void Excute<T>(this Action<T> function, T data)
    {
        if (function != null)
            function(data);
    }

    public static void Excute<T1, T2>(this Action<T1, T2> function, T1 data1, T2 data2)
    {
        if (function != null)
            function(data1, data2);
    }

    /// <summary>
    /// 执行一次然后清空
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="function"></param>
    /// <param name="data"></param>
    public static void ExcuteOnce<T>(this Action<T> function, T data)
    {
        if (function != null)
            function(data);

        function = null;
    }
}
