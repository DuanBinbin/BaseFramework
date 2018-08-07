using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class HashMap<TKey,TValue> {

    List<TKey> _KeyList = new List<TKey>();
    List<TValue> _ValueList = new List<TValue>();
    Dictionary<TKey, TValue> _Dic = new Dictionary<TKey, TValue>();
    Dictionary<TValue, TKey> _DicReverse = new Dictionary<TValue, TKey>();

    public void Add(TKey key ,TValue value)
    {
        if(!_KeyList.Contains(key))
        {

            _KeyList.Add(key);
            _ValueList.Add(value);
            _Dic.Add(key, value);
            _DicReverse.Add(value, key);
        }

    }

    public bool ContainsKey(TKey key)
    {
        return _KeyList.Contains(key);
    }

    public TKey GetKeyFromValue(TValue value)
    {
        if (value.Equals(null))
        {
            Debug.LogFormat("HashMap GetKeyFromVal() value null");
            return default(TKey);
        }
        if (_DicReverse.ContainsKey(value))
        {
            return _DicReverse[value];
        }
     
        return default(TKey);
    }

    public TValue GetValueFromKey(TKey key)
    {
        if (_Dic.ContainsKey(key))
        {
            return _Dic[key];
        }
        return default(TValue);
    }

    public void RemoveValue(TValue value)
    {
       

        TKey key = GetKeyFromValue(value);

        if (_Dic.ContainsKey(key))
        {
            _Dic.Remove(key);
        }

        if (_DicReverse.ContainsKey(value))
        {
            _DicReverse.Remove(value);
        }

        if (_KeyList.Contains(key))
        {
            _KeyList.Remove(key);
        }
        if (_ValueList.Contains(value))
        {
            _ValueList.Remove(value);
        }
    }

    public void RemoveKey(TKey key)
    {
        TValue value = GetValueFromKey(key);

        if (_Dic.ContainsKey(key))
        {
            _Dic.Remove(key);
        }
        if (_DicReverse.ContainsKey(value))
        {
            _DicReverse.Remove(value);
        }

        if (_KeyList.Contains(key))
        {
            _KeyList.Remove(key);
        }
        if (_ValueList.Contains(value))
        {
            _ValueList.Remove(value);
        }
    }

    public TKey GetKeyFromKeyIndex(int i)
    {
        if (i < _KeyList.Count && i >= 0)
        {
            TKey key = _KeyList[i];
            return key;

        }
        return default(TKey);
    }
    public TValue GetValueFromKeyIndex(int i)
    {
        if (i < _KeyList.Count && i >= 0)
        {
            TKey key = _KeyList[i];
            return GetValueFromKey(key);

        }
        return default(TValue);
    }

      public TKey GetKeyFromValueIndex(int i)
    {
        if(i < _ValueList.Count && i >= 0)
        {
            TValue value = _ValueList[i];
            return GetKeyFromValue(value);

        }
        return default(TKey);
    }
    public TValue GetValueFromValueIndex(int i)
    {
        if(i < _ValueList.Count && i >= 0)
        {
            TValue value = _ValueList[i];
            return value;

        }
        return default(TValue);
    }

    public int GetCount()
    {
        return _KeyList.Count;
    }

    public void Clear()
    {
        _KeyList.Clear();
        _ValueList.Clear();
        _Dic.Clear();
        _DicReverse.Clear();
    }
    
    public void SortByKey(Comparison<TKey> comparison)
    {
        _KeyList.Sort(comparison);
    }
    public void SortByValue(Comparison<TValue> comparison)
    {

        _ValueList.Sort(comparison);
    }

    public void ReverseKey()
    {
        _KeyList.Reverse();
    }
    public void ReverseValue()
    {
        _ValueList.Reverse();
    }
}
