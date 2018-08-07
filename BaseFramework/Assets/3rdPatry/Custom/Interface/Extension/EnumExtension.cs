using UnityEngine;
using System.Reflection;
using System.Collections.Generic;
using System;
/// <summary>
/// Enum的扩展方法
/// </summary>
public static class EnumExtension
{
    /// <summary>
    /// 获取枚举的描述
    /// </summary>
    public static RemarkAttribute GetRemark(this System.Enum enumData)
    {
        return GetAttribute<RemarkAttribute>(enumData);
    }

    public static bool HasRemark(this System.Enum enumData)
    {
        return GetAttribute<RemarkAttribute>(enumData) != null;
    }

    /// <summary>
    /// 获取枚举的描述
    /// </summary>
    public static string GetRemarkInfo(this System.Enum enumData)
    {
        string info = "";
        RemarkAttribute remark = GetAttribute<RemarkAttribute>(enumData);
        if (remark == null)
            Debug.LogWarning(enumData.GetType() + "." + enumData.ToString() + " Doesn't Have Remark !");
        else
            info = remark.Info;
        return info;
    }

    public static Type GetRemarkType(this System.Enum enumData)
    {
        Type type = typeof(string);
        RemarkAttribute remark = GetAttribute<RemarkAttribute>(enumData);
        if (remark == null)
            Debug.LogWarning(enumData.GetType() + "." + enumData.ToString() + " Doesn't Have Remark !");
        else
            type = remark.Type;
        return type;
    }

    public static T GetRemarkData<T>(this System.Enum enumData)
    {
        T data = default(T);
        RemarkAttribute remark = GetAttribute<RemarkAttribute>(enumData);
        if (remark == null)
            Debug.LogWarning(enumData.GetType() + "." + enumData.ToString() + " Doesn't Have Remark !");
        else
            data = remark.Data.ConvertTo<T>();
        return data;
    }
    public static T GetAttribute<T>(this object enumVal) where T : System.Attribute
    {
        var type = enumVal.GetType();
        var memInfos = type.GetMember(enumVal.ToString());
        var attributes = memInfos[0].GetCustomAttributes(typeof(T), false);
        return (attributes.Length > 0) ? (T)attributes[0] : null;
    }

}

/// <summary>
/// 枚举的说明
/// </summary>
[System.AttributeUsage(System.AttributeTargets.Enum | System.AttributeTargets.Field)]
public class RemarkAttribute : System.Attribute
{
    public RemarkAttribute(string info)
    {
        Info = info;
        Type = typeof(string);
    }

    public RemarkAttribute(string remark, Type type)
    {
        Info = remark;
        Type = type;
    }

    public RemarkAttribute(string remark, object data)
    {
        Info = remark;
        Data = data;
    }

    public RemarkAttribute(string info, string description)
    {
        Info = info;
        Description = description;
    }
    /// <summary>
    /// 信息
    /// </summary>
    public string Info { get; private set; }

    /// <summary>
    /// 描述
    /// </summary>
    public string Description { get; private set; }

    /// <summary>
    /// Info的类型，默认是string
    /// </summary>
    public Type Type { get; private set; }

    /// <summary>
    /// 通用类型对象，如Enum
    /// </summary>
    public object Data { get; private set; }

}

