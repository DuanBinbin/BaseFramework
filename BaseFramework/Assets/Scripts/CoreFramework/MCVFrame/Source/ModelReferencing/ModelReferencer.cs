/********************************************************************
	created:	2016/12/19
	created:	19:12:2016   9:44
	file base:	ModelReferencer
	file ext:	cs
	author:		michael lee
	
	purpose:	ModelReferencer
*********************************************************************/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace CoreFramework.Internal {

    public abstract class ModelReferencer {

        public abstract void Delete();
        internal abstract List<Model> GetReferences();
        internal abstract void CollectReferences();

    }

}