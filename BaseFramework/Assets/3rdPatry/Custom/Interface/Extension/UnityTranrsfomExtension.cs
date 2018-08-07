using UnityEngine;
using System.Collections;
using System.Collections.Generic;
/// <summary>
/// Unity Transform的相关处理
/// </summary>
public static class UnityTransformExtension
{
    #region For

    /// <summary>
    /// 是否至少有一个父物体符合条件
    /// </summary>
    /// <param name="target"></param>
    /// <param name="func"></param>
    /// <returns></returns>
    public static bool IsAnyParent(this Transform target, System.Func<Transform, bool> func)
    {
        Transform tmpTF = target;

        if (func(tmpTF))
            return true;

        while (tmpTF.parent)
        {
            tmpTF = tmpTF.parent;
            if (func(tmpTF))
                return true;
        }
        return false;
    }

    /// <summary>
    /// 遍历处理自身和所有子层物体
    /// </summary>
    /// <param name="target"></param>
    /// <param name="func"></param>
    public static void ForSubFamily(this Transform target, System.Action<Transform> func)
    {
        func(target);
        foreach (Transform tfSon in target)
        {
            tfSon.ForSubFamily(func);
        }
    }

    /// <summary>
    /// 遍历所有子层物体，找到匹配的物体
    /// </summary>
    /// <param name="target"></param>
    /// <param name="name">物体名</param>
    /// <returns>找不到就返回null</returns>
    public static Transform FindFromSubFamily(this Transform target, string name)
    {
        Transform tfMatch = null;
        target.ForSubFamily
             (
             (tfChild) =>
             {
                 if (tfChild.name == name)
                 {
                     tfMatch = tfChild;
                 }
             }
             );
        return tfMatch;
    }
    #endregion

    #region Get

    public static T FindChild<T>(this Transform target, string name) where T : Component
    {
        Transform tfSon = target.FindChild(name);
        if (tfSon)
            return tfSon.GetComponent<T>();
        else
        {
            Debug.LogError("Not Child !");
            return null;
        }
    }

    public static List<T> GetComponentsInChildrenEx<T>(this Transform target) where T : Component
    {
        List<T> listT = new List<T>();
        foreach (Transform tfSon in target)
        {
            if (tfSon.GetComponent<T>() != null)
                listT.Add(tfSon.GetComponent<T>());
        }
        return listT;
    }


    #endregion

    #region Set

    public static void ForAllChild(this Transform target, System.Action<Transform> func)
    {
        foreach (Transform tsfSon in target)
        {
            func(tsfSon);
        }
    }

    /// <summary>
    /// 设置激活状态
    /// </summary>
    /// <param name="target"></param>
    /// <param name="isActive"></param>
    public static void SetActive(this Transform target, bool isActive)
    {
        target.gameObject.SetActive(isActive);
    }

    /// <summary>
    /// 设置所有子物体的激活状态
    /// </summary>
    /// <param name="target"></param>
    /// <param name="isActive"></param>
    public static void SetAllChildActive(this Transform target, bool isActive)
    {
        foreach (Transform tsfSon in target)
        {
            tsfSon.SetActive(isActive);
        }
    }

    #endregion

    #region Destroy

    /// <summary>
    /// 删除物体（注意：因为Destroy是由Unity管理，所以不是立即删除。如果在组中调用该函数删除旧物体后立即初始化并查找新物体，可以通过把旧物体移到其他父节点下再调用该函数）
    /// </summary>
    /// <param name="target"></param>
    public static void Destroy(this Transform target)
    {
        Object.Destroy(target.gameObject);
    }


    /// <summary>
    /// 删除所有子物体
    /// </summary>
    /// <param name="target"></param>
    public static void DestroyAllChild(this Transform target)
    {
        foreach (Transform tsfSon in target)
        {
            Destroy(tsfSon);
        }
    }

    #endregion

    #region Init


    /// <summary>
    /// 在一行中实例化并初始化物体
    /// </summary>
    public static Transform InitInLine(this Transform prefab, Transform parent, float localXPosition, string name = "")
    {
        return prefab.Init(parent, new Vector3(localXPosition, 0, 0), name);
    }

    ///// <summary>
    ///// 在二维中实例化并初始化物体
    ///// </summary>
    //public static Transform Init(this Transform prefab, Transform parent, Vector2 localPosition=default(Vector2), string name = "")
    //{
    //    return prefab.Init(parent, new Vector3(localPosition.x, localPosition.y, 0), name);
    //}

    /// <summary>
    /// 在三维中实例并初始化物体
    /// </summary>
    /// <returns>返回该物体的指定组件</returns>
    public static T Init<T>(this Transform prefab, Transform parent, Vector3 localPosition = default(Vector3), string name = "") where T : Component
    {
        Transform target = prefab.Init(parent, localPosition, name);
        return target.GetComponent<T>();
    }


    /// <summary>
    /// 实例化并初始化物体
    /// </summary>
    /// <param name="prefab">预制件</param>
    /// <param name="parent">父物体</param>
    /// <param name="localPosition">局部位置</param>
    /// <param name="name">名字(默认不修改它的名字）</param>
    /// <returns></returns>
    public static Transform Init(this Transform prefab, Transform parent, Vector3 localPosition = default(Vector3), string name = "")
    {
        Transform target = (Object.Instantiate(prefab.gameObject) as GameObject).transform;
        target.SetParent(parent, false);//UGUI设置
        //target.parent = parent;
        target.localPosition = localPosition;
        target.localScale = new Vector3(1, 1, 1);

        if (name != "")
        {
            target.name = name;
        }

        return target;
    }

    #endregion


    #region Tools

    #region 增加/删除 Component

    /// <summary>
    /// 添加Component
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="target"></param>
    /// <returns></returns>
    public static T AddComponent<T>(this Transform target) where T : Component
    {
        T comp = target.GetComponent<T>();
        if (comp == null)
            return target.gameObject.AddComponent<T>();
        else
            return comp;
    }

    /// <summary>
    /// 删除Component
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="target"></param>
    public static void RemoveComponent<T>(this Transform target) where T : Component
    {
        if (target.GetComponent<T>() != null)
            Object.DestroyImmediate(target.GetComponent<T>());
    }

    #endregion

    #endregion

}
