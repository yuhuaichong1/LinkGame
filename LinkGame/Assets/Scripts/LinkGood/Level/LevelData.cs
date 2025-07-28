using UnityEngine;
using System.Collections;
using SimpleJSON;
using System.IO;
using cfg;
using XrCode;
using Unity.VisualScripting;
using System.Collections.Generic;

//关卡数据
public class LevelData 
{
	public float time_dead;
	public float time_star1;
	public float time_star2;
	public float time_star3;

	public ArrayList list_auto_gen 				 = new ArrayList();//延迟冰冻的物体
	public ArrayList list_block_good_fixed	     = new ArrayList();//不可移动的一般物体
	public ArrayList list_block_stone_fixed 	 = new ArrayList();//不可移动的石块障碍物
	public ArrayList list_block_stone_moving 	 = new ArrayList();//可移动的石块障碍物
	public ArrayList list_block_frozen_fixed 	 = new ArrayList();//不可移动的冰冻物体
    public ArrayList list_boloc_frozen_moving    = new ArrayList();//可移动的冰冻物体（新添，原始代码中不存在此变量）
    public ArrayList list_block_stone_and_frozen = new ArrayList();//冰冻物体和石块物体
	public ArrayList list_constraint 			 = new ArrayList();//关卡物体移动方向
	public ArrayList list_reward 				 = new ArrayList();//关卡奖励
    public ArrayList list_fixedLevel_good        = new ArrayList();//当关卡类型ELevelType为Fixed时，固定显示的物体的位置和类型

    public int LevelXCount;//关卡横轴的数量
    public int LevelYCount;//关卡竖轴的数量
    public int GoodKinds;//关卡物品的种类
    public int levelDirEnum;//关卡移动方向

    public ELevelType levelType;

    public LevelData(int level, ELevelMode mode = ELevelMode.Normal) 
    {
        _makeLevelData(level, mode);
	}

