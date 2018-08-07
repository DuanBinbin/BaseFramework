/********************************************************************
	created:	2016/12/19
	created:	19:12:2016   9:31
	file base:	SerializeHelper
	file ext:	cs
	author:		michael lee
	
	purpose:	Serialize辅助类
*********************************************************************/

using UnityEngine;
using System.Collections;
using System.Xml.Serialization;
using System.Text;
using System.IO;
using System;

namespace CoreFramework.Internal {

    public static class SerializeHelper {

        public static string XmlSerializeToString(this object objectInstance) {
            var serializer = new XmlSerializer(objectInstance.GetType());
            var sb = new StringBuilder();

            using (TextWriter writer = new StringWriter(sb)) {
                serializer.Serialize(writer, objectInstance);
            }

            return sb.ToString();
        }

        public static T XmlDeserializeFromString<T>(this string objectData) {
            return (T)XmlDeserializeFromString(objectData, typeof(T));
        }

        public static object XmlDeserializeFromString(this string objectData, Type type) {
            try {
                var serializer = new XmlSerializer(type);
                object result;

                using (TextReader reader = new StringReader(objectData)) {
                    result = serializer.Deserialize(reader);
                }

                return result;
            } catch {
                return null;
            }
        }

    }

}