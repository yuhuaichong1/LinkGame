using cfg;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;
using static UnityEngine.ParticleSystem;


namespace XrCode
{
    public class GamePlayModule : BaseModule
    {
        private int curLevel;//当前关卡
        private int tipCount;//提示次数
        private int refushCount;//刷新次数
        private int removeCount;//移除次数
        private bool isTutorial;//是否通过了新手教程

        private AudioModule AudioModule;
        private LanguageModule LanguageModule;

        private GameObject[][] MAP_Goods;//场景中的所有物体（包含障碍物，不包含隐藏物）
        private int[][] MAP;//场景中的所有物体的id（包含障碍物，不包含隐藏物）
        private int[][] MAP_FROZEN;//场景中的所有物体是否为冰冻状态（非冰冻状态为-1，否则为“固定冰块”（GameDefines.HID_FIXED_ID）或“可移动冰块”（GameDefines.HID_MOVING_ID））
        private Vec2 POS1;//被选中的第1个物体or提示的第一个物品
        private Vec2 POS2;//被选中的第2个物体or提示的第二个物品
        private Vec2 HINT_POS1;//提示的第一个物品
        private Vec2 HINT_POS2;//提示的第二个物品
        private Vector3[][] POS;//所有位置的坐标集合
        private static int MIN_X;//长最下角（起始位置的X）
        private static int MIN_Y;//高最下角（起始位置的Y）
        private static int CELL_WIDH = 28;//单个物体的宽
        private static int CELL_HEIGHT = 28;//单个物体的高
        private static int MAP_HEIGHT = 28;//整体排布的高
        private static int MAP_WIDTH = 28;//整体排布的宽

        private int col = 8;//布局长度（包含左右空位）
        private int row = 10;//布局高度（包含上下空位）
        private ArrayList LPath;//画线用的
        private ArrayList keyLPath;//画线用的

        private GameObject goodPrefab;//物体预制体
        private GameObject autoGenPrefab;//自动冰冻管理器预制体（后续可能改为Module）

        private LevelData curLevelData;//当前关卡的数据
        private EMapState curMapState;//关卡当前的状态

        private ArrayList list_moving_frozen;//关卡中可移动的物体的冰冻物体
        private ArrayList list_block_frozen;//关卡中不可移动的物体的冰冻物体？
        private ArrayList list_block_frozen_normal;//关卡中不可移动？
        private ArrayList list_block_frozen_special;//关卡中不可移动的？
        private ArrayList listAutoGens;//自动冰冻完毕的物品集合
        public int numberGoodRemain = 0;
        private ArrayList list_good_can_eat1 = new ArrayList();
        private ArrayList list_good_can_eat2 = new ArrayList();

        private Transform mapTrans;//生成物体的父对象
        private Transform obsTrans;//生成隐藏物体的父对象

        private bool checkingPaire;//当前关卡是否在检测配对

        private bool isGameOver;//是否GaneOver
        private bool isReseting;//是否重置关卡？（待验证）

        private int _numberResetMap;//重排关卡的次数？（待验证）

        private bool _isCheckWrong;//是否检测了错误？

        private ArrayList check_id;

        public bool checking_paire;

        private Dictionary<int, Sprite> goodIcons;//物品图片字典
        private bool isShowOtherPlane;//是否打开了其他面板
        private Dictionary<int, GameObject> pathObj;//线预制体

        private ArrayList list_pos_need_update;//需要更新（可位移的物品）
        private ArrayList curLevelDirection;//移动方向

        
        private int curTotalLinkCount;//累计消除次数
        private int curLuckMomentCount;//老虎机计数
        private int curTopNoticeCount;//顶部消息计数
        private int curAwesomeCount;//送钱计数
        private int curRateCount;//让评论计数

        private Dictionary<int, List<int>> GoodIconRange;//图样随机区间
        private Dictionary<int, int> randomGoodIcon;//让图样随机的词典

        private Queue<int> withdrawableLevel;//可提现的关卡
        private int curWLevel;//当前提现关卡Id

        private int ifkindShake;//是否全同类物体震动（判断是否快速双击）
        private STimer ifKSTimer;//是否全同类物体震动双击间隔计算器

        private bool ifRemoveFunc;//是否处于移除功能中
        private bool ifHintFunc;//是否处于提示功能中

        private int totalGood;//总物品数量
        private int remainGood;//剩余物品数量

        private float passTime;                 //通关用时

        protected override void OnLoad()
        {
            base.OnLoad();
            RegisetUpdateObj();
            GamePlayFacade.CreateLevel += CreateLevel;
            GamePlayFacade.CheckIfLink += CheckIfLink;
            GamePlayFacade.TipFunc += TipFunc;
            GamePlayFacade.RefushFunc += RefushFunc;
            GamePlayFacade.RemoveFunc += RemoveFunc;
            GamePlayFacade.RemoveFunc2 += RemoveFunc2;
            GamePlayFacade.ChangeTipCount += ChangeTipCount;
            GamePlayFacade.ChangeRefushCount += ChangeRefushCount;
            GamePlayFacade.ChangeRemoveCount += ChangeRemoveCount;
            GamePlayFacade.ChangeDirection += ChangeDirection;
            GamePlayFacade.GetCurLevel += GetCurLevel;
            GamePlayFacade.NextLevel += NextLevel;
            GamePlayFacade.GetTipCount += GetTipCount;
            GamePlayFacade.GetRefushCount += GetRefushCount;
            GamePlayFacade.GetRemoveCount += GetRemoveCount;
            GamePlayFacade.ChangeMapState += ChangeMapState;
            GamePlayFacade.GetMapState += GetMapState;
            GamePlayFacade.UpdateHiddleState += UpdateHiddleState;
            GamePlayFacade.RemoveHidden += RemoveHidden;
            GamePlayFacade.RemoveGood += RemoveGood;
            GamePlayFacade.UpdateMap += UpdateMap;
            GamePlayFacade.GetRow += GetRow;
            GamePlayFacade.SetIsCheckWrong += SetIsCheckWrong;
            GamePlayFacade.CheckIdAdd += CheckIdAdd;
            GamePlayFacade.GetGoodIcon += GetGoodIcon;
            GamePlayFacade.Select += Select;
            GamePlayFacade.GetMAPGoods += GetMAPGoods;
            GamePlayFacade.GetCurTotalLinkCount += GetCurTotalLinkCount;
            GamePlayFacade.GetCurLuckMomentCount += GetCurLuckMomentCount;
            GamePlayFacade.SetCurLuckMomentCount += SetCurLuckMomentCount;
            GamePlayFacade.GetWithdrawableLevel += GetWithdrawableLevel;
            GamePlayFacade.GetCurWLevel += GetCurWLevel;
            GamePlayFacade.GetIsTutorial += GetIsTutorial;
            GamePlayFacade.SetIsTutorial += SetIsTutorial;
            GamePlayFacade.GetNumberGoodCanEat += GetNumberGoodCanEat;
            GamePlayFacade.GetIfRemoveFunc += GetIfRemoveFunc;
            GamePlayFacade.GetIfHintFunc += GetIfHintFunc;
            GamePlayFacade.GetRemainPCT += GetRemainPCT;

            AudioModule = ModuleMgr.Instance.AudioMod;
            LanguageModule = ModuleMgr.Instance.LanguageMod;

            GoodIconRange = new Dictionary<int, List<int>>();
            randomGoodIcon = new Dictionary<int, int>();   

            LPath = new ArrayList();
            keyLPath = new ArrayList();
            list_moving_frozen = new ArrayList();
            list_block_frozen = new ArrayList();
            list_block_frozen_normal = new ArrayList();
            list_block_frozen_special = new ArrayList();
            listAutoGens = new ArrayList();
            _numberResetMap = 0;
            _isCheckWrong = false;
            check_id = new ArrayList();
            list_pos_need_update = new ArrayList();//当前关卡需要移动的物品集合
            curLevelDirection = new ArrayList();//当前关卡的方向

            LevelDefines.maxLevel = ConfigModule.Instance.Tables.TBLevel.DataList.Count;

            goodIcons = new Dictionary<int, Sprite>();
            SetGoodIcon();
            pathObj = new Dictionary<int, GameObject>();
            SetPathObj();

            RandomGoodIconRange();

            LoadData();
        }

        protected override void OnUpdate()
        {
            if (passTime < 1800)
            {
                passTime += Time.deltaTime;
            }
        }

        //加载数据
        private void LoadData()
        {
            curLevel = SPlayerPrefs.GetInt(PlayerPrefDefines.curLevel);
            tipCount = SPlayerPrefs.GetInt(PlayerPrefDefines.tipCount);
            refushCount = SPlayerPrefs.GetInt(PlayerPrefDefines.refushCount);
            removeCount = SPlayerPrefs.GetInt(PlayerPrefDefines.removeCount);
            isTutorial = SPlayerPrefs.GetBool(PlayerPrefDefines.isTutorial);

            curTotalLinkCount = SPlayerPrefs.GetInt(PlayerPrefDefines.curTotalLinkCount);
            curLuckMomentCount = SPlayerPrefs.GetInt(PlayerPrefDefines.curLuckMomentCount);
            //curTopNoticeCount = SPlayerPrefs.GetInt(PlayerPrefDefines.curTopNoticeCount);
            //curAwesomeCount = SPlayerPrefs.GetInt(PlayerPrefDefines.curAwesomeCount);

            withdrawableLevel = SPlayerPrefs.GetQueue<int>(PlayerPrefDefines.withdrawableLevel, true);
            if(withdrawableLevel.Count == 0)
            {
                withdrawableLevel.Enqueue(ConfigModule.Instance.Tables.TBWithdrawableLevels.Get(1).Level);
                curWLevel = 1;
            }
            else
                curWLevel = SPlayerPrefs.GetInt(PlayerPrefDefines.curWLevel);

            if (curLevel == 0)
            {
                D.Error("第一次进游戏");
                curLevel = 1;
                isTutorial = true;
                SPlayerPrefs.SetBool(PlayerPrefDefines.isTutorial, isTutorial);
            }
        }

        /// <summary>
        /// 加载物品图片数据
        /// </summary>
        private void SetGoodIcon()
        {
            List<ConfGoodIcon> confs = ConfigModule.Instance.Tables.TBGoodIcon.DataList;
            foreach(ConfGoodIcon conf in confs)
            {
                goodIcons.Add(conf.Sn, ResourceMod.Instance.SyncLoad<Sprite>(conf.GoodPath));
            }
        }

        /// <summary>
        /// 获取物品图片
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private Sprite GetGoodIcon(int id)
        {
            return goodIcons[randomGoodIcon[id]];
        }

        /// <summary>
        /// 加载连线物体数据
        /// </summary>
        private void SetPathObj()
        {
            List<ConfLinkLine> confs = ConfigModule.Instance.Tables.TBLinkLine.DataList;
            foreach (ConfLinkLine conf in confs)
            {
                pathObj.Add(conf.PathObjId, ResourceMod.Instance.SyncLoad<GameObject>(conf.PathObjPath));
            }

        }

        #region 创建关卡
        private void CreateLevel()
        {
            passTime = 0;
            totalGood = 0;
            remainGood = 0;

            curLevelData = new LevelData(curLevel);
            row = curLevelData.LevelXCount;
            col = curLevelData.LevelYCount;
            curLevelDirection = curLevelData.list_constraint;

            goodPrefab = ResourceMod.Instance.SyncLoad<GameObject>("Prefabs/Good/item.prefab");

            SetRandomGoodIcon(curLevelData.GoodKinds);

            LMap(row, col);

            //if (isTutorial)
            //{
                
                
            //}
            //else
            //{
                
            //}
            curMapState = EMapState.Playing;

            mapTrans = GamePlayFacade.GetMapTrans?.Invoke();
            obsTrans = GamePlayFacade.GetObsTrans?.Invoke();

            if (curLevelData.levelType == ELevelType.Fixed)
            {
                _fixedMap();
            }
            else
            {
                _randomMap();
            }

            remainGood = totalGood;
        }

        /// <summary>
        /// 添加物体排布表格数据
        /// </summary>
        /// <param name="row">横列数量</param>
        /// <param name="col">纵列数量</param>
        public void LMap(int row, int col)
        {
            //float camHalfHeight = Camera.main.orthographicSize;
            //float camHalfWidth = Camera.main.aspect * camHalfHeight;
            float camHalfHeight = (GoodDefine.height) * (curLevelData.LevelXCount - 2) / 2;
            float camHalfWidth = (GoodDefine.width) * (curLevelData.LevelYCount - 2) / 2;
            MAP_WIDTH = (int)(camHalfWidth) * 2;
            MAP_HEIGHT = (int)(camHalfHeight) * 2;
            CELL_WIDH = (int)(MAP_WIDTH / (col - 2));
            CELL_HEIGHT = (int)((MAP_HEIGHT) / (row - 2));

            //if (CELL_WIDH < CELL_HEIGHT)
            //{
            //    CELL_HEIGHT = CELL_WIDH;
            //}
            //else
            //{
            //    CELL_WIDH = CELL_HEIGHT;
            //}

            MAP = new int[row][];
            MAP_FROZEN = new int[row][];
            MAP_Goods = new GameObject[row][];
            POS = new Vector3[row][];

            MIN_X = -col * CELL_WIDH / 2;
            MIN_Y = -(int)camHalfHeight - CELL_HEIGHT / 2 + GameDefines.map_margin_bottom;


            for (int i = 0; i < row; i++)
            {
                MAP[i] = new int[col];
                MAP_FROZEN[i] = new int[col];
                MAP_Goods[i] = new GameObject[col];
                POS[i] = new Vector3[col];
                for (int j = 0; j < col; j++)
                {
                    MAP[i][j] = -1;
                    MAP_FROZEN[i][j] = -1;
                    MAP_Goods[i][j] = null;
                    POS[i][j] = new Vector3(0, 0, 0);
                    POS[i][j].x = (MIN_X + j * CELL_WIDH + CELL_WIDH / 2);
                    POS[i][j].y = (MIN_Y + i * CELL_HEIGHT);
                    POS[i][j].z = i / 10.0f;
                }
            }
            D.Log("khoi tao map");
        }

