/********************************************************************
	created:	2016/12/19
	created:	19:12:2016   9:40
	file base:	ModelEntry
	file ext:	cs
	author:		michael lee
	
	purpose:	ModelEntry
*********************************************************************/

using UnityEngine;
using System.Collections;
using System;

namespace CoreFramework.Internal {

    [Serializable]
    public class ModelEntry {

        public string Id { get; private set; }
        public string Type { get; private set; }

        public ModelEntry() { }

        public ModelEntry(string id, Type type) {
            Id = id;
            Type = type.ToString();
        }

    }

}