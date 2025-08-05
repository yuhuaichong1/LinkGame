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
        private string appKey;                      //开发者申请的appkey
        private bool isDebugModel;                  //是否开启 Debug 模式，Debug模式请勿发布到线上
        private bool logEnabled;                    //是否开启 本地调试日志
        private bool isGDPRArea;                    //是否为GDPR区域（仅海外版设置有效）
        private bool isCoppaEnabled;                //是否支持coppa合规（仅海外版设置有效）
        private bool isKidsAppEnabled;              //是否支持Kids App应用（仅海外版设置有效）
        private bool isEnable2GReporting;           //是否允许2G上报数据
        private bool deferredDeeplinkenable;        //是否开启延迟deeplink

        private string fbAppID;                     //需要用到meta归因，此处设置meta appid（仅Android）
        private bool adPersonalizationEnabled;      //用户是否允许Google将其数据用于个性化广告（仅Android）
        private bool adUserDataEnabled;             //用户是否同意将其数据发送到Google（仅Android）

        private string caid;                        //iOS caid（仅IOS）
        private int attAuthorizationWaitingInterval;//ATT 授权等待时间（仅IOS）
        //private bool odmInfoEnable;                 //iOS odmInfo（仅IOS,仅非中国大陆设置有效）

        private bool enable;                        //
        private string receiverDomain;              //
        private string ruleDomain;                  //
        private string receiverTcpHost;             //
        private string gatewayTcpHost;              //

        //private Dictionary<string, object> attribution;   //归因信息

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
            
            seConfig.fbAppID = fbAppID;//仅Android
            seConfig.adPersonalizationEnabled = adPersonalizationEnabled;//仅Android
            seConfig.adUserDataEnabled = adUserDataEnabled;// 仅Android

            seConfig.caid = caid;//仅IOS
            seConfig.attAuthorizationWaitingInterval = attAuthorizationWaitingInterval;//仅IOS
            //seConfig.odmInfoEnable = odmInfoEnable;//仅IOS

            SECustomDomain sECustomDomain = new SECustomDomain()
            {
                enable = enable,
                receiverDomain = receiverDomain,
                ruleDomain = ruleDomain,
                receiverTcpHost = receiverTcpHost,
                gatewayTcpHost = gatewayTcpHost,
            };
            seConfig.customDomain = sECustomDomain;////仅IOS

            seConfig.initCompletedCallback = onInitCallback;
            seConfig.attributionCallback = onAttributionCallback;
            initSeSdk(appKey, seConfig);
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
