using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using ThinkingData.Analytics;
using ThinkingData.Analytics.Utils;
using UnityEngine;
using UnityEngine.Analytics;
namespace XrCode
{
    public class TDAnalyticsManager : BaseModule
    {
        //开关
#if UNITY_EDITOR
        bool isOpenTD = false;
#else
    bool isOpenTD = true;
#endif

        //玩家id
        private string accoundId = "";
        //游戏版本
        private string GameVersion = "1.0.0.0";
        //游戏编号，区分上线平台
        private string GameAppId = "505001";

        
        private DateTime AdTime;//广告时长统计
        private DateTime loginTime;//登录时的时间

        protected override void OnLoad()
        {
            //GameLoad(PlayerPrefs.GetString("accoundId"));
        }

        //游戏加载
        public void GameLoad(string accoundId)
        {
            if (!isOpenTD) return;
            //设置公共事件属性以后，每个事件都会带有公共事件属性
            Dictionary<string, object> superProperties = new Dictionary<string, object>();
            superProperties["GameVersion"] = GameVersion;
            superProperties["GameAppId"] = GameAppId;
            if (this.accoundId != "")
            {
                this.accoundId = accoundId;
                //superProperties["accountId"] = accoundId;//字符串
                TDAnalytics.Login(accoundId);
            }
            else
            {
                this.accoundId = UnityEngine.Random.Range(1000000, 9999999).ToString();
                SPlayerPrefs.SetString("GoodMatch_accoundId", this.accoundId);
                //superProperties["accountId"] = accoundId;//字符串
                RegisterFinish(accoundId, DateTime.Now);
            }
            TDAnalytics.SetSuperProperties(superProperties);//设置公共事件属性

            TDAnalytics.UserSet(new Dictionary<string, object>() { { "accountId", this.accoundId }, { "GameVersion", GameVersion }, { "GameAppId", GameAppId } }); //设置用户属性

            if (isOpenTD)
            {
                TDAnalytics.EnableAutoTrack(TDAutoTrackEventType.AppStart, new Dictionary<string, object>()
                {
                    { "accountId", this.accoundId }
                });
                TDAnalytics.EnableAutoTrack(TDAutoTrackEventType.AppEnd, new Dictionary<string, object>()
                {
                    { "accountId", this.accoundId },
                });
                TDAnalytics.EnableAutoTrack(TDAutoTrackEventType.AppCrash, new Dictionary<string, object>() 
                {
                    { "accountId", this.accoundId }
                });
            }

            loginTime = DateTime.Now;
        }

        /// <summary>
        /// 广告开始播放
        /// </summary>
        /// <param name="adtype">广告类型</param>
        /// <param name="diasource">广告位</param>
        /// <param name="ecpm">ecpm</param>
        /// <param name="platform">广告平台</param>
        /// <param name="precision">广告精度</param>
        public void AdStart(EAdtype adtype, EAdSource diasource, double ecpm, string platform, string precision)
        {
            if (!isOpenTD) return;

            Dictionary<string, object> properties = new Dictionary<string, object>();
            properties.Add("adtype", adtype.ToString());
            properties.Add("diasource", diasource.ToString());
            properties.Add("ecpm", ecpm);
            properties.Add("platform", platform);
            properties.Add("precision", precision);

            //SetUserMD();
            TDAnalytics.Track("LG_AdStart", properties);

            AdTime = DateTime.Now;
        }

        /// <summary>
        /// 广告观看失败
        /// </summary>
        /// <param name="adtype">广告类型</param>
        /// <param name="diasource">广告位</param>
        /// <param name="ecpm">ecpm</param>
        /// <param name="platform">广告平台</param>
        /// <param name="precision">广告精度</param>
        public void AdFail(EAdtype adtype, EAdSource diasource, string errmsg, string platform, string precision)
        {
            if (!isOpenTD) return;

            Dictionary<string, object> properties = new Dictionary<string, object>();
            properties.Add("adtype", adtype.ToString());
            properties.Add("diasource", diasource.ToString());
            properties.Add("errmsg", errmsg);
            properties.Add("platform", platform);
            properties.Add("precision", precision);

            //SetUserMD();
            TDAnalytics.Track("LG_AdFail", properties);
        }

