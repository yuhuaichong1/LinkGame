﻿/**
 * 对象管理类
 */

using System;
using System.Collections.Generic;

namespace XrCode
{
    public class ClassObjectManager : Singleton<ClassObjectManager>, ILoad, IDispose
    {
        /// <summary>
        /// 存储类对象池的字典
        /// </summary>
        protected Dictionary<Type, object> mClassPoolDict;

        public void Load()
        {
            mClassPoolDict = new Dictionary<Type, object>();
        }
        public void Dispose()
        {
        }

        /// <summary>
        /// 获取类对象池
        /// </summary>
        public ClassObjectPool<T> GetOrCreateClassPool<T>(int maxCount = -1) where T : class, new()
        {
            var type = typeof(T);
            if (!mClassPoolDict.ContainsKey(type))
            {
                var pool = new ClassObjectPool<T>(maxCount);
                mClassPoolDict.Add(type, pool);
                return pool;
            }
            return mClassPoolDict[type] as ClassObjectPool<T>;
        }


    }
}