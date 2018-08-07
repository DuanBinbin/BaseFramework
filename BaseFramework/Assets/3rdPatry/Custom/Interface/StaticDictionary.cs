
using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// 静态数据集，便于动态增减对象
/// </summary>
public class StaticCollection<TCollection>
{

    public static Dictionary<string, StaticCollection<TCollection>> caches = new Dictionary<string, StaticCollection<TCollection>>();

    private TCollection _Collection;


    public virtual TCollection Collection
    {
        get { return _Collection; }
    }

    public virtual void InitCollection(TCollection collection)
    {
        _Collection = collection;
    }

}

/// <summary>
/// 静态字典表
/// </summary>
public class StaticDictionary<TKey, TValue> : StaticCollection<Dictionary<TKey, TValue>>
{
    public TValue this[TKey key]
    {
        get { return Collection[key]; }
        set { Collection[key] = value; }
    }

    public TKey this[TValue value]
    {
        get { return GetKey(value); }
    }
    public TKey GetKey(TValue value)
    {
        return Collection.FirstOrDefault(x => object.Equals(x.Value, value)).Key;
    }

    public TValue GetDictionaryValue(TKey key)
    {
        var value = default(TValue);
        if (!Collection.TryGetValue(key, out value))
        {
            //Log
        }
        return value;
    }
}

/// <summary>
/// 扩展方法能检查是否为空
/// </summary>
public static class IStaticCollectionExtension
{
    /// <summary>
    /// 获取单类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TCollection"></typeparam>
    /// <param name="target"></param>
    /// <param name="collection"></param>
    /// <returns></returns>
    public static T GetOrCreateObject<T, TCollection>(this T target, TCollection collection) where T : StaticCollection<TCollection>, new()
    {
        //扩展方法可以检验对象是否为null
        if (target == null)
        {
            target = new T();
            target.InitCollection(collection);
        }
        return target;
    }

}