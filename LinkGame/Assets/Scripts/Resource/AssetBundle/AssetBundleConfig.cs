﻿/**
 * AssetBundle配置
 */

using System.Collections.Generic;
using ProtoBuf;

namespace XrCode
{
    public class AssetBundleConfig
    {
        public const string AssetBundlesFolder = "AssetBundles";
        public static readonly string PathBundleConfigAssetName = "Assets/AssetBundleLocal/Config/PathBundleConfig.bytes";
        public static readonly string PathBundleConfigAssetBundle = PathBundleConfigAssetName.ToLower() + ".assetbundle";
    }

    [ProtoContract]
    public class PathBundleInfoList
    {
        [ProtoMember(1)]
        public List<PathBundleInfo> List = new List<PathBundleInfo>();
    }

    [ProtoContract]
    public class PathBundleInfo
    {
        [ProtoMember(1)]
        public string Path;
        [ProtoMember(2)]
        public string AssetBundleName;
    }
}