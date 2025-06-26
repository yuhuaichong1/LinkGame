using System.Collections.Generic;
using UnityEngine;

namespace XrCode
{
    public class GamePlayModule : BaseModule
    {
        private Dictionary<Vector2, GoodItem> levelData; //��ǰ�ؿ�������
        private int curLevel;//��ǰ�ؿ�
        private int tipCount;//��ʾ����
        private int refushCount;//ˢ�´���
        private int removeCount;//�Ƴ�����

        private int selectedGood;//��ѡ�е�����

        protected override void OnLoad()
        {
            base.OnLoad();
            levelData = new Dictionary<Vector2, GoodItem>();

            GamePlayFacade.CreateLevel += CreateLevel;
            GamePlayFacade.CheckIfLink += CheckIfLink;
            GamePlayFacade.TipFunc += TipFunc;
            GamePlayFacade.RefushFunc += RefushFunc;
            GamePlayFacade.RemoveFunc += RemoveFunc;
            GamePlayFacade.ChangeTipCount += ChangeTipCount;
            GamePlayFacade.ChangeRefushCount += ChangeRefushCount;
            GamePlayFacade.ChangeRemoveCount += ChangeRemoveCount;
            GamePlayFacade.GetTipCount += GetTipCount;
            GamePlayFacade.GetRefushCount += GetRefushCount;
            GamePlayFacade.GetRemoveCount += GetRemoveCount;

            LoadData();

        }

        //��������
        private void LoadData()
        {
            curLevel = PlayerPrefs.GetInt(PlayerPrefDefines.curLevel);
            tipCount = PlayerPrefs.GetInt(PlayerPrefDefines.tipCount);
            refushCount = PlayerPrefs.GetInt(PlayerPrefDefines.refushCount);
            removeCount = PlayerPrefs.GetInt(PlayerPrefDefines.removeCount);
        }

        //�����ؿ�
        private void CreateLevel()
        {

        }

        #region �����ж�
        //�Ƿ��ܹ�����
        private void CheckIfLink(Vector2 startP, Vector2 endP)
        {
            if (CheckHV(startP, endP))
            {
                if (Angle0(startP, endP))
                {
                    LinkAndRemove(new Vector2[] { startP, endP });
                }
                else
                {
                    LinkAndRemove(Angle2_1(startP, endP));
                }
            }
            else
            {
                Vector2[] linePoints = Angle2_2(startP, endP);
                if (linePoints.Length > 0)
                {
                    LinkAndRemove(linePoints);
                }
                else
                {
                    LinkAndRemove(Angle2_1(startP, endP));
                }
            }
        }

        //�ж��Ƿ�ˮƽ
        private bool CheckHV(Vector2 startP, Vector2 endP)
        {
            return startP.x == endP.x || startP.y == endP.y;
        }

        //ֱ���ж�
        private bool Angle0(Vector2 startP, Vector2 endP)
        {
            return true;
        }

        //1���ж�
        private Vector2[] Angle1(Vector2 startP, Vector2 endP)
        {
            return null;
        }

        //2���жϣ�����յ�ͬһ��/��ʱ��
        private Vector2[] Angle2_1(Vector2 startP, Vector2 endP)
        {
            return null;
        }

        //2���жϣ�����յ㲻ͬ��/��ʱ��
        private Vector2[] Angle2_2(Vector2 startP, Vector2 endP)
        {
            return null;
        }
        #endregion

        #region ������������
        //����Ŀ��
        private void LinkAndRemove(Vector2[] linePoint)
        {
            DrawLine(linePoint);
            CheckSutck();
        }

        //����
        private void DrawLine(Vector2[] linePoint)
        {

        }

        //��������ʱչ�ֹ����Ļ���ʱ���жϺ��޴�ʱ������
        private void ShowEncouragement()
        {

        }

        //�����Ϸ�Ƿ񿨹�
        private void CheckSutck()
        {

        }
        #endregion

        #region �������
        //��ѡ��ʱ����
        private void SelectLight()
        {

        }

        //˫������
        private void SameKindShake(int kind)
        {

        }
        #endregion

        #region ��������
        //��ʾ����������
        private void TipFunc()
        {

        }

        //�����������
        private void RemoveFunc()
        {

        }

        //ˢ�¹���
        private void RefushFunc()
        {

        }

        private void ChangeTipCount(int num)
        {
            tipCount += num;
            PlayerPrefs.SetInt(PlayerPrefDefines.tipCount, tipCount);
        }

        private void ChangeRefushCount(int num)
        {
            refushCount += num;
            PlayerPrefs.SetInt(PlayerPrefDefines.refushCount, refushCount);
        }

        private void ChangeRemoveCount(int num)
        {
            removeCount += num;
            PlayerPrefs.SetInt(PlayerPrefDefines.removeCount, removeCount);
        }

        private int GetTipCount()
        {
            return tipCount;
        }
        private int GetRefushCount() 
        { 
            return refushCount;
        }
        private int GetRemoveCount() 
        { 
            return removeCount;
        }
        #endregion

    }
}