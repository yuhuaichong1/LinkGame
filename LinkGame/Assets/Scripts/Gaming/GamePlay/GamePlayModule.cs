using System.Collections.Generic;
using UnityEngine;

namespace XrCode
{
    public class GamePlayModule : BaseModule
    {
        private Dictionary<Vector2, GoodItem> levelData; //当前关卡的数据
        private int curLevel;//当前关卡
        private int tipCount;//提示次数
        private int refushCount;//刷新次数
        private int removeCount;//移除次数

        private int selectedGood;//被选中的物体

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

        //加载数据
        private void LoadData()
        {
            curLevel = PlayerPrefs.GetInt(PlayerPrefDefines.curLevel);
            tipCount = PlayerPrefs.GetInt(PlayerPrefDefines.tipCount);
            refushCount = PlayerPrefs.GetInt(PlayerPrefDefines.refushCount);
            removeCount = PlayerPrefs.GetInt(PlayerPrefDefines.removeCount);
        }

        //创建关卡
        private void CreateLevel()
        {

        }

        #region 连接判断
        //是否能够连接
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

        //判断是否水平
        private bool CheckHV(Vector2 startP, Vector2 endP)
        {
            return startP.x == endP.x || startP.y == endP.y;
        }

        //直线判断
        private bool Angle0(Vector2 startP, Vector2 endP)
        {
            return true;
        }

        //1折判断
        private Vector2[] Angle1(Vector2 startP, Vector2 endP)
        {
            return null;
        }

        //2折判断（起点终点同一行/列时）
        private Vector2[] Angle2_1(Vector2 startP, Vector2 endP)
        {
            return null;
        }

        //2折判断（起点终点不同行/列时）
        private Vector2[] Angle2_2(Vector2 startP, Vector2 endP)
        {
            return null;
        }
        #endregion

        #region 消除周期流程
        //消除目标
        private void LinkAndRemove(Vector2[] linePoint)
        {
            DrawLine(linePoint);
            CheckSutck();
        }

        //画线
        private void DrawLine(Vector2[] linePoint)
        {

        }

        //连续消除时展现鼓励的话（时间判断和无错时弹出）
        private void ShowEncouragement()
        {

        }

        //检测游戏是否卡关
        private void CheckSutck()
        {

        }
        #endregion

        #region 点击物体
        //被选中时闪光
        private void SelectLight()
        {

        }

        //双击抖动
        private void SameKindShake(int kind)
        {

        }
        #endregion

        #region 下三功能
        //提示可消除功能
        private void TipFunc()
        {

        }

        //随机消除功能
        private void RemoveFunc()
        {

        }

        //刷新功能
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