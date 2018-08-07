using CoreFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIViewSwitchToggle : Toggle {

    public UIView _View;
    public UIViewSwitchCollection _ParentCollection;


    //return true if go on
    public delegate bool OnToggleDelegate(bool b);
    public OnToggleDelegate _ToggleOverrideListener;

    protected override void Start()
    {
        base.Start();
        onValueChanged.AddListener(OnToggle);
    }
    protected override void OnDestroy()
    {
        base.OnDestroy();
        onValueChanged.RemoveAllListeners();

    }

    public void EnableEvent(bool b)
    {
        enabled = b;
    }
    

    public void OnToggle(bool b)
    {
        if(b)
        {
            if (_ToggleOverrideListener != null)
            {
                if (_ToggleOverrideListener(b))
                {

                    _ParentCollection.ShowOneView(_View);
                }

            }
            else
            {
                _ParentCollection.ShowOneView(_View);

            }
        }
    }


}
