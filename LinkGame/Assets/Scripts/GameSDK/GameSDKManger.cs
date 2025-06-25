using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace XrCode
{
    public class GameSDKManger : Singleton<GameSDKManger>, ILoad
    {
        SDKBase sdkBase;

        public SDKBase SdkBase { get => sdkBase; set => sdkBase = value; }

        public void Load()
        {

        }
        public void StartUp()
        {
#if UNITY_EDITOR || UNITY_ANDROID
            sdkBase = new EditorSDK();
#elif WX
        sdkBase = new WeChatSDK();
#endif
            sdkBase.Init();
        }
        public bool CheckDirectoryExists(string path)
        {
            return sdkBase.CheckDirectoryExists(path);//����ļ����Ƿ����
        }
        public void CreateDirectoryMkdir(string path)
        {
            sdkBase.CreateDirectoryMkdir(path);//�����ļ���
        }
        public void DeleteFile(string path)
        {
            sdkBase.DeleteFile(path);//ɾ��һ���ļ�
        }
        public void CreatFile(string path)
        {
            sdkBase.CreatFile(path);//����һ���ļ�
        }
        public bool CheckFileExists(string path)
        {
            return sdkBase.CheckFileExists(path); //���һ���ļ��Ƿ����
        }
        public void WriteFileSync(string path, string content)
        {
            sdkBase.WriteFileSync(path, content);//ͬ��д���ļ�
        }
        public void WriteFileSync(string path, byte[] content)
        {
            sdkBase.WriteFileSync(path, content);//ͬ��д���ļ�
        }
        public void ReadFileSync(string path, Action<string> action = null)
        {
            sdkBase.ReadFileSync(path, action);//��ȡ�ļ�
        }
        public void ReadFileSync(string path, Action<byte[]> action = null)
        {
            sdkBase.ReadFileSync(path, action);//��ȡ�ļ�
        }
        public void ShowVideoAd(string adId, Action<bool, int> closeCallBack, Action<int, string> errorCallBack)
        {
            sdkBase.ShowVideoAd(adId, closeCallBack, errorCallBack);//չʾ���
        }
        public void DeleteDirectory(string path)
        {
            sdkBase.DeleteDirectory(path);
        }

    }
}