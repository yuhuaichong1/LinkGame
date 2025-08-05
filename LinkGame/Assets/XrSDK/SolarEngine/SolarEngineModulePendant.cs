using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XrSDK
{
    //Solar Engine ģ��Ҽ�
    [RegisterModule("Solar Engine Module")]
    public class SolarEngineModulePendant : BaseModulePendant
    {
        public string AppKey;                       //�����������appkey
        [Space]
        public bool IsDebugModel;                   //�Ƿ��� Debug ģʽ��Debugģʽ���𷢲�������
        public bool LogEnabled;                     //�Ƿ��� ���ص�����־
        [Space]
        public bool IsGDPRArea;                     //�Ƿ�ΪGDPR���򣨽������������Ч��
        public bool IsCoppaEnabled;                 //�Ƿ�֧��coppa�Ϲ棨�������������Ч��
        public bool IsKidsAppEnabled;               //�Ƿ�֧��Kids AppӦ�ã��������������Ч��
        public bool IsEnable2GReporting;            //�Ƿ�����2G�ϱ�����
        public bool DeferredDeeplinkenable;         //�Ƿ����ӳ�deeplink
        [Space]
        [Header("Android")]
        public string FbAppID;                      //��Ҫ�õ�meta���򣬴˴�����meta appid����Android��
        public bool AdPersonalizationEnabled;       //�û��Ƿ�����Google�����������ڸ��Ի���棨��Android��
        public bool AdUserDataEnabled;              //�û��Ƿ�ͬ�⽫�����ݷ��͵�Google����Android��
        [Space]
        [Header("IOS")]
        public string Caid;                         //iOS caid����IOS��
        public int AttAuthorizationWaitingInterval; //ATT ��Ȩ�ȴ�ʱ�䣨��IOS��
        //public bool OdmInfoEnable;                  //iOS odmInfo����IOS,�����й���½������Ч��

        [Space]
        [Header("SE Custom Domain")]
        public bool Enable;                         //
        public string ReceiverDomain;               //
        public string RuleDomain;                   //
        public string ReceiverTcpHost;              //
        public string GatewayTcpHost;               //


        public override string ModuleName => "SolarEngine";

        public override void CreateModule()
        {
            SolarEngineData data = new SolarEngineData();

            data.appKey = AppKey;
            data.isDebugModel = IsDebugModel;
            data.logEnabled = LogEnabled;
            data.isGDPRArea = IsGDPRArea;
            data.isCoppaEnabled = IsCoppaEnabled;
            data.isKidsAppEnabled = IsKidsAppEnabled;
            data.isEnable2GReporting = IsEnable2GReporting;
            data.deferredDeeplinkenable = DeferredDeeplinkenable;
            data.attAuthorizationWaitingInterval = AttAuthorizationWaitingInterval;
            data.fbAppID = FbAppID;
            data.adPersonalizationEnabled = AdPersonalizationEnabled;
            data.adUserDataEnabled = AdUserDataEnabled;
            data.caid = Caid;
            data.enable = Enable;
            data.receiverDomain = ReceiverDomain;
            data.ruleDomain = RuleDomain;
            data.receiverTcpHost = ReceiverTcpHost;
            data.gatewayTcpHost = GatewayTcpHost;
            //data.odmInfoEnable = OdmInfoEnable;

            SolarEngineModule module = new SolarEngineModule(data);
            module.Load();
        }
    }
}
