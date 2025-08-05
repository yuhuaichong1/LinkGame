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
        //private Dictionary<string, object> attribution;//������Ϣ

        public SolarEngineModule(SolarEngineData data)
        {
            if (data != null)
            {

            }
        }

        protected override void OnLoad()
        {
            base.OnLoad();

            Analytics.preInitSeSdk("13123");

            SEConfig seConfig = new SEConfig();
            seConfig.logEnabled = true;
            seConfig.isGDPRArea = true;
            seConfig.isCoppaEnabled = true;
            seConfig.isKidsAppEnabled = true;
            seConfig.isEnable2GReporting = true;
            seConfig.deferredDeeplinkenable = true;
            seConfig.attAuthorizationWaitingInterval = 1;//��IOS
            seConfig.fbAppID = "123";//��Android
            seConfig.adPersonalizationEnabled = true;//��Android
            seConfig.adUserDataEnabled = true;// ��Android
            seConfig.caid = "123";//��IOS
            SECustomDomain sECustomDomain = new SECustomDomain()//��IOS
            {
                enable = true,
                receiverDomain = "123",
                ruleDomain = "123",
                receiverTcpHost = "123",
                gatewayTcpHost = "123",
            };
            seConfig.customDomain = sECustomDomain;////��IOS

            seConfig.initCompletedCallback = onInitCallback;
            seConfig.attributionCallback = onAttributionCallback;
            SolarEngine.Analytics.initSeSdk("13123", seConfig);
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
