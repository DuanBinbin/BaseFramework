using UnityEngine;
using System.Collections;


namespace CoreFramework
{
    public abstract class ViewBase : MonoBehaviour
    {
        protected Controller _ParentContrller;

        protected virtual void Awake()
        {
            FindParentController<Controller>();
        }

        protected virtual void Start()
        {
        }


        protected virtual void OnEnable()
        {
        }
        protected virtual void OnDisable()
        {
        }

        public virtual void Show()
        {
            gameObject.SetActive(true);
        }
        public virtual void Hide()
        {
            gameObject.SetActive(false);
        }

        protected T ControllerInstance<T>() where T : Controller
        {

            return FindParentController<T>();
        }

        T FindParentController<T>() where T : Controller
        {
            T c = _ParentContrller as T;
            if (c == null)
            {
                _ParentContrller = gameObject.GetComponent<T>();
                c = (T)_ParentContrller;
            }

            if (c == null)
            {
                _ParentContrller = (T)gameObject.GetComponentInParent<T>();
                c = (T)_ParentContrller;
            }

            //全局查询
            if (c == null)
            {
                _ParentContrller = gameObject.transform.root.GetComponentInChildren<T>(true);
                c = (T)_ParentContrller;
            }


            return c;
        }

    }
}

