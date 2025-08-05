using SolarEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolarEngineData
{
    public string appKey;                       //�����������appkey
    public bool isDebugModel;                   //�Ƿ��� Debug ģʽ��Debugģʽ���𷢲�������
    public bool logEnabled;                     //�Ƿ��� ���ص�����־
    public bool isGDPRArea;                     //�Ƿ�ΪGDPR���򣨽������������Ч��
    public bool isCoppaEnabled;                 //�Ƿ�֧��coppa�Ϲ棨�������������Ч��
    public bool isKidsAppEnabled;               //�Ƿ�֧��Kids AppӦ�ã��������������Ч��
    public bool isEnable2GReporting;            //�Ƿ�����2G�ϱ�����
    public bool deferredDeeplinkenable;         //�Ƿ����ӳ�deeplink
    
    public string fbAppID;                      //��Ҫ�õ�meta���򣬴˴�����meta appid����Android��
    public bool adPersonalizationEnabled;       //�û��Ƿ�����Google�����������ڸ��Ի���棨��Android��
    public bool adUserDataEnabled;              //�û��Ƿ�ͬ�⽫�����ݷ��͵�Google����Android��

    public string caid;                         //iOS caid����IOS��
    public int attAuthorizationWaitingInterval; //ATT ��Ȩ�ȴ�ʱ�䣨��IOS��
    //public bool odmInfoEnable;                  //iOS odmInfo����IOS,�����й���½������Ч��

    public bool enable;                         //
    public string receiverDomain;               //
    public string ruleDomain;                   //
    public string receiverTcpHost;              //
    public string gatewayTcpHost;               //
}
