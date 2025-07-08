using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XrCode;

namespace XrCode
{
    public class PlayerModule : BaseModule
    {
        public float wMoney;//��ҵ�ǰ�Ľ�Ǯ��
        public string wName;//��Ҷ��ֽ�����������
        public int wPhone;//��Ҷ��ֽ�������绰
        public string wEmail;//��Ҷ��ֽ����������䣨��绰��

        protected override void OnLoad()
        {
            base.OnLoad();

            PlayerFacade.GetWMoney += GetWMoney;
            PlayerFacade.GetWName += GetWName;
            PlayerFacade.GetWPhone += GetWPhone;
            PlayerFacade.GetEmail += GetEmail;
            PlayerFacade.SetNameAndPhone += SetNameAndPhone;
            PlayerFacade.SetNameAndEmail += SetNameAndEmail;
            PlayerFacade.SetEmail += SetEmail;

            wMoney = SPlayerPref.GetFloat(PlayerPrefDefines.moneyCount);
            wName = SPlayerPref.GetString(PlayerPrefDefines.wName);
            wPhone = SPlayerPref.GetInt(PlayerPrefDefines.wPhone);
            wEmail = SPlayerPref.GetString(PlayerPrefDefines.wEmail);
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
            return wName;
        }

        private int GetWPhone() 
        {
            return wPhone;
        }

        private string GetEmail()
        {
            return wEmail;
        }

        private void SetNameAndPhone(string name, int phone)
        {
            wName = name;
            wPhone = phone;
            SPlayerPref.SetString(PlayerPrefDefines.wName, name);
            SPlayerPref.SetInt(PlayerPrefDefines.wPhone, phone);
        }

        private void SetNameAndEmail(string name, string email)
        {
            wName = name;
            wEmail = email;
            SPlayerPref.SetString(PlayerPrefDefines.wName, name);
            SPlayerPref.SetString(PlayerPrefDefines.wEmail, email);
        }

        private void SetEmail(string email)
        {
            wEmail = email;
            SPlayerPref.SetString(PlayerPrefDefines.wEmail, email);
        }

    }
}


