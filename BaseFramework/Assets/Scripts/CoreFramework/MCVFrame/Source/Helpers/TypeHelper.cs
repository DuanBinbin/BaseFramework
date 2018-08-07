/********************************************************************
	created:	2016/12/19
	created:	19:12:2016   9:32
	file base:	TypeHelper
	file ext:	cs
	author:		michael lee
	
	purpose:	类型反射辅助类
*********************************************************************/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Reflection;


namespace CoreFramework.Internal {

    public static class TypeHelper {

        private static Dictionary<string, Type> types = new Dictionary<string,Type>();

        public static Type GetGlobalType(string typeName) {
            if (types.ContainsKey(typeName)) { return types[typeName]; }
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (Assembly assembly in assemblies) {
                Type type = Type.GetType(typeName + "," + assembly.GetName());
                if (type != null) {
                    types.Add(typeName, type);
                    return type;
                }
            }
            return null;
        }

    }

}