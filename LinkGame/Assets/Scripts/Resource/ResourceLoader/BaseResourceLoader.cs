﻿using UnityEngine;

namespace XrCode
{
    // 资源控制基类
    public abstract class BaseResourceLoader
    {
        /// <summary>
        /// 同步加载资源
        /// </summary>
        public virtual T SyncLoad<T>(string path) where T : Object { return null; }

        /// <summary>
        /// 异步加载资源
        /// </summary>
        public virtual void AsyncLoad(string path, System.Action<Object> callback) { }

        /// <summary>
        /// 卸载资源
        /// </summary>
        public virtual void Unload(string path) { }
    }
}