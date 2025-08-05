using System;
using static MaxSdkBase;

// Max SDK �ӿڶ���
public static class MaxSdkDefines
{
    #region �������
    public static Action ShowInterstitial;                                                  //չʾ�������

    public static Action<string, AdInfo> OnInterstitialLoadedEvent;                         //���������سɹ�
    public static Action<string, ErrorInfo> OnInterstitialLoadFailedEvent;                  //����������ʧ��
    public static Action<string, AdInfo> OnInterstitialDisplayedEvent;                      //�������չʾ�ɹ�
    public static Action<string, ErrorInfo, AdInfo> OnInterstitialFailedToDisplayEvent;     //�������չʾʧ��
    public static Action<string, AdInfo> OnInterstitialDismissedEvent;                      //������汻����
    public static Action<string, AdInfo> OnInterstitialClickedEvent;                        //������汻���
    public static Action<string, AdInfo> OnInterstitialRevenuePaidEvent;                    //������潱�����轱�����
    #endregion

    #region ������Ƶ
    public static Action ShowRewardedAd;                                                    //չʾ������Ƶ

    public static Action<string, AdInfo> OnRewardedAdLoadedEvent;                           //���������سɹ�
    public static Action<string, ErrorInfo> OnRewardedAdLoadFailedEvent;                    //����������ʧ��
    public static Action<string, AdInfo> OnRewardedAdDisplayedEvent;                        //�������չʾ�ɹ�
    public static Action<string, ErrorInfo, AdInfo> OnRewardedAdFailedToDisplayEvent;       //�������չʾʧ��
    public static Action<string, AdInfo> OnRewardAdClickedEvent;                            //������汻���
    public static Action<string, AdInfo> OnRewardAdHiddenEvent;                             //������汻����
    public static Action<string, Reward, AdInfo> OnRewardAdReceivedRewardEvent;             //�������չʾ��ϣ����轱��
    public static Action<string, AdInfo> OnRewardAdRevenuePaidEvent;                        //������潱�����轱�����

    public static Func<double> GetRewardedAdRevenue;                                         //��ȡ������Ƶ��ecpm
    public static Action RewardAdNotReadyEvent;                                              //�������δ׼����ɻص�
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
