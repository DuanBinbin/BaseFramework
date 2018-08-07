/********************************************************************
	created:	2016/12/19
	created:	19:12:2016   15:16
	file base:	uiviewcollection
	file ext:	cs
	author:		michael lee
	
	purpose:	UIViewCollection
*********************************************************************/


using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

namespace CoreFramework
{
    public class UIViewCollection : UIView
    {

        public List<UIView> _Collection = new List<UIView>();
        public UIView _FirstView;
        protected UIView _CurrentView;

        protected override void Start()
        {
            foreach (UIView view in _Collection)
            {
                view.gameObject.SetActive(false);
            }


            if (_FirstView == null)
            {
                foreach (UIView view in _Collection)
                {
                    if (_FirstView == null)
                    {
                        _FirstView = view;
                        break;
                    }
                }

            }
          

            ShowOneView(_FirstView);
        }
        protected virtual void OnDestroy()
        {
            _Collection.Clear();
        }

        public bool AddView(UIView view)
        {
            if (!_Collection.Contains(view))
            {
                _Collection.Add(view);

                return true;
            }
            return false;
        }


        public void DelView(UIView view)
        {
            if (_Collection.Contains(view))
            {
                _Collection.Remove(view);
            }
        }
        public void ShowOneView(UIView view)
        {
            if (_Collection.Contains(view))
            {
                UIView willenable = null;
                foreach (UIView v in _Collection)
                {
                    if (view == v)
                    {
                        willenable = v;
                    }
                    else
                    {
                        if(v.gameObject.activeSelf)
                        {
                            v.gameObject.SetActive(false);

                        }
                    }
                }

                //先全设置为FALSE再设true,保证DISABLE 在ONENABLE之前执行
                if(willenable != null)
                {

                    TimerManager.Instance.StartCoroutine(NextFrameDoEnable(willenable));
                }
            }
        }


        IEnumerator NextFrameDoEnable(UIView willenable)
        {
            yield return new WaitForEndOfFrame();
            _CurrentView = willenable;
            willenable.gameObject.SetActive(true);
        }

        public UIView GetCurrentView()
        {
            return _CurrentView;
        }
    }
}