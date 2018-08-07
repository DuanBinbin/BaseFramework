
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CoreFramework;

public class TestMessage : MonoBehaviour {

    public Button mSendMessage;

    #region Unity API

    private void Awake()
    {
        mSendMessage.onClick.AddListener(SendMessage);
    }

    private void Start()
    {

        Message.AddListener<N_Broadcast_ChangeLanguage>(ChangeLanguage);
    }

    private void OnDestroy()
    {
        Message.RemoveListener<N_Broadcast_ChangeLanguage>(ChangeLanguage);
        mSendMessage.onClick.RemoveListener(SendMessage);
    }

    #endregion


    private void ChangeLanguage(N_Broadcast_ChangeLanguage msg)
    {
        Debug.Log("The current language version is " + msg.language);
    }

    private void SendMessage()
    {
        N_Broadcast_ChangeLanguage msg = new N_Broadcast_ChangeLanguage();
        msg.language = 10;
        Message.Send(msg);
    }
}
