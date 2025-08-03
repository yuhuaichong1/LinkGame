using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace XrCode
{
    public abstract class SDKBase
    {
        public abstract void Init();//��ʼ��
        public abstract bool CheckDirectoryExists(string path);//����ļ����Ƿ����
        public abstract void CreateDirectoryMkdir(string path);//�����ļ���
        public abstract void DeleteDirectory(string path);//ɾ��һ���ļ���
        public abstract void CreatFile(string path);//�����ļ�
        public abstract void DeleteFile(string path);//ɾ��һ���ļ�
        public abstract bool CheckFileExists(string path);//���һ���ļ��Ƿ����
        public abstract void WriteFileSync(string path, string content);//ͬ��д���ļ�
        public virtual void WriteFileSync(string path, byte[] content) { }//ͬ��д���ļ�
        public abstract void ReadFileSync(string path, Action<string> action = null);//��ȡ�ļ�
        public virtual void ReadFileSync(string path, Action<byte[]> action = null) { }//��ȡ�ļ�
        public abstract void ShowVideoAd(string adId, Action<bool, int> closeCallBack, Action<int, string> errorCallBack);//չʾ���

    }


}
