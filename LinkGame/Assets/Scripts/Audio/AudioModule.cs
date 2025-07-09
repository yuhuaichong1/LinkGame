using cfg;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
namespace XrCode
{
    public class AudioModule : BaseModule
    {
        private AudioSource musicSource;                                  // 音效和背景音乐的音源
        private List<AudioSource> effectSources;
        private Dictionary<int, AudioClip> clipMap;                       // 音频缓存
        private bool isPlayBgming;                                        // 是否正在播放背景音乐
        private GameObject gameObject;
        private float musicVolume = 0.45f;                                 // 音量控制
        private float effectsVolume = 0.6f;
        private float mCoolDownDur = 0.05f;                                // 控制按钮点击频率
        private bool enableBtn = true;                                    // 控制按钮是否激活

        private bool ifVibrate = true;                                    //是否震动

        private AnimationCurve curve;

        protected override void OnLoad()
        {
            gameObject = new GameObject("AudioModule");
            GameObject.DontDestroyOnLoad(gameObject);
            musicSource = gameObject.AddComponent<AudioSource>();
            musicSource.loop = true;  // 背景音乐默认循环
            effectSources = new List<AudioSource>();
            clipMap = new Dictionary<int, AudioClip>();
            PlayBgm();
            RedirectButton();

            curve = UIManager.Instance.GetAnimCurve("UniformMotion");

            LoadMusicData();
            LoadVibrateData();
        }

        //获取音频数据
        private void LoadMusicData()
        {
            float value = SPlayerPref.GetFloat(PlayerPrefDefines.musicToggle);
            musicVolume = value;
            effectsVolume = value;
        }
        //获取震动数据
        private void LoadVibrateData()
        {
            bool value = SPlayerPref.GetBool(PlayerPrefDefines.musicToggle);
            ifVibrate = value;
        }

        // 统一处理按钮事件
        private void RedirectButton()
        {
            //typeof(ExecuteEvents).GetField("s_PointerClickHandler", BindingFlags.NonPublic | BindingFlags.Static).SetValue(null, new ExecuteEvents.EventFunction<IPointerClickHandler>(OnPointerClick));

            //typeof(ExecuteEvents).GetField("s_PointerDownHandler", BindingFlags.NonPublic | BindingFlags.Static).SetValue(null, new ExecuteEvents.EventFunction<IPointerDownHandler>(OnPointerDown));
            //typeof(ExecuteEvents).GetField("s_PointerUpHandler", BindingFlags.NonPublic | BindingFlags.Static).SetValue(null, new ExecuteEvents.EventFunction<IPointerUpHandler>(OnPointerUp));
        }
        void OnPointerClick(IPointerClickHandler handler, BaseEventData eventData)
        {
            PointerEventData pointerEventData = ExecuteEvents.ValidateEventData<PointerEventData>(eventData);
            if (pointerEventData != null)
            {
                if (!enableBtn) return;

                //区分物品和按钮

                if (eventData.selectedObject == null)
                {
                    PlayButtonSound();
                }
                else
                {
                    
                }
                handler.OnPointerClick(pointerEventData);
                enableBtn = false;
                // 自己实现的一个计时器
                TimerManager.Instance.CreateTimer(mCoolDownDur, () => { enableBtn = true; }, 0);

                //ModuleMgr.Instance.TDAnalyticsManager.ButtonClick(eventData.selectedObject);
            }
        }

        public void OnPointerDown(IPointerDownHandler handler, BaseEventData eventData)
        {
            PointerEventData pointerEventData = ExecuteEvents.ValidateEventData<PointerEventData>(eventData);
            if (pointerEventData != null)
            {
                if (pointerEventData.pointerPressRaycast.gameObject != null)
                {
                    GameObject obj = pointerEventData.pointerPressRaycast.gameObject;
                    if (obj.transform.tag != "Good" && obj.transform.tag != "NoUpDownAnim")
                    {
                        STimerManager.Instance.CreateSTimer(0.2f, 0, false, true, null, (detalTime) =>
                        {
                            float s = curve.Evaluate(1 - 0.1f * (detalTime / 0.2f));
                            if (obj != null)
                                obj.GetComponent<RectTransform>().localScale = new Vector3(s, s, s);
                        });
                    }
                }
                handler.OnPointerDown(pointerEventData);
            }
        }