    /// <summary>
    /// 设置关卡数据
    /// </summary>
    /// <param name="level">目标关卡</param>
    /// <param name="mode">关卡模式</param>
	public void _makeLevelData(int level, ELevelMode mode)
    {
		try
        {
            D.Log("level :: " + level + " - mode :: " + mode + " - maxLevel :: " + LevelDefines.maxLevel);

            list_auto_gen.Clear();
            list_block_good_fixed.Clear();
            list_block_stone_fixed.Clear();
            list_block_stone_moving.Clear();
            list_block_frozen_fixed.Clear();
            list_block_stone_and_frozen.Clear();
            list_constraint.Clear();
            list_reward.Clear();

            switch(mode)
            {
                case ELevelMode.Normal:
                    break;
                case ELevelMode.Daily:
                    level = 100001;
                    break;
                default: 
                    break;
            }

            ConfLevel conf = ConfigModule.Instance.Tables.TBLevel.GetOrDefault(level);
            int dicStr = conf.MoveDic;
            string hidGodMovs = conf.HiddleGoodMove;
            string hidGodStys = conf.HiddleGoodStay;
            string obsMovs = conf.ObstacleMove;
            string obsStys = conf.ObstacleStay;
            string hidGodDey = conf.HiddleGoodDelay;

            string[] size = conf.LevelSize.Split(",");
            LevelXCount = int.Parse(size[1]) + 2;
            LevelYCount = int.Parse(size[0]) + 2;
            GoodKinds = conf.GoodKinds;
            levelDirEnum = dicStr;

            list_constraint = SetDic(dicStr);
            SetHiddleGood_Moves(hidGodMovs);
            SetHiddleGood_Stays(hidGodStys);
            SetObstacle_Moves(obsMovs);
            SetObstacle_Stays(obsStys);
            SetHiddleGood_Delays(hidGodDey);

            levelType = (ELevelType)conf.LevelType;
            if (levelType == ELevelType.Fixed)
            {
                SetFixedLevel_Good(conf.FixedGridMap);
            }

            #region old
            //if(level <= LevelDefines.maxLevel)
            //{
            //	Debug.Log("currentLevel :: " + level);
            //	if (mode == ELevelMode.Normal) {
            //		text = (Resources.Load("Level/NormalMode/level" + level) as TextAsset).text;
            //	} else if (mode == ELevelMode.Daily) {
            //		text = (Resources.Load("Level/DailyMode/level" + level) as TextAsset).text;
            //	}
            //}else{
            //	Debug.LogError($"level is out of range: >{LevelDefines.maxLevel}");
            //}
            //JSONNode data = JSON.Parse(text);

            ////清除旧数据
            //list_auto_gen.Clear();
            //list_block_good_fixed.Clear ();
            //list_block_stone_fixed.Clear ();
            //list_block_stone_moving.Clear ();
            //list_block_frozen_fixed.Clear ();
            //list_block_stone_and_frozen.Clear ();
            //list_constraint.Clear();
            //list_reward.Clear();

            //根据“当前关卡类型（currentMode）”和“当前关卡值（level）”来判断去获取哪个json数据（maxLevel用途不明，估计后续得改）
            //时间限制赋值
            //time_dead  = float.Parse (data [LevelDefines.time_dead]);
            //time_star1 = float.Parse (data [LevelDefines.time_star_1]);
            //time_star2 = float.Parse (data [LevelDefines.time_star_2]);
            //time_star3 = float.Parse (data [LevelDefines.time_star_3]);

            ////不动的一般物品数据赋值
            //JSONArray block_good_fixed = data[LevelDefines.good_fixed].AsArray;
            //for (int i = 0; i < block_good_fixed.Count; i++) {
            //	JSONClass good = block_good_fixed[i].AsObject;
            //	int row = good[LevelDefines.row].AsInt;
            //	int col = good[LevelDefines.col].AsInt;
            //	int id  = good[LevelDefines.id].AsInt;
            //	list_block_good_fixed.Add(new Vector3(row, col, id));
            //}

            //不可移动的石块障碍物赋值
            //JSONArray block_stone_fixed = data[LevelDefines.stones_fixed].AsArray;
            //for (int i = 0; i < block_stone_fixed.Count; i++) {
            //	JSONClass stone = block_stone_fixed[i].AsObject;
            //	int row = stone[LevelDefines.row].AsInt;
            //	int col = stone[LevelDefines.col].AsInt;
            //	list_block_stone_fixed.Add(new Vec2(row, col));
            //	list_block_stone_and_frozen.Add(new Vec2(row, col));
            //}

            ////可移动的石块障碍物赋值
            //JSONArray block_stone_moving = data[LevelDefines.stones_moving].AsArray;
            //for (int i = 0; i < block_stone_moving.Count; i++) {
            //	JSONClass stone = block_stone_moving[i].AsObject;
            //	int row = stone[LevelDefines.row].AsInt;
            //	int col = stone[LevelDefines.col].AsInt;
            //	list_block_stone_moving.Add(new Vec2(row, col));
            //}

            ////不可移动的冰冻物体赋值
            //JSONArray block_frozen_fixed = data[LevelDefines.frozens_fixed].AsArray;
            //for (int i = 0; i < block_frozen_fixed.Count; i++) {
            //	JSONClass stone = block_frozen_fixed[i].AsObject;
            //	int row = stone[LevelDefines.row].AsInt;
            //	int col = stone[LevelDefines.col].AsInt;
            //	list_block_frozen_fixed.Add(new Vec2(row, col));
            //	list_block_stone_and_frozen.Add(new Vec2(row, col));
            //}

            ////关卡物体移动方向赋值
            //JSONArray contraint = data [LevelDefines.constraint].AsArray;
            //for (int i = 0; i < contraint.Count; i++) {
            //	JSONClass option = contraint[i].AsObject;
            //	int direction = option[LevelDefines.direction].AsInt;
            //	JSONClass json_cell1 = option[LevelDefines.cell1].AsObject;
            //	JSONClass json_cell2 = option[LevelDefines.cell2].AsObject;
            //	Vec2 cell1 = new Vec2 (json_cell1[LevelDefines.row].AsInt, json_cell1[LevelDefines.col].AsInt);
            //	Vec2 cell2 = new Vec2 (json_cell2[LevelDefines.row].AsInt, json_cell2[LevelDefines.col].AsInt);
            //	list_constraint.Add(new ConstraintData(direction, cell1, cell2));
            //}

            ////关卡奖励赋值
            //JSONArray rewards = data [LevelDefines.reward].AsArray;
            //for (int i = 0; i < rewards.Count; i++) {
            //	JSONClass reward = (JSONClass) rewards[i];
            //	list_reward.Add(new RewardData(reward));
            //}

            ////自动冰冻的物体赋值
            //if (data[LevelDefines.auto_gen] != null) {
            //	JSONArray auto_gens = data [LevelDefines.auto_gen].AsArray;
            //	for (int i = 0; i < auto_gens.Count; i++) {
            //		JSONClass auto_gen = (JSONClass) auto_gens[i];
            //		int auto_gen_type = auto_gen [LevelDefines.type].AsInt;
            //		float time_gen = auto_gen [LevelDefines.time_gen].AsFloat;
            //		float time_gen_wait = auto_gen [LevelDefines.time_gen_wait].AsFloat;
            //		AutoGenData auto_gen_data = new AutoGenData(auto_gen_type, time_gen_wait, time_gen);
            //		list_auto_gen.Add(auto_gen_data);
            //	}
            //}
            #endregion
        }
        catch (IOException e){
			Debug.LogError(e.ToString());
		}
	}

