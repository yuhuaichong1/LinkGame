using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XrCode
{
    public static class PlayerFacade
    {                                                                   
        public static Func<float> GetWMoney;                            //�õ���ǰ��Ǯ��
        public static Func<string> GetWName;                               //�õ����ֽ�����д������
        public static Func<int> GetWPhone;                              //�õ����ֽ�����д�ĵ绰
        public static Func<string> GetEmail;                            //�õ����ֽ�����д���ʼ�
        public static Action<string, int> SetNameAndPhone;              //���ö��ֽ���������͵绰
        public static Action<string, string> SetNameAndEmail;           //���ö��ֽ��������������
        public static Action<string> SetEmail;                          //���ö��ֽ��������
    }
}