        public void OnPointerUp(IPointerUpHandler handler, BaseEventData eventData)
        {
            PointerEventData pointerEventData = ExecuteEvents.ValidateEventData<PointerEventData>(eventData);
            if (pointerEventData != null)
            {
                if (eventData.selectedObject != null)
                {
                    GameObject obj = eventData.selectedObject;

                    if (obj.transform.tag != "Good" && obj.transform.tag != "NoUpDownAnim")
                    {
                        STimerManager.Instance.CreateSTimer(0.2f, 0, false, true, null, (detalTime) =>
                        {
                            float s = curve.Evaluate(0.9f + 0.1f * (detalTime / 0.2f));
                            if (obj != null)
                                obj.GetComponent<RectTransform>().localScale = new Vector3(s, s, s);
                        });
                    }
                }
                handler.OnPointerUp(pointerEventData);
            }
        }


        protected override void OnDispose()
        {
            base.OnDispose();
            StopBgm();

            for (int i = 0; i < effectSources.Count; i++)
            {
                effectSources[i].clip = null;
                effectSources = null;
            }
            effectSources.Clear();

            clipMap.Clear();
            clipMap = null;
        }

        // 获取音频文件，优先缓存里面取，没有再加载。
        private AudioClip GetAudioClip(EAudioType aType)
        {
            int audioId = (int)aType;
            if (!clipMap.TryGetValue(audioId, out AudioClip clip))
            {
                ConfAudio conf = ConfigModule.Instance.Tables.TBAudio.Get(audioId);
                if (conf == null) return null;
                clip = ResourceMod.Instance.SyncLoad<AudioClip>(conf.AudioPath);
                clipMap.Add(audioId, clip);
            }
            return clip;
        }

        private void PlayButtonSound()
        {
            D.Log("统一播放按钮音效");
            AudioClip clip = GetAudioClip(EAudioType.EButton);
            if (clip == null) return;
            AudioSource source = GetAvailableAudioSource();
            source.clip = clip;
            source.volume = effectsVolume;
            source.Play();
        }

        // 播放背景音乐
        public void PlayBgm()
        {
            if (isPlayBgming) return;
            isPlayBgming = true;
            AudioClip bgm = GetAudioClip(EAudioType.EBgm);
            musicSource.clip = bgm;
            musicSource.volume = musicVolume;
            musicSource.Play();
        }
        // 停止背景音乐
        public void StopBgm()
        {
            if (!isPlayBgming) return;
            isPlayBgming = false;
            musicSource.Stop();
        }

        // 播放音效
        public void PlayEffect(EAudioType aType)
        {
            AudioClip clip = GetAudioClip(aType);
            if (clip == null) return;
            AudioSource source = GetAvailableAudioSource();
            source.clip = clip;
            source.volume = effectsVolume;
            source.Play();
        }
        // 获取一个空闲的音源
        private AudioSource GetAvailableAudioSource()
        {
            AudioSource source = effectSources.Find(s => !s.isPlaying);
            if (source == null)
            {
                source = gameObject.AddComponent<AudioSource>();
                effectSources.Add(source);
            }
            return source;
        }

        // 设置背景音乐音量
        public void SetMusicVolume(float volume)
        {
            musicVolume = Mathf.Clamp01(volume);
            musicSource.volume = musicVolume;

            SPlayerPref.SetFloat(PlayerPrefDefines.musicToggle, volume);
        }

        // 设置音效音量
        public void SetEffectsVolume(float volume)
        {
            effectsVolume = Mathf.Clamp01(volume);
            foreach (var source in effectSources)
            {
                source.volume = effectsVolume;
            }
        }


        public void PlayVibrate()
        {
            if(ifVibrate)
            {
#if UNITY_ANDROID
                if(SystemInfo.supportsVibration)
                    Handheld.Vibrate();
#elif UNITY_IOS
                if(SystemInfo.supportsVibration)
                    Handheld.Vibrate();
#endif
            }
        }

        public void SetVibrate(bool b)
        {
            ifVibrate = b;
            SPlayerPref.SetBool(PlayerPrefDefines.vibrateToggle, b);
        }

    }
}