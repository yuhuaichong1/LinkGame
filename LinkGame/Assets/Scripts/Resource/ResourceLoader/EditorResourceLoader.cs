﻿/**
 * 编辑模式下资源控制
 * 使用AssetDataBase加载
 */

using System.Text.RegularExpressions;
using UnityEngine;

namespace XrCode
{

    public class EditorResourceLoader : BaseResourceLoader
    {
        /// <summary>
        /// 同步加载资源
        /// </summary>
        public override T SyncLoad<T>(string path)
        {
            if (!AppConfig.LoadAssetWithResources)
            {
#if UNITY_EDITOR
                return UnityEditor.AssetDatabase.LoadAssetAtPath<T>(path);
#else
        return null;
#endif
            }
            else
            {
                // 使用正则表达式去除文件扩展名
                path = Regex.Replace(path, @"\.[^.]+$", ""); // 去掉最后一个点及其后的所有字符
                return Resources.Load<T>(path);
            }
        }

        /// <summary>
        /// 编辑器模式下的异步加载就是同步加载模拟
        /// </summary>
        public override void AsyncLoad(string path, System.Action<Object> callback)
        {
            Object obj = IsSprite(path) ? SyncLoad<Sprite>(path) : SyncLoad<Object>(path);
            callback(obj);
        }

        /// <summary>
        /// 判断是否Sprite
        /// </summary>
        private bool IsSprite(string path)
        {
            return path.EndsWith(".png", System.StringComparison.OrdinalIgnoreCase) || path.EndsWith(".jpg", System.StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// 编辑器下的卸载
        /// </summary>
        /// <param name="path">Path.</param>
        public override void Unload(string path)
        {
            Resources.UnloadUnusedAssets();
        }
    }
}