
/********************************************************************
	created:	2016/12/19
	created:	19:12:2016   9:34
	file base:	ModelBlobs
	file ext:	cs
	author:		michael lee
	
	purpose:	Model XML序列化存储模块
*********************************************************************/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using CoreFramework.Internal;
using System;

namespace CoreFramework {

    /// <summary>
    /// 带ID的XML的字符串字典，存储MODEL的数据集合
    /// </summary>
    public class ModelBlobs : Dictionary<string, string> {

        /// <summary>
        /// 字符串转 ModelBlobs  '~' char 为分隔符
        /// </summary>
        /// <param name="data">字符串 被转为ModelBlobs</param>
        /// <returns>ModelBlobs</returns>
        public static ModelBlobs FromString(string data) {
            return FromString(data, '~');
        }

        /// <summary>
        /// 字符串转 ModelBlobs
        /// </summary>
        /// <param name="data">字符串 被转为ModelBlobs</param>
        /// <param name="seperator">分隔符</param>
        /// <returns>A ModelBlobs</returns>
        public static ModelBlobs FromString(string data, char seperator) {
            if (string.IsNullOrEmpty(data)) {
                Debug.LogError("Can't split string into ModelBlobs if the given string is empty or null!");
                return null;
            }
            return FromStringArray(data.Split(seperator));
        }

        /// <summary>
        /// 字符串转 ModelBlobs
        /// </summary>
        /// <param name="data">字符串 被转为ModelBlobs</param>
        /// <param name="seperator">分隔符</param>
        /// <returns>A ModelBlobs</returns>
        public static ModelBlobs FromString(string data, string seperator) {
            if (string.IsNullOrEmpty(data)) {
                Debug.LogError("Can't split string into ModelBlobs if the given string is empty or null!");
                return null;
            }
            if (string.IsNullOrEmpty(seperator)) {
                Debug.LogError("Can't split string into ModelBlobs if the given seperator is empty or null!");
                return null;
            }
            return FromStringArray(Regex.Split(data, seperator));
        }

        /// <summary>
        /// ModelBlobs转string, '~' char为分隔符
        /// </summary>
        /// <returns>string</returns>
        public override string ToString() {
            return ToString('~');
        }

        /// <summary>
        /// ModelBlobs转string
        /// </summary>
        /// <param name="seperator">分隔符</param>
        /// <returns>string</returns>
        public string ToString(char seperator) {
            return ToString(seperator.ToString());
        }

        /// <summary>
        ///  ModelBlobs转string
        /// </summary>
        /// <param name="seperator">分隔符</param>
        /// <returns>string</returns>
        public string ToString(string seperator) {
            if (string.IsNullOrEmpty(seperator)) {
                Debug.LogError("Can't join ModelBlobs if the given seperator is empty or null!");
                return null;
            }
            if (this.Count == 0) {
                Debug.LogError("Can't join ModelBlobs because there are none!");
                return null;
            }
            List<string> dataList = new List<string>();
            foreach (KeyValuePair<string, string> pair in this) {
                dataList.Add(pair.Key);
                dataList.Add(pair.Value);
            }
            string[] dataArray = dataList.ToArray();
            return string.Join(seperator, dataArray);
        }

        private static ModelBlobs FromStringArray(string[] splittedData) {
            ModelBlobs modelBlobs = new ModelBlobs();
            string id = null;
            foreach (string blob in splittedData) {
                if (string.IsNullOrEmpty(id)) {
                    id = blob;
                } else {
                    modelBlobs.Add(id, blob);
                    id = null;
                }
            }
            return modelBlobs;
        }

    }

}