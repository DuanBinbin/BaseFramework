/********************************************************************
	created:	2016/12/19
	created:	19:12:2016   9:31
	file base:	ListHelper
	file ext:	cs
	author:		michael lee
	
	purpose:	List辅助类
*********************************************************************/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace CoreFramework.Internal {

    public static class ListHelper {

        public static void Distinct<T>(this List<T> list) {
            for (int i = list.Count - 1; i >= 0; i--) {
                T item = list[i];
                list.RemoveAll(x => EqualityComparer<T>.Default.Equals(x, item));
                list.Add(item);
                i = Mathf.Min(i, list.Count);
            }
        }

        public static void AddList<T>(this List<T> original, List<T> addition) {
            foreach (T item in addition) {
                original.Add(item);
            }
        }

    }

}