using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XrCode
{
    public static class PlayerFacade
    {                                                                   
        public static Func<float> GetWMoney;                            //�õ���ǰ��Ǯ��

        public static Func<string> GetWName;                            //�õ����ֽ�����д������
        public static Func<string> GetWEmailOrPhone;                    //�õ����ֽ�����д�ĵ绰/�ʼ�

        public static Func<int> GetUserLevel;                           //�õ���ҵȼ�
        public static Func<int> GetCurUserExp;                          //�õ���Ҿ���



        public static Action<float> SetWMoney;                          //���õ�ǰ��Ǯ��

        public static Action<string, string> SetNameAndWEmailOrPhone;    //���ö��ֽ��������������
        public static Action<string> SetEmail;                          //���ö��ֽ��������

        public static Action<int> SetUserLevel;                         //������ҵȼ�
        public static Action<int> SetCurUserExp;                        //������Ҿ���
    }
}
