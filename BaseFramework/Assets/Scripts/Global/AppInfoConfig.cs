using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AppInfoConfig : Singleton<AppInfoConfig>
{

    XmlConfigReader _AppInfoReader;


    Dictionary<string, string> _kvDic = new Dictionary<string, string>();
    List<string> _KeyList = new List<string>();

    public void Init(string path)
    {
        _kvDic.Clear();
        _KeyList.Clear();
        _AppInfoReader = new XmlConfigReader(XmlConfigReader.ConnectPath(path, "appinfo"));

        foreach (string s in _AppInfoReader.GetMainKeys())
        {
            int id = _AppInfoReader.GetValue<int>(s, "id");
            string key = _AppInfoReader.GetValue<string>(s, "key");
            string value = _AppInfoReader.GetValue<string>(s, "value");

            if(!_kvDic.ContainsKey(key))
            {
                _kvDic.Add(key, value);
                _KeyList.Add(key);

            }
        }

    }

    public string GetValueByKey(string s)
    {
        string v;
        if (_kvDic.TryGetValue(s, out v))
        {
            return v;
        }
        return s;
    }

    public int GetItemCount()
    {
        return _KeyList.Count;
    }
    public List<string> GetKeyList()
    {
        return _KeyList;
    }
}