        /// <summary>
        /// 广告观看成功
        /// </summary>
        /// <param name="adtype">广告类型</param>
        /// <param name="diasource">广告位</param>
        /// <param name="ecpm">ecpm</param>
        /// <param name="platform">广告平台</param>
        /// <param name="precision">广告精度</param>
        public void AdComplete(EAdtype adtype, EAdSource diasource, double ecpm, string platform, string precision)
        {
            if (!isOpenTD) return;

            Dictionary<string, object> properties = new Dictionary<string, object>();
            properties.Add("adtype", adtype.ToString());
            properties.Add("diasource", diasource.ToString());
            properties.Add("ecpm", ecpm);
            properties.Add("platform", platform);
            properties.Add("precision", precision);

            //SetUserMD();
            TDAnalytics.Track("TGM_AdComplete", properties);

            TotalAdData((int)Math.Round((DateTime.Now - AdTime).TotalSeconds), ecpm / 1000);
        }

        /// <summary>
        /// 广告收入发放成功
        /// </summary>
        /// <param name="revenue">广告收入</param>
        /// <param name="precision">广告精度</param>
        /// <param name="diasource">广告位</param>
        /// <param name="platform">广告平台</param>
        public void AdRevenuePaid(double revenue, string precision, EAdSource diasource,string platform)
        {
            if (!isOpenTD) return;

            Dictionary<string, object> properties = new Dictionary<string, object>();
            properties.Add("revenue", revenue);
            properties.Add("precision", precision);
            properties.Add("precision", diasource);
            properties.Add("platform", platform);

            //SetUserMD();
            TDAnalytics.Track("TGM_AdRevenuePaid", properties);
        }

        /// <summary>
        /// 5次广告收入成功时累计收入
        /// </summary>
        /// <param name="accu_revenue">累计值</param>
        public void Times_5_Ad(double accu_revenue)
        {
            if (!isOpenTD) return;

            Dictionary<string, object> properties = new Dictionary<string, object>();
            properties.Add("accu_revenue", accu_revenue);

            TDAnalytics.Track("LG_Times_5_Ad", properties);
        }

        /// <summary>
        /// 10次广告收入成功时累计收入
        /// </summary>
        /// <param name="accu_revenue">累计值</param>
        public void Times_10_Ad(double accu_revenue)
        {
            if (!isOpenTD) return;

            Dictionary<string, object> properties = new Dictionary<string, object>();
            properties.Add("accu_revenue", accu_revenue);

            TDAnalytics.Track("LG_Times_10_Ad", properties);
        }

        /// <summary>
        /// 15次广告收入成功时累计收入
        /// </summary>
        /// <param name="accu_revenue">累计值</param>
        public void Times_15_Ad(double accu_revenue)
        {
            if (!isOpenTD) return;

            Dictionary<string, object> properties = new Dictionary<string, object>();
            properties.Add("accu_revenue", accu_revenue);

            TDAnalytics.Track("LG_Times_15_Ad", properties);
        }

        /// <summary>
        /// 20次广告收入成功时累计收入
        /// </summary>
        /// <param name="accu_revenue">累计值</param>
        public void Times_20_Ad(double accu_revenue)
        {
            if (!isOpenTD) return;

            Dictionary<string, object> properties = new Dictionary<string, object>();
            properties.Add("accu_revenue", accu_revenue);

            TDAnalytics.Track("LG_Times_20_Ad", properties);
        }

        /// <summary>
        /// 注册成功，首次登录
        /// </summary>
        /// <param name="firstCheckId">首次事件校验</param>
        /// <param name="regtime">注册时间</param>
        public void RegisterFinish(string firstCheckId, DateTime regtime)
        {
            if (!isOpenTD) return;
            
            Dictionary<string, object> properties = new Dictionary<string, object>();
            //properties.Add("#first_check_id", firstCheckId);
            properties.Add("#first_check_id", SystemInfo.deviceUniqueIdentifier);
            properties.Add("regtime", regtime);

            TDAnalytics.Track("LG_RegisterFinish", properties);
        }

        /// <summary>
        /// 加载开始
        /// </summary>
        public void LoadingStart()
        {
            if (!isOpenTD) return;

            Dictionary<string, object> properties = new Dictionary<string, object>();

            TDAnalytics.Track("LG_LoadingStart", properties);
        }

        /// <summary>
        /// 加载完成
        /// </summary>
        public void LoadFinish()
        {
            if (!isOpenTD) return;

            Dictionary<string, object> properties = new Dictionary<string, object>();

            TDAnalytics.Track("LG_LoadFinish", properties);
        }

        /// <summary>
        /// 进入主界面
        /// </summary>
        public void EnterMainUI()
        {
            if (!isOpenTD) return;

            Dictionary<string, object> properties = new Dictionary<string, object>();

            TDAnalytics.Track("LG_EnterMainUI", properties);
        }

