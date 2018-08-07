using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
/// <summary>
/// 查找并初始化该物体及子物体中所有单例
/// </summary>
public class InstanceManager : MonoBehaviour
{
    void Awake()
    {
        transform.ForSubFamily(
            target =>
            {
                if (target.GetComponent(typeof(IInstance)) != null)
                {
                    //一个物体可能有多个实例
                    target.GetComponents(typeof(IInstance)).ToList().ForEach((com) => (com as IInstance).SetInstance());
                }
            });
    }

}
