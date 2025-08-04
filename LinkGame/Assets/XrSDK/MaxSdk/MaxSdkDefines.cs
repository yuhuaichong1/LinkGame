using System;

// Max SDK �ӿڶ���
public static class MaxSdkDefines
{
    #region �������
    public static Action ShowInterstitial;                                                  //չʾ�������

    public static Action InterstitialLoadedEvent;                                           //������������ϣ�����չʾ��
    public static Action<string, MaxSdkBase.ErrorCode> InterstitialFailedEvent;             //����������ʧ��
    public static Action<string, MaxSdkBase.ErrorCode> InterstitialFailedToDisplayEvent;    //���������ʾʧ�ܣ��Զ�������һ�����
    public static Action<double> InterstitialDismissedEvent;                                //������������أ��Զ�������һ�����

    public static Action PlayInterTimer;                                                    //��ʼ�������60s��ʱ
    public static Action PauseInterTimer;                                                   //��ͣ�������60s��ʱ
    #endregion

    #region ������Ƶ
    public static Action ShowRewardedAd;                                                    //չʾ������Ƶ

    public static Action RewardedAdLoadedEvent;                                             //������Ƶ������ϣ�����չʾ
    public static Action<string, MaxSdkBase.ErrorCode> RewardedAdFailedEvent;               //������Ƶ����ʧ��
    public static Action<string, MaxSdkBase.ErrorCode> RewardedAdFailedToDisplayEvent;      //������Ƶ��ʾʧ�ܣ����������һ�����
    public static Action RewardedAdDismissedEvent;                                          //������Ƶ�Ѿ����أ���ʼ������һ��������Ƶ
    public static Action<double> RewardedAdReceivedRewardEvent;                             //������Ƶչʾ��ϣ����Ӧ�û�ý���

    public static Func<double> GetRewardedAdRevenue;                                         //��ȡ������Ƶ��ecpm
    public static Action failShowAdButReward;                                                //���δ��ȡʱ�Ļص�
    public static Action stopfailShowAdButReward;                                            //���failShowAdButReward��ʱ�п��Լ��ع���ˣ���Ӧ���жϼ�ʱ
    #endregion

    #region ������
    public static Action ToggleBannerVisibility;                                            //������չʾor�ر�
    public static Action BannerAdLoadedEvent;                                               //���������ʧ��
    public static Action<string, MaxSdkBase.ErrorCode> BannerAdFailedEvent;                 //���������ʧ��


    #endregion

    #region MREC
    public static Action ToggleMRecVisibility;                                              //չʾMREC���
    public static Action MRecAdLoadedEvent;                                                 //MREC���������
    public static Action<string, MaxSdkBase.ErrorCode> MRecAdFailedEvent;                   //MREC������ʧ��

    #endregion
}
