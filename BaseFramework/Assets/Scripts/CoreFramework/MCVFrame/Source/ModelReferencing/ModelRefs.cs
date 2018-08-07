/********************************************************************
	created:	2016/12/19
	created:	19:12:2016   9:44
	file base:	ModelRefs
	file ext:	cs
	author:		michael lee
	
	purpose:	ModelRefs
*********************************************************************/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System;
using System.Xml.Serialization;
using System.Xml;
using System.Xml.Schema;
using CoreFramework.Internal;

namespace CoreFramework {

    /// <summary>
    /// MODEL引用的集合
    /// </summary>
    [Serializable]
    public sealed class ModelRefs<T> : ModelReferencer, IEnumerable<T>, IXmlSerializable
        where T : Model {

        public T this[int i] {
            get { return models[i]; }
            set { models[i] = value; }
        }
        
        public int Count { get { return models.Count; } }
        
        public T Last { get { return models.Count == 0 ? null : models[models.Count - 1]; } }


        private List<T> models;
        private List<string> ids;

        public ModelRefs() {
            models = new List<T>();
        }
        
        public void Clear() {
            for (int i = models.Count - 1; i >= 0; i--) {
                models[i].RemoveDeleteListener(OnModelDelete);
                models[i].DecreaseRefCount();
            }
            models.Clear();
        }
        
        public bool Contains(T model) { return models.Contains(model); }
        
        public void Add(T model) {
            models.Add(model);
            model.IncreaseRefCount();
            model.AddDeleteListener(OnModelDelete);
        }
        
        public void Remove(T model) {
            model.RemoveDeleteListener(OnModelDelete);
            model.DecreaseRefCount();
            models.Remove(model);
        }
        
        public void RemoveAt(int index) {
            if (index >= models.Count) {
                Debug.LogError("Can't remove because '" + index + "' is out of index");
                return;
            }
            models[index].RemoveDeleteListener(OnModelDelete);
            models[index].DecreaseRefCount();
            models.RemoveAt(index);
        }
        public override void Delete() {
            Clear();
        }

        public IEnumerator<T> GetEnumerator() {
            for (int i = 0; i < models.Count; i++) {
                yield return models[i];
            }
        }

        public XmlSchema GetSchema() {
            return null;
        }

        public void ReadXml(XmlReader reader) {
            ids = new List<string>();

            reader.MoveToContent();
            if (reader.IsEmptyElement) { return; }

            reader.ReadStartElement();

            while (reader.NodeType != XmlNodeType.EndElement) {
                string id = reader.ReadInnerXml();
                ids.Add(id);
            }

            reader.ReadEndElement();
        }

        public void WriteXml(XmlWriter writer) {
            foreach (Model model in models) {
                writer.WriteElementString("Id", model.Id);
            }
        }

        internal override void CollectReferences() {
            Clear();
            if (ids == null) { return; }
            foreach (string id in ids) {
                T model = (T)Model.Find(id);
                if (model == null) {
                    Debug.LogError("Could not collect reference of '" + typeof(T) + "' with id '" + id + "'");
                    continue;
                }
                Add(model);
            }
        }

        internal override List<Model> GetReferences() {
            List<Model> references = new List<Model>();
            foreach (T model in models) {
                references.Add(model);
                references.AddList<Model>(model.GetReferences());
            }
            references.Distinct<Model>();
            return references;
        }

        private void OnModelDelete(Model model) {
            models.Remove((T)model);
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }
    }

}