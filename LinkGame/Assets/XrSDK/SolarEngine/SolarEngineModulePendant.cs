using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XrSDK
{
    //Solar Engine 模块挂件
    [RegisterModule("Solar Engine Module")]
    public class SolarEngineModulePendant : BaseModulePendant
    {
        public string AppKey;                       //开发者申请的appkey
        [Space]
        public bool IsDebugModel;                   //是否开启 Debug 模式，Debug模式请勿发布到线上
        public bool LogEnabled;                     //是否开启 本地调试日志
        [Space]
        public bool IsGDPRArea;                     //是否为GDPR区域（仅海外版设置有效）
        public bool IsCoppaEnabled;                 //是否支持coppa合规（仅海外版设置有效）
        public bool IsKidsAppEnabled;               //是否支持Kids App应用（仅海外版设置有效）
        public bool IsEnable2GReporting;            //是否允许2G上报数据
        public bool DeferredDeeplinkenable;         //是否开启延迟deeplink
        [Space]
        [Header("Android")]
        public string FbAppID;                      //需要用到meta归因，此处设置meta appid（仅Android）
        public bool AdPersonalizationEnabled;       //用户是否允许Google将其数据用于个性化广告（仅Android）
        public bool AdUserDataEnabled;              //用户是否同意将其数据发送到Google（仅Android）
        [Space]
        [Header("IOS")]
        public string Caid;                         //iOS caid（仅IOS）
        public int AttAuthorizationWaitingInterval; //ATT 授权等待时间（仅IOS）
        //public bool OdmInfoEnable;                  //iOS odmInfo（仅IOS,仅非中国大陆设置有效）

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
