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
            if (!isOpenTD) return;
            //���ù����¼������Ժ�ÿ���¼�������й����¼�����
            Dictionary<string, object> superProperties = new Dictionary<string, object>();
            superProperties["GameVersion"] = GameVersion;
            superProperties["GameAppId"] = GameAppId;
            if (this.accoundId != "")
            {
                this.accoundId = accoundId;
                //superProperties["accountId"] = accoundId;//�ַ���
                TDAnalytics.Login(accoundId);
            }
            else
            {
                this.accoundId = UnityEngine.Random.Range(1000000, 9999999).ToString();
                PlayerPrefs.SetString("GoodMatch_accoundId", this.accoundId);
                //superProperties["accountId"] = accoundId;//�ַ���
                RegisterFinish(accoundId, DateTime.Now);
            }
            TDAnalytics.SetSuperProperties(superProperties);//���ù����¼�����

            TDAnalytics.UserSet(new Dictionary<string, object>() { { "accountId", this.accoundId }, { "GameVersion", GameVersion }, { "GameAppId", GameAppId } }); //�����û�����

            if (isOpenTD)
            {
                TDAnalytics.EnableAutoTrack(TDAutoTrackEventType.AppStart, new Dictionary<string, object>()
                {
                    { "accountId", this.accoundId }
                });
                TDAnalytics.EnableAutoTrack(TDAutoTrackEventType.AppEnd, new Dictionary<string, object>()
                {
                    { "accountId", this.accoundId }
                });
                TDAnalytics.EnableAutoTrack(TDAutoTrackEventType.AppCrash, new Dictionary<string, object>() 
                {
                    { "accountId", this.accoundId }
                });
            }
        }

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
            TDAnalytics.Track("LG_AdStart", properties);
        }

        /// <summary>
        /// ���ۿ�ʧ��
        /// </summary>
        /// <param name="adtype">�������</param>
        /// <param name="diasource">���λ</param>
        /// <param name="ecpm">ecpm</param>
        /// <param name="platform">���ƽ̨</param>
        public void AdFail(EAdtype adtype, EAdSource diasource, string errmsg, string platform)
        {
            if (!isOpenTD) return;

            Dictionary<string, object> properties = new Dictionary<string, object>();
            properties.Add("adtype", adtype.ToString());
            properties.Add("diasource", diasource.ToString());
            properties.Add("errmsg", errmsg);
            properties.Add("platform", platform);

            //SetUserMD();
            TDAnalytics.Track("LG_AdFail", properties);
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
            TDAnalytics.Track("TGM_AdComplete", properties);
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

            TDAnalytics.Track("LG_Times_5_Ad", properties);
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

            TDAnalytics.Track("LG_Times_10_Ad", properties);
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

            TDAnalytics.Track("LG_Times_15_Ad", properties);
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

            TDAnalytics.Track("LG_Times_20_Ad", properties);
        }

        /// <summary>
        /// ע��ɹ����״ε�¼
        /// </summary>
        /// <param name="firstCheckId">�״��¼�У��</param>
        /// <param name="regtime">ע��ʱ��</param>
        public void RegisterFinish(string firstCheckId, DateTime regtime)
        {
            if (!isOpenTD) return;

            Dictionary<string, object> properties = new Dictionary<string, object>();
            properties.Add("#first_check_id", firstCheckId);
            properties.Add("regtime", regtime);

            TDAnalytics.Track("LG_RegisterFinish", properties);
        }

        /// <summary>
        /// ���ؿ�ʼ
        /// </summary>
        public void LoadingStart()
        {
            if (!isOpenTD) return;

            Dictionary<string, object> properties = new Dictionary<string, object>();

            TDAnalytics.Track("LG_LoadingStart", properties);
        }

        /// <summary>
        /// �������
        /// </summary>
        public void LoadFinish()
        {
            if (!isOpenTD) return;

            Dictionary<string, object> properties = new Dictionary<string, object>();

            TDAnalytics.Track("LG_LoadFinish", properties);
        }

        /// <summary>
        /// ����������
        /// </summary>
        public void EnterMainUI()
        {
            if (!isOpenTD) return;

            Dictionary<string, object> properties = new Dictionary<string, object>();

            TDAnalytics.Track("LG_EnterMainUI", properties);
        }

        /// <summary>
        /// ��¼�ɹ�
        /// </summary>
        public void LoginSuccess()
        {
            if (!isOpenTD) return;

            Dictionary<string, object> properties = new Dictionary<string, object>();

            TDAnalytics.Track("LG_LoginSuccess", properties);
        }

        /// <summary>
        /// ��ǰ��������
        /// </summary>
        /// <param name="step">��ǰ��������</param>
        public void GuideStep(int step)
        {
            if (!isOpenTD) return;

            Dictionary<string, object> properties = new Dictionary<string, object>();
            properties.Add("step", step * 100);

            TDAnalytics.Track("LG_GuideStep", properties);
        }

        /// <summary>
        /// ���btn
        /// </summary>
        /// <param name="buttonpath">btn��Ӧ·��</param>
        public void ButtonClick(string buttonpath)
        {
            if (!isOpenTD) return;

            Dictionary<string, object> properties = new Dictionary<string, object>();
            properties.Add("buttonpath", buttonpath);

            TDAnalytics.Track("LG_ButtonClick", properties);
        }

        //Kwai������濪ʼ
        public void KwaiAdNameAndIncomeStart(string kAdName, float kAdIncome)
        {
            
        }

        //Kwai����������
        public void KwaiAdNameAndIncomeEnd(string kAdName, float kAdIncome)
        {

        }

        //MAX������濪ʼ
        public void IAdNameAndIncomeStart(string adName, float adIncome)
        {
            
        }

        //MAX����������
        public void IAdNameAndIncomeEnd(string networkName, float revenue)
        {
            
        }

        //MAX������濪ʼ
        public void RAdNameAndIncomeStart(string rAdName, float rAdIncome)
        {
            
        }

        //MAX����������
        public void RAdNameAndIncomeEnd(string networkName, float revenue)
        {
            
        }


        /// <summary>
        /// ��һ��ע��͵�¼����һ�ν�����Ϸ��
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
        /// ÿ�ε�¼ʱ��
        /// </summary>
        public void Last_Login()
        {
            TDAnalytics.UserSetOnce(new Dictionary<string, object>()
            {
                {"last_login_time", DateTime.Now}
            });
        }

        /// <summary>
        /// �ǳ�
        /// </summary>
        public void LoginOut()
        {
            TDAnalytics.UserSetOnce(new Dictionary<string, object>()
            {
                {"total_taptime", 1},
                {"total_runtime", 1},
                {"current_money", 1},
                {"current_runtime", 1},
            });
        }

        /// <summary>
        /// �ۼƹ������
        /// </summary>
        public void TotalAdData()
        {
            TDAnalytics.UserSetOnce(new Dictionary<string, object>()
            {
                {"total_ad_num", 1},
                {"total_ad_time", 1},
                {"total_ad_revenue", 1},
            });
        }

        /// <summary>
        /// �������������Ϣ
        /// </summary>
        /// <param name="attribution">Adjust������Ϣ</param>
        public void SolarEngineMsg(Dictionary<string, object> msg)
        {
            TDAnalytics.UserSetOnce(msg);
        }
    }
}