using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XrCode;

public class AutoCarousel : MonoBehaviour
{
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

        int times = UnityEngine.Random.Range(1, 50);
        float money = times * UnityEngine.Random.Range(8f, 12f);
        string moneyShow = FacadePayType.RegionalChange(money);

        obj.transform.GetChild(1).GetComponent<Text>().text = string.Format(ModuleMgr.Instance.LanguageMod.GetText(""), name, times, moneyShow);
    }
}
