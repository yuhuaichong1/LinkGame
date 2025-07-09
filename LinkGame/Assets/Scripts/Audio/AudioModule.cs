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
        private AudioSource musicSource;                                  // ��Ч�ͱ������ֵ���Դ
        private List<AudioSource> effectSources;
        private Dictionary<int, AudioClip> clipMap;                       // ��Ƶ����
        private bool isPlayBgming;                                        // �Ƿ����ڲ��ű�������
        private GameObject gameObject;
        private float musicVolume = 0.45f;                                 // ��������
        private float effectsVolume = 0.6f;
        private float mCoolDownDur = 0.05f;                                // ���ư�ť���Ƶ��
        private bool enableBtn = true;                                    // ���ư�ť�Ƿ񼤻�

        private bool ifVibrate = true;                                    //�Ƿ���

        private AnimationCurve curve;

        protected override void OnLoad()
        {
            gameObject = new GameObject("AudioModule");
            GameObject.DontDestroyOnLoad(gameObject);
            musicSource = gameObject.AddComponent<AudioSource>();
            musicSource.loop = true;  // ��������Ĭ��ѭ��
            effectSources = new List<AudioSource>();
            clipMap = new Dictionary<int, AudioClip>();
            PlayBgm();
            RedirectButton();

            curve = UIManager.Instance.GetAnimCurve("UniformMotion");

            LoadMusicData();
            LoadVibrateData();
        }

        //��ȡ��Ƶ����
        private void LoadMusicData()
        {
            float value = SPlayerPref.GetFloat(PlayerPrefDefines.musicToggle);
            musicVolume = value;
            effectsVolume = value;
        }
        //��ȡ������
        private void LoadVibrateData()
        {
            bool value = SPlayerPref.GetBool(PlayerPrefDefines.musicToggle);
            ifVibrate = value;
        }

        // ͳһ����ť�¼�
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

                //������Ʒ�Ͱ�ť

                if (eventData.selectedObject == null)
                {
                    PlayButtonSound();
                }
                else
                {
                    
                }
                handler.OnPointerClick(pointerEventData);
                enableBtn = false;
                // �Լ�ʵ�ֵ�һ����ʱ��
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

        // ��ȡ��Ƶ�ļ������Ȼ�������ȡ��û���ټ��ء�
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
            D.Log("ͳһ���Ű�ť��Ч");
            AudioClip clip = GetAudioClip(EAudioType.EButton);
            if (clip == null) return;
            AudioSource source = GetAvailableAudioSource();
            source.clip = clip;
            source.volume = effectsVolume;
            source.Play();
        }

        // ���ű�������
        public void PlayBgm()
        {
            if (isPlayBgming) return;
            isPlayBgming = true;
            AudioClip bgm = GetAudioClip(EAudioType.EBgm);
            musicSource.clip = bgm;
            musicSource.volume = musicVolume;
            musicSource.Play();
        }
        // ֹͣ��������
        public void StopBgm()
        {
            if (!isPlayBgming) return;
            isPlayBgming = false;
            musicSource.Stop();
        }

        // ������Ч
        public void PlayEffect(EAudioType aType)
        {
            AudioClip clip = GetAudioClip(aType);
            if (clip == null) return;
            AudioSource source = GetAvailableAudioSource();
            source.clip = clip;
            source.volume = effectsVolume;
            source.Play();
        }
        // ��ȡһ�����е���Դ
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

        // ���ñ�����������
        public void SetMusicVolume(float volume)
        {
            musicVolume = Mathf.Clamp01(volume);
            musicSource.volume = musicVolume;

            SPlayerPref.SetFloat(PlayerPrefDefines.musicToggle, volume);
        }

        // ������Ч����
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