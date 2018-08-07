using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestFilesConfig : Singleton<TestFilesConfig>
{ 

    XmlConfigReader _TestFilesReader; 

    Dictionary<string  , string> _kvDic = new Dictionary<string  , string>();
    public static int _DicCount;

    public void Init(string path)
    {
        _kvDic.Clear();
        _TestFilesReader  = new XmlConfigReader(path,true);
        foreach (string s in _TestFilesReader.GetMainKeys())
        {
            int id = _TestFilesReader.GetValue<int>(s, "id");
            string key = _TestFilesReader.GetValue<string >(s, "key");
            string value = _TestFilesReader.GetValue<string>(s, "value");
            if (!_kvDic.ContainsKey(key))
            {
                _kvDic.Add(key, value);
            }
        }
        _DicCount = _kvDic.Count;

    }


    public string  GetValueByKey(string  s)
    {
        string v;
        if (_kvDic.TryGetValue(s , out v))
        {
            return v;
        }
        return v;
       
    }


}
