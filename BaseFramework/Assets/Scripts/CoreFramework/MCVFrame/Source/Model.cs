/********************************************************************
	created:	2016/12/19
	created:	19:12:2016   9:58
	file base:	Model
	file ext:	cs
	author:		michael lee
	
	purpose:	Model基类
*********************************************************************/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Xml.Serialization;
using CoreFramework.Internal;
using System.Diagnostics;

namespace CoreFramework {
    
    /// <summary>Model用于存储数据，被CONTROLLER控制</summary>
    [Serializable]
    public abstract class Model : ModelReferencer {

        /// <summary>
        /// NotifyChange调用时调用. 仅工作于Editor.
        /// </summary>
        /// <param name="affecterType">调用NotifyChange的类型</param>
        /// <param name="modelType">Model类型</param>
        public delegate void OnModelAffectDelegate(Type affecterType, Type modelType);

        /// <summary>仅工作于Editor.</summary>
        public static OnModelAffectDelegate OnModelChangeNotified;

        /// <summary>仅工作于Editor.</summary>
        public static OnModelAffectDelegate OnModelChangeHandled;

        /// <summary>仅工作于Editor.</summary>
        public static OnModelAffectDelegate OnModelDeleted;

        /// <summary>仅工作于Editor.</summary>
        public static OnModelAffectDelegate OnModelDeleteHandled;

        /// <summary>
        /// Model ID
        /// </summary>
        public string Id {
            get {
                return id;
            }
            private set {
                Unregister();
                id = value;
                Register();
            }
        }

        private static Dictionary<string, Model> sortedInstances = new Dictionary<string, Model>();
        private static Dictionary<Type, List<Model>> typeSortedInstances = new Dictionary<Type, List<Model>>();
        private static List<Model> instances = new List<Model>();

        private static bool isSerializing;

        private string id;
        private bool isRegistered;
        private bool referencesCollected;
        private int refCount;
        private List<Delegate> onChangeHandlers;
        private List<Delegate> onDeleteHandlers;

        /// <summary>
        /// 保存所有数据
        /// </summary>
        /// <returns>ModelBlobs</returns>
        public static ModelBlobs SaveAll() {
            return Save(instances, false);
        }

        /// <summary>
        /// 保存指定Model数据及其中涉及的引用Model
        /// </summary>
        /// <param name="rootModel">指定的Model</param>
        /// <returns>ModelBlobs</returns>
        public static ModelBlobs Save(Model rootModel) {
            if (rootModel == null) {
                UnityEngine.Debug.LogError("Can't save Model if the given value is null!");
                return null;
            }
            List<Model> models = new List<Model>();
            if (rootModel != null) {
                models.Add(rootModel);
                models.AddList<Model>(rootModel.GetReferences());
            }
            return Save(models, false);
        }

        /// <summary>
        /// 保存指定Model List数据及其中涉及的引用Model
        /// </summary>
        /// <param name="models">Model 列表</param>
        /// <param name="saveReferenced">是否保存Model中的引用</param>
        /// <returns>modelBlobs</returns>
        public static ModelBlobs Save(List<Model> models, bool saveReferenced) {
            if (models == null) {
                UnityEngine.Debug.LogError("Can't save Models if the given list is null");
                return null;
            }
            models.Distinct<Model>();
            if (saveReferenced) {
                foreach (Model model in models) {
                    models.AddList<Model>(model.GetReferences());
                }
            }
            models.Distinct<Model>();
            ModelBlobs modelBlobs = new ModelBlobs();
            modelBlobs.Add("manifest", Manifest.Save(models));
            isSerializing = true;
            foreach (Model model in models) {
                modelBlobs.Add(model.Id, model.XmlSerializeToString());
            }
            isSerializing = false;
            return modelBlobs;
        }

        /// <summary>
        /// 保存所有Model数据到单独的Xml文件
        /// </summary>
        /// <param name="dir">目标目录</param>
        public static void SaveAll(string dir) {
            Save(dir, instances, false);
        }

        /// <summary>
        /// 保存指定MODEL到XML
        /// </summary>
        /// <param name="dir">目标目录</param>
        /// <param name="model">指定MODEL</param>
        public static void Save(string dir, Model model) {
            if (model == null) {
                UnityEngine.Debug.LogError("Can't save Model if the given value is null!");
                return;
            }
            List<Model> models = model.GetReferences();
            models.Add(model);
            Save(dir, models, false);
        }

