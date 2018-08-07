/********************************************************************
	created:	2016/12/19
	created:	19:12:2016   15:16
	file base:	uiviewcollection
	file ext:	cs
	author:		michael lee
	
	purpose:	UIViewSwitchCollection
*********************************************************************/


using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
namespace CoreFramework
{
    public class UIViewSwitchCollection : UIViewCollection
    {

        ToggleGroup _ToggleGroup;
        public UIViewSwitchToggle[] _Toggles;

        protected override void Awake()
        {
            base.Awake();


        }


        protected override void Start()
        {
            base.Start();

            _ToggleGroup = gameObject.GetComponentInChildren<ToggleGroup>();

            int i = 0;
            foreach (UIViewSwitchToggle t in _Toggles)
            {
                if(t != null)
                {
                    t._ParentCollection = this;
                    t._View = _Collection[i];
                    i++;
                }
            }
        }
        public void DisableSwitch()
        {
            foreach (UIViewSwitchToggle t in _Toggles)
            {
                t.EnableEvent(false);
            }
        }
        public void EnableSwitch()
        {
            foreach (UIViewSwitchToggle t in _Toggles)
            {
                t.EnableEvent(true);
            }
        }

        public void ShowOneView<T>() where T : UIView
        {
            int i = 0;
            foreach(UIView view in _Collection)
            {
                if (view.gameObject.GetComponent<T>() != null)
                {
                    ShowOneView(view);

                    if(i < _Toggles.Length  - 1 && i >= 0)
                    {
                        _Toggles[i].isOn = true;

                    }

                    break;
                }
                i++;
            }
        }
    }
}