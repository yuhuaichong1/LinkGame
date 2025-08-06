using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UI;
using XrCode;

public class AutoCarousel : MonoBehaviour
{
    private LanguageModule LanguageModule;

    public int showCount;
    public float height;
    public RectTransform content;
    private Vector2 startP;
    private Vector2 endP;
    private GameObject item;
    private Queue<GameObject> childrens;
    private STimer timer;

    //private PayTypeManager PayTypeManager;
    //private RankManager RankManager;

    void Awake()
    {
        LanguageModule = ModuleMgr.Instance.LanguageMod;
    }

    public void Play()
    {
        if (item == null)
        {
            item = ResourceMod.Instance.SyncLoad<GameObject>("Prefabs/UI/Carousel/CarouselItem.prefab");
            childrens = new Queue<GameObject>();
            startP = content.anchoredPosition;
            endP = content.anchoredPosition + new Vector2(0, height);
            for (int i = 0; i <= showCount; i++)
            {
                GameObject go = Instantiate(item, content);
                go.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, -i * height);
                RandomMsg(go);
                childrens.Enqueue(go);
            }

            timer = STimerManager.Instance.CreateSTimer(4, -1, true, true, () =>
            {
                GameObject temp = childrens.Dequeue();
                content.anchoredPosition = startP;
                temp.GetComponent<RectTransform>().anchoredPosition -= new Vector2(0, showCount * height);
                foreach (GameObject go in childrens)
                {
                    go.GetComponent<RectTransform>().anchoredPosition += new Vector2(0, height);
                }

                childrens.Enqueue(temp);

                RandomMsg(temp);

            }, null, new timingActions()
                {
                    timing = 3f,
                    clockActionType = ClockActionType.After,
                    clockAction = (detalTime)=>
                    {
                        content.anchoredPosition = Vector2.Lerp(startP, endP, (detalTime - 3f)/ 1);
                    }
                }
            );
        }
        else
        {
            content.anchoredPosition = startP;

            int i = 0;
            foreach (GameObject go in childrens)
            {
                int j = i++;
                go.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -j * height);
                RandomMsg(go);
            }

            timer.Start();

        }
    }

    public void Stop()
    {
        timer.Stop();
    }

    private void RandomMsg(GameObject obj)
    {
        List<PayItem> payItems = FacadePayType.GetPayItems();

        int count = payItems.Count;
        obj.transform.GetChild(0).GetComponent<Image>().sprite = payItems[UnityEngine.Random.Range(0, count)].icon;

        string name = PlayerFacade.GetRandomPlayerName();
        int level = UnityEngine.Random.Range(1, LevelDefines.maxLevel);
        int times = UnityEngine.Random.Range(1, 50);
        float money = times * UnityEngine.Random.Range(8f, 12f);
        string moneyShow = FacadePayType.RegionalChange(money);

        // 获取当前时间
        //System.DateTime currentTime = System.DateTime.Now;
        //int hour = currentTime.Hour;
        //int minute = currentTime.Minute;
        //int second = currentTime.Second;
        //string HMS = $"{hour:D2}:{minute:D2}:{second:D2}";
        //string content = string.Format(LanguageModule.GetText("10095"), name, times, moneyShow);
        //obj.transform.GetChild(1).GetComponent<Text>().text = $"<color=#FF0000>{HMS}</color> {content}";
        obj.transform.GetChild(1).GetComponent<Text>().text = string.Format(LanguageModule.GetText("10095"), name, level, times, moneyShow);
    }
}
