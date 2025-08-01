using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
//using ThinkingData.Analytics;
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
        private string GameAppId = "405001";

        protected override void OnLoad()
        {
            //GameLoad(PlayerPrefs.GetString("accoundId"));
        }

        //游戏加载
        public void GameLoad(string accoundId)
        {
            //if (!isOpenTD) return;
            ////设置公共事件属性以后，每个事件都会带有公共事件属性
            //Dictionary<string, object> superProperties = new Dictionary<string, object>();
            //superProperties["GameVersion"] = GameVersion;
            //superProperties["GameAppId"] = GameAppId;
            //if (this.accoundId != "")
            //{
            //    this.accoundId = accoundId;
            //    //superProperties["accountId"] = accoundId;//字符串
            //    TDAnalytics.Login(accoundId);
            //}
            //else
            //{
            //    this.accoundId = UnityEngine.Random.Range(1000000, 9999999).ToString();
            //    PlayerPrefs.SetString("GoodMatch_accoundId", this.accoundId);
            //    //superProperties["accountId"] = accoundId;//字符串
            //    RegisterFinish(accoundId);
            //}
            //TDAnalytics.SetSuperProperties(superProperties);//设置公共事件属性
            //TDAnalytics.UserSet(new Dictionary<string, object>() { { "accountId", this.accoundId }, { "GameVersion", GameVersion }, { "GameAppId", GameAppId } }); //设置用户属性

            //if (isOpenTD)
            //{
            //    TDAnalytics.EnableAutoTrack(TDAutoTrackEventType.AppStart, new Dictionary<string, object>()
            //{
            //    { "accountId", this.accoundId }
            //});
            //    TDAnalytics.EnableAutoTrack(TDAutoTrackEventType.AppEnd, new Dictionary<string, object>()
            //{
            //    { "accountId", this.accoundId }
            //});
            //}
        }

        //例子，请勿调用
        //public void Example(int ex)
        //{
        //    if (!isOpenTD) return;
        //    Dictionary<string, object> properties = new Dictionary<string, object>();
        //    properties.Add("ex", ex);
        //    TDAnalytics.Track("ExampleKey", properties);
        //}

        /// <summary>
        /// 广告开始播放
        /// </summary>
        /// <param name="adtype">广告类型</param>
        /// <param name="diasource">广告位</param>
        /// <param name="ecpm">ecpm</param>
        /// <param name="platform">广告平台</param>
        public void AdStart(EAdtype adtype, EAdSource diasource, float ecpm, string platform)
        {
            if (!isOpenTD) return;

            Dictionary<string, object> properties = new Dictionary<string, object>();
            properties.Add("adtype", adtype.ToString());
            properties.Add("diasource", diasource.ToString());
            properties.Add("ecpm", ecpm);
            properties.Add("platform", platform);

            //SetUserMD();
            //TDAnalytics.Track("LG_AdStart", properties);
        }

        /// <summary>
        /// 广告观看失败
        /// </summary>
        /// <param name="adtype">广告类型</param>
        /// <param name="diasource">广告位</param>
        /// <param name="ecpm">ecpm</param>
        /// <param name="platform">广告平台</param>
        public void AdFail(EAdtype adtype, EAdSource diasource, float ecpm, string platform)
        {
            if (!isOpenTD) return;

            Dictionary<string, object> properties = new Dictionary<string, object>();
            properties.Add("adtype", adtype.ToString());
            properties.Add("diasource", diasource.ToString());
            properties.Add("ecpm", ecpm);
            properties.Add("platform", platform);

            //SetUserMD();
            //TDAnalytics.Track("LG_AdFail", properties);
        }

        /// <summary>
        /// 广告观看成功
        /// </summary>
        /// <param name="adtype">广告类型</param>
        /// <param name="diasource">广告位</param>
        /// <param name="ecpm">ecpm</param>
        /// <param name="platform">广告平台</param>
        public void AdComplete(EAdtype adtype, EAdSource diasource, float ecpm, string platform)
        {
            if (!isOpenTD) return;

            Dictionary<string, object> properties = new Dictionary<string, object>();
            properties.Add("adtype", adtype.ToString());
            properties.Add("diasource", diasource.ToString());
            properties.Add("ecpm", ecpm);
            properties.Add("platform", platform);

            //SetUserMD();
            //TDAnalytics.Track("TGM_AdComplete", properties);
        }

        /// <summary>
        /// 5次广告收入成功时累计收入
        /// </summary>
        /// <param name="accu_revenue">累计值</param>
        public void Times_5_Ad(float accu_revenue)
        {
            if (!isOpenTD) return;

            Dictionary<string, object> properties = new Dictionary<string, object>();
            properties.Add("accu_revenue", accu_revenue);

            //TDAnalytics.Track("LG_Times_5_Ad", properties);
        }

        /// <summary>
        /// 10次广告收入成功时累计收入
        /// </summary>
        /// <param name="accu_revenue">累计值</param>
        public void Times_10_Ad(float accu_revenue)
        {
            if (!isOpenTD) return;

            Dictionary<string, object> properties = new Dictionary<string, object>();
            properties.Add("accu_revenue", accu_revenue);

            //TDAnalytics.Track("LG_Times_10_Ad", properties);
        }

        /// <summary>
        /// 15次广告收入成功时累计收入
        /// </summary>
        /// <param name="accu_revenue">累计值</param>
        public void Times_15_Ad(float accu_revenue)
        {
            if (!isOpenTD) return;

            Dictionary<string, object> properties = new Dictionary<string, object>();
            properties.Add("accu_revenue", accu_revenue);

            //TDAnalytics.Track("LG_Times_15_Ad", properties);
        }

        /// <summary>
        /// 20次广告收入成功时累计收入
        /// </summary>
        /// <param name="accu_revenue">累计值</param>
        public void Times_20_Ad(float accu_revenue)
        {
            if (!isOpenTD) return;

            Dictionary<string, object> properties = new Dictionary<string, object>();
            properties.Add("accu_revenue", accu_revenue);

            //TDAnalytics.Track("LG_Times_20_Ad", properties);
        }
    
        public void RegisterFinish()
        {

        }

        public void LoadingStart()
        {

        }

        public void LoadFinish()
        {

        }

        public void EnterMainUI()
        {

        }

        public void LoginSuccess()
        {

        }

        public void GuideStep(int step)
        {
            if (!isOpenTD) return;

            Dictionary<string, object> properties = new Dictionary<string, object>();
            properties.Add("step", step);

            //TDAnalytics.Track("LG_GuideStep", properties);
        }

        public void ButtonClick(string buttonpath)
        {

        }
    }
}