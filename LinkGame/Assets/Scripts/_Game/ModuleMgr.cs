using System.Collections.Generic;

namespace XrCode
{
    public class ModuleMgr : Singleton<ModuleMgr>, ILoad, IDispose
    {
        private SceneMod sceneMod;
        private NotifyModule notifyMod;
        private UserModule userMod;
        private GamePlayModule gamePlayMod;
        private RedDotModule redDotModule;
        //private GameSDKManger gameSDKManger;
        private AudioModule audioMod;
        private TDAnalyticsManager tDAnalyticsManager;
        private LanguageModule languageMod;
        private GuideModule guideModule;
        private GamePlayModule gamePlayModule;
        private AdModule adModule;
        private TaskModule taskModule;
        public List<BaseModule> updateModList;

        public SceneMod SceneMod { get { return sceneMod; } }
        //public BulletMod BulletMod { get { return bulletMod; } }
        public NotifyModule NotifyMod { get { return notifyMod; } }
        public UserModule UserMod { get { return userMod; } }
        public GamePlayModule GamePlayMod { get { return gamePlayMod; } }
        public RedDotModule RedDotModule { get { return redDotModule; } }
        //public GameSDKManger GameSDKManger { get { return gameSDKManger; } }
        public AudioModule AudioMod { get { return audioMod; } }
        public TDAnalyticsManager TDAnalyticsManager { get { return tDAnalyticsManager; } }
        public LanguageModule LanguageMod { get { return languageMod; } }
        public GuideModule GuideModule { get { return guideModule; } }
        public AdModule AdModule { get { return adModule; } }
        public TaskModule TaskModule { get { return taskModule; } }

        private bool isLoaded = false;
        public void Load()
        {
            languageMod = new LanguageModule();
            updateModList = new List<BaseModule>();
            notifyMod = new NotifyModule();
            userMod = new UserModule();
            sceneMod = new SceneMod();
            gamePlayMod = new GamePlayModule();
            redDotModule = new RedDotModule();
            //gameSDKManger = new GameSDKManger();
            audioMod = new AudioModule();
            tDAnalyticsManager = new TDAnalyticsManager();
            guideModule = new GuideModule();
            adModule = new AdModule();
            taskModule = new TaskModule();
        }
        public void Dispose()
        {
            userMod.Dispose();
            isLoaded = false;
            SceneMod.Dispose();
            notifyMod.Dispose();
            gamePlayMod.Dispose();
            redDotModule.Dispose();
            //gameSDKManger.Dispose();
            audioMod.Dispose();
            tDAnalyticsManager.Dispose();
            guideModule.Dispose();
            adModule.Dispose();
            taskModule.Dispose();
        }

        public void Start()
        {
            languageMod.LoadCache();
            isLoaded = true;
            notifyMod.Load();
            userMod.Load();
            gamePlayMod.Load();
            redDotModule.Load();
            //gameSDKManger.Load();
            audioMod.Load();
            guideModule.Load();
            tDAnalyticsManager.Load();
            sceneMod.Load();
            adModule.Load();
            taskModule.Load();
            sceneMod.LoadScene(ESceneType.MainScene);
        }

        public void Update()
        {
            if (isLoaded)
            {
                AssetBundleMod.Instance.Update();
                for (int i = 0; i < updateModList.Count; i++)
                {
                    updateModList[i].Update();
                }

                UIManager.Instance.Update();
            }
        }

        public void FixedUpdate()
        {
            if (isLoaded)
            {
                //AssetBundleMod.Instance.Update();
                for (int i = 0; i < updateModList.Count; i++)
                {
                    updateModList[i].FixedUpdate();
                }
            }
        }

        //注册更新模块
        public void RegistUpdateObj(BaseModule module)
        {
            updateModList.Add(module);
        }
    }
}