        //随机生成物体序列并于场景中生成
        private void _randomMap()
        {
            D.Log("::::Random MAP::::");
            curMapState = EMapState.None;

            ArrayList list_good_fixed = curLevelData.list_block_good_fixed;
            ArrayList list_obs_fixed = curLevelData.list_block_stone_fixed;
            ArrayList list_obs_moving = curLevelData.list_block_stone_moving;
            list_block_frozen = curLevelData.list_block_frozen_fixed;
            list_moving_frozen = curLevelData.list_block_frozen_moving;

            int total_good = (row - 2) * (col - 2) - list_obs_fixed.Count - list_obs_moving.Count - list_good_fixed.Count;
            int total_good_type = curLevelData.GoodKinds;
            //int number_good_4 = (total_good - 2 * total_good_type) / 2;
            //int number_good_2 = total_good_type - number_good_4;
            int number_good_average = total_good / 2 / total_good_type;
            int number_good_remainder = (total_good - (number_good_average * 2 * total_good_type)) / 2;
                   
            ArrayList list_good = new ArrayList();
            for (int i = 0; i < total_good_type; i++)
            {
                for (int j = 0; j < number_good_average * 2; j++)
                {
                    list_good.Add(i);
                }
            }
            for (int i = 0; i < number_good_remainder; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    list_good.Add(i);
                }
            }
            #region old
            //ArrayList list_good = new ArrayList();
            //for (int i = 0; i < number_good_4; i++)
            //{
            //    for (int j = 0; j < 4; j++)
            //    {
            //        list_good.Add(i);
            //    }
            //}
            //for (int i = number_good_4; i < number_good_4 + number_good_2; i++)
            //{
            //    for (int j = 0; j < 2; j++)
            //    {
            //        list_good.Add(i);
            //    }
            //}
            #endregion

            int list_pk_count = list_good.Count;
            int temp2 = (row - 2) * (col - 2) - list_obs_fixed.Count - list_obs_moving.Count - list_pk_count - list_good_fixed.Count;
            if (temp2 % 2 != 0)
            {
                D.Log("Amount of good must be even");
                return;
            }
            int max_id = (int)list_good[list_pk_count - 1];
            for (int i = 0; i < temp2; i++)
            {
                list_good.Add(max_id + 1);
            }
            list_pk_count = list_good.Count;
            for (int i = 0; i < list_obs_fixed.Count; i++)
            {
                Vec2 pos = (Vec2)list_obs_fixed[i];
                AddSpecialItem(GameDefines.OBS_FIXED_ID, pos.R, pos.C);
                MAP[pos.R][pos.C] = GameDefines.OBS_FIXED_ID;
            }

            for (int i = 0; i < list_obs_moving.Count; i++)
            {
                Vec2 pos = (Vec2)list_obs_moving[i];
                AddSpecialItem(GameDefines.OBS_MOVING_ID, pos.R, pos.C);
                MAP[pos.R][pos.C] = GameDefines.OBS_MOVING_ID;
            }

            for (int i = 0; i < list_good_fixed.Count; i++)
            {
                Vector3 good = (Vector3)list_good_fixed[i];
                int p_row = (int)good.x;
                int p_col = (int)good.y;
                int p_id = (int)good.z;
                AddGood(p_id, p_row, p_col);
                MAP[p_row][p_col] = p_id;
            }

            for (int i = row - 2; i > 0; i--)
            {
                for (int j = 1; j < col - 1; j++)
                {
                    if (MAP[i][j] == -1)
                    {
                        int r = (int)(UnityEngine.Random.value * list_pk_count);
                        int type = (int)list_good[r];
                        AddGood(type, i, j);
                        MAP[i][j] = type;
                        list_good.RemoveAt(r);
                        list_pk_count--;
                    }
                    GetGoodClass(i, j).setOrder();
                }
            }

            for (int i = 0; i < list_block_frozen.Count; i++)
            {
                Vec2 pos = (Vec2)list_block_frozen[i];
                AddSpecialItem(GameDefines.HID_FIXED_ID, GameDefines.HFName, pos.R, pos.C);
            }
            for(int i = 0; i < list_moving_frozen.Count;i++)
            {
                Vec2 pos = (Vec2)list_moving_frozen[i];
                AddSpecialItem(GameDefines.HID_MOVING_ID, GameDefines.HMName, pos.R, pos.C);
            }

            for (int i = 0; i < curLevelData.list_auto_gen.Count; i++)
            {
                AutoGenData auto_gen_data = (AutoGenData)curLevelData.list_auto_gen[i];
                GenAutoGen(auto_gen_data);
            }

            UpdateListHiddle();

            int numbergoodCanEat = GetNumberGoodCanEat();
            if (numbergoodCanEat == 0)
            {
                _resetMap();
            }
            else
            {
                ChangeMapState(EMapState.Playing);
            }
        }

        //固定生成物体序列并于场景中生成
        private void _fixedMap()
        {
            D.Log("::::Fixed MAP::::");
            curMapState = EMapState.None;

            ArrayList list_good_fixed = curLevelData.list_block_good_fixed;
            ArrayList list_obs_fixed = curLevelData.list_block_stone_fixed;
            ArrayList list_obs_moving = curLevelData.list_block_stone_moving;
            list_block_frozen = curLevelData.list_block_frozen_fixed;
            list_moving_frozen = curLevelData.list_block_frozen_moving;

            for (int i = 0; i < list_obs_fixed.Count; i++)
            {
                Vec2 pos = (Vec2)list_obs_fixed[i];
                AddSpecialItem(GameDefines.OBS_FIXED_ID, pos.R, pos.C);
                MAP[pos.R][pos.C] = GameDefines.OBS_FIXED_ID;
            }

            for (int i = 0; i < list_obs_moving.Count; i++)
            {
                Vec2 pos = (Vec2)list_obs_moving[i];
                AddSpecialItem(GameDefines.OBS_MOVING_ID, pos.R, pos.C);
                MAP[pos.R][pos.C] = GameDefines.OBS_MOVING_ID;
            }

            ArrayList list_fixedLevel_good = curLevelData.list_fixedLevel_good;
            foreach(FixedLevelGood good in list_fixedLevel_good)
            {
                int p_row = (int)good.X;
                int p_col = (int)good.Y;
                int p_id = (int)good.id;
                AddGood(p_id, p_row, p_col);
                MAP[p_row][p_col] = p_id;
            }

            for (int i = 0; i < list_block_frozen.Count; i++)
            {
                Vec2 pos = (Vec2)list_block_frozen[i];
                AddSpecialItem(GameDefines.HID_FIXED_ID, GameDefines.HFName, pos.R, pos.C);
            }
            for (int i = 0; i < list_moving_frozen.Count; i++)
            {
                Vec2 pos = (Vec2)list_moving_frozen[i];
                AddSpecialItem(GameDefines.HID_MOVING_ID, GameDefines.HMName, pos.R, pos.C);
            }

            for (int i = 0; i < curLevelData.list_auto_gen.Count; i++)
            {
                AutoGenData auto_gen_data = (AutoGenData)curLevelData.list_auto_gen[i];
                GenAutoGen(auto_gen_data);
            }

            UpdateListHiddle();

            ChangeMapState(EMapState.Playing);
        }

        //向场景中添加一个物品预制体
        private void AddGood(int type, int row, int col, int online_id = 0)
        {
            if (type == -1)
                return;
            MAP[row][col] = type;
            GameObject obj = GameObject.Instantiate(goodPrefab);
            Good good = obj.GetComponent<Good>();
            good.setOnlineId(online_id);
            good.setInfo(type, row, col, POS[row][col], CELL_WIDH, CELL_HEIGHT, mapTrans);
            MAP_Goods[row][col] = obj;

            totalGood += 1;
        }

        //得到场景中的某物体上的Good类
        public Good GetGoodClass(int row, int col)
        {
            GameObject obj = GetGood(row, col);
            if (obj)
            {
                return obj.GetComponent<Good>();
            }
            return null;
        }

        //得到场景中的某物体（MAP_Goods队列中的物体)
        public GameObject GetGood(int row, int col)
        {
            return MAP_Goods == null ? null : MAP_Goods[row][col];
        }

        //向场景中添加一个特殊的物品预制体（障碍物）
        public void AddSpecialItem(int type, int row, int col)
        {
            if (type == -1)
                return;
            GameObject obj = GameObject.Instantiate(goodPrefab);
            Good goodComp = obj.GetComponent<Good>();
            goodComp.setSpecialInfo(type, row, col, POS[row][col], CELL_WIDH, CELL_HEIGHT, obsTrans);
            MAP_Goods[row][col] = obj;
        }
        //向场景中添加一个特殊的物品预制体（冰冻物品）（带返回值版本）
        public Good AddSpecialItem(int type, string name, int row, int col)
        {
            if (type == -1)
                return null;
            GameObject obj = GameObject.Instantiate(goodPrefab);
            Good good = obj.GetComponent<Good>();
            good.setSpecialInfo(type, name, row, col, POS[row][col], CELL_WIDH, CELL_HEIGHT, obsTrans);
            good.setFrozen(true);
            MAP_FROZEN[row][col] = type;
            return good;
        }

        /// <summary>
        /// 创建自动冰冻的管理器
        /// </summary>
        /// <param name="auto_gen_data"></param>
        public void GenAutoGen(AutoGenData auto_gen_data)
        {
            GameObject obj = GameObject.Instantiate(autoGenPrefab);
            AutoGenController autoGenController = obj.GetComponent<AutoGenController>();
            autoGenController.setAutoGenData(auto_gen_data);
            listAutoGens.Add(autoGenController);
        }

        // 重新生成地图
        public void _resetMap()
        {
            if (CheckGameOverByAllHiddle())
            {
                isGameOver = true;
                Game.Instance.StartCoroutine(ShowRecoverLifePopupDelay());
                //似乎是保存数据（持久化）用的
                //GameStatic.logLevel(GameStatic.currentMode, ItemController.getNumHintItem(), ItemController.getNumRandomItem(), GameStatic.currentLevel, 0, GameStatic.currentScore, false);
                return;
            }
            _numberResetMap++;
            if (_numberResetMap > 500)
                return;
            RemoveHint();
            D.Log(":::::Reset MAP::::");
            //get list good and stone in map
            ArrayList list_good = new ArrayList();
            ArrayList list_stone_moving = new ArrayList();
            ArrayList list_slot_no_frozen = new ArrayList();
            ArrayList list_slot_has_frozen = new ArrayList();
            for (int i = 1; i < row - 1; i++)
            {
                for (int j = 1; j < col - 1; j++)
                {
                    if (MAP[i][j] != -1 && MAP[i][j] != GameDefines.OBS_FIXED_ID)
                    {
                        if (MAP[i][j] == GameDefines.OBS_MOVING_ID)
                        {
                            list_stone_moving.Add(GetGood(i, j));
                        }
                        else
                        {
                            list_good.Add(GetGood(i, j));
                        }
                    }
                }
            }
            //get list slot avaiable
            for (int i = 1; i < row - 1; i++)
            {
                for (int j = 1; j < col - 1; j++)
                {
                    if (MAP[i][j] != -1 && MAP[i][j] != GameDefines.OBS_FIXED_ID)
                    {
                        if (MAP_FROZEN[i][j] == -1)
                        {
                            list_slot_no_frozen.Add(new Vec2(i, j));
                        }
                        else
                        {
                            list_slot_has_frozen.Add(new Vec2(i, j));
                        }
                    }
                }
            }
            list_slot_no_frozen = GetRandomList(list_slot_no_frozen);
            list_slot_has_frozen = GetRandomList(list_slot_has_frozen);
            //random list stone moving
            for (int i = 0; i < list_stone_moving.Count; i++)
            {
                GameObject obj = (GameObject)list_stone_moving[i];
                Good good = obj.GetComponent<Good>();
                int type = good.id;
                Vec2 pos = (Vec2)list_slot_no_frozen[0];
                ChangeGood(good, type, pos);
                MAP_Goods[pos.R][pos.C] = obj;
                MAP[pos.R][pos.C] = type;
                list_slot_no_frozen.RemoveAt(0);
                good.setFrozen(false);
            }

            ArrayList list_slot = new ArrayList();
            list_slot.AddRange(list_slot_no_frozen);
            list_slot.AddRange(list_slot_has_frozen);
            for (int i = 0; i < list_good.Count; i++)
            {
                GameObject obj = (GameObject)list_good[i];
                Good good = obj.GetComponent<Good>();
                int type = good.id;
                Vec2 pos = (Vec2)list_slot[i];
                if (MAP_FROZEN[pos.R][pos.C] == -1)
                {
                    good.setFrozen(false);
                }
                else
                {
                    good.setFrozen(true);
                }
                ChangeGood(good, type, pos);
                MAP_Goods[pos.R][pos.C] = obj;
                MAP[pos.R][pos.C] = type;
            }
            int numberGoodCanEat = GetNumberGoodCanEat();
            if (numberGoodCanEat == 0)
            {
                _resetMap();
            }
            else
            {
                //reloadUI();
                _numberResetMap = 0;
                isReseting = false;
            }
        }

