using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XrCode;

namespace XrCode
{
    public class PlayerModule : BaseModule
    {
        private float wMoney;//��ҵ�ǰ�Ľ�Ǯ��
        private string wName;//��Ҷ��ֽ�����������
        private string wPhoneOrEmail;//��Ҷ��ֽ����������䣨��绰��
        private int userLevel;//��ҵȼ�
        private int curUserExp;//��Ҿ���

        protected override void OnLoad()
        {
            base.OnLoad();

            PlayerFacade.GetWMoney += GetWMoney;
            PlayerFacade.GetWName += GetWName;
            PlayerFacade.GetWEmailOrPhone += GetWEmailOrPhone;
            PlayerFacade.GetUserLevel += GetUserLevel;
            PlayerFacade.GetCurUserExp += GetCurUserExp;
            PlayerFacade.SetWMoney += SetWMoney;
            PlayerFacade.SetNameAndWEmailOrPhone += SetNameAndPhoneOrEmail;
            PlayerFacade.SetUserLevel += SetUserLevel;
            PlayerFacade.SetCurUserExp += SetCurUserExp;

            wMoney = SPlayerPref.GetFloat(PlayerPrefDefines.moneyCount);
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

        private float GetWMoney()
        {
            return wMoney;
        }

        private string GetWName() 
        {
            Debug.LogError("wName  " + wName);
            return wName;
        }

        private string GetWEmailOrPhone()
        {
            Debug.LogError("wPhoneOrEmail  " + wPhoneOrEmail);
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

        private void SetWMoney(float value) 
        { 
            wMoney = value;
            SPlayerPref.SetFloat(PlayerPrefDefines.moneyCount, value);
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

    }
}


