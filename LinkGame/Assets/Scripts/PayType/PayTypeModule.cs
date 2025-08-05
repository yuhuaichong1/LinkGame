using cfg;
using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

namespace XrCode
{
    public class PayTypeModule : BaseModule
    {
        List<PayItem> payItems;//����֧������

        private string mark;//���ҵ�λ
        private int decimals;//��ʾ������С����
        private float exchangeRate;//����
        private int NANP;//���ʳ�;�绰����
        private string countryCode;//������

        protected override void OnLoad()
        {
            base.OnLoad();

            payItems = new List<PayItem>();

            FacadePayType.GetPayItems += GetPayItems;
            FacadePayType.RegionalChange += RegionalChange;
            FacadePayType.GetNANP += GetNANP;
            FacadePayType.GetCountryCode += GetCountryCode;

            CountryCodeToInfo();
        }


        //���ݹ������ȡ���Ժ�֧����Ϣ
        private void CountryCodeToInfo()
        {
            CultureInfo currentCulture = CultureInfo.CurrentCulture;
            countryCode = currentCulture.Name.Split("-")[1];
            List<ConfPayRegion> payRegionList = ConfigModule.Instance.Tables.TBPayRegion.DataList;
            foreach (ConfPayRegion payRegion in payRegionList) 
            {
                if (payRegion.Code == countryCode)
                {
                    string[] pays = payRegion.Channels.Split(',');
                    GetPayTypes(pays);

                    mark = payRegion.Mark;
                    decimals = payRegion.Decimal;
                    exchangeRate = payRegion.ExchangeRate;

                    ModuleMgr.Instance.LanguageMod.SetLanguage((ELanguageType)Enum.Parse(typeof(ELanguageType), payRegion.Lang));

                    NANP = payRegion.NANP;

                    return;
                }
                
            }

            //Ĭ�������ȡֵ
            string[] defPays = GameDefines.Default_Channels.Split(',');
            GetPayTypes(defPays);
            mark = GameDefines.Default_Mark;
            decimals = GameDefines.Default_Decimal;
            exchangeRate = GameDefines.Default_ExchangeRate;
            ModuleMgr.Instance.LanguageMod.SetLanguage(GameDefines.Default_Language);
            NANP = GameDefines.Default_NANP;

        }

        //�õ��õ�����֧����Ϣ
        private void GetPayTypes(string[] pays)
        {
            foreach (string pay in pays) 
            {
                ConfPayChannel pc = ConfigModule.Instance.Tables.TBPayChannel.Get(int.Parse(pay));
                payItems.Add(new PayItem
                {
                    payType = (EPayType)(pc.Sn + 1),
                    info = pc.Info,
                    picture = ResourceMod.Instance.SyncLoad<Sprite>(pc.PicPath),
                    icon = ResourceMod.Instance.SyncLoad<Sprite>(pc.IconPath),
                });
            }
        }
    
        //��ȡ֧������
        private List<PayItem> GetPayItems()
        {
            return payItems;
        }

        //��ֵ�Ի��ʵķ�ʽ��ʾ
        private string RegionalChange(float value)
        {
            if (GameDefines.ifWithdrawal)
            {
                value *= exchangeRate;
                string deci = $"F{decimals}";
                string str = $"{mark}{value.ToString(deci)}";
                return str;
            }
            else
            {
                return $"{Mathf.Round(value * 10)}";
            }
        }

        private int GetNANP()
        {
            return NANP;
        }
        private string GetCountryCode()
        {
            return countryCode;
        }
    }
}


