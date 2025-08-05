using SolarEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolarEngineData
{
    public string appKey;                       //开发者申请的appkey
    public bool isDebugModel;                   //是否开启 Debug 模式，Debug模式请勿发布到线上
    public bool logEnabled;                     //是否开启 本地调试日志
    public bool isGDPRArea;                     //是否为GDPR区域（仅海外版设置有效）
    public bool isCoppaEnabled;                 //是否支持coppa合规（仅海外版设置有效）
    public bool isKidsAppEnabled;               //是否支持Kids App应用（仅海外版设置有效）
    public bool isEnable2GReporting;            //是否允许2G上报数据
    public bool deferredDeeplinkenable;         //是否开启延迟deeplink
    
    public string fbAppID;                      //需要用到meta归因，此处设置meta appid（仅Android）
    public bool adPersonalizationEnabled;       //用户是否允许Google将其数据用于个性化广告（仅Android）
    public bool adUserDataEnabled;              //用户是否同意将其数据发送到Google（仅Android）

    public string caid;                         //iOS caid（仅IOS）
    public int attAuthorizationWaitingInterval; //ATT 授权等待时间（仅IOS）
    //public bool odmInfoEnable;                  //iOS odmInfo（仅IOS,仅非中国大陆设置有效）

    public bool enable;                         //
    public string receiverDomain;               //
    public string ruleDomain;                   //
    public string receiverTcpHost;              //
    public string gatewayTcpHost;               //
}
