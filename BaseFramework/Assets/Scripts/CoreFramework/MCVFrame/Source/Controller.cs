/********************************************************************
	created:	2016/12/19
	created:	19:12:2016   9:16
	file base:	Controller
	file ext:	cs
	author:		michael lee
	
	purpose:	Controller基类
*********************************************************************/
using UnityEngine;
using System.Collections;
using System;

namespace CoreFramework
{


    /// <summary>
    /// Controller抽象类,.
    /// </summary>
    public abstract class Controller : MonoBehaviour
    {
        protected ViewBase _View { get; private set; }
        protected virtual void Awake()
        {
            if(_View == null)
            {
                _View = gameObject.GetComponent<ViewBase>();

            }

        }
        protected virtual void Start()
        {

        }
        protected virtual void OnDestroy()
        {

        }
        protected virtual void OnEnable()
        {

        }
        protected virtual void OnDisable()
        {

        }


        public T ViewInstance<T>() where T : ViewBase
        {
            if(_View == null)
            {
                _View = gameObject.GetComponent<ViewBase>();
            }
            return (T)_View;
        }
    }
}