        /// <summary>
        /// 保存指定Model列表到单独XML
        /// </summary>
        /// <param name="dir">目标目录</param>
        /// <param name="models">Model列表</param>
        /// <param name="saveReferenced">是否保存其中引用</param>
        public static void Save(string dir, List<Model> models, bool saveReferenced) {
            if (string.IsNullOrEmpty(dir)) {
                UnityEngine.Debug.LogError("Can't save Models if the given directory name is empty!");
                return;
            }
            if (models == null) {
                UnityEngine.Debug.LogError("Can't save Models if the given list is null");
                return;
            }
            models.Distinct<Model>();
            if (saveReferenced) {
                foreach (Model model in models) {
                    models.AddList<Model>(model.GetReferences());
                }
            }
            models.Distinct<Model>();
            Manifest.Save(dir, models);
            isSerializing = true;
            foreach (Model model in models) {
                var serializer = new XmlSerializer(model.GetType());
                var stream = new FileStream(dir + "/" + model.Id + ".xml", FileMode.Create);
                serializer.Serialize(stream, model);
                stream.Close();
            }
            isSerializing = false;
        }

        /// <summary>
        /// 从ModelBlobs中读取数据
        /// </summary>
        /// <param name="data">要读取的ModelBlobs</param>
        /// <param name="onStart">开始读取回调</param>
        /// <param name="onProgress">读取进度回调 0.0f - 1.0f.</param>
        /// <param name="onDone">读取结束回调</param>
        /// <param name="onError">读取错误回调</param>
        public static void Load(ModelBlobs data, Action onStart, Action<float> onProgress, Action onDone, Action<string> onError) {
            Load<Model>(data, onStart, onProgress, delegate(Model model) { if (onDone != null) { onDone(); } }, onError);
        }
        /// <summary>
        /// 从ModelBlobs中读取数据
        /// </summary>
        /// <typeparam name="T">onDone里面返回读取到的数据类型</typeparam>
        /// <param name="data">要读取的ModelBlobs</param>
        /// <param name="onStart">开始读取回调</param>
        /// <param name="onProgress">读取进度回调 0.0f - 1.0f.</param>
        /// <param name="onDone">读取结束回调</param>
        /// <param name="onError">读取错误回调</param>
        public static void Load<T>(ModelBlobs data, Action onStart, Action<float> onProgress, Action<T> onDone, Action<string> onError) where T : Model {
            if (onStart != null) { onStart(); }
            if (data == null) {
                if (onError != null) { onError("Failed to load modelBlobs because it is null."); }
                return;
            }
            Manifest.LoadAndConstruct<T>(data, onProgress, delegate(T rootModel) {
                foreach (Model model in instances) {
                    model.CollectReferences();
                }
                if (onDone != null) { onDone(rootModel); }
            }, onError);
        }

        /// <summary>
        /// 从XML读取MODEL
        /// </summary>
        /// <param name="dir">目标目录</param>
        /// <param name="onStart">开始读取回调</param>
        /// <param name="onProgress">读取进度回调 0.0f - 1.0f.</param>
        /// <param name="onDone">读取结束回调</param>
        /// <param name="onError">读取错误回调</param>
        public static void Load(string dir, Action onStart, Action<float> onProgress, Action onDone, Action<string> onError) {
            Load<Model>(dir, onStart, onProgress, delegate(Model model) { if (onDone != null) { onDone(); } }, onError);
        }

        /// <summary>
        /// Loads the xml files from the given directory and constructs models accordingly.
        /// </summary>
        /// /// <typeparam name="T">onDone里面返回读取到的数据类型</typeparam>
        /// <param name="dir">目标目录 </param>
        /// <param name="dir">目标目录</param>
        /// <param name="onProgress">读取进度回调 0.0f - 1.0f.</param>
        /// <param name="onDone">读取结束回调</param>
        /// <param name="onError">读取错误回调</param>
        public static void Load<T>(string dir, Action onStart, Action<float> onProgress, Action<T> onDone, Action<string> onError) where T : Model {
            if (onStart != null) { onStart(); }
            if (string.IsNullOrEmpty(dir)) {
                if (onError != null) { onError("Failed to load models because the given directory is null or empty."); }
                return;
            }
            if (!Directory.Exists(dir)) {
                if (onError != null) { onError("Failed to load models because the given directory does not exist."); }
                return;
            }
            Manifest.LoadAndConstruct<T>(dir, onProgress, delegate(T rootModel) {
                foreach (Model model in instances) {
                    model.CollectReferences();
                }
                if (onDone != null) { onDone(rootModel); }
            }, onError);
        }

        /// <summary>
        /// 根据ID找到Model
        /// </summary>
        /// <param name="id">Model ID</param>
        /// <returns>Model</returns>
        public static Model Find(string id) {
            if (!sortedInstances.ContainsKey(id)) {
                UnityEngine.Debug.LogError("Could not find model with id '" + id + "'");
                return null;
            }
            return sortedInstances[id];
        }