        /// <summary>
        /// 登录成功
        /// </summary>
        public void LoginSuccess()
        {
            if (!isOpenTD) return;

            Dictionary<string, object> properties = new Dictionary<string, object>();
            properties.Add("data", GameDefines.ifIAA);

            TDAnalytics.Track("LG_LoginSuccess", properties);

            First_Register_And_Login();
            Last_Login();

        }

        /// <summary>
        /// 当前引导进度
        /// </summary>
        /// <param name="step">当前引导步骤</param>
        public void GuideStep(int step)
        {
            if (!isOpenTD) return;

            Dictionary<string, object> properties = new Dictionary<string, object>();
            properties.Add("step", step * 100);

            TDAnalytics.Track("LG_GuideStep", properties);
        }

        /// <summary>
        /// 点击btn
        /// </summary>
        /// <param name="buttonpath">btn对应路径</param>
        public void ButtonClick(GameObject buttonObj)
        {
            if (!isOpenTD) return;
            if (buttonObj == null) return;
            List<string> pathList = new List<string>();
            pathList.Add(buttonObj.name);
            Transform trans = buttonObj.transform.parent;
            while (trans.parent != null)
            {
                pathList.Add(trans.gameObject.name);
                trans = trans.parent;
            }
            pathList.Reverse();
            Dictionary<string, object> properties = new Dictionary<string, object>();
            properties.Add("buttonPath", string.Join("/", pathList));

            TDAnalytics.Track("LG_ButtonClick", properties);
        }

        //Kwai激励广告开始
        public void KwaiAdNameAndIncomeStart(string kAdName, float kAdIncome)
        {
            
        }

        //Kwai激励广告结束
        public void KwaiAdNameAndIncomeEnd(string kAdName, float kAdIncome)
        {

        }

        //MAX插屏广告开始
        public void IAdNameAndIncomeStart(string adName, float adIncome)
        {
            
        }

        //MAX插屏广告结束
        public void IAdNameAndIncomeEnd(string networkName, float revenue)
        {
            
        }

        //MAX激励广告开始
        public void RAdNameAndIncomeStart(string rAdName, float rAdIncome)
        {
            
        }

        //MAX激励广告结束
        public void RAdNameAndIncomeEnd(string networkName, float revenue)
        {
            
        }


        /// <summary>
        /// 第一次注册和登录（第一次进入游戏）
        /// </summary>
        public void First_Register_And_Login()
        {
            TDAnalytics.UserSetOnce(new Dictionary<string, object>()
            {
                {"register_time", DateTime.Now},
                {"first_login_time", DateTime.Now}
            });
        }

        /// <summary>
        /// 每次登录时间
        /// </summary>
        public void Last_Login()
        {
            TDAnalytics.UserSetOnce(new Dictionary<string, object>()
            {
                {"last_login_time", DateTime.Now}
            });
        }

        /// <summary>
        /// 登出
        /// </summary>
        public void LoginOut()
        {
            TDAnalytics.UserAdd(new Dictionary<string, object>()
            {
                //{ "total_taptime", 1},
                { "total_runtime", (int)Math.Round((DateTime.Now - loginTime).TotalSeconds)},
            });

            TDAnalytics.UserSet(new Dictionary<string, object>()
            {
                {"current_money", PlayerFacade.GetWMoney()},
                {"current_runtime", (int)Math.Round((DateTime.Now - loginTime).TotalSeconds)},
            });
        }

        /// <summary>
        /// 累计广告数据
        /// </summary>
        public void TotalAdData(float time, double revenue)
        {
            TDAnalytics.UserSetOnce(new Dictionary<string, object>()
            {
                {"total_ad_num", 1},
                {"total_ad_time", time},
                {"total_ad_revenue", revenue},
            });
        }

        /// <summary>
        /// 热力引擎归因信息
        /// </summary>
        /// <param name="attribution">Adjust归因信息</param>
        public void SolarEngineMsg(Dictionary<string, object> msg)
        {
            TDAnalytics.UserSetOnce(msg);
        }
    }

    public class LoginOut2 : TDAutoTrackEventHandler
    {
        public Dictionary<string, object> GetAutoTrackEventProperties(int type, Dictionary<string, object> properties)
        {
            ModuleMgr.Instance.TDAnalyticsManager.LoginOut();

            return new Dictionary<string, object>()
            {
                {"AutoTrackEventProperty", DateTime.Today}
            };
        }
    }
}