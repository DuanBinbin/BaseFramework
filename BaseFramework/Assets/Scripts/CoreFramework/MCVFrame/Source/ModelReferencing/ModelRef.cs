/********************************************************************
	created:	2016/12/19
	created:	19:12:2016   9:41
	file base:	ModelRef
	file ext:	cs
	author:		michael lee
	
	purpose:	Model的引用结构
*********************************************************************/

using UnityEngine;
using System.Collections;
using System;
using System.Xml.Serialization;
using System.Xml;
using System.Xml.Schema;
using System.Collections.Generic;
using CoreFramework.Internal;

namespace CoreFramework {

    /// <summary>
    /// Model的引用结构
    /// </summary>
    /// <typeparam name="T">The model类型</typeparam>
    [Serializable]
    public sealed class ModelRef<T> : ModelReferencer, IXmlSerializable
        where T : Model {

        /// <summary>
        /// Model的引用结构. 修改会重新删除，注册MODEL回调.
        /// </summary>
        public T Model {
            get {
                return model;
            }
            set {
                if (model == value) { return; }
                if (model != null) {
                    model.RemoveDeleteListener(OnModelDelete);
                    model.DecreaseRefCount();
                }
                model = value;
                if (model != null) {
                    model.IncreaseRefCount();
                    model.AddDeleteListener(OnModelDelete);
                }
            }
        }

        private T model;
        private string id;

        public ModelRef() { }

        /// <summary>
        /// 新建构造
        /// </summary>
        /// <param name="model">被引用的Model</param>
        public ModelRef(T model) {
            Model = model;
        }

        /// <summary>
        /// 删除实例
        /// </summary>
        public override void Delete() {
            Model = null;
        }

        public XmlSchema GetSchema() {
            return null;
        }

        public void ReadXml(XmlReader reader) {
            if (reader.IsEmptyElement) { return; }
            reader.Read();
            id = reader.ReadElementString("Id");
        }

        public void WriteXml(XmlWriter writer) {
            if (model != null) {
                writer.WriteElementString("Id", model.Id);
            }
        }

        internal override void CollectReferences() {
            if (string.IsNullOrEmpty(id)) { return; }
            Model = ModelProxy.Find(id) as T;
        }

        internal override List<Model> GetReferences() {
            if (model != null) {
                List<Model> references = model.GetReferences();
                if (!references.Contains(model)) {
                    references.Add(model);
                }
                return references;
            } else {
                return new List<Model>();
            }
        }

        private void OnModelDelete() {
            model = null;
        }

    }

}