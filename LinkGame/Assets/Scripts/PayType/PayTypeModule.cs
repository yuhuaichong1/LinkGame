using cfg;
using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

namespace XrCode
{
    public class PayTypeModule : BaseModule
    {
        List<PayItem> payItems;//可用支付类型

        private string mark;//货币单位
        private int decimals;//显示暴保留小数点
        private float exchangeRate;//汇率
        private int NANP;//国际长途电话区号
        private string countryCode;//国家码

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


        //根据国家码获取语言和支付信息
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

            //默认情况下取值
            string[] defPays = GameDefines.Default_Channels.Split(',');
            GetPayTypes(defPays);
            mark = GameDefines.Default_Mark;
            decimals = GameDefines.Default_Decimal;
            exchangeRate = GameDefines.Default_ExchangeRate;
            ModuleMgr.Instance.LanguageMod.SetLanguage(GameDefines.Default_Language);
            NANP = GameDefines.Default_NANP;

        }

        //得到该地区的支付信息
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
    
        //获取支付类型
        private List<PayItem> GetPayItems()
        {
            return payItems;
        }

        //将值以汇率的方式显示
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