    /// <summary>
    /// 设置关卡移动方向
    /// </summary>
    /// <param name="dicStr">表格得到的string数据</param>
    public ArrayList SetDic(int dicStr)
    {
        int halfH = (LevelXCount - 2) / 2; //6 ----> 12
        int halfV = (LevelYCount - 2) / 2; //4 ----> 8

        Vec2 startPos;
        Vec2 endPos;
        Vec2 startPos2;
        Vec2 endPos2;

        ArrayList constraint_temp = new ArrayList();

        if (dicStr != 0)
        {
            switch ((EGoodMoveDic)dicStr)
            {
                case EGoodMoveDic.Up:
                case EGoodMoveDic.Down:
                case EGoodMoveDic.Left:
                case EGoodMoveDic.Right:
                    startPos = new Vec2(1, 1);
                    endPos = new Vec2(halfH * 2, halfV * 2);
                    constraint_temp.Add(new ConstraintData(dicStr, startPos, endPos));
                    break;
                case EGoodMoveDic.UpDown_Away:
                    startPos = new Vec2(1, 1);
                    endPos = new Vec2(halfH, halfV * 2);
                    constraint_temp.Add(new ConstraintData(2, startPos, endPos));
                    startPos2 = new Vec2(halfH + 1, 1);
                    endPos2 = new Vec2(halfH * 2, halfV * 2);
                    constraint_temp.Add(new ConstraintData(1, startPos2, endPos2));
                    break;
                case EGoodMoveDic.UpDown_Closer:
                    startPos = new Vec2(1, 1);
                    endPos = new Vec2(halfH, halfV * 2);
                    constraint_temp.Add(new ConstraintData(1, startPos, endPos));
                    startPos2 = new Vec2(halfH + 1, 1);
                    endPos2 = new Vec2(halfH * 2, halfV * 2);
                    constraint_temp.Add(new ConstraintData(2, startPos2, endPos2));
                    break;
                case EGoodMoveDic.LeftRight_Away:
                    startPos = new Vec2(1, 1);
                    endPos = new Vec2(halfH * 2, halfV);
                    constraint_temp.Add(new ConstraintData(3, startPos, endPos));
                    startPos2 = new Vec2(1, halfV + 1);
                    endPos2 = new Vec2(halfH * 2, halfV * 2);
                    constraint_temp.Add(new ConstraintData(4, startPos2, endPos2));
                    break;
                case EGoodMoveDic.LeftRight_Closer:
                    startPos = new Vec2(1, 1);
                    endPos = new Vec2(halfH * 2, halfV);
                    constraint_temp.Add(new ConstraintData(4, startPos, endPos));
                    startPos2 = new Vec2(1, halfV + 1);
                    endPos2 = new Vec2(halfH * 2, halfV * 2);
                    constraint_temp.Add(new ConstraintData(3, startPos2, endPos2));
                    break;
                default: 
                    break;
            }
            //string[] dics = dicStr.Split('/');
            //for (int i = 0; i < dics.Length; i++) 
            //{
            //    string data = dics[i];
            //    string[] values = data.Split(",");

            //    Vec2 startPos = new Vec2(int.Parse(values[2]), int.Parse(values[3]));
            //    Vec2 endPos = new Vec2(int.Parse(values[4]), int.Parse(values[5]));
            //    list_constraint.Add(new ConstraintData(int.Parse(values[0]), startPos, endPos));
            //}
        }
        return constraint_temp;
    }