        /// <summary>
        /// 根据ID找到Model
        /// </summary>
        /// <typeparam name="T">Model类型</typeparam>
        /// <param name="id">Model ID</param>
        /// <returns>Model</returns>
        public static Model Find<T>(string id) where T : Model {
            Model model = Find(id);
            if (model.GetType() != typeof(T)) {
                UnityEngine.Debug.LogError("Could not find model with id '" + id + "' and type '" + typeof(T) + "'");
                return null;
            }
            return model;
        }

        /// <summary>
        ///根据ID找到Model
        /// </summary>
        /// <typeparam name="T">Model类型</typeparam>
        /// <returns>Model</returns>
        public static T First<T>() where T : Model {
            Type type = typeof(T);
            if (!typeSortedInstances.ContainsKey(type)) { return null; }
            if (typeSortedInstances[type].Count == 0) { return null; }
            return typeSortedInstances[type][0] as T;
        }

        /// <summary>
        /// 返回所有存在的Model
        /// </summary>
        /// <returns>返回所有存在的Model</returns>
        public static List<Model> GetAll() {
            return instances;
        }

        /// <summary>
        /// 返回所有存在的Model
        /// </summary>
        /// <typeparam name="T">Model类型</typeparam>
        /// <returns>返回所有指定类型的Model</returns>
        public static List<T> GetAll<T>() where T : Model {
            List<T> models = new List<T>();
            Type type = typeof(T);
            if (!typeSortedInstances.ContainsKey(type)) { return models; }
            foreach (Model model in typeSortedInstances[type]) {
                models.Add((T)model);
            }
            return models;
        }

        /// <summary>
        /// 删除所有MODEL
        /// </summary>
        public static void DeleteAll() {
            while (instances.Count > 0) {
                instances[0].Delete();
            }
        }

        /// <summary>
        /// 删除所有MODEL
        /// </summary>
        /// <typeparam name="T">MODEL的类型</typeparam>
        public static void DeleteAll<T>() where T : Model {
            List<T> models = GetAll<T>();
            while (models.Count > 0) {
                if (sortedInstances.ContainsKey(models[0].Id)) {
                    models[0].Delete();
                }
                models.RemoveAt(0);
            }
        }

        public Model() {
            Id = Guid.NewGuid().ToString();
            refCount = 0;
            onChangeHandlers = new List<Delegate>();
            onDeleteHandlers = new List<Delegate>();
        }

        /// <summary>
        /// 添加MODEL数据改变NotifyChange的监听事件
        /// </summary>
        /// <param name="callback">回调函数</param>
        public void AddChangeListener(Action callback) {
            if (callback == null) {
                UnityEngine.Debug.LogError("Failed to add ChangeListener on Model but the given callback is null!");
                return;
            }
            onChangeHandlers.Add(callback);
        }

        /// <summary>
        ///添加MODEL数据改变NotifyChange的监听事件 
        /// </summary>
        /// <param name="callback">回调函数</param>
        public void AddChangeListener(Action<Model> callback) {
            if (callback == null) {
                UnityEngine.Debug.LogError("Failed to add ChangeListener on Model but the given callback is null!");
                return;
            }
            onChangeHandlers.Add(callback);
        }

        /// <summary>
        /// 删除监听事件
        /// </summary>
        /// <param name="callback">回调函数</param>
        public void RemoveChangeListener(Action callback) {
            onChangeHandlers.Remove(callback);
        }

        /// <summary>
        /// 删除监听事件
        /// </summary>
        /// <param name="callback">回调函数</param>
        public void RemoveChangeListener(Action<Model> callback) {
            onChangeHandlers.Remove(callback);
        }

        /// <summary>
        /// 添加MODEL数据被删除的监听事件
        /// </summary>
        /// <param name="callback">回调函数</param>
        public void AddDeleteListener(Action callback) {
            if (callback == null) {
                UnityEngine.Debug.LogError("Failed to add DeleteListener on Model but the given callback is null!");
                return;
            }
            onDeleteHandlers.Add(callback);
        }

        /// <summary>
        /// 添加MODEL数据被删除的监听事件
        /// </summary>
        /// <param name="callback">回调函数</param>
        public void AddDeleteListener(Action<Model> callback) {
            if (callback == null) {
                UnityEngine.Debug.LogError("Failed to add DeleteListener on Model but the given callback is null!");
                return;
            }
            onDeleteHandlers.Add(callback);
        }

        /// <summary>
        /// 删除监听事件
        /// </summary>
        /// <param name="callback">回调函数</param>
        public void RemoveDeleteListener(Action callback) {
            onDeleteHandlers.Remove(callback);
        }

