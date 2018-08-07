using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraFilterConfig : Singleton<CameraFilterConfig>
{

    XmlConfigReader _InfoReader;


    Dictionary<string, string> _kvDic = new Dictionary<string, string>();
    List<string> _KeyList = new List<string>();

    public void Init(string path)
    {
        _kvDic.Clear();
        _KeyList.Clear();
        _InfoReader = new XmlConfigReader(XmlConfigReader.ConnectPath(path, "camerafilter"));

        foreach (string s in _InfoReader.GetMainKeys())
        {
            int id = _InfoReader.GetValue<int>(s, "id");
            string key = _InfoReader.GetValue<string>(s, "key");
            string value = _InfoReader.GetValue<string>(s, "value");
            string param = _InfoReader.GetValue<string>(s, "param");

            _kvDic.Add(key, value);
            _KeyList.Add(key);
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
