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
        //����
#if UNITY_EDITOR
        bool isOpenTD = false;
#else
    bool isOpenTD = true;
#endif

        //���id
        private string accoundId = "";
        //��Ϸ�汾
        private string GameVersion = "1.0.0.0";
        //��Ϸ��ţ���������ƽ̨
        private string GameAppId = "405001";

        protected override void OnLoad()
        {
            //GameLoad(PlayerPrefs.GetString("accoundId"));
        }

        //��Ϸ����
        public void GameLoad(string accoundId)
        {
            //if (!isOpenTD) return;
            ////���ù����¼������Ժ�ÿ���¼�������й����¼�����
            //Dictionary<string, object> superProperties = new Dictionary<string, object>();
            //superProperties["GameVersion"] = GameVersion;
            //superProperties["GameAppId"] = GameAppId;
            //if (this.accoundId != "")
            //{
            //    this.accoundId = accoundId;
            //    //superProperties["accountId"] = accoundId;//�ַ���
            //    TDAnalytics.Login(accoundId);
            //}
            //else
            //{
            //    this.accoundId = UnityEngine.Random.Range(1000000, 9999999).ToString();
            //    PlayerPrefs.SetString("GoodMatch_accoundId", this.accoundId);
            //    //superProperties["accountId"] = accoundId;//�ַ���
            //    RegisterFinish(accoundId);
            //}
            //TDAnalytics.SetSuperProperties(superProperties);//���ù����¼�����
            //TDAnalytics.UserSet(new Dictionary<string, object>() { { "accountId", this.accoundId }, { "GameVersion", GameVersion }, { "GameAppId", GameAppId } }); //�����û�����

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

        //���ӣ��������
        //public void Example(int ex)
        //{
        //    if (!isOpenTD) return;
        //    Dictionary<string, object> properties = new Dictionary<string, object>();
        //    properties.Add("ex", ex);
        //    TDAnalytics.Track("ExampleKey", properties);
        //}

        /// <summary>
        /// ��濪ʼ����
        /// </summary>
        /// <param name="adtype">�������</param>
        /// <param name="diasource">���λ</param>
        /// <param name="ecpm">ecpm</param>
        /// <param name="platform">���ƽ̨</param>
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
        /// ���ۿ�ʧ��
        /// </summary>
        /// <param name="adtype">�������</param>
        /// <param name="diasource">���λ</param>
        /// <param name="ecpm">ecpm</param>
        /// <param name="platform">���ƽ̨</param>
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
        /// ���ۿ��ɹ�
        /// </summary>
        /// <param name="adtype">�������</param>
        /// <param name="diasource">���λ</param>
        /// <param name="ecpm">ecpm</param>
        /// <param name="platform">���ƽ̨</param>
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
        /// 5�ι������ɹ�ʱ�ۼ�����
        /// </summary>
        /// <param name="accu_revenue">�ۼ�ֵ</param>
        public void Times_5_Ad(float accu_revenue)
        {
            if (!isOpenTD) return;

            Dictionary<string, object> properties = new Dictionary<string, object>();
            properties.Add("accu_revenue", accu_revenue);

            //TDAnalytics.Track("LG_Times_5_Ad", properties);
        }

        /// <summary>
        /// 10�ι������ɹ�ʱ�ۼ�����
        /// </summary>
        /// <param name="accu_revenue">�ۼ�ֵ</param>
        public void Times_10_Ad(float accu_revenue)
        {
            if (!isOpenTD) return;

            Dictionary<string, object> properties = new Dictionary<string, object>();
            properties.Add("accu_revenue", accu_revenue);

            //TDAnalytics.Track("LG_Times_10_Ad", properties);
        }

        /// <summary>
        /// 15�ι������ɹ�ʱ�ۼ�����
        /// </summary>
        /// <param name="accu_revenue">�ۼ�ֵ</param>
        public void Times_15_Ad(float accu_revenue)
        {
            if (!isOpenTD) return;

            Dictionary<string, object> properties = new Dictionary<string, object>();
            properties.Add("accu_revenue", accu_revenue);

            //TDAnalytics.Track("LG_Times_15_Ad", properties);
        }

        /// <summary>
        /// 20�ι������ɹ�ʱ�ۼ�����
        /// </summary>
        /// <param name="accu_revenue">�ۼ�ֵ</param>
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