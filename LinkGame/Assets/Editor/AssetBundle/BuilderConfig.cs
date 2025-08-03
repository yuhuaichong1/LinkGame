﻿/**
 * 构建配置
 */

using UnityEngine;
using UnityEditor;

namespace Assets.Editor.AssetBundle
{
    public class BuilderConfig
    {
        /// <summary>
        /// 要打成AssetBundle的资源根目录
        /// </summary>
        public static readonly string AssetRootPath = "Assets/AssetBundleLocal";
        /// <summary>
        /// lua脚本路径
        /// </summary>
        public static readonly string LuaScriptsSrcPath = "Assets/LuaScripts";
        public static readonly string LuaScriptsDestPath = "Assets/AssetBundleLocal/LuaScripts";
        /// <summary>
        /// 图集资源路径
        /// </summary>
        public static readonly string SpriteAtlasPath = AssetRootPath + "/UI/Atlas";
        /// <summary>
        /// AssetBundle的导出目录
        /// </summary>
        public static readonly string AssetBundleExportPath = Application.streamingAssetsPath + "/AssetBundles";
        /// <summary>
        /// 路径和AB包名对应配置表的路径
        /// </summary>
        public static readonly string PathBundleConfig = AssetRootPath + "/Config/PathBundleConfig.bytes";
        /// <summary>
        /// 构建参数
        /// </summary>
        public static readonly BuildAssetBundleOptions Options = BuildAssetBundleOptions.ChunkBasedCompression;
    }
}
