using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

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