    /// <summary>
    /// 设置移动的隐藏物
    /// </summary>
    /// <param name="hidGodMovs">表格得到的string数据</param>
    private void SetHiddleGood_Moves(string hidGodMovs)
    {
        if (hidGodMovs != null && hidGodMovs != "")
        {
            string[] hidGodMovsStr = hidGodMovs.Split('/');
            for (int i = 0; i < hidGodMovsStr.Length; i++)
            {
                string[] xy = hidGodMovsStr[i].Split(",");
                int row = int.Parse(xy[0]);
                int col = int.Parse(xy[1]);
                list_boloc_frozen_moving.Add(new Vec2(row, col));
            }
        }
    }

    /// <summary>
    /// 设置不移动的隐藏物
    /// </summary>
    /// <param name="hidGodStys">表格得到的string数据</param>
    private void SetHiddleGood_Stays(string hidGodStys) 
    {
        if (hidGodStys != null && hidGodStys != "")
        {
            string[] hidGodStysStr = hidGodStys.Split('/');
            for (int i = 0; i < hidGodStysStr.Length; i++)
            {
                string[] xy = hidGodStysStr[i].Split(",");
                int row = int.Parse(xy[0]);
                int col = int.Parse(xy[1]);
                Vec2 pos = new Vec2(row, col);
                list_block_frozen_fixed.Add(pos);
                list_block_stone_and_frozen.Add(pos);
            }
        }
    }

    /// <summary>
    /// 设置移动的障碍物
    /// </summary>
    /// <param name="hidGodMovs">表格得到的string数据</param>
    private void SetObstacle_Moves(string obsMovs)
    {
        if (obsMovs != null && obsMovs != "")
        {
            string[] obsMovsStr = obsMovs.Split('/');
            for(int i = 0; i < obsMovsStr.Length; i++)
            {
                string[] xy = obsMovsStr[i].Split(",");
                int row = int.Parse(xy[0]);
                int col = int.Parse(xy[1]);
                list_block_stone_moving.Add(new Vec2(row, col));
            }
        }
    }

    /// <summary>
    /// 设置不移动的障碍物
    /// </summary>
    /// <param name="obsStys">表格得到的string数据</param>
    private void SetObstacle_Stays(string obsStys)
    {
        if (obsStys != null && obsStys != "")
        {
            string[] obsStysStr = obsStys.Split('/');
            for (int i = 0; i < obsStysStr.Length; i++)
            {
                string[] xy = obsStysStr[i].Split(",");
                int row = int.Parse(xy[0]);
                int col = int.Parse(xy[1]);
                Vec2 pos = new Vec2(row, col);
                list_block_stone_fixed.Add(pos);
                list_block_stone_and_frozen.Add(pos);
            }
        }
    }

    /// <summary>
    /// 设置延迟隐藏数据
    /// </summary>
    /// <param name="hidGodDey">表格得到的string数据</param>
    private void SetHiddleGood_Delays(string hidGodDey)
    {
        if (hidGodDey != null && hidGodDey != "")
        {
            string[] hidGodDeyStr = hidGodDey.Split('/');

            for (int i = 0; i < hidGodDeyStr.Length; i++)
            {
                string[] data = hidGodDeyStr[i].Split(",");
                int auto_gen_type = int.Parse(data[0]);
                float time_gen = int.Parse(data[1]);
                float time_gen_wait = int.Parse(data[2]);
                AutoGenData auto_gen_data = new AutoGenData(auto_gen_type, time_gen_wait, time_gen);
                list_auto_gen.Add(auto_gen_data);
            }
        }
    }

    private void SetFixedLevel_Good(Dictionary<string, int> fixedLevGods)
    {
        foreach(KeyValuePair<string, int> keyValuePair in fixedLevGods)
        {
            string[] PosStr = keyValuePair.Key.Split("_");
            int goodId = keyValuePair.Value;

            int x = int.Parse(PosStr[0]);
            int y = int.Parse(PosStr[1]);
            int id = goodId;
            FixedLevelGood fixedLevelGood = new FixedLevelGood();
            fixedLevelGood.id = id;
            fixedLevelGood.X = x;
            fixedLevelGood.Y = y;
            list_fixedLevel_good.Add(fixedLevelGood);
        }   
    }

    //加载不可移动的石块障碍物和冰冻物体（可选择）
    public void loadListBlockStoneAndFrozen(bool hasStone = true, bool hasFrozen = true) 
    {
        list_block_stone_and_frozen.Clear ();
		if (hasStone) {
			list_block_stone_and_frozen.AddRange (list_block_stone_fixed);
		}
		if (hasFrozen) {
			list_block_stone_and_frozen.AddRange (list_block_frozen_fixed);
		}
	}
}
