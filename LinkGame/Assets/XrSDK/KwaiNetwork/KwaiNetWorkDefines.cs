using System;


public static class KwaiNetWorkDefines
{
    #region ������Ƶ
    public static Action KNW_ShowRewardedAd;                            //չʾ�������

    public static Action LoadRewardAd;                                  //���ؼ������
    public static Action<string, int, string> KNW_OnRAdLoadFailed;      //����������ʧ��
    public static Action<string> KNW_OnRAdLoadStart;                    //������濪ʼ����
    public static Action<string, string> KNW_OnRAdLoadSuccess;          //���������سɹ�

    public static Action KNW_OnRAdClick;                                //������汻���
    public static Action KNW_OnRAdClose;                                //�������ر�
    public static Action KNW_OnRAdPlayComplete;                         //������沥�����
    public static Action KNW_OnRAdShow;                                 //������濪ʼ����
    public static Action<int, string> KNW_OnRAdShowFailed;              //������沥��ʧ��
    public static Action KNW_OnRewardEarned;                            //��������ȡ����

    public static Func<double> GetRewardedAdECPM;                       //��ȡ������Ƶ��ecpm
    public static Func<float> GetRewardedAdRevenue;                    //��ȡ������Ƶ�Ľ���
    public static Func<string> GetRewardedAdName;                       //��ȡ������Ƶ��ƽ̨����
    public static Action failShowAdButReward;                           //���δ��ȡʱ�Ļص�
    public static Action stopfailShowAdButReward;                       //���failShowAdButReward��ʱ�п��Լ��ع���ˣ���Ӧ���жϼ�ʱ

    public static Action PlayInterTimer;                                //��ʼ�������60s��ʱ
    public static Action PauseInterTimer;                               //��ͣ�������60s��ʱ
    #endregion

    #region �������
    public static Action KNW_ShowInterstitial;                          //չʾ�������

    public static Action LoadInterstitial;                              //���ز������
    public static Action<string, int, string> KNW_OnIAdLoadFailed;      //����������ʧ��
    public static Action<string> KNW_OnIAdLoadStart;                    //������濪ʼ����
    public static Action<string, string> KNW_OnIAdLoadSuccess;          //���������سɹ�

    public static Action KNW_OnIAdClick;                                //������汻���
    public static Action KNW_OnIAdClose;                                //�������ر�
    public static Action<double> KNW_OnIAdPlayComplete;                 //������沥�����
    public static Action KNW_OnIAdShow;                                 //������濪ʼ����
    public static Action<int, string> KNW_OnIAdShowFailed;              //������沥��ʧ��


    #endregion
}
