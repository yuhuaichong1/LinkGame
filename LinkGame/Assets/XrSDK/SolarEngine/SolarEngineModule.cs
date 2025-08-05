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
        //private Dictionary<string, object> attribution;//归因信息

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
            seConfig.attAuthorizationWaitingInterval = 1;//仅IOS
            seConfig.fbAppID = "123";//仅Android
            seConfig.adPersonalizationEnabled = true;//仅Android
            seConfig.adUserDataEnabled = true;// 仅Android
            seConfig.caid = "123";//仅IOS
            SECustomDomain sECustomDomain = new SECustomDomain()//仅IOS
            {
                enable = true,
                receiverDomain = "123",
                ruleDomain = "123",
                receiverTcpHost = "123",
                gatewayTcpHost = "123",
            };
            seConfig.customDomain = sECustomDomain;////仅IOS

            seConfig.initCompletedCallback = onInitCallback;
            seConfig.attributionCallback = onAttributionCallback;
            SolarEngine.Analytics.initSeSdk("13123", seConfig);
        }

        //初始化回调
        private void onInitCallback(int code)
        {
            switch(code) 
            { 
                case 0:
                    onInitSuccessful();
                    break;
                case 101:
                    D.Error("热力引擎 SDK未预初始化");
                    break;
                case 102:
                    D.Error("热力引擎 appkey非法");
                    break;
                case 103:
                    D.Error("热力引擎 context为null");
                    break;
                case 104:
                    D.Error("热力引擎 distinct_id生成失败");
                    break;
            }
        }

        //归因回调
        private void onAttributionCallback(int code, Dictionary<string, object> attribution) 
        { 
            if(code == 0) 
            {
                //this.attribution = attribution;
                ModuleMgr.Instance.TDAnalyticsManager.SolarEngineMsg(attribution);
            }
            else
            {
                D.Error($"热力引擎 归因失败：{code}");
            }
        }
        
        //初始化成功回调
        private void onInitSuccessful()
        {

        }
    }
}
