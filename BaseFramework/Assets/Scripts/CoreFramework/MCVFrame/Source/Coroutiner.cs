/********************************************************************
	created:	2016/12/19
	created:	19:12:2016   9:49
	file base:	Coroutiner
	file ext:	cs
	author:		michael lee
	
	purpose:	协程辅助类
*********************************************************************/

using UnityEngine;
using System.Collections;

namespace CoreFramework.Internal {

    public class Coroutiner : MonoBehaviour {

        private static Coroutiner instance;

        public static void Start(IEnumerator routine) {
            GetInstance().StartLocalCoroutine(routine);
        }

        public void StartLocalCoroutine(IEnumerator routine) {
            StartCoroutine(routine);
        }

        private static Coroutiner GetInstance() {
            if (instance == null) {
                GameObject go = new GameObject("Coroutiner");
                instance = go.AddComponent<Coroutiner>();
            }
            return instance;
        }

    }

}