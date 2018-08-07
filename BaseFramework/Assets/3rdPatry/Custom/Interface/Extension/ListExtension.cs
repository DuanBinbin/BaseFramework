using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

//namespace CustomExtension
//{
    public static class ListExtension
    {
        public static T GetData<T>(this List<T> listData, Predicate<T> match) where T : class, new()
        {
            T data = listData.Find(match);
            if (data == default(T))
            {
                Debug.LogError("找不到 " + typeof(T).ToString() + " 对应的数据");
                data = new T();
            }

            return data;
        }

        /// <summary>
        /// 输出全部数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="listData"></param>
        /// <returns></returns>
        public static string GetEachString<T>(this IEnumerable<T> listData, string spaceContent = "")
        {
            string content = "";
            foreach (var data in listData)
            {
                content += data.ToString() + spaceContent;
            }
            return content;
        }

        /// <summary>
        /// 获取值，越界循环
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="listData"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static T GetLoop<T>(this List<T> listData, int index) where T : class
        {
            T nextData = null;
            if (listData == null)
            {
                Debug.LogError(listData + " is Null!");
            }
            else
            {
                int sum = listData.Count;
                if (sum > 0)
                {
                    //越界值：循环.可以用%代替， 加：i/sum ; 减：(i+sum)%sum）
                    if (index < 0)
                        index = sum - 1;
                    else if (index >= sum)
                        index = 0;

                    nextData = listData[index];
                }
            }
            return nextData;
        }


        /// <summary>
        /// 获取累加值，越界循环
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="listData"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static T GetLoop<T>(this List<T> listData, int curIndex, int delta) where T : class
        {
            T nextData = null;
            int sum = listData.Count;
            if (sum > 0)
            {
                //错误值
                if (curIndex < 0)
                {
                    Debug.LogError(curIndex + " is smaller than zero!");
                    return nextData;
                }
                nextData = GetLoop(listData, curIndex + delta);
            }
            else
            {
                Debug.LogError(listData + " is Null!");
            }
            return nextData;
        }
    }
//}