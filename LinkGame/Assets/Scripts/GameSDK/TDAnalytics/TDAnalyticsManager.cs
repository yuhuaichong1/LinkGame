using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
//using ThinkingData.Analytics;
using UnityEngine;
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
    }
}