        /// <summary>
        /// 删除监听事件
        /// </summary>
        /// <param name="callback">回调函数</param>
        public void RemoveDeleteListener(Action<Model> callback) {
            onDeleteHandlers.Remove(callback);
        }

        /// <summary>
        /// 发送数据改变事件
        /// </summary>
        public void NotifyChange() {
            if (Application.isEditor && OnModelChangeNotified != null) {
                StackTrace stackTrace = new StackTrace();
                Type notifierType = stackTrace.GetFrame(1).GetMethod().DeclaringType;
                OnModelChangeNotified(notifierType, GetType());
            }

            List<Delegate> callbacks = new List<Delegate>(onChangeHandlers);
            while (callbacks.Count > 0) {
                Delegate callback = callbacks[0];
                if (Application.isEditor && OnModelChangeHandled != null) {
                    OnModelChangeHandled(callback.Target.GetType(), GetType());
                }
                CallbackModelDelegate(callback);
                callbacks.Remove(callback);
            }
        }

        /// <summary>
        /// 删除MODEL及相对应的CONTROLLER
        /// </summary>
        public override void Delete() {
            if (!sortedInstances.ContainsKey(id)) {
                return;
            }

            if (Application.isEditor && OnModelDeleted != null) {
                StackTrace stackTrace = new StackTrace();
                Type deleterType = stackTrace.GetFrame(1).GetMethod().DeclaringType;
                OnModelDeleted(deleterType, GetType());
            }

            while (onDeleteHandlers.Count > 0) {
                Delegate callback = onDeleteHandlers[0];
                if (Application.isEditor && OnModelDeleteHandled != null) {
                    OnModelDeleteHandled(callback.Target.GetType(), GetType());
                }
                CallbackModelDelegate(callback);
                onDeleteHandlers.Remove(callback);
            }

            Unregister();

            List<ModelReferencer> modelReferencers = GetModelReferencersInFields();
            foreach (ModelReferencer referencer in modelReferencers) {
                if (referencer == null) { continue; }
                referencer.Delete();
            }
        }

        internal override List<Model> GetReferences() {
            List<Model> references = new List<Model>();
            List<ModelReferencer> referencers = GetModelReferencersInFields();
            foreach (ModelReferencer referencer in referencers) {
                references.AddList<Model>(referencer.GetReferences());
            }
            references.Distinct<Model>();
            return references;
        }

        internal override void CollectReferences() {
            if (referencesCollected) { return; }
            referencesCollected = true;
            List<ModelReferencer> referencers = GetModelReferencersInFields();
            foreach (ModelReferencer referencer in referencers) {
                referencer.CollectReferences();
            }
        }

        internal void IncreaseRefCount() {
            refCount++;
        }

        internal void DecreaseRefCount() {
            refCount--;
            if (refCount <= 0) {
                Delete();
            }
        }

        private List<ModelReferencer> GetModelReferencersInFields() {
            FieldInfo[] fields = GetType().GetFields();
            List<ModelReferencer> modelReferencers = new List<ModelReferencer>();
            foreach (FieldInfo field in fields) {
                if (field.GetValue(this) is ModelReferencer) {
                    modelReferencers.Add(field.GetValue(this) as ModelReferencer);
                }
            }
            return modelReferencers;
        }

        private void Register() {
            if (isSerializing) { return; }
            if (isRegistered) { return; }
            isRegistered = true;

            if (!sortedInstances.ContainsKey(id)) {
                sortedInstances.Add(id, this);
            }

            if (!typeSortedInstances.ContainsKey(GetType())) {
                typeSortedInstances.Add(GetType(), new List<Model>());
            }
            typeSortedInstances[GetType()].Add(this);

            instances.Add(this);
        }

        private void Unregister() {
            if (!isRegistered) { return; }
            isRegistered = false;
            if (sortedInstances.ContainsValue(this)) {
                foreach (KeyValuePair<string, Model> pair in sortedInstances) {
                    if (pair.Value == this) {
                        sortedInstances.Remove(pair.Key);
                        break;
                    }
                }
            }
            if (typeSortedInstances.ContainsKey(GetType())) {
                typeSortedInstances[GetType()].Remove(this);
            }
            instances.Remove(this);
            if (!string.IsNullOrEmpty(id)) {
                sortedInstances.Remove(id);
            }
            instances.Remove(this);
        }

        private void CallbackModelDelegate(Delegate callback) {
            if (callback is Action<Model>) {
                Action<Model> action = callback as Action<Model>;
                action(this);
            } else {
                Action action = callback as Action;
                action();
            }
        }

    }

}