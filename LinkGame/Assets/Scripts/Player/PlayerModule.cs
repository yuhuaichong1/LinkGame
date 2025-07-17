using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using XrCode;

namespace XrCode
{
    public class PlayerModule : BaseModule
    {
        private LanguageModule LanguageModule;

        private float wMoney;//��ҵ�ǰ�Ľ�Ǯ��
        private EPayType wPayType;//��Ҷ��ֽ�����
        private string wName;//��Ҷ��ֽ�����������
        private string wPhoneOrEmail;//��Ҷ��ֽ����������䣨��绰��
        private int userLevel;//��ҵȼ�
        private int curUserExp;//��Ҿ���

        private char[] nameChars;//����������char��

        protected override void OnLoad()
        {
            base.OnLoad();

            LanguageModule = new LanguageModule();

            PlayerFacade.GetWMoney += GetWMoney;
            PlayerFacade.GetPayType += GetPayType;
            PlayerFacade.GetWName += GetWName;
            PlayerFacade.GetWEmailOrPhone += GetWEmailOrPhone;
            PlayerFacade.GetUserLevel += GetUserLevel;
            PlayerFacade.GetCurUserExp += GetCurUserExp;
            PlayerFacade.SetWMoney += SetWMoney;
            PlayerFacade.AddWMoney += AddWMoney;
            PlayerFacade.SetPayType += SetPayType;
            PlayerFacade.SetNameAndWEmailOrPhone += SetNameAndPhoneOrEmail;
            PlayerFacade.SetUserLevel += SetUserLevel;
            PlayerFacade.SetCurUserExp += SetCurUserExp;
            PlayerFacade.GetRandomPlayerName += GetRandomPlayerName;
            PlayerFacade.GetRandomAttemptAndMoney += GetRandomAttemptAndMoney;

            wMoney = SPlayerPref.GetFloat(PlayerPrefDefines.moneyCount);
            wPayType = (EPayType)SPlayerPref.GetInt(PlayerPrefDefines.wPayType);
            wName = SPlayerPref.GetString(PlayerPrefDefines.wName);
            wPhoneOrEmail = SPlayerPref.GetString(PlayerPrefDefines.wPhoneOrEmail);
            userLevel = SPlayerPref.GetInt(PlayerPrefDefines.userLevel);
            curUserExp = SPlayerPref.GetInt(PlayerPrefDefines.curUserExp);

            if (userLevel == 0)
                SetUserLevel(1);
        }

        protected override void OnDispose()
        {
            base.OnDispose();
        }


        #region GetValue
        private float GetWMoney()
        {
            return wMoney;
        }

        private EPayType GetPayType()
        {
            return wPayType;
        }

        private string GetWName() 
        {
            return wName;
        }

        private string GetWEmailOrPhone()
        {
            return wPhoneOrEmail;
        }
        
        private int GetUserLevel()
        {
            return userLevel;
        }

        private int GetCurUserExp()
        {
            return curUserExp;
        }
        #endregion

        #region SetValue
        private void SetWMoney(float value) 
        { 
            wMoney = value;
            SPlayerPref.SetFloat(PlayerPrefDefines.moneyCount, value);
        }

        private void AddWMoney(float value)
        {
            wMoney += value;
            SPlayerPref.SetFloat(PlayerPrefDefines.moneyCount, wMoney);
        }

        private void SetPayType(EPayType payType)
        {
            wPayType = payType;
            SPlayerPref.SetInt(PlayerPrefDefines.wPayType, (int)payType);
        }

        private void SetNameAndPhoneOrEmail(string name, string PhoneOrEmail)
        {
            wName = name;
            wPhoneOrEmail = PhoneOrEmail;
            SPlayerPref.SetString(PlayerPrefDefines.wName, name);
            SPlayerPref.SetString(PlayerPrefDefines.wPhoneOrEmail, PhoneOrEmail);
        }

        private void SetUserLevel(int level) 
        { 
            userLevel = level;
            SPlayerPref.SetInt(PlayerPrefDefines.userLevel, level);
        }

        private void SetCurUserExp(int exp)
        {
            curUserExp = exp;
            SPlayerPref.SetInt(PlayerPrefDefines.curUserExp, exp);
        }
        #endregion
    

        /// <summary>
        /// ��ȡ����������
        /// </summary>
        /// <returns>����������</returns>
        private string GetRandomPlayerName()
        {
            nameChars = GameDefines.nameString.ToCharArray();
            int length = nameChars.Length;
            char c1 = nameChars[UnityEngine.Random.Range(0, length)];
            char c2 = nameChars[UnityEngine.Random.Range(0, length)];
            char c3 = nameChars[UnityEngine.Random.Range(0, length)];

            return $"{LanguageModule.GetText("")}_{c1}{c2}{c3}";
        }

        /// <summary>
        /// ��ȡ������Դ�����������ֽ��
        /// </summary>
        /// <returns>������Դ�����������ֽ��</returns>
        private string[] GetRandomAttemptAndMoney()
        {
            int attempt = UnityEngine.Random.Range(1, 4);
            float money = attempt * UnityEngine.Random.Range(1f, 3f);
            string aText = attempt.ToString();
            string mText = FacadePayType.RegionalChange(money);
            //string mText = string.Format(LanguageModule.GetText(""), money);

            return new string[2] { aText, mText };
        }
    }
}


