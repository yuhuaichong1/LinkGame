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
        private string GameAppId = "505001";

        
        private DateTime AdTime;//���ʱ��ͳ��
        private DateTime loginTime;//��¼ʱ��ʱ��

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
                SPlayerPrefs.SetString("GoodMatch_accoundId", this.accoundId);
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
        /// ��濪ʼ����
        /// </summary>
        /// <param name="adtype">�������</param>
        /// <param name="diasource">���λ</param>
        /// <param name="ecpm">ecpm</param>
        /// <param name="platform">���ƽ̨</param>
        /// <param name="precision">��澫��</param>
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
        /// ���ۿ�ʧ��
        /// </summary>
        /// <param name="adtype">�������</param>
        /// <param name="diasource">���λ</param>
        /// <param name="ecpm">ecpm</param>
        /// <param name="platform">���ƽ̨</param>
        /// <param name="precision">��澫��</param>
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
        /// ���ۿ��ɹ�
        /// </summary>
        /// <param name="adtype">�������</param>
        /// <param name="diasource">���λ</param>
        /// <param name="ecpm">ecpm</param>
        /// <param name="platform">���ƽ̨</param>
        /// <param name="precision">��澫��</param>
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
        /// ������뷢�ųɹ�
        /// </summary>
        /// <param name="revenue">�������</param>
        /// <param name="precision">��澫��</param>
        /// <param name="diasource">���λ</param>
        /// <param name="platform">���ƽ̨</param>
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
        /// 5�ι������ɹ�ʱ�ۼ�����
        /// </summary>
        /// <param name="accu_revenue">�ۼ�ֵ</param>
        public void Times_5_Ad(double accu_revenue)
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
        public void Times_10_Ad(double accu_revenue)
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
        public void Times_15_Ad(double accu_revenue)
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
        public void Times_20_Ad(double accu_revenue)
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
            //properties.Add("#first_check_id", firstCheckId);
            properties.Add("#first_check_id", SystemInfo.deviceUniqueIdentifier);
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
            properties.Add("data", GameDefines.ifIAA);

            TDAnalytics.Track("LG_LoginSuccess", properties);

            First_Register_And_Login();
            Last_Login();

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
        /// �ۼƹ������
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
        /// �������������Ϣ
        /// </summary>
        /// <param name="attribution">Adjust������Ϣ</param>
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