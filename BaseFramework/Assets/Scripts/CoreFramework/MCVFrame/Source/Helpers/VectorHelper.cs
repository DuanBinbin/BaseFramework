/********************************************************************
	created:	2016/12/19
	created:	19:12:2016   9:33
	file base:	VectorHelper
	file ext:	cs
	author:		michael lee
	
	purpose:	Vector向量辅助类
*********************************************************************/

using UnityEngine;
using System.Collections;

namespace CoreFramework.Internal {

    public static class VectorHelper {

        public static Vector2 GetPerpendicular(this Vector2 vector) {
            Vector3 v3 = new Vector3(vector.x, 0.0f, vector.y);
            Vector3 perp = Vector3.Cross(v3, Vector3.up);
            return new Vector2(perp.x, perp.z);
        }

    }

}