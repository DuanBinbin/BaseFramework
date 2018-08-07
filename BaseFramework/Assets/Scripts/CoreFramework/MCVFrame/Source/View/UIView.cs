using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace CoreFramework
{
    public class UIView : ViewBase
    {

        protected virtual void OnDestroy()
        {
            StopAllCoroutines();
        }
        #region Unit UI ControlSet
        public void SetLabel(Text label, string text)
        {
            if (label != null)
            {
                label.text = text;
            }
        }
        public void SetSprite(Image sprite, string name)
        {
            if (sprite != null)
            {
                sprite.name = name;
            }
        }

        protected T GetFirstComponentInChildren<T>() where T : MonoBehaviour
        {
            T[] arr = GetComponentsInChildren<T>(true);
            if (arr.Length > 0)
            {
                return arr[0];
            }
            return null;
        }

        /// <summary> Gets the component that specifies the child object </summary>
        protected T GetIndexCompoentInChildren<T>(int index) where T : MonoBehaviour
        {
            T[] arr = GetComponentsInChildren<T>(true);
            if (arr.Length >= 0)
            {
                return arr[index];
            }
            else
            {
                Debug.Log("It doesn't exist" + arr + "type components");
            }
            return null;
        }

        protected void SetTransformZero(GameObject go, GameObject parent)
        {
            go.transform.SetParent(parent.transform);
            go.transform.localPosition = Vector3.zero;
            go.transform.localRotation = Quaternion.identity;
            go.transform.localScale = Vector3.one;
        }

        protected void SetTransformToTarget(GameObject go, GameObject target, GameObject parent)
        {
            go.transform.SetParent(parent.transform);
            go.transform.localPosition = target.transform.localPosition;
            go.transform.localRotation = target.transform.localRotation;
            go.transform.localScale = target.transform.localScale;
        }
        #endregion


    }
}