        IEnumerator ShowRecoverLifePopupDelay()
        {
            yield return new WaitForFixedUpdate();
            ShowRecoverLifePopup();
        }

        //显示复活窗口
        void ShowRecoverLifePopup()
        {
            if (curMapState != EMapState.Dialog_recover)
            {
                UIManager.Instance.OpenWindowAsync<UIRecover>(EUIType.EUIFuncPopup);
                curMapState = EMapState.Dialog_recover;
            }
        }

        #endregion

        #region 连接判断

        #region old
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

        #region new
        bool checkPaire(Vec2 POS1, Vec2 POS2, bool showPath)
        {
            if (POS1 == null)
                return false;
            if (POS2 == null)
            {
                return false;
            }
            if (POS1.C == POS2.C)
            {
                if (check_vertical(POS1, POS2, showPath))
                {
                    return true;
                }
            }
            else if (POS1.R == POS2.R)
            {
                if (check_horizontal(POS1, POS2, showPath))
                {
                    return true;
                }
            }
            else if (checkL(POS1, POS2, showPath))
            {
                return true;
            }
            else if (checkZ(POS1, POS2, showPath))
            {
                return true;
            }

            if (checkU(POS1, POS2, showPath))
            {
                return true;
            }
            return false;
        }

        //检测两位置的竖列间是否有物体
        bool check_vertical(Vec2 POS1, Vec2 POS2, bool showPath)
        {
            if (get_pos_between_vertical(POS1, POS2) == POS2.R)
            {
                if (showPath)
                {
                    add_path_vertical(POS1, POS2);
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        //检测两位置的横列间是否有物体
        bool check_horizontal(Vec2 POS1, Vec2 POS2, bool showPath)
        {
            if (get_pos_between_horizontal(POS1, POS2) == POS2.C)
            {
                if (showPath)
                {
                    add_path_horizontal(POS1, POS2);
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        //检测两位置是否能三线相连（Z型）
        bool checkZ(Vec2 POS1, Vec2 POS2, bool showPath)
        {
            int ph1 = get_pos_between_horizontal(POS1, POS2);
            int ph2 = get_pos_between_horizontal(POS2, POS1);
            int pv1 = get_pos_between_vertical(POS1, POS2);
            int pv2 = get_pos_between_vertical(POS2, POS1);

            if (ph1 > POS1.C)
            {
                if (ph1 > POS2.C)
                {
                    ph1 = POS2.C;
                }
                if (ph2 < ph1 - 1)
                {
                    for (int i = ph2 + 1; i < ph1; i++)
                    {
                        if (Mathf.Abs(POS1.R - POS2.R) < 2)
                        {
                            if (MAP[POS1.R][i] == -1 && MAP[POS2.R][i] == -1)
                            {
                                if (showPath)
                                {
                                    add_path_horizontal(POS1, new Vec2(POS1.R, i));
                                    add_path_horizontal(new Vec2(POS2.R, i), POS2);
                                }
                                return true;
                            }
                        }
                        else
                        {
                            int pv = get_pos_between_vertical(new Vec2(POS1.R, i), new Vec2(POS2.R, i));
                            if (pv == POS2.R)
                            {
                                if (showPath)
                                {
                                    add_path_horizontal(POS1, new Vec2(POS1.R, i));
                                    add_path_vertical(new Vec2(POS1.R, i), new Vec2(POS2.R, i));
                                    add_path_horizontal(new Vec2(POS2.R, i), POS2);
                                }
                                return true;
                            }
                        }
                    }
                }
            }
            else if (ph1 < POS1.C)
            {
                if (ph1 < POS2.C)
                {
                    ph1 = POS2.C;
                }
                if (ph2 > ph1 + 1)
                {
                    for (int i = ph1 + 1; i < ph2; i++)
                    {
                        if (Mathf.Abs(POS1.R - POS2.R) < 2)
                        {
                            if (MAP[POS1.R][i] == -1 && MAP[POS2.R][i] == -1)
                            {
                                if (showPath)
                                {
                                    add_path_horizontal(POS1, new Vec2(POS1.R, i));
                                    add_path_horizontal(new Vec2(POS2.R, i), POS2);
                                }
                                return true;
                            }
                        }
                        else
                        {
                            int pv = get_pos_between_vertical(new Vec2(POS1.R, i), new Vec2(POS2.R, i));
                            if (pv == POS2.R)
                            {
                                if (showPath)
                                {
                                    add_path_horizontal(POS1, new Vec2(POS1.R, i));
                                    add_path_vertical(new Vec2(POS1.R, i), new Vec2(POS2.R, i));
                                    add_path_horizontal(new Vec2(POS2.R, i), POS2);
                                }
                                return true;
                            }
                        }
                    }
                }
            }

            if (pv1 > POS1.R)
            {
                if (pv1 > POS2.R)
                {
                    pv1 = POS2.R;
                }
                if (pv2 < pv1 - 1)
                {
                    for (int i = pv2 + 1; i < pv1; i++)
                    {
                        if (Mathf.Abs(POS1.C - POS2.C) < 2)
                        {
                            if (MAP[i][POS1.C] == -1 && MAP[i][POS2.C] == -1)
                            {
                                if (showPath)
                                {
                                    add_path_vertical(POS1, new Vec2(i, POS1.C));
                                    add_path_vertical(new Vec2(i, POS2.C), POS2);
                                }
                                return true;
                            }
                        }
                        else
                        {
                            int ph = get_pos_between_horizontal(new Vec2(i, POS1.C), new Vec2(i, POS2.C));
                            if (ph == POS2.C)
                            {
                                if (showPath)
                                {
                                    add_path_vertical(POS1, new Vec2(i, POS1.C));
                                    add_path_horizontal(new Vec2(i, POS1.C), new Vec2(i, POS2.C));
                                    add_path_vertical(new Vec2(i, POS2.C), POS2);
                                }
                                return true;
                            }
                        }
                    }
                }
            }
            else if (pv1 < POS1.R)
            {
                if (pv1 < POS2.R)
                {
                    pv1 = POS2.R;
                }
                if (pv2 > pv1 + 1)
                {
                    for (int i = pv1 + 1; i < pv2; i++)
                    {
                        if (pv1 == pv2 && Mathf.Abs(POS1.C - POS2.C) < 2)
                        {
                            if (MAP[i][POS1.C] == -1 && MAP[i][POS2.C] == -1)
                            {
                                if (showPath)
                                {
                                    add_path_vertical(POS1, new Vec2(i, POS1.C));
                                    add_path_vertical(new Vec2(i, POS2.C), POS2);
                                }
                                return true;
                            }
                        }
                        else
                        {
                            int ph = get_pos_between_horizontal(new Vec2(i, POS1.C), new Vec2(i, POS2.C));
                            if (ph == POS2.C)
                            {
                                if (showPath)
                                {
                                    add_path_vertical(POS1, new Vec2(i, POS1.C));
                                    add_path_horizontal(new Vec2(i, POS1.C), new Vec2(i, POS2.C));
                                    add_path_vertical(new Vec2(i, POS2.C), POS2);
                                }
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }

        //检测两列是否能二线相连（L型）
        bool checkL(Vec2 POS1, Vec2 POS2, bool showPath)
        {
            int ph1 = get_pos_between_horizontal(POS1, POS2);
            int pv1 = get_pos_between_vertical(POS1, POS2);
            int ph2 = get_pos_between_horizontal(POS2, POS1);
            int pv2 = get_pos_between_vertical(POS2, POS1);

            if (ph1 == POS2.C && pv2 == POS1.R && MAP[pv2][ph1] == -1)
            {
                if (showPath)
                {
                    add_path_horizontal(POS1, new Vec2(POS1.R, POS2.C));
                    add_path_vertical(new Vec2(POS1.R, POS2.C), POS2);
                }
                return true;
            }
            else if (pv1 == POS2.R && ph2 == POS1.C && MAP[pv1][ph2] == -1)
            {
                if (showPath)
                {
                    add_path_vertical(POS1, new Vec2(POS2.R, POS1.C));
                    add_path_horizontal(new Vec2(POS2.R, POS1.C), POS2);
                }
                return true;
            }
            return false;
        }

        //检测两物体是否能三线相连（U型）
        bool checkU(Vec2 POS1, Vec2 POS2, bool showPath)
        {
            if (checkU_Left(POS1, POS2, showPath))
            {
                return true;
            }
            if (checkU_Right(POS1, POS2, showPath))
            {
                return true;
            }
            if (checkU_Up(POS1, POS2, showPath))
            {
                return true;
            }
            if (checkU_Down(POS1, POS2, showPath))
            {
                return true;
            }
            return false;
        }

        //左U型
        bool checkU_Left(Vec2 POS1, Vec2 POS2, bool showPath)
        {
            if (POS1.R == POS2.R)
            {
                return false;
            }
            int ph1 = get_pos_between_horizontal(POS1, new Vec2(POS1.R, 0));
            int ph2 = get_pos_between_horizontal(POS2, new Vec2(POS2.R, 0));
            if (POS1.C < POS2.C && ph2 > POS1.C)
            {
                if (ph1 == 0 && ph2 == 0)
                {
                    if (showPath)
                    {
                        add_path_horizontal(POS1, new Vec2(POS1.R, 0));
                        add_path_vertical(new Vec2(POS1.R, 0), new Vec2(POS2.R, 0));
                        add_path_horizontal(new Vec2(POS2.R, 0), POS2);
                    }
                    return true;
                }
                return false;
            }
            else if (POS1.C > POS2.C && ph1 > POS2.C)
            {
                if (ph1 == 0 && ph2 == 0)
                {
                    if (showPath)
                    {
                        add_path_horizontal(POS1, new Vec2(POS1.R, 0));
                        add_path_vertical(new Vec2(POS1.R, 0), new Vec2(POS2.R, 0));
                        add_path_horizontal(new Vec2(POS2.R, 0), POS2);
                    }
                    return true;
                }
                return false;
            }

            int start = POS1.C > POS2.C ? POS2.C : POS1.C;
            int end = ph1 > ph2 ? ph1 : ph2;
            for (int i = start - 1; i > end; i--)
            {
                int pv = get_pos_between_vertical(new Vec2(POS1.R, i), new Vec2(POS2.R, i));
                if (pv == POS2.R)
                {
                    if (showPath)
                    {
                        add_path_horizontal(POS1, new Vec2(POS1.R, i));
                        add_path_vertical(new Vec2(POS1.R, i), new Vec2(POS2.R, i));
                        add_path_horizontal(new Vec2(POS2.R, i), POS2);
                    }
                    return true;
                }
            }
            if (ph1 == 0 && ph2 == 0)
            {
                if (showPath)
                {
                    add_path_horizontal(POS1, new Vec2(POS1.R, 0));
                    add_path_vertical(new Vec2(POS1.R, 0), new Vec2(POS2.R, 0));
                    add_path_horizontal(new Vec2(POS2.R, 0), POS2);
                }
                return true;
            }
            return false;
        }

        //右U型
        bool checkU_Right(Vec2 POS1, Vec2 POS2, bool showPath)
        {
            if (POS1.R == POS2.R)
            {
                return false;
            }
            int ph1 = get_pos_between_horizontal(POS1, new Vec2(POS1.R, col - 1));
            int ph2 = get_pos_between_horizontal(POS2, new Vec2(POS2.R, col - 1));
            if (POS1.C < POS2.C && ph1 < POS2.C)
            {
                if (ph1 == col - 1 && ph2 == col - 1)
                {
                    if (showPath)
                    {
                        add_path_horizontal(POS1, new Vec2(POS1.R, col - 1));
                        add_path_vertical(new Vec2(POS1.R, col - 1), new Vec2(POS2.R, col - 1));
                        add_path_horizontal(new Vec2(POS2.R, col - 1), POS2);
                    }
                    return true;
                }
                return false;
            }
            else if (POS1.C > POS2.C && ph2 < POS1.C)
            {
                if (ph1 == col - 1 && ph2 == col - 1)
                {
                    if (showPath)
                    {
                        add_path_horizontal(POS1, new Vec2(POS1.R, col - 1));
                        add_path_vertical(new Vec2(POS1.R, col - 1), new Vec2(POS2.R, col - 1));
                        add_path_horizontal(new Vec2(POS2.R, col - 1), POS2);
                    }
                    return true;
                }
                return false;
            }

            int start = POS1.C > POS2.C ? POS1.C : POS2.C;
            int end = ph1 > ph2 ? ph2 : ph1;
            for (int i = start + 1; i < end; i++)
            {
                int pv = get_pos_between_vertical(new Vec2(POS1.R, i), new Vec2(POS2.R, i));
                if (pv == POS2.R)
                {
                    if (showPath)
                    {
                        add_path_horizontal(POS1, new Vec2(POS1.R, i));
                        add_path_vertical(new Vec2(POS1.R, i), new Vec2(POS2.R, i));
                        add_path_horizontal(new Vec2(POS2.R, i), POS2);
                    }
                    return true;
                }
            }
            if (ph1 == col - 1 && ph2 == col - 1)
            {
                if (showPath)
                {
                    add_path_horizontal(POS1, new Vec2(POS1.R, col - 1));
                    add_path_vertical(new Vec2(POS1.R, col - 1), new Vec2(POS2.R, col - 1));
                    add_path_horizontal(new Vec2(POS2.R, col - 1), POS2);
                }
                return true;
            }
            return false;
        }

        //上U型
        bool checkU_Up(Vec2 POS1, Vec2 POS2, bool showPath)
        {
            if (POS1.C == POS2.C)
            {
                return false;
            }
            int pv1 = get_pos_between_vertical(POS1, new Vec2(row - 1, POS1.C));
            int pv2 = get_pos_between_vertical(POS2, new Vec2(row - 1, POS2.C));

            if (POS1.R < POS2.R && pv1 < POS2.R)
            {
                if (pv1 == row - 1 && pv2 == row - 1)
                {
                    if (showPath)
                    {
                        add_path_vertical(POS1, new Vec2(row - 1, POS1.C));
                        add_path_horizontal(new Vec2(row - 1, POS1.C), new Vec2(row - 1, POS2.C));
                        add_path_vertical(new Vec2(row - 1, POS2.C), POS2);
                    }
                    return true;
                }
                return false;
            }
            else if (POS1.R > POS2.R && pv2 < POS1.R)
            {
                if (pv1 == row - 1 && pv2 == row - 1)
                {
                    if (showPath)
                    {
                        add_path_vertical(POS1, new Vec2(row - 1, POS1.C));
                        add_path_horizontal(new Vec2(row - 1, POS1.C), new Vec2(row - 1, POS2.C));
                        add_path_vertical(new Vec2(row - 1, POS2.C), POS2);
                    }
                    return true;
                }
                return false;
            }

            int start = POS1.R > POS2.R ? POS1.R : POS2.R;
            int end = pv1 > pv2 ? pv2 : pv1;
            for (int i = start + 1; i < end; i++)
            {
                int ph = get_pos_between_horizontal(new Vec2(i, POS1.C), new Vec2(i, POS2.C));
                if (ph == POS2.C)
                {
                    if (showPath)
                    {
                        add_path_vertical(POS1, new Vec2(i, POS1.C));
                        add_path_horizontal(new Vec2(i, POS1.C), new Vec2(row - 1, POS2.C));
                        add_path_vertical(new Vec2(i, POS2.C), POS2);
                    }
                    return true;
                }
            }
            if (pv1 == row - 1 && pv2 == row - 1)
            {
                if (showPath)
                {
                    add_path_vertical(POS1, new Vec2(row - 1, POS1.C));
                    add_path_horizontal(new Vec2(row - 1, POS1.C), new Vec2(row - 1, POS2.C));
                    add_path_vertical(new Vec2(row - 1, POS2.C), POS2);
                }
                return true;
            }
            return false;
        }

        //下U型
        bool checkU_Down(Vec2 POS1, Vec2 POS2, bool showPath)
        {
            if (POS1.C == POS2.C)
            {
                return false;
            }
            int pv1 = get_pos_between_vertical(POS1, new Vec2(0, POS1.C));
            int pv2 = get_pos_between_vertical(POS2, new Vec2(0, POS2.C));

            if (POS1.R < POS2.R && pv2 > POS1.R)
            {
                if (pv1 == 0 && pv2 == 0)
                {
                    if (showPath)
                    {
                        add_path_vertical(POS1, new Vec2(0, POS1.C));
                        add_path_horizontal(new Vec2(0, POS1.C), new Vec2(0, POS2.C));
                        add_path_vertical(new Vec2(0, POS2.C), POS2);
                    }
                    return true;
                }
                return false;
            }
            else if (POS1.R > POS2.R && pv1 > POS2.R)
            {
                if (pv1 == 0 && pv2 == 0)
                {
                    if (showPath)
                    {
                        add_path_vertical(POS1, new Vec2(0, POS1.C));
                        add_path_horizontal(new Vec2(0, POS1.C), new Vec2(0, POS2.C));
                        add_path_vertical(new Vec2(0, POS2.C), POS2);
                    }
                    return true;
                }
                return false;
            }

            int start = POS1.R > POS2.R ? POS2.R : POS1.R;
            int end = pv1 > pv2 ? pv1 : pv2;
            for (int i = start - 1; i > end; i--)
            {
                int ph = get_pos_between_horizontal(new Vec2(i, POS1.C), new Vec2(i, POS2.C));
                if (ph == POS2.C)
                {
                    if (showPath)
                    {
                        add_path_vertical(POS1, new Vec2(i, POS1.C));
                        add_path_horizontal(new Vec2(i, POS1.C), new Vec2(row - 1, POS2.C));
                        add_path_vertical(new Vec2(i, POS2.C), POS2);
                    }
                    return true;
                }
            }
            if (pv1 == 0 && pv2 == 0)
            {
                if (showPath)
                {
                    add_path_vertical(POS1, new Vec2(0, POS1.C));
                    add_path_horizontal(new Vec2(0, POS1.C), new Vec2(0, POS2.C));
                    add_path_vertical(new Vec2(0, POS2.C), POS2);
                }
                return true;
            }
            return false;
        }

        //得到两物体横列间第一个遇到的物体
        int get_pos_between_horizontal(Vec2 POS1, Vec2 POS2)
        {
            if (POS1.C >= POS2.C)
            {
                for (int i = POS1.C - 1; i > POS2.C; i--)
                {
                    if (MAP[POS1.R][i] != -1)
                    {
                        return i;
                    }
                }
            }
            else
            {
                for (int i = POS1.C + 1; i < POS2.C; i++)
                {
                    if (MAP[POS1.R][i] != -1)
                    {
                        return i;
                    }
                }
            }
            return POS2.C;
        }

        //得到两物体竖列间第一个遇到的物体
        int get_pos_between_vertical(Vec2 POS1, Vec2 POS2)
        {
            if (POS1.R >= POS2.R)
            {
                for (int i = POS1.R - 1; i > POS2.R; i--)
                {
                    if (MAP[i][POS1.C] != -1)
                    {
                        return i;
                    }
                }
            }
            else
            {
                for (int i = POS1.R + 1; i < POS2.R; i++)
                {
                    if (MAP[i][POS1.C] != -1)
                    {
                        return i;
                    }
                }
            }
            return POS2.R;
        }

        //添加横向路径
        void add_path_horizontal(Vec2 POS1, Vec2 POS2)
        {
            if (POS1.C >= POS2.C)
            {
                for (int i = POS1.C; i >= POS2.C; i--)
                {
                    Vec2 pos = new Vec2(POS1.R, i);
                    if (!keyLPath.Contains(POS1.R + "_" + i))
                    {
                        LPath.Add(pos);
                        keyLPath.Add(POS1.R + "_" + i);
                    }
                }
            }
            else
            {
                for (int i = POS1.C; i <= POS2.C; i++)
                {
                    Vec2 pos = new Vec2(POS1.R, i);
                    if (!keyLPath.Contains(POS1.R + "_" + i))
                    {
                        LPath.Add(pos);
                        keyLPath.Add(POS1.R + "_" + i);
                    }
                }
            }
        }

        //添加竖向路径
        void add_path_vertical(Vec2 POS1, Vec2 POS2)
        {
            if (POS1.R >= POS2.R)
            {
                for (int i = POS1.R; i >= POS2.R; i--)
                {
                    Vec2 pos = new Vec2(i, POS1.C);
                    if (!keyLPath.Contains(i + "_" + POS1.C))
                    {
                        LPath.Add(pos);
                        keyLPath.Add(i + "_" + POS1.C);
                    }
                }
            }
            else
            {
                for (int i = POS1.R; i <= POS2.R; i++)
                {
                    Vec2 pos = new Vec2(i, POS1.C);
                    if (!keyLPath.Contains(i + "_" + POS1.C))
                    {
                        LPath.Add(pos);
                        keyLPath.Add(i + "_" + POS1.C);
                    }
                }
            }
        }

        #region 画线
        //画线
        private void DrawLine(ArrayList list, bool isEnemy)
        {
            if (list.Count > 2)
            {
                for (int i = 1; i < list.Count - 1; i++)
                {
                    Vec2 preObj = (Vec2)list[i - 1];
                    Vec2 curObj = (Vec2)list[i];
                    Vec2 nextObj = (Vec2)list[i + 1];
                    if (curObj.R == preObj.R
                        && curObj.R == nextObj.R)
                    {
                        CreateLine(POS[curObj.R][curObj.C], EPathType.Long_H);
                    }
                    if (curObj.C == preObj.C
                        && curObj.C == nextObj.C)
                    {
                        CreateLine(POS[curObj.R][curObj.C], EPathType.Long_V);
                    }
                    if (curObj.C == getSameRow(curObj, nextObj, preObj).C + 1
                        && curObj.R == getSameCol(curObj, nextObj, preObj).R + 1)
                    {
                        CreateLine(POS[curObj.R][curObj.C], EPathType.Left_Down);
                    }
                    if (curObj.C == getSameRow(curObj, nextObj, preObj).C + 1
                        && curObj.R == getSameCol(curObj, nextObj, preObj).R - 1)
                    {
                        CreateLine(POS[curObj.R][curObj.C], EPathType.Left_Up);
                    }
                    if (curObj.C == getSameRow(curObj, nextObj, preObj).C - 1
                        && curObj.R == getSameCol(curObj, nextObj, preObj).R - 1)
                    {
                        CreateLine(POS[curObj.R][curObj.C], EPathType.Right_Up);
                    }
                    if (curObj.C == getSameRow(curObj, nextObj, preObj).C - 1
                        && curObj.R == getSameCol(curObj, nextObj, preObj).R + 1)
                    {
                        CreateLine(POS[curObj.R][curObj.C], EPathType.Right_Down);
                    }
                }
            }
            else if (list.Count == 2)
            {
                Vec2 preObj = (Vec2)list[0];
                Vec2 curObj = (Vec2)list[1];
                if (curObj.R == preObj.R)
                {
                    CreateLine(POS[curObj.R][curObj.C] / 2 + POS[preObj.R][preObj.C] / 2, EPathType.Short_H);
                }
                if (curObj.C == preObj.C)
                {
                    CreateLine(POS[curObj.R][curObj.C] / 2 + POS[preObj.R][preObj.C] / 2, EPathType.Short_V);
                }
            }
            Vec2 first = (Vec2)list[0];
            Vec2 last = (Vec2)list[list.Count - 1];
            drawExplore(POS[first.R][first.C], isEnemy);
            drawExplore(POS[last.R][last.C], isEnemy);
        }

        //生成连线物体
        private void CreateLine(Vector3 pos, EPathType type)
        {
            GameObject instance = GameObject.Instantiate(pathObj[(int)type]);
            instance.transform.SetParent(mapTrans);
            instance.GetComponent<RectTransform>().localScale = Vector3.one;
            instance.GetComponent<RectTransform>().anchoredPosition = pos;
        }

        public Vec2 getSameRow(Vec2 cur, Vec2 v1, Vec2 v2)
        {
            if (cur.R == v1.R)
                return v1;
            if (cur.R == v2.R)
                return v2;
            return new Vec2(-1000, -1000);
        }

        public Vec2 getSameCol(Vec2 cur, Vec2 v1, Vec2 v2)
        {
            if (cur.C == v1.C)
                return v1;
            if (cur.C == v2.C)
                return v2;
            return new Vec2(-1000, -1000);
        }

        //绘制消除爆炸特效
        void drawExplore(Vector3 pos, bool isEnemy)
        {
            GameObject instance = null;
            if (isEnemy)
                instance = GameObject.Instantiate(ResourceMod.Instance.SyncLoad<GameObject>("Prefabs/Effect/ItemExploreEnemy.prefab"));
            else
                instance = GameObject.Instantiate(ResourceMod.Instance.SyncLoad<GameObject>("Prefabs/Effect/ItemExplore.prefab"));
            instance.transform.SetParent(mapTrans);
            instance.GetComponent<RectTransform>().localScale = Vector3.one;
            instance.GetComponent<RectTransform>().anchoredPosition = pos;
            
        }
        #endregion

        #endregion

        #endregion

        #region 消除周期流程

        #region old
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

        #region new
        //检查是否为1对（即是否可消除）
        void CheckPair(Vec2 pos)
        {
            POS2 = new Vec2(pos.R, pos.C);

            if (MAP_FROZEN[pos.R][pos.C] != -1)
            {
                return;
            }
            if (POS1 != null && MAP[POS1.R][POS1.C] != MAP[POS2.R][POS2.C])
            {
                ShakeTwoGood();

                AudioModule.PlayEffect(EAudioType.ECantMove);
                DeSelect();
                POS1 = null;
                POS2 = null;
                LPath.Clear();
                keyLPath.Clear();
            }
            else if (checkPaire(POS1, POS2, true))
            {
                //logicLevel.updateScore(nextScore);
                DrawLine(LPath, false);
                Eat(POS1, POS2, true);
            }
            else
            {
                ShakeTwoGood();

                AudioModule.PlayEffect(EAudioType.ECantMove);
                DeSelect();
                POS1 = null;
                POS2 = null;
                LPath.Clear();
                keyLPath.Clear();
            }
        }

        //震动两个物体
        private void ShakeTwoGood()
        {
            Good good1 = GetGood(POS1.R, POS1.C).GetComponent<Good>();
            Good good2 = GetGood(POS2.R, POS2.C).GetComponent<Good>();
            good1.Hint2();
            good2.Hint2();
        }

        //消除选中的两个物体
        public void Eat(Vec2 pos1, Vec2 pos2, bool isOffline)
        {
            AudioModule.PlayEffect(EAudioType.ElinkRemove);

            D.Log($"消去的是: ({ pos2.R},{ pos2.C}) , ({pos1.R},{pos1.C})");
            remainGood -= 2;

            ChangeMapState(EMapState.Eating);
            GameObject eatGood1 = GetGood(pos1.R, pos1.C);
            eatGood1.GetComponent<Animator>().SetTrigger("Dissapear");
            GameObject eatGood2 = GetGood(pos2.R, pos2.C);
            eatGood2.GetComponent<Animator>().SetTrigger("Dissapear");
            if (isOffline)
            {
                Game.Instance.StartCoroutine(execute_check_paire(pos1, pos2));
            }
            else
            {
                Game.Instance.StartCoroutine(execute_check_paire_online(pos1, pos2));
            }

            EatCountAddAndCallback();

            //特效部分
            FacadeEffect.PlayPluralFlyMoney(GameDefines.FlyMoney_Effect_LinkCount, eatGood1.transform, GamePlayFacade.GetFlyMoneyTarget());
            FacadeEffect.PlayPluralFlyMoney(GameDefines.FlyMoney_Effect_LinkCount, eatGood2.transform, GamePlayFacade.GetFlyMoneyTarget());
            STimerManager.Instance.CreateSDelay(GameDefines.FlyEffect_Start_Delay, () => 
            {
                FacadeEffect.PlayGetMoneyTipEffect(GameDefines.Single_Link_Money);
                PlayerFacade.AddWMoney(GameDefines.Single_Link_Money);
                GamePlayFacade.ChangeMoneyShow();
            });

            AudioModule.PlayVibrate();
        }

        //消除数累计添加及其回调
        private void EatCountAddAndCallback()
        {
            curTotalLinkCount += 1;
            FacadeTask.CheckLinkCount(curTotalLinkCount);

            if (curLevel == 1 || curLevel == 2) return;

            if(curLuckMomentCount == GameDefines.LuckMoment_Count_Max)
            {
                UIManager.Instance.OpenWindowAsync<UILuckMoment>(EUIType.EUILuckMoment);
            }
            else
                curLuckMomentCount += 1;

            if(curTopNoticeCount == GameDefines.TopNotice_Count_Max)
            {
                curTopNoticeCount = 0;
                FacadeEffect.PlayRewardNoticeEffect();
            }
            else
                curTopNoticeCount += 1;

            if(curAwesomeCount == GameDefines.Awesome_Count_Max)
            {
                curAwesomeCount = 0;
                UIManager.Instance.OpenWindowAsync<UIAwesome>(EUIType.EUIAwesome);
            }
            else
                curAwesomeCount += 1;

            if(curRateCount == GameDefines.Rate_Count_Max && FacadeTimeZone.IfNextDay())
            {
                UIManager.Instance.OpenWindowAsync<UIRate>(EUIType.EUIRate);
            }
            else
                curRateCount += 1;

            SPlayerPrefs.SetInt(PlayerPrefDefines.curTotalLinkCount, curTotalLinkCount);
            SPlayerPrefs.SetInt(PlayerPrefDefines.curLuckMomentCount, curLuckMomentCount);
            //SPlayerPrefs.SetInt(PlayerPrefDefines.curTopNoticeCount, curTopNoticeCount);
            //SPlayerPrefs.SetInt(PlayerPrefDefines.curAwesomeCount, curAwesomeCount);
        }

        //同步更新一些列检测（包含是否消除冰冻、是否消除冰冻倒计时）
        IEnumerator execute_check_paire(Vec2 pos1, Vec2 pos2)
        {
            yield return new WaitForSeconds(0.4f);
            RemoveGood(pos1);
            RemoveGood(pos2);
            UpdateHiddleState(pos1);
            UpdateHiddleState(pos2);
            UpdateClockState(pos1);
            UpdateClockState(pos2);
            UpdateMap(true);
        }

        //同步更新一些列检测（Test方法测试用，此处不调用）
        IEnumerator execute_check_paire_online(Vec2 pos1, Vec2 pos2)
        {
            yield return new WaitForSeconds(0.4f);
            RemoveGood(pos1);
            RemoveGood(pos2);
            UpdateMap(false);
        }

        //更新地图数据（是否进入下一关）
        public void UpdateMap(bool isOffline)
        {
            if (ClearMap())
            {
                if (curMapState == EMapState.Result)
                {
                    return;
                }
                ChangeMapState(EMapState.Result);
                D.Log($"+++++++++++++++++++++++++++++++++++++++++关卡 {curLevel} 通过+++++++++++++++++++++++++++++++++++++++++++++++++");
                D.Error($" ___________ 通关关卡：{curLevel}，用时：{passTime}");
                DeSelect();
                LPath.Clear();
                keyLPath.Clear();
                //logicLevel.collectReward();

                if (curLevel == LevelDefines.maxLevel)
                {
                    // final win
                    //logicLevel.updateScore(logicLevel.getScoreBonus() + GameConfig.bonus_victory);
                    //resultBar.showResult(timeBar.getNumStar(), logicLevel.getScore(), GameStatic.currentLevel, true);
                    //GameStatic.logLevel(GameStatic.currentMode, ItemController.getNumHintItem(), ItemController.getNumRandomItem(), GameStatic.currentLevel, 1, GameStatic.currentScore, false);
                    //GameStatic.endGame();
                }
                else
                {
                    float delayTime = GameDefines.FlyEffect_Start_Delay + GameDefines.FlyMoney_Effect_LinkCount * GameDefines.FlyMoney_ObjTime + GameDefines.FlyMoneyTip_ObjTime;
                    STimerManager.Instance.CreateSDelay(delayTime, () => 
                    {
                        UIManager.Instance.CloseUI(EUIType.EUIGamePlay);
                        UIManager.Instance.OpenAsync<UIChallengeSuccessful>(EUIType.EUIChallengeSuccessful);

                        FacadeTask.CheckLevelPass(curLevel);

                        NextLevel();
                    });

                    //logicLevel.updateScore(logicLevel.getScoreBonus());
                    //resultBar.showResult(timeBar.getNumStar(), logicLevel.getScore(), GameStatic.currentLevel, false);
                    //GameStatic.logLevel(GameStatic.currentMode, ItemController.getNumHintItem(), ItemController.getNumRandomItem(), GameStatic.currentLevel, 1, GameStatic.currentScore, false);
                    //GameStatic.saveGameWithoutMap();
                }
                //Save.countLevelPass();
            }
            else
            {
                Game.Instance.StartCoroutine(updateLogicLevel(isOffline));
            }
        }

        //是否清除场景（当场景中是否还存在可消除物（排除障碍物和“外围空位置（Id为-1的物体）”）存在，若存在则返回false，不进行清除）
        private bool ClearMap()
        {
            for (int i1 = 1; i1 < row - 1; i1++)
            {
                for (int j1 = 1; j1 < col - 1; j1++)
                {
                    if (MAP[i1][j1] != -1 && MAP[i1][j1] != GameDefines.OBS_FIXED_ID && MAP[i1][j1] != GameDefines.OBS_MOVING_ID)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        //更新关卡逻辑数据
        IEnumerator updateLogicLevel(bool isOffline)
        {
            yield return new WaitForSeconds(Time.deltaTime);
            //logicLevel.list_pos_need_update.Add(POS1);
            //logicLevel.list_pos_need_update.Add(POS2);
            //logicLevel.UpdateMap();
            list_pos_need_update.Add(POS1);
            list_pos_need_update.Add(POS2);
            UpdateMap();
            Game.Instance.StartCoroutine(end_update_map(isOffline));
        }

        #region 让地图物块移动
        //让地图物块移动
        private void UpdateMap()
        {
            for (int j = 0; j < list_pos_need_update.Count; j++)
            {
                for (int i = 0; i < curLevelDirection.Count; i++)
                {
                    Vec2 POS = (Vec2)list_pos_need_update[j];
                    ConstraintData constraint = (ConstraintData)curLevelDirection[i];
                    HandleMove(POS, constraint.cell1, constraint.cell2, constraint.direction, curLevelData.list_block_stone_and_frozen);
                }
            }
            list_pos_need_update.Clear();
        }

        //确认移动目标点并移动
        void HandleMove(Vec2 POS, Vec2 cell1, Vec2 cell2, int direction, ArrayList list_block_stone_and_frozen)
        {
            //Debug.LogError((EGoodMoveDic)direction + "   " + cell1.C + "," + cell1.R + "   " + cell2.C + "," + cell2.R);

            if (isInBound(POS, cell1, cell2))
            {
                Vec2 boundLeft;
                Vec2 boundRight;
                Vec2 boundBottom;
                Vec2 boundTop;
                switch ((EGoodMoveDic)direction)
                {
                    case EGoodMoveDic.Left:
                        boundLeft = getBoundLeft(list_block_stone_and_frozen, POS, cell1, cell2);
                        boundRight = getBoundRight(list_block_stone_and_frozen, POS, cell1, cell2);
                        MoveLeft(boundLeft, boundLeft.C, boundRight.C);
                        break;
                    case EGoodMoveDic.Right:
                        boundLeft = getBoundLeft(list_block_stone_and_frozen, POS, cell1, cell2);
                        boundRight = getBoundRight(list_block_stone_and_frozen, POS, cell1, cell2);
                        MoveRight(boundRight, boundLeft.C, boundRight.C);
                        break;
                    case EGoodMoveDic.Up:
                        boundBottom = getBoundBottom(list_block_stone_and_frozen, POS, cell1, cell2);
                        boundTop = getBoundTop(list_block_stone_and_frozen, POS, cell1, cell2);
                        MoveUp(boundTop, boundBottom.R, boundTop.R);
                        break;
                    case EGoodMoveDic.Down:
                        boundBottom = getBoundBottom(list_block_stone_and_frozen, POS, cell1, cell2);
                        boundTop = getBoundTop(list_block_stone_and_frozen, POS, cell1, cell2);
                        MoveDown(boundBottom, boundBottom.R, boundTop.R);
                        break;
                    case EGoodMoveDic.UpDown_Away:
                        break;
                    case EGoodMoveDic.UpDown_Closer:
                        break;
                    case EGoodMoveDic.LeftRight_Away:
                        
                        break;
                    case EGoodMoveDic.LeftRight_Closer: 
                        break;
                    default: 
                        break;
                }

                if (direction == (int)EGoodMoveDic.Left)
                {
                    
                }
                else if (direction == (int)EGoodMoveDic.Right)
                {

                }
                else if (direction == (int)EGoodMoveDic.Up)
                {
                    
                }
                else if (direction == (int)EGoodMoveDic.Down)
                {
                    
                }
            }
        }

        //判断目标物体是否位于由另外范围cell1和cell2所界定的矩形区域内
        bool isInBound(Vec2 POS, Vec2 cell1, Vec2 cell2)
        {
            if (POS == null)
            {
                return false;
            }
            if (Mathf.Min(cell1.C, cell2.C) <= POS.C && POS.C <= Mathf.Max(cell1.C, cell2.C)
                && Mathf.Min(cell1.R, cell2.R) <= POS.R && POS.R <= Mathf.Max(cell1.R, cell2.R))
            {
                return true;
            }
            return false;
        }

        //得到目标左边界位置
        private Vec2 getBoundLeft(ArrayList list_pos, Vec2 POS, Vec2 cell1, Vec2 cell2)
        {
            int temp = Mathf.Min(cell1.C, cell2.C) - 1;
            for (int i = 0; i < list_pos.Count; i++)
            {
                Vec2 pos = (Vec2)list_pos[i];
                if (pos.R == POS.R && pos.C < POS.C && pos.C > temp)
                {
                    temp = pos.C;
                }
            }
            return new Vec2(POS.R, temp + 1);
        }

        //得到目标右边界位置
        private Vec2 getBoundRight(ArrayList list_pos, Vec2 POS, Vec2 cell1, Vec2 cell2)
        {
            int temp = Mathf.Max(cell1.C, cell2.C) + 1;

            for (int i = 0; i < list_pos.Count; i++)
            {
                Vec2 pos = (Vec2)list_pos[i];
                if (pos.R == POS.R && pos.C > POS.C && pos.C < temp)
                {
                    temp = pos.C;
                }
            }
            return new Vec2(POS.R, temp - 1);
        }

        //得到目标上边界位置
        private Vec2 getBoundTop(ArrayList list_pos, Vec2 POS, Vec2 cell1, Vec2 cell2)
        {
            int temp = Mathf.Max(cell1.R, cell2.R) + 1;

            for (int i = 0; i < list_pos.Count; i++)
            {
                Vec2 pos = (Vec2)list_pos[i];
                if (pos.C == POS.C && pos.R > POS.R && pos.R < temp)
                {
                    temp = pos.R;
                }
            }
            return new Vec2(temp - 1, POS.C);
        }

        //得到目标下边界位置
        private Vec2 getBoundBottom(ArrayList list_pos, Vec2 POS, Vec2 cell1, Vec2 cell2)
        {
            int temp = Mathf.Min(cell1.R, cell2.R) - 1;

            for (int i = 0; i < list_pos.Count; i++)
            {
                Vec2 pos = (Vec2)list_pos[i];
                if (pos.C == POS.C && pos.R < POS.R && pos.R > temp)
                {
                    temp = pos.R;
                }
            }
            return new Vec2(temp + 1, POS.C);
        }

        //向上移动
        private void MoveUp(Vec2 POS, int row_down, int row_up)
        {
            ArrayList list_good_col1 = new ArrayList();
            for (int i = POS.R; i >= row_down; i--)
            {
                if (MAP[i][POS.C] != -1 && MAP[i][POS.C] != GameDefines.OBS_FIXED_ID && MAP_FROZEN[i][POS.C] == -1 || MAP_FROZEN[i][POS.C] == GameDefines.HID_MOVING_ID)
                {
                    list_good_col1.Add(GetGood(i, POS.C));
                }
            }
            int size1 = list_good_col1.Count;
            int count1 = list_good_col1.Count;
            bool hasDrop = false;
            for (int i = POS.R; i >= row_down; i--)
            {
                int type = -1;
                if (count1 > 0)
                {
                    GameObject obj = (GameObject)list_good_col1[size1 - count1];
                    Good good = obj.GetComponent<Good>();
                    type = good.id;
                    if (ChangeGood(obj, good, new Vec2(i, POS.C), GoodDefine.moveTime))
                        hasDrop = true;
                    count1--;
                }
                MAP[i][POS.C] = type;
            }
            if (hasDrop)
            {
                STimerManager.Instance.CreateSTimer(GoodDefine.moveTime, 0, false, true, () =>
                {
                    AudioModule.PlayEffect(EAudioType.EGoodMove);
                });
            }
        }

        //向下移动
        private void MoveDown(Vec2 POS, int row_down, int row_up)
        {
            ArrayList list_good_col1 = new ArrayList();
            for (int i = POS.R; i <= row_up; i++)
            {
                if (MAP[i][POS.C] != -1 && MAP[i][POS.C] != GameDefines.OBS_FIXED_ID && MAP_FROZEN[i][POS.C] == -1 || MAP_FROZEN[i][POS.C] == GameDefines.HID_MOVING_ID)
                {
                    list_good_col1.Add(GetGood(i, POS.C));
                }
            }
            int size1 = list_good_col1.Count;
            int count1 = list_good_col1.Count;
            bool hasDrop = false;
            for (int i = POS.R; i <= row_up; i++)
            {
                int type = -1;
                if (count1 > 0)
                {
                    GameObject obj = (GameObject)list_good_col1[size1 - count1];
                    Good good = obj.GetComponent<Good>();
                    type = good.id;
                    if (ChangeGood(obj, good, new Vec2(i, POS.C), GoodDefine.moveTime))
                    {
                        hasDrop = true;
                    }
                    count1--;
                }
                MAP[i][POS.C] = type;
            }
            if (hasDrop)
            {
                STimerManager.Instance.CreateSTimer(GoodDefine.moveTime, 0, false, true, () =>
                {
                    AudioModule.PlayEffect(EAudioType.EGoodMove);
                });
            }
        }

        //向左移动
        private void MoveLeft(Vec2 POS, int col_left, int col_right)
        {
            ArrayList list_good_row1 = new ArrayList();
            for (int i = POS.C; i <= col_right; i++)
            {
                if (MAP[POS.R][i] != -1 && MAP[POS.R][i] != GameDefines.OBS_FIXED_ID && MAP_FROZEN[POS.R][i] == -1 || MAP_FROZEN[POS.R][i] == GameDefines.HID_MOVING_ID)
                {
                    list_good_row1.Add(GetGood(POS.R, i));
                }
            }
            int size1 = list_good_row1.Count;
            int count1 = list_good_row1.Count;
            bool hasDrop = false;
            for (int i = POS.C; i <= col_right; i++)
            {
                int type = -1;
                if (count1 > 0)
                {
                    GameObject obj = (GameObject)list_good_row1[size1 - count1];
                    Good good = obj.GetComponent<Good>();
                    type = good.id;
                    if (ChangeGood(obj, good, new Vec2(POS.R, i), GoodDefine.moveTime))
                        hasDrop = true;
                    count1--;
                }
                MAP[POS.R][i] = type;
            }
            if (hasDrop)
            {
                STimerManager.Instance.CreateSTimer(GoodDefine.moveTime, 0, false, true, () =>
                {
                    AudioModule.PlayEffect(EAudioType.EGoodMove);
                });
            }
        }

        //向右移动
        private void MoveRight(Vec2 POS, int col_left, int col_right)
        {
            ArrayList list_good_row1 = new ArrayList();
            for (int i = POS.C; i >= col_left; i--)
            {
                if (MAP[POS.R][i] != -1 && MAP[POS.R][i] != GameDefines.OBS_FIXED_ID && MAP_FROZEN[POS.R][i] == -1 || MAP_FROZEN[POS.R][i] == GameDefines.HID_MOVING_ID)
                {
                    list_good_row1.Add(GetGood(POS.R, i));
                }
            }
            int size1 = list_good_row1.Count;
            int count1 = list_good_row1.Count;
            bool hasDrop = false;
            for (int i = POS.C; i >= col_left; i--)
            {
                int type = -1;
                if (count1 > 0)
                {
                    GameObject obj = (GameObject)list_good_row1[size1 - count1];
                    Good good = obj.GetComponent<Good>();
                    type = good.id;
                    if (ChangeGood(obj, good, new Vec2(POS.R, i), GoodDefine.moveTime))
                        hasDrop = true;
                    count1--;
                }
                MAP[POS.R][i] = type;
            }
            if (hasDrop)
            {
                STimerManager.Instance.CreateSTimer(GoodDefine.moveTime, 0, false, true, () =>
                {
                    AudioModule.PlayEffect(EAudioType.EGoodMove);
                });
            }
        }
        #endregion

        //数据逻辑更新完毕，开始更新关卡视图
        IEnumerator end_update_map(bool isOffline)
        {
            yield return new WaitForSeconds(0.1f);
            if (!isTutorial)
            {
                //GameStatic.saveGame();
            }
            if (isOffline)
            {
                DeSelect();
                LPath.Clear();
                keyLPath.Clear();
            }
            if (CheckGameOver())
            {
                isGameOver = true;
                //lastTime = currentTime;
                //GameStatic.logLevel(GameStatic.currentMode, ItemController.getNumHintItem(), ItemController.getNumRandomItem(), GameStatic.currentLevel, 0, GameStatic.currentScore, false);
            }
            int numberGoodCanEat = GetNumberGoodCanEat();
            D.Log("剩余可消除对数 : " + numberGoodCanEat);
            if (!isReseting && numberGoodCanEat == 0 && numberGoodRemain > 0)
            {
                UIManager.Instance.OpenNotice(LanguageModule.GetText("10094"));
                //Game.Instance.StartCoroutine(StartResetMap());
            }
            if (curMapState == EMapState.Eating)
                ChangeMapState(EMapState.Playing);
        }

        //开始重置地图
        IEnumerator StartResetMap()
        {
            AudioModule.PlayEffect(EAudioType.EGoodShuffle);
            isReseting = true;
            yield return new WaitForSeconds(Time.deltaTime + 0.05f);
            RandomMap();
        }

        // 重新生成地图（接口？）
        public void RandomMap()
        {
            _resetMap();
        }
        #endregion

        #endregion

        #region 点击物体
        //双击抖动
        private void SameKindShake(int kind)
        {

        }

        //被选中
        private void Select(Vec2 pos)
        {
            //IfShakeSameKind();

            if (checking_paire)
            {
                return;
            }
            if (pos.R < 1 || pos.R > row - 1 || pos.C < 1 || pos.C > col - 1)
            {
                return;
            }
            if (MAP[pos.R][pos.C] == -1 || MAP[pos.R][pos.C] == GameDefines.OBS_FIXED_ID
                || MAP[pos.R][pos.C] == GameDefines.OBS_MOVING_ID)
            {
                return;
            }
            if (MAP_FROZEN[pos.R][pos.C] != -1)
            {
                return;
            }
            GameObject obj = GetGood(pos.R, pos.C);
            Good good = obj.GetComponent<Good>();
            if (good.getState() != GameDefines.STATE_NORMAL)
            {
                return;
            }
            if (POS1 != null && POS1.C == pos.C && POS1.R == pos.R)//点击同一物体时
            {
                //if (ifkindShake >= 2)
                    ShakeSameKindGood(MAP_Goods[POS1.R][POS1.C].GetComponent<Good>().id);

                DeSelect();
                AudioModule.PlayEffect(EAudioType.EDeSelect);
            }
            else if (POS1 == null)
            {
                POS1 = new Vec2(pos.R, pos.C);
                if (obj != null)
                {
                    obj.GetComponent<Good>().Select();
                    RemoveHint();
                }
                AudioModule.PlayEffect(EAudioType.ESelect);
            }
            else if (POS2 == null)
            {
                POS2 = new Vec2(pos.R, pos.C);
                if (obj != null)
                {
                    obj.GetComponent<Good>().Select();
                    RemoveHint();
                    CheckPair(pos);
                }
            }
            else
            {
                return;
            }
        }

        //取消选中物体
        private void DeSelect()
        {
            D.Log("DESELECT :::");
            GameObject good = null;
            if (POS1 != null)
            {
                good = GetGood(POS1.R, POS1.C);
            }
            if (good != null)
            {
                good.GetComponent<Good>().DeSelect();
            }
            if (POS2 != null)
            {
                good = GetGood(POS2.R, POS2.C);
            }
            if (good != null)
            {
                good.GetComponent<Good>().DeSelect();
            }
            POS1 = null;
            POS2 = null;
        }


        #endregion

        #region 下三功能
        //提示可消除功能
        private void TipFunc()
        {
            ifHintFunc = true;

            int count = list_good_can_eat1.Count;
            if (count == 0)
            {
                return;
            }
            int r = Random.Range(0, count);
            if (r == count)
            {
                r = count - 1;
            }
            HINT_POS1 = (Vec2)list_good_can_eat1[r];
            HINT_POS2 = (Vec2)list_good_can_eat2[r];
            GameObject good = null;
            if (HINT_POS1 != null)
            {
                good = GetGood(HINT_POS1.R, HINT_POS1.C);
            }
            if (good != null)
            {
                good.GetComponent<Good>().Hint();
            }
            if (HINT_POS2 != null)
            {
                good = GetGood(HINT_POS2.R, HINT_POS2.C);
            }
            if (good != null)
            {
                good.GetComponent<Good>().Hint();
            }
        }

        //指定1个+随机1个消除功能
        private void RemoveFunc()
        {
            DeSelect();
            ifRemoveFunc = true;
        }
        private bool RemoveFunc2(Good good)
        {
            if(ifRemoveFunc)
            {
                ifRemoveFunc = false;

                List<Good> sameGoods = new List<Good>();

                for (int i1 = 1; i1 < row - 1; i1++)
                {
                    for (int j1 = 1; j1 < col - 1; j1++)
                    {
                        if (MAP[i1][j1] != -1 && MAP[i1][j1] != GameDefines.OBS_FIXED_ID && MAP[i1][j1] != GameDefines.OBS_MOVING_ID && MAP_FROZEN[i1][j1] == -1
                            && good.POS.C!= j1 && good.POS.R !=i1 && MAP[i1][j1] == good.id)
                        {
                            sameGoods.Add(MAP_Goods[i1][j1].GetComponent<Good>());
                        }
                    }
                }

                Vec2 good2POS = sameGoods[UnityEngine.Random.Range(0, sameGoods.Count)].POS;
                Eat(good.POS, good2POS, true);
                drawExplore(POS[good.POS.R][good.POS.C], false);
                drawExplore(POS[good2POS.R][good2POS.C], false);

                POS1 = good.POS;
                POS2 = good2POS;
                UpdateMap(false);

                return false;
            }
            else
            {
                return true;
            }

            
        }

        //改变当前关卡方向功能
        private EGoodMoveDic ChangeDirection()
        {
            curLevelDirection.Clear();

            int newDir;
            while (true)
            {
                newDir = UnityEngine.Random.Range(1, 9);
                if (newDir != curLevelData.levelDirEnum)
                    break;
            }
            curLevelDirection = curLevelData.SetDic(newDir);

            int XGoodCount = curLevelData.LevelXCount - 2; //12
            int YGoodCount = curLevelData.LevelYCount - 2; //8

            EGoodMoveDic EGMD = (EGoodMoveDic)newDir;

            D.Error("现在的方向是：" + EGMD);

            //上&上下离去的情况
            if (EGMD == EGoodMoveDic.Up || EGMD == EGoodMoveDic.UpDown_Away)
            {
                for(int i = 1; i <= YGoodCount; i++)
                {
                    list_pos_need_update.Add(new Vec2(XGoodCount, i));
                }
            }
            //下&上下离去的情况
            if (EGMD == EGoodMoveDic.Down || EGMD == EGoodMoveDic.UpDown_Away)
            {
                for (int i = 1; i <= YGoodCount; i++)
                {
                    list_pos_need_update.Add(new Vec2(1, i));
                }
            }
            //上下聚拢的情况
            if(EGMD == EGoodMoveDic.UpDown_Closer)
            {
                int temp = XGoodCount / 2;
                for(int i = 1;i <= YGoodCount;i++)
                {
                    list_pos_need_update.Add(new Vec2(temp, i));
                    list_pos_need_update.Add(new Vec2(temp + 1, i));
                }    
            }
            //左&左右离去的情况
            if (EGMD == EGoodMoveDic.Left || EGMD == EGoodMoveDic.LeftRight_Away)
            {
                for (int i = 1; i <= XGoodCount; i++)
                {
                    list_pos_need_update.Add(new Vec2(i, 1));
                }
            }
            //右&左右离去的情况
            if (EGMD == EGoodMoveDic.Right || EGMD == EGoodMoveDic.LeftRight_Away)
            {
                for (int i = 1; i <= XGoodCount; i++)
                {
                    list_pos_need_update.Add(new Vec2(i, YGoodCount));
                }
            }
            //左右聚拢的情况
            if (EGMD == EGoodMoveDic.LeftRight_Closer)
            {
                int temp = YGoodCount / 2;
                for (int i = 1; i <= XGoodCount; i++)
                {
                    list_pos_need_update.Add(new Vec2(i, temp));
                    list_pos_need_update.Add(new Vec2(i, temp + 1));
                }
            }
            list_pos_need_update.Clear();
            UpdateMap();

            D.Error("目前会和“提示功能”有冲突，当方向改变后，第一次提示会显示错误的位置");

            return EGMD;
        }

        //刷新功能
        private void RefushFunc()
        {
            if (curMapState == EMapState.Eating)
                return;
            AudioModule.PlayEffect(EAudioType.EGoodShuffle);
            _resetMap();
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

        private int GetCurLevel()
        {
            return curLevel;
        }

        /// <summary>
        /// 前进1关
        /// </summary>
        private void NextLevel()
        {
            if (ConfigModule.Instance.Tables.TBLevel.Get(curLevel).WithdrawType == 1)
            {
                curWLevel += 1;
                withdrawableLevel.Enqueue(ConfigModule.Instance.Tables.TBWithdrawableLevels.Get(curWLevel).Level);
                SPlayerPrefs.SetInt(PlayerPrefDefines.curWLevel, curWLevel);
                SPlayerPrefs.SetQueue<int>(PlayerPrefDefines.withdrawableLevel, withdrawableLevel);
            }

            curLevel += 1;
            SPlayerPrefs.SetInt(PlayerPrefDefines.curLevel, curLevel);
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

        #region 其他
        /// <summary>
        /// 改变当前关卡的状态
        /// </summary>
        /// <param name="state">改变状态</param>
        private void ChangeMapState(EMapState state)
        {
            if (curMapState == state)
                return;
            curMapState = state;
            if (curMapState == EMapState.Playing)
            {
                checkingPaire = false;
                for (int i = 0; i < listAutoGens.Count; i++)
                {
                    AutoGenController autoGen = (AutoGenController)listAutoGens[i];
                    autoGen.playClockCountDown();
                }
            }
            else if (curMapState == EMapState.Eating)
            {
                checkingPaire = true;
            }
            else if (curMapState == EMapState.Pause)
            {
                checkingPaire = true;
                for (int i = 0; i < listAutoGens.Count; i++)
                {
                    AutoGenController autoGen = (AutoGenController)listAutoGens[i];
                    autoGen.pauseClockCountDown();
                }
            }
            else if (curMapState == EMapState.Result)
            {
                checkingPaire = true;
                for (int i = 0; i < listAutoGens.Count; i++)
                {
                    AutoGenController autoGen = (AutoGenController)listAutoGens[i];
                    autoGen.pauseClockCountDown();
                }
            }
            else
            {
                checkingPaire = false;
            }
        }

        private EMapState GetMapState()
        {
            return curMapState;
        }

        //得到当前场景中可销毁的物体的数量
        public int GetNumberGoodCanEat()
        {
            numberGoodRemain = 0;
            int number = 0;
            list_good_can_eat1.Clear();
            list_good_can_eat2.Clear();
            for (int i1 = 1; i1 < row - 1; i1++)
            {
                for (int j1 = 1; j1 < col - 1; j1++)
                {
                    if (MAP[i1][j1] != -1 && MAP[i1][j1] != GameDefines.OBS_FIXED_ID
                        && MAP[i1][j1] != GameDefines.OBS_MOVING_ID && MAP_FROZEN[i1][j1] == -1)
                    {
                        for (int i2 = 1; i2 < row - 1; i2++)
                        {
                            for (int j2 = 1; j2 < col - 1; j2++)
                            {
                                if (i2 <= i1 && j2 <= j1) continue;
                                if (MAP_FROZEN[i2][j2] != -1) continue;
                                if (MAP[i2][j2] == MAP[i1][j1])
                                {
                                    HINT_POS1 = new Vec2(i1, j1);
                                    HINT_POS2 = new Vec2(i2, j2);
                                    if (checkPaire(HINT_POS1, HINT_POS2, false))
                                    {
                                        list_good_can_eat1.Add(HINT_POS1);
                                        list_good_can_eat2.Add(HINT_POS2);
                                        number++;
                                    }
                                }
                            }
                        }
                    }
                    //get number pokemon remain
                    if (MAP[i1][j1] != -1 && MAP[i1][j1] != GameDefines.OBS_FIXED_ID
                        && MAP[i1][j1] != GameDefines.OBS_MOVING_ID)
                    {
                        numberGoodRemain++;
                    }
                }
            }
            return number;
        }

        private void GetAllSameKindGood(int tagetId)
        {
            List<Good> sameGoods = new List<Good>();
            for (int i1 = 1; i1 < row - 1; i1++)
            {
                for (int j1 = 1; j1 < col - 1; j1++)
                {
                    if (MAP[i1][j1] != -1 && MAP[i1][j1] != GameDefines.OBS_FIXED_ID && MAP[i1][j1] != GameDefines.OBS_MOVING_ID && MAP_FROZEN[i1][j1] == -1
                        && MAP[i1][j1] == tagetId)
                    {
                        MAP_Goods[i1][j1].GetComponent<Good>().Hint2();
                        sameGoods.Add(MAP_Goods[i1][j1].GetComponent<Good>());
                    }
                }
            }

            Vec2 good2Vec2 = sameGoods[UnityEngine.Random.Range(0, sameGoods.Count)].POS;


        }

        //同种类的物体全部进行摇晃
        private void ShakeSameKindGood(int tagetId)
        {
            for (int i1 = 1; i1 < row - 1; i1++)
            {
                for (int j1 = 1; j1 < col - 1; j1++)
                {
                    if (MAP[i1][j1] != -1 && MAP[i1][j1] != GameDefines.OBS_FIXED_ID && MAP[i1][j1] != GameDefines.OBS_MOVING_ID && MAP_FROZEN[i1][j1] == -1
                        && MAP[i1][j1] == tagetId)
                    {
                        MAP_Goods[i1][j1].GetComponent<Good>().Hint2();
                    }
                }
            }
        }

        //判断是否双击才能进行摇晃
        private void IfShakeSameKind()
        {
            ifkindShake += 1;
            if (ifKSTimer != null && ifKSTimer.STimerState != STimerState.Standby)
                ifKSTimer.ReStart();
            else
            {
                ifKSTimer = STimerManager.Instance.CreateSDelay(0.5f, () =>
                {
                    ifkindShake = 0;
                });
            }
        }

        //更新冰冻物体列表
        private void UpdateListHiddle()
        {
            list_block_frozen_normal.Clear();
            list_block_frozen_special.Clear();
            if (list_block_frozen.Count == 0) return;
            Hashtable list_frozen_multi = new Hashtable();
            for (int i = 0; i < list_block_frozen.Count; i++)
            {
                Vec2 pos = (Vec2)list_block_frozen[i];
                if (isSingleHid(pos))
                {
                    list_block_frozen_normal.Add(pos);
                }
                else
                {
                    list_frozen_multi.Add(pos.R + "_" + pos.C, pos);
                }
            }
            bool isCreated = false;
            bool isCalculator = list_frozen_multi.Count > 0 ? true : false;
            while (isCalculator)
            {
                Hashtable list_frozen_special;
                if (!isCreated)
                {
                    isCreated = true;
                    list_frozen_special = new Hashtable();
                    list_block_frozen_special.Add(list_frozen_special);
                }
                else
                {
                    list_frozen_special = (Hashtable)list_block_frozen_special[list_block_frozen_special.Count - 1];
                }
                bool check = false;
                foreach (string key in list_frozen_multi.Keys)
                {
                    Vec2 pos = (Vec2)list_frozen_multi[key];
                    string pos_key = pos.R + "_" + pos.C;
                    if (list_frozen_special.Count == 0)
                    {
                        list_frozen_special.Add(pos_key, pos);
                        check = true;
                    }
                    else
                    {
                        ArrayList list_frozen_around = GetHidAround(pos);
                        bool isAdded = false;
                        for (int i = 0; i < list_frozen_around.Count; i++)
                        {
                            Vec2 pos_around = (Vec2)list_frozen_around[i];
                            string pos_around_key = pos_around.R + "_" + pos_around.C;
                            if (list_frozen_special.ContainsKey(pos_around_key) && !list_frozen_special.ContainsKey(pos_key))
                            {
                                list_frozen_special.Add(pos_key, pos);
                                check = true;
                                isAdded = true;
                                break;
                            }
                        }
                        if (isAdded)
                        {
                            for (int i = 0; i < list_frozen_around.Count; i++)
                            {
                                Vec2 pos_around = (Vec2)list_frozen_around[i];
                                string pos_around_key = pos_around.R + "_" + pos_around.C;
                                if (!list_frozen_special.ContainsKey(pos_around_key))
                                {
                                    list_frozen_special.Add(pos_around_key, pos_around);
                                }
                            }
                        }
                    }
                }
                foreach (string key in list_frozen_special.Keys)
                {
                    Vec2 pos = (Vec2)list_frozen_special[key];
                    list_frozen_multi.Remove(pos.R + "_" + pos.C);
                }
                if (!check)
                {
                    isCreated = false;
                }
                if (list_frozen_multi.Count == 0)
                {
                    isCalculator = false;
                }
            }
        }

        //更新冰冻状态(消除的物品周围的状态)
        public void UpdateHiddleState(Vec2 POS)
        {
            if (POS == null) return;
            bool isRemove = false;
            if (POS.R > 1)
            {
                if (MAP_FROZEN[POS.R - 1][POS.C] != -1)
                {
                    MAP_FROZEN[POS.R - 1][POS.C] = -1;
                    RemoveHidden(POS.R - 1, POS.C, true);
                    UpdateDataHiddle(POS.R - 1, POS.C);
                    //logicLevel.list_pos_need_update.Add(new Vec2(POS.R - 1, POS.C));
                    list_pos_need_update.Add(new Vec2(POS.R - 1, POS.C));
                    isRemove = true;
                }
            }
            if (POS.R < row - 2)
            {
                if (MAP_FROZEN[POS.R + 1][POS.C] != -1)
                {
                    MAP_FROZEN[POS.R + 1][POS.C] = -1;
                    RemoveHidden(POS.R + 1, POS.C, true);
                    UpdateDataHiddle(POS.R + 1, POS.C);
                    //logicLevel.list_pos_need_update.Add(new Vec2(POS.R + 1, POS.C));
                    list_pos_need_update.Add(new Vec2(POS.R + 1, POS.C));
                    isRemove = true;
                }
            }
            if (POS.C > 1)
            {
                if (MAP_FROZEN[POS.R][POS.C - 1] != -1)
                {
                    MAP_FROZEN[POS.R][POS.C - 1] = -1;
                    RemoveHidden(POS.R, POS.C - 1, true);
                    UpdateDataHiddle(POS.R, POS.C - 1);
                    //logicLevel.list_pos_need_update.Add(new Vec2(POS.R, POS.C - 1));
                    list_pos_need_update.Add(new Vec2(POS.R, POS.C - 1));
                    isRemove = true;
                }
            }
            if (POS.C < col - 2)
            {
                if (MAP_FROZEN[POS.R][POS.C + 1] != -1)
                {
                    MAP_FROZEN[POS.R][POS.C + 1] = -1;
                    RemoveHidden(POS.R, POS.C + 1, true);
                    UpdateDataHiddle(POS.R, POS.C + 1);
                    //logicLevel.list_pos_need_update.Add(new Vec2(POS.R, POS.C + 1));
                    list_pos_need_update.Add(new Vec2(POS.R, POS.C + 1));
                    isRemove = true;
                }
            }
            if (isRemove)
            {
                UpdateListHiddle();
            }
        }

        //移除场景中某行某列的冰冻特效
        public void RemoveHidden(int row, int col, bool showParticle)
        {
            GameObject hidden = GetHidden(row, col);
            if (hidden != null)
            {
                UnityEngine.Object.DestroyImmediate(hidden);
                if (showParticle)
                {
                    GameObject obj = GameObject.Instantiate(ResourceMod.Instance.SyncLoad<GameObject>(GameDefines.HidBreak_ObjPath));
                    obj.transform.SetParent(mapTrans);
                    obj.GetComponent<RectTransform>().localScale = Vector3.one;
                    obj.GetComponent<RectTransform>().anchoredPosition = POS[row][col];
                    obj.GetComponent<PathItem>().live(2);
                    AudioModule.PlayEffect(EAudioType.EReleaseHiddle);
                }
            }
            GameObject good = GetGood(row, col);
            if (good != null)
            {
                ((Good)good.GetComponent<Good>()).setFrozen(false);
            }
            MAP_FROZEN[row][col] = -1;
            for (int i = 0; i < list_block_frozen.Count; i++)
            {
                Vec2 pos = (Vec2)list_block_frozen[i];
                if (pos.R == row && pos.C == col)
                {
                    list_block_frozen.RemoveAt(i);
                }
            }
        }

        //检测目标周围的是否存在隐藏物体（存在返回false）
        bool isSingleHid(Vec2 POS)
        {
            if (POS.R > 1)
            {
                if (MAP_FROZEN[POS.R - 1][POS.C] != -1)
                {
                    return false;
                }
            }
            if (POS.R < row - 1)
            {
                if (MAP_FROZEN[POS.R + 1][POS.C] != -1)
                {
                    return false;
                }
            }
            if (POS.C > 1)
            {
                if (MAP_FROZEN[POS.R][POS.C - 1] != -1)
                {
                    return false;
                }
            }
            if (POS.C < col - 1)
            {
                if (MAP_FROZEN[POS.R][POS.C + 1] != -1)
                {
                    return false;
                }
            }
            return true;
        }

        //是否能去除冰冻（非冰冻、非空，非不移动障碍物时返回true）（游戏GameOver时用）
        bool CanDestroyFrozen(Vec2 POS)
        {
            if (POS == null) return true;
            if (POS.R > 1)
            {
                if (MAP_FROZEN[POS.R - 1][POS.C] == -1 && MAP[POS.R - 1][POS.C] != -1
                    && MAP[POS.R - 1][POS.C] != GameDefines.OBS_FIXED_ID
                    )
                {
                    return true;
                }
            }
            if (POS.R < row - 1)
            {
                if (MAP_FROZEN[POS.R + 1][POS.C] == -1 && MAP[POS.R + 1][POS.C] != -1
                    && MAP[POS.R + 1][POS.C] != GameDefines.OBS_FIXED_ID
                    )
                {
                    return true;
                }
            }
            if (POS.C > 1)
            {
                if (MAP_FROZEN[POS.R][POS.C - 1] == -1 && MAP[POS.R][POS.C - 1] != -1
                    && MAP[POS.R][POS.C - 1] != GameDefines.OBS_FIXED_ID
                    )
                {
                    return true;
                }
            }
            if (POS.C < col - 1)
            {
                if (MAP_FROZEN[POS.R][POS.C + 1] == -1 && MAP[POS.R][POS.C + 1] != -1
                    && MAP[POS.R][POS.C + 1] != GameDefines.OBS_FIXED_ID
                    )
                {
                    return true;
                }
            }
            return false;
        }

        //得当场景中某行某列的冰冻特效
        public GameObject GetHidden(int row, int col)
        {
            GameObject temp = GameObject.Find($"{GameDefines.HMName}{row}_{col}");
            if(temp == null)
                temp = GameObject.Find($"{GameDefines.HFName}{row}_{col}");
            return temp;
        }

        //得到目标周围的冰冻物体
        ArrayList GetHidAround(Vec2 POS)
        {
            ArrayList list_frozen = new ArrayList();
            if (POS.R > 1)
            {
                if (MAP_FROZEN[POS.R - 1][POS.C] != -1)
                {
                    list_frozen.Add(new Vec2(POS.R - 1, POS.C));
                }
            }
            if (POS.R < row - 1)
            {
                if (MAP_FROZEN[POS.R + 1][POS.C] != -1)
                {
                    list_frozen.Add(new Vec2(POS.R + 1, POS.C));
                }
            }
            if (POS.C > 1)
            {
                if (MAP_FROZEN[POS.R][POS.C - 1] != -1)
                {
                    list_frozen.Add(new Vec2(POS.R, POS.C - 1));
                }
            }
            if (POS.C < col - 1)
            {
                if (MAP_FROZEN[POS.R][POS.C + 1] != -1)
                {
                    list_frozen.Add(new Vec2(POS.R, POS.C + 1));
                }
            }
            return list_frozen;
        }

        //得到一个随机列表（随机冰冻时用）
        ArrayList GetRandomList(ArrayList listBefore)
        {
            ArrayList listAfter = new ArrayList();
            int count = listBefore.Count;
            while (count > 0)
            {
                int r = UnityEngine.Random.Range(0, count);
                listAfter.Add(listBefore[r]);
                listBefore.RemoveAt(r);
                count--;
            }
            return listAfter;
        }

        //更新冰冻状态的数据
        void UpdateDataHiddle(int row, int col)
        {
            for (int i = 0; i < curLevelData.list_block_stone_and_frozen.Count; i++)
            {
                Vec2 pos = (Vec2)curLevelData.list_block_stone_and_frozen[i];
                if (pos.R == row && pos.C == col)
                {
                    curLevelData.list_block_stone_and_frozen.RemoveAt(i);
                    return;
                }
            }
        }

        //去除自动冰冻的上的倒计时时钟
        private void UpdateClockState(Vec2 pos)
        {
            if (pos == null) return;
            for (int i = 0; i < listAutoGens.Count; i++)
            {
                AutoGenController autoGenControl = (AutoGenController)listAutoGens[i];
                Vec2 pos_clock = autoGenControl.pos;
                if (pos_clock == null)
                {
                    continue;
                }
                if (pos_clock.R == pos.R && pos_clock.C == pos.C
                    || pos_clock.R - 1 == pos.R && pos_clock.C == pos.C
                    || pos_clock.R + 1 == pos.R && pos_clock.C == pos.C
                    || pos_clock.R == pos.R && pos_clock.C - 1 == pos.C
                    || pos_clock.R == pos.R && pos_clock.C + 1 == pos.C)
                {
                    autoGenControl.lockAutoGen();
                }
            }
        }

        //检测是否游戏结束
        private bool CheckGameOver()
        {
            if (CheckGameOverByAllHiddle())
            {
                return true;
            }
            for (int i = 0; i < list_block_frozen_normal.Count; i++)
            {
                Vec2 pos = (Vec2)list_block_frozen_normal[i];
                if (!CanDestroyFrozen(pos))
                {
                    ZoomToFrozenWhenGameOver(pos);
                    return true;
                }
            }
            if (list_block_frozen_special.Count == 0) return false;
            for (int i = 0; i < list_block_frozen_special.Count; i++)
            {
                Hashtable list_frozen_special = (Hashtable)list_block_frozen_special[i];
                bool check = true;
                Vec2 posFrozen = new Vec2();
                foreach (string key in list_frozen_special.Keys)
                {
                    Vec2 pos = (Vec2)list_frozen_special[key];
                    if (CanDestroyFrozen(pos))
                    {
                        check = false;
                        break;
                    }
                    else
                    {
                        posFrozen = pos;
                    }
                }
                if (check)
                {
                    ZoomToFrozenWhenGameOver(posFrozen);
                    return true;
                }
            }

            return false;
        }

        //判断是否因冰冻而导致GameOver
        private bool CheckGameOverByAllHiddle()
        {
            int count = 0;
            for (int i = 1; i < row - 1; i++)
            {
                for (int j = 1; j < col - 1; j++)
                {
                    if (MAP_FROZEN[i][j] == -1 && MAP[i][j] != GameDefines.OBS_MOVING_ID
                        && MAP[i][j] != GameDefines.OBS_FIXED_ID && MAP[i][j] != -1)
                    {
                        count++;
                        if (count > 1)
                        {
                            break;
                        }
                    }
                }
                if (count > 1)
                {
                    break;
                }
            }
            if (count < 2)
            {
                return true;
            }
            return false;
        }

        //因冰冻而导致的GameOver时，移动摄像机到冰冻位置
        void ZoomToFrozenWhenGameOver(Vec2 pos)
        {
            return;
        }




        //改变场景中的物品的信息1版本
        public void ChangeGood(Good good, int type, Vec2 next_pos)
        {
            if (type == -1)
            {
                return;
            }
            if (type == GameDefines.OBS_FIXED_ID || type == GameDefines.OBS_MOVING_ID)
            {
                good.setSpecialInfo(type, next_pos.R, next_pos.C, POS[next_pos.R][next_pos.C], CELL_WIDH, CELL_HEIGHT, obsTrans);
                MAP[next_pos.R][next_pos.C] = type;
            }
            else
            {
                good.setInfo(type, next_pos.R, next_pos.C, POS[next_pos.R][next_pos.C], CELL_WIDH, CELL_HEIGHT, this.mapTrans);
                MAP[next_pos.R][next_pos.C] = type;
            }
        }

        //改变场景中的物品的信息2版本
        public bool ChangeGood(GameObject obj, Good good, Vec2 next_pos, float time_move)
        {
            if(obj.GetComponent<TestMark>() != null)
            {
                Debug.LogError(obj.name + " --> nextPos：(" + next_pos.R + "," + next_pos.C + ")");
            }

            if (MAP_FROZEN[good.POS.R][good.POS.C] == GameDefines.HID_MOVING_ID)
            {
                GameObject hidObj = GetHidden(good.POS.R, good.POS.C);
                if (hidObj != null) 
                {
                    hidObj.GetComponent<Good>().updateInfo(next_pos, POS[next_pos.R][next_pos.C], time_move);
                    MAP_FROZEN[good.POS.R][good.POS.C] = -1;
                    MAP_FROZEN[next_pos.R][next_pos.C] = GameDefines.HID_MOVING_ID;
                    hidObj.name = $"{GameDefines.HMName}{next_pos.R}_{next_pos.C}";
                }
            }


            if (good.POS.R == next_pos.R && good.POS.C == next_pos.C)
                return false;
            good.updateInfo(next_pos, POS[next_pos.R][next_pos.C], time_move);
            MAP[next_pos.R][next_pos.C] = good.id;
            MAP_Goods[next_pos.R][next_pos.C] = obj;
            return true;
        }

        //改变场景中的物品的信息3版本
        public void ChangeGood(Good good, Vec2 next_pos, float time_move)
        {
            good.updateInfo(next_pos, POS[next_pos.R][next_pos.C], time_move);
            MAP[next_pos.R][next_pos.C] = good.id;
        }

        //移除场景中的物体
        public void RemoveGood(Vec2 POS)
        {
            if (MAP[POS.R][POS.C] == -1)
                return;
            GameObject obj = GetGood(POS.R, POS.C);
            if (obj != null)
            {
                UnityEngine.Object.DestroyImmediate(obj);
            }
            MAP_Goods[POS.R][POS.C] = null;
            MAP[POS.R][POS.C] = -1;
        }

        //消去摇动（提示）特效
        void RemoveHint()
        {
            ifHintFunc = false;

            GameObject good = null;
            if (HINT_POS1 != null)
            {
                good = GetGood(HINT_POS1.R, HINT_POS1.C);
            }
            if (good != null)
            {
                good.GetComponent<Good>().RemoveHint();
            }
            if (HINT_POS2 != null)
            {
                good = GetGood(HINT_POS2.R, HINT_POS2.C);
            }
            if (good != null)
            {
                good.GetComponent<Good>().RemoveHint();
            }
        }

        /// <summary>
        /// 获取布局高度（包含上下空位）
        /// </summary>
        /// <returns>布局高度（包含上下空位）</returns>
        private int GetRow()
        {
            return row;
        }

        private void SetIsCheckWrong(bool b)
        {
            _isCheckWrong = b;
        }

        private void CheckIdAdd(object target)
        {
            check_id.Add(target);
        }

        //获取当前关卡的所有物体
        private GameObject[][] GetMAPGoods()
        {
            return MAP_Goods;
        }

        //获得累计消除次数
        private int GetCurTotalLinkCount()
        {
            return curTotalLinkCount;
        }

        //获得老虎机累计消除次数
        private int GetCurLuckMomentCount()
        {
            return curLuckMomentCount;
        }
        //设置老虎机累计消除次数
        private void SetCurLuckMomentCount(int value)
        {
            curLuckMomentCount = value;
            SPlayerPrefs.SetInt(PlayerPrefDefines.curLuckMomentCount, value);
        }

        //随机图片优先显示区间
        private void RandomGoodIconRange()
        {
            Dictionary<int, ConfGoodIcon> cgis = ConfigModule.Instance.Tables.TBGoodIcon.DataMap;
            
            foreach(KeyValuePair<int, ConfGoodIcon> item in cgis)
            {
                int priority = item.Value.Priority;
                if(!GoodIconRange.ContainsKey(priority)) 
                    GoodIconRange.Add(priority, new List<int>());
                GoodIconRange[priority].Add(item.Key);
            }
        }

        //设置随机图片
        private void SetRandomGoodIcon(int kinds)
        {
            randomGoodIcon.Clear();

            List<int> oldIds = new List<int>();
            List<int> auxiliaryIds = new List<int>();
            oldIds.AddRange(GoodIconRange[0]);
            int adds = oldIds.Count;
            int flowIdsCount = adds;
            if (kinds > adds)
            {
                int diff = kinds - adds;
                for (int i = 1; i < GoodIconRange.Count; i++)
                {
                    int curPIds = GoodIconRange[i].Count;

                    if (curPIds < diff)
                    {
                        oldIds.AddRange(GoodIconRange[i]);
                        diff -= curPIds;
                    }
                    else if (GoodIconRange[i].Count == diff)
                    {
                        flowIdsCount = kinds;
                        break;
                    }
                    else//GoodIconRange[i].Count > diff
                    {
                        List<int> temp = new List<int>(GoodIconRange[i]);
                        List<int> temp2 = new List<int>(temp);
                        ShuffleHelper.Shuffle(temp2);
                        for(int j = 0; j < diff; j++)
                        {
                            oldIds.Add(temp[j]);
                            auxiliaryIds.Add(temp2[j]);
                        }
                        flowIdsCount = kinds - diff;
                        break;
                    }
                }
            }

            if(kinds > oldIds.Count)
            {
                D.Error("种类超出了物品图片的总数");
                return;
            }

            List<int> newIds = new List<int>(oldIds);
            for(int i = 0; i < auxiliaryIds.Count; i ++)
            {
                newIds[newIds.Count - 1 - i] = auxiliaryIds[i];
            }

            ShuffleHelper.Shuffle(newIds);
            for (int i = 0; i < oldIds.Count; i++)
            {
                randomGoodIcon.Add(oldIds[i], newIds[i]);
            }
        }

        private Queue<int> GetWithdrawableLevel()
        {
            return withdrawableLevel;
        }

        private int GetCurWLevel()
        {
            return curWLevel;
        }

        private bool GetIsTutorial()
        {
            return isTutorial;
        }
        private void SetIsTutorial(bool b)
        {
            isTutorial = b;
            SPlayerPrefs.SetBool(PlayerPrefDefines.isTutorial, isTutorial);
        }

        private bool GetIfRemoveFunc()
        {
            return ifRemoveFunc;
        }

        private bool GetIfHintFunc()
        {
            return ifHintFunc;
        }

        private float GetRemainPCT()
        {
            float temp = (1 - 1f * remainGood / totalGood) * 100;
            string temp2 = temp.ToString("F2");
            return float.Parse(temp2);
        }

        #endregion
    }
}