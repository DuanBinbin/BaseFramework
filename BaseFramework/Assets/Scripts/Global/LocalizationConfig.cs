using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LocalizationConfig : Singleton<LocalizationConfig> {

    XmlConfigReader _LocalizationReader;

    
    Dictionary<string, Dictionary<string,string>> ValueDic = new Dictionary<string, Dictionary<string, string>>();
    string _CurrentLanguage = "en";

    List<string> _Languages = new List<string>();

    public void Init(string path)
    {
        _CurrentLanguage = "en";

        GetSystemLanguage();

        _Languages.Add("en");
        _Languages.Add("zh");
        _Languages.Add("ko");
        _Languages.Add("es");
        _Languages.Add("fr");
        _Languages.Add("it");
        _Languages.Add("ja");
        _Languages.Add("de");
        _Languages.Add("pt");
        _Languages.Add("ru");
        _Languages.Add("zh-rTW");


        _LocalizationReader = new XmlConfigReader(XmlConfigReader.ConnectPath(path, "value"));

        foreach (string s in _LocalizationReader.GetMainKeys())
        {

            string key = _LocalizationReader.GetValue<string>(s, "key");
            string en = _LocalizationReader.GetValue<string>(s, "en");
            string zh = _LocalizationReader.GetValue<string>(s, "zh");
            string ko = _LocalizationReader.GetValue<string>(s, "ko");
            string es = _LocalizationReader.GetValue<string>(s, "es");
            string fr = _LocalizationReader.GetValue<string>(s, "fr");
            string it = _LocalizationReader.GetValue<string>(s, "it");
            string ja = _LocalizationReader.GetValue<string>(s, "ja");
            string de = _LocalizationReader.GetValue<string>(s, "de");
            string pt = _LocalizationReader.GetValue<string>(s, "pt");
            string ru = _LocalizationReader.GetValue<string>(s, "ru");
            string zhrTW = _LocalizationReader.GetValue<string>(s, "zh-rTW");


            Dictionary<string, string> dic = null;
            if (ValueDic.ContainsKey(key))
            {
            }
            else
            {
                ValueDic.Add(key,new Dictionary<string, string>());

                dic = ValueDic[key];



                dic.Add("en", en);
                dic.Add("zh", zh);
                dic.Add("ko", ko);
                dic.Add("es", es);
                dic.Add("fr", fr);
                dic.Add("it", it);
                dic.Add("ja", ja);
                dic.Add("de", de);
                dic.Add("pt", pt);
                dic.Add("ru", ru);
                dic.Add("zh-rTW", zhrTW);
            }
           
        }

        int v = GetLanguage();

        if (v > 100)
        {
            GetSystemLanguage();
        }
        else if(v < 0)
        {
            SetLanguage(999);
        }
        else
        {
            _CurrentLanguage = _Languages[v];
        }

        
    }

    public void SetLanguage(int v)
    {
        if (v > 100)
        {
            GetSystemLanguage();
        }
        else {
            _CurrentLanguage = _Languages[v];
        }
        
        
        PlayerPrefs.SetInt("CurrentLanguage", v);

        N_Broadcast_ChangeLanguage.Send<N_Broadcast_ChangeLanguage>();
    }

    public void GetSystemLanguage()
    {
        switch (Application.systemLanguage)
        {
            case SystemLanguage.ChineseSimplified:
                _CurrentLanguage = "zh";
                break;
            case SystemLanguage.ChineseTraditional:
                _CurrentLanguage = "zh-rTW";
                break;
            case SystemLanguage.Chinese:
                _CurrentLanguage = "zh";
                break;
            case SystemLanguage.Russian:
                _CurrentLanguage = "ru";
                break;
            case SystemLanguage.German:
                _CurrentLanguage = "de";
                break;
            case SystemLanguage.English:
                _CurrentLanguage = "en";
                break;
            case SystemLanguage.Spanish:
                _CurrentLanguage = "es";
                break;
            case SystemLanguage.French:
                _CurrentLanguage = "fr";
                break;
            case SystemLanguage.Italian:
                _CurrentLanguage = "it";
                break;
            case SystemLanguage.Japanese:
                _CurrentLanguage = "ja";
                break;
            case SystemLanguage.Korean:
                _CurrentLanguage = "ko";
                break;
            case SystemLanguage.Portuguese:
                _CurrentLanguage = "pt";
                break;
            case SystemLanguage.Polish: //波兰语
                _CurrentLanguage = "pl";
                break;
            case SystemLanguage.Dutch: //荷兰语
                _CurrentLanguage = "dt";
                break;
            case SystemLanguage.Thai: //泰语
                _CurrentLanguage = "ti";
                break;
            default:
                _CurrentLanguage = "en";
                break;
        }
    }
    public int GetLanguage()
    {
        //PlayerPrefs.SetInt("CurrentLanguage", 999);
        //PlayerPrefs.Save();

        // -1 declares the first start up app ;100 or 999 explains set currrent system language by excel
        int v = PlayerPrefs.GetInt("CurrentLanguage", -1);

        if (v < _Languages.Count && v >= 0)
        {
            _CurrentLanguage = _Languages[v];
        }
        if (v >= 100)
        {            
            return GetCurrentLanguageIndex();
        }
        if (v < 0)
        {
            return v;
        }
        else
        {
            for (int i = 0; i < _Languages.Count; i++)
            {
                if (_CurrentLanguage == _Languages[i])
                {
                    return i;
                }

            }

        }
        return 0;
    }

    public int GetCurrentLanguageIndex()
    {
        for(int i = 0;i< _Languages.Count;i++)
        {
            if (_CurrentLanguage == _Languages[i])
            {
                return i;
            }
        }
        return 0;
    }

    public string GetStringByName(string key)
    {
        key = key.Trim();
        string v;
        if (ValueDic.ContainsKey(key))
        {
            if(ValueDic[key].TryGetValue(_CurrentLanguage,out v))
            {
                return v.Replace(@"\n", "\n");
            }
        }
        return key;
    }

    public List<string> GetArrayByName(string s)
    {
        List<string> v = new List<string>();
       
        return v;
    }

}
