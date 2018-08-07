using UnityEngine;
using System.Collections;
using System.Text;
using System.Collections.Generic;

/// <summary>
/// Filename: WWWHelper.cs
/// Description: Manage http request
/// Author: Star
/// Date: [12/12/10]
/// </summary>
/// 
[ExecuteInEditMode]
public class HttpHelper : MonoBehaviour
{
    public delegate void OnHttpResponseDelegate(string rsp);
    static event OnHttpResponseDelegate _OnRspEvent;
    

    private WWW m_www;
    private bool m_bIsBeginRequest = false;
    private bool m_bIsDone = true;

    public bool IsBeginRequest
    {
        get { return m_bIsBeginRequest; }
        set { m_bIsBeginRequest = value; }
    }

    public bool IsDone
    {
        get { return m_bIsDone; }
        set { m_bIsDone = value; }
    }

    public static HttpHelper Instance
    {
        get
        {
            GameObject go = new GameObject();
            DontDestroyOnLoad(go);

            HttpHelper component = go.AddComponent<HttpHelper>();
            return component;
        }
    }


    public void GET(string url, OnHttpResponseDelegate cb)
    {

        ATrace.Log(url);

#if UNITY_EDITOR
        Debug.Log(url);
#endif


        if (m_bIsDone)
        {
            _OnRspEvent = null;
            _OnRspEvent += cb;
            this.m_www = new WWW(url);
            m_bIsBeginRequest = true;
            m_bIsDone = false;
        }
    }

    public void POST(string url, WWWForm form , OnHttpResponseDelegate cb)
    {
        Debug.Log(url);
        if (m_bIsDone)
        {
            _OnRspEvent = null;
            _OnRspEvent += cb;

       

            this.m_www = new WWW(url,form);
            m_bIsBeginRequest = true;
            m_bIsDone = false;
        }
    }

    // Update is called once per frame
    [ExecuteInEditMode]
    void Update()
    {
        if (m_bIsBeginRequest)
        {
            if (this.m_www.isDone)
            {
                if (null != _OnRspEvent)
                {
                    _OnRspEvent(m_www.text);
                }
                m_bIsDone = true;
                m_bIsBeginRequest = false;

                GameObject.Destroy(gameObject);
            }
        }
    }

    public static void ClearRspEvent()
    {
        _OnRspEvent = null;
    }


    #region Command

    string _PhpProjectName = "";

    string GetPlatform()
    {
#if UNITY_IOS
        return "iOS";
#elif UNITY_ANDROID
        return "Android";
#endif
        return null;
    }

    string GetFullUrl(string func, string[] argnamearr, string[] argsarr)
    {
        string url = AssetBundlePlatformPathManager._Server + _PhpProjectName + "/app/index.php/Home/ProductVerify/" + func + "/";

        for (int i = 0; i < argnamearr.Length; i++)
        {
            string argname = argnamearr[i];
            string argvalue = argsarr[i];

            byte[] utf8Bytes = Encoding.UTF8.GetBytes(argvalue);
            string vutf8 = Encoding.UTF8.GetString(utf8Bytes);


            vutf8 = WWW.EscapeURL(argvalue);

            url += argname + "/" + vutf8;
            url += "/";
        }
        url = url.Substring(0, url.Length - 1);

        ATrace.Log("Http Command :" + url);
        return url;
    }
    public void GetLatestVersion( OnHttpResponseDelegate cb)
    {
        string url = "";



#if UNITY_ANDROID || UNITY_EDITOR

#if OVERSEAS
        url = AppInfoConfig.Instance.GetValueByKey("SERVER_VERSION_F_URL_ANDROID");
#else
        url = AppInfoConfig.Instance.GetValueByKey("SERVER_VERSION_URL_ANDROID");
#endif
#elif UNITY_IOS
#if OVERSEAS
        url = AppInfoConfig.Instance.GetValueByKey("SERVER_VERSION_F_URL_IOS");
#else
        url = AppInfoConfig.Instance.GetValueByKey("SERVER_VERSION_URL_IOS");
#endif
#endif

        GET(url, cb);
    }
#endregion
}