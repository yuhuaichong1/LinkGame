using SolarEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using XrCode;
using static SolarEngine.Analytics;

namespace XrSDK
{
    public class SolarEngineModule : BaseModule
    {
        private string appKey;                      //�����������appkey
        private bool isDebugModel;                  //�Ƿ��� Debug ģʽ��Debugģʽ���𷢲�������
        private bool logEnabled;                    //�Ƿ��� ���ص�����־
        private bool isGDPRArea;                    //�Ƿ�ΪGDPR���򣨽������������Ч��
        private bool isCoppaEnabled;                //�Ƿ�֧��coppa�Ϲ棨�������������Ч��
        private bool isKidsAppEnabled;              //�Ƿ�֧��Kids AppӦ�ã��������������Ч��
        private bool isEnable2GReporting;           //�Ƿ�����2G�ϱ�����
        private bool deferredDeeplinkenable;        //�Ƿ����ӳ�deeplink

        private string fbAppID;                     //��Ҫ�õ�meta���򣬴˴�����meta appid����Android��
        private bool adPersonalizationEnabled;      //�û��Ƿ�����Google�����������ڸ��Ի���棨��Android��
        private bool adUserDataEnabled;             //�û��Ƿ�ͬ�⽫�����ݷ��͵�Google����Android��

        private string caid;                        //iOS caid����IOS��
        private int attAuthorizationWaitingInterval;//ATT ��Ȩ�ȴ�ʱ�䣨��IOS��
        //private bool odmInfoEnable;                 //iOS odmInfo����IOS,�����й���½������Ч��

        private bool enable;                        //
        private string receiverDomain;              //
        private string ruleDomain;                  //
        private string receiverTcpHost;             //
        private string gatewayTcpHost;              //

        //private Dictionary<string, object> attribution;   //������Ϣ

        public SolarEngineModule(SolarEngineData data)
        {
            if (data != null)
            {
                appKey = data.appKey;
                isDebugModel = data.isDebugModel;
                logEnabled = data.logEnabled;
                isGDPRArea = data.isGDPRArea;
                isCoppaEnabled = data.isCoppaEnabled;
                isKidsAppEnabled = data.isKidsAppEnabled;
                isEnable2GReporting = data.isEnable2GReporting;
                deferredDeeplinkenable = data.deferredDeeplinkenable;

                fbAppID = data.fbAppID;
                adPersonalizationEnabled = data.adPersonalizationEnabled;
                adUserDataEnabled = data.adUserDataEnabled;

                caid = data.caid;
                attAuthorizationWaitingInterval = data.attAuthorizationWaitingInterval;
                //odmInfoEnable = data.odmInfoEnable;
                
                enable = data.enable;
                receiverDomain = data.receiverDomain;
                ruleDomain = data.ruleDomain;
                receiverTcpHost = data.receiverTcpHost;
                gatewayTcpHost = data.gatewayTcpHost;
            }
        }

        protected override void OnLoad()
        {
            base.OnLoad();

            preInitSeSdk(appKey);

            SEConfig seConfig = new SEConfig();
            seConfig.isDebugModel = isDebugModel;
            seConfig.logEnabled = logEnabled;
            seConfig.isGDPRArea = isGDPRArea;
            seConfig.isCoppaEnabled = isCoppaEnabled;
            seConfig.isKidsAppEnabled = isKidsAppEnabled;
            seConfig.isEnable2GReporting = isEnable2GReporting;
            seConfig.deferredDeeplinkenable = deferredDeeplinkenable;
            
            seConfig.fbAppID = fbAppID;//��Android
            seConfig.adPersonalizationEnabled = adPersonalizationEnabled;//��Android
            seConfig.adUserDataEnabled = adUserDataEnabled;// ��Android

            seConfig.caid = caid;//��IOS
            seConfig.attAuthorizationWaitingInterval = attAuthorizationWaitingInterval;//��IOS
            //seConfig.odmInfoEnable = odmInfoEnable;//��IOS

            SECustomDomain sECustomDomain = new SECustomDomain()
            {
                enable = enable,
                receiverDomain = receiverDomain,
                ruleDomain = ruleDomain,
                receiverTcpHost = receiverTcpHost,
                gatewayTcpHost = gatewayTcpHost,
            };
            seConfig.customDomain = sECustomDomain;////��IOS

            seConfig.initCompletedCallback = onInitCallback;
            seConfig.attributionCallback = onAttributionCallback;
            initSeSdk(appKey, seConfig);
        }

        //��ʼ���ص�
        private void onInitCallback(int code)
        {
            switch(code) 
            { 
                case 0:
                    onInitSuccessful();
                    break;
                case 101:
                    D.Error("�������� SDKδԤ��ʼ��");
                    break;
                case 102:
                    D.Error("�������� appkey�Ƿ�");
                    break;
                case 103:
                    D.Error("�������� contextΪnull");
                    break;
                case 104:
                    D.Error("�������� distinct_id����ʧ��");
                    break;
            }
        }

        //����ص�
        private void onAttributionCallback(int code, Dictionary<string, object> attribution) 
        { 
            if(code == 0) 
            {
                //this.attribution = attribution;
                ModuleMgr.Instance.TDAnalyticsManager.SolarEngineMsg(attribution);
            }
            else
            {
                D.Error($"�������� ����ʧ�ܣ�{code}");
            }
        }
        
        //��ʼ���ɹ��ص�
        private void onInitSuccessful()
        {

        }
    }
}
