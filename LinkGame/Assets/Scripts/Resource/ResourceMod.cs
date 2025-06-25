﻿/**
 * 资源加载管理类
 */

using UnityEngine;
using System.Collections.Generic;
namespace XrCode
{
    public class ResourceMod : Singleton<ResourceMod>, ILoad, IDispose
    {
        /// <summary>
        /// 对应平台模式下的资源加载器
        /// </summary>
        private BaseResourceLoader mResourcesLoader;
        /// <summary>
        /// 缓存使用的资源列表
        /// </summary>
        private Dictionary<string, ResourceUnit> mResourceUnitDict = new Dictionary<string, ResourceUnit>();
        /// <summary>
        /// 缓存引用计数为0的资源, 达到缓存上限时情掉最早没用的资源(尾部资源)
        /// </summary>
        private LinkedList<ResourceUnit> mNoRefResourceUnitLinkedList = new LinkedList<ResourceUnit>();
        /// <summary>
        /// 最大资源缓存数（视具体项目而定）
        /// </summary>
        private const int MAXCACHECOUNT = 100;

        public void Load()
        {
            if (AppConfig.UseAssetBundle)
            {
                mResourcesLoader = new AssetBundleResourceLoader();
            }
            else
            {
                mResourcesLoader = new EditorResourceLoader();
            }
        }
        public void Dispose()
        {
            Resources.UnloadUnusedAssets();
        }

        /// <summary>
        /// 同步加载
        /// </summary>
        public T SyncLoad<T>(string path) where T : Object
        {
            path = PathUtil.GetFullAssetPath(path);
            var cache = GetResourceUnitCache(path);
            if (cache != null)
            {
                return cache.Asset as T;
            }
            var obj = mResourcesLoader.SyncLoad<T>(path);
            CacheResourceUnit(path, obj);
            return obj;
        }

        /// <summary>
        /// 异步加载
        /// </summary>
        /// <param name="path"></param>
        /// <param name="callback"></param>
        public void AsyncLoad<T>(string path, System.Action<Object> callback) where T : Object
        {
            path = PathUtil.GetFullAssetPath(path);
            var cache = GetResourceUnitCache(path);
            if (cache != null)
            {
                if (callback != null) callback(cache.Asset as T);
                return;
            }
            mResourcesLoader.AsyncLoad(path, (asset) =>
            {
                // 同时多个异步加载同时请求时，就都会跳过第一步的缓存判断，这时第一个加载完的就缓存资源
                cache = GetResourceUnitCache(path);
                if (cache == null)
                {
                    CacheResourceUnit(path, asset);
                }
                if (callback != null) callback(asset as T);
            });
        }

        /// <summary>
        /// 清缓存
        /// </summary>
        private void ClearCache()
        {
            var clearList = new List<string>();
            foreach (var path in mResourceUnitDict.Keys)
            {
                var unit = mResourceUnitDict[path];
                if (unit.Clear)
                {
                    clearList.Add(path);
                }
            }
            foreach (var path in clearList)
            {
                Unload(path, true);
            }
        }

        /// <summary>
        /// 清理未引用列表
        /// </summary>
        private void ClearNoRefCache()
        {
            // 缓存达到上限时，释放一半缓存
            if (mNoRefResourceUnitLinkedList.Count > MAXCACHECOUNT)
            {
                while (mNoRefResourceUnitLinkedList.Count > MAXCACHECOUNT / 2)
                {
                    var tail = mNoRefResourceUnitLinkedList.Last;
                    if (tail != null && tail.Value != null)
                    {
                        Unload(tail.Value.Path, true);
                    }
                }
            }
        }

        /// <summary>
        /// 卸载资源
        /// </summary>
        public void Unload(string path, bool destroy = false, int refCount = 1)
        {
            // 注意 这里ReCount传0
            var unit = GetResourceUnitCache(path, 0);
            if (unit == null) return;
            // 马上销毁的资源，不管引用计数
            if (destroy)
            {
                if (unit.NoRefNode != null)
                {
                    mNoRefResourceUnitLinkedList.Remove(unit.NoRefNode);
                    unit.NoRefNode = null;
                }
                mResourceUnitDict.Remove(path);
                mResourcesLoader.Unload(path);
                return;
            }
            if (unit.RefCount <= 0) return;
            unit.RefCount -= refCount;
            if (unit.RefCount <= 0)
            {
                unit.RefCount = 0;
                // 不是马上销毁的资源存入未引用列表中
                unit.NoRefNode = mNoRefResourceUnitLinkedList.AddFirst(unit);
            }
        }

        /// <summary>
        /// 获取缓存里的资源项
        /// </summary>
        private ResourceUnit GetResourceUnitCache(string path, int refCount = 1)
        {
            ResourceUnit unit;
            if (mResourceUnitDict.TryGetValue(path, out unit))
            {
                // 当资源重新被引用的时候，从未引用列表中删除
                if (unit.RefCount <= 0 && unit.RefCount + refCount > 0 && unit.NoRefNode != null)
                {
                    mNoRefResourceUnitLinkedList.Remove(unit.NoRefNode);
                    unit.NoRefNode = null;
                }
                unit.RefCount += refCount;
            }
            return unit;
        }

        /// <summary>
        /// 缓存资源
        /// </summary>
        private void CacheResourceUnit(string path, Object asset)
        {
            ClearNoRefCache();
            if (asset == null) return;
            if (mResourceUnitDict.ContainsKey(path))
            {
                D.Error("Duplicate cache asset {0}", path);
            }
            else
            {
                mResourceUnitDict.Add(path, new ResourceUnit() { Path = path, Asset = asset, RefCount = 1 });
            }
        }


    }

    public class ResourceUnit
    {
        /// <summary>
        /// 资源路径
        /// </summary>
        public string Path;
        /// <summary>
        /// 资源对象
        /// </summary>
        public Object Asset;
        /// <summary>
        /// 引用次数
        /// </summary>
        public int RefCount;
        /// <summary>
        /// 是否要清理
        /// </summary>
        public bool Clear;
        /// <summary>
        /// 当计数减为0，移入到NoRefResourceUnitDList的节点
        /// </summary>
        public LinkedListNode<ResourceUnit> NoRefNode;
    }
}