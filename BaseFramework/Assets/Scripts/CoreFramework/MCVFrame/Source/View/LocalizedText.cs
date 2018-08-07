using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using CoreFramework;

public class LocalizedText : Text {

    public string _LocalizedName = "";

	// Use this for initialization
	protected override void Awake(){

        base.Awake();

        this.text = LocalizationConfig.Instance.GetStringByName(_LocalizedName);

        if(_LocalizedName == "" || text == "")
        {
            this.text = "string not found";
        }

        //this.fontSize = 24;
        //this.color = Color.black;
        //this.alignment = TextAnchor.MiddleCenter;

        Message.AddListener<N_Broadcast_ChangeLanguage>(OnLanguageChanged);
    }


    protected override void OnDestroy()
    {
        base.OnDestroy();


        Message.RemoveListener<N_Broadcast_ChangeLanguage>(OnLanguageChanged);
    }

    public void SetLocalizationName(string name)
    {
        _LocalizedName = name;
        this.text = LocalizationConfig.Instance.GetStringByName(_LocalizedName);
    }


    void OnLanguageChanged(N_Broadcast_ChangeLanguage msg)
    {

        this.text = LocalizationConfig.Instance.GetStringByName(_LocalizedName);
    }

}
