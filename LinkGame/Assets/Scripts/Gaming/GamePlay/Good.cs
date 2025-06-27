using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XrCode;
using DG.Tweening;

public class Good : MonoBehaviour
{
    public int id;
    public int onlineId;
    public Vec2 POS;
    public GameObject back;
    public GameObject backDefault;
    public GameObject backNew;
    public GameObject backKawai;
    public GameObject backBox;
    private Vector3 localScale;
    private Animator animator;
    private bool isHint = false;
    private int state;
    private bool isFrozen;
    public static List<Sprite> sprites = new List<Sprite>();
    public List<Sprite> _sprites = new List<Sprite>();

    private GameObject selectPaticle = null;
    void Start()
    {
        animator = GetComponent<Animator>();
        state = GameDefines.STATE_NORMAL;
        if (_sprites.Count > 0)
            sprites = _sprites;
    }

    // Update is called once per frame
    void Update()
    {
        if (isHint && animator.GetCurrentAnimatorStateInfo(0).IsName("ItemDefault"))
        {
            Hint();
        }
    }

    public void setInfo(int id, int row, int col, Vector3 pos, int width, int height, Transform parent)
    {
        this.id = id;
        this.POS = new Vec2(row, col);
        transform.position = pos;
        transform.parent = parent;
        backBox.SetActive(false);
        name = "pokemon_clone_" + row + "_" + col;
    }

    public void setSpecialInfo(int id, int row, int col, Vector3 pos, int width, int height, Transform parent)
    {
        this.id = id;
        this.POS = new Vec2(row, col);
        transform.position = pos;
        transform.parent = parent;
        string item_name = "";
        if (id == GameDefines.OBS_FIXED_ID)
        {
            item_name = "Obs3D_Fixed";
            transform.position = new Vector3(pos.x - width / 2, pos.y + height / 2, pos.z);
        }
        else if (id == GameDefines.OBS_MOVING_ID)
        {
            item_name = "Obs3D_Moving";
            backKawai.SetActive(false);
            backDefault.SetActive(false);
            backNew.SetActive(false);
            backBox.SetActive(true);
            back = backBox;
        }
        else if (id == GameDefines.HID_FIXED_ID)
        {
            item_name = "HidBox";
            SetSpecialOrder();
        }
        Sprite sprite = GetSprite(item_name);
        GetComponent<SpriteRenderer>().sprite = sprite;
        localScale = new Vector3(Mathf.Abs(width * 1.0f / sprite.bounds.size.x), Mathf.Abs(-height * 1.0f / sprite.bounds.size.y), 1);
        if (id == GameDefines.OBS_FIXED_ID)
        {
            localScale = localScale * 27f / 26f;
        }
        transform.localScale = localScale;
        name = "good_clone_" + row + "_" + col;
    }

    public void setSpecialInfo(int id, string special_name, int row, int col, Vector3 pos, int width, int height, Transform parent)
    {
        this.id = id;
        this.POS = new Vec2(row, col);
        transform.position = pos;
        transform.parent = parent;
        string item_name = "";
        if (id == GameDefines.OBS_FIXED_ID)
        {
            item_name = "stone3d";
            transform.position = new Vector3(pos.x - width / 2, pos.y + height / 2, pos.z);
        }
        else if (id == GameDefines.OBS_MOVING_ID)
        {
            item_name = "Crate";
            back = backBox;
            backKawai.SetActive(false);
            backDefault.SetActive(false);
            backNew.SetActive(false);
            backBox.SetActive(true);
        }
        else if (id == GameDefines.HID_FIXED_ID)
        {
            item_name = "IceBox";
            SetSpecialOrder();
        }

        Sprite sprite = GetSprite(item_name);
        GetComponent<SpriteRenderer>().sprite = sprite;
        localScale = new Vector3(Mathf.Abs(width * 1.0f / sprite.bounds.size.x), Mathf.Abs(-height * 1.0f / sprite.bounds.size.y), 1);
        if (id == GameDefines.OBS_FIXED_ID)
        {
            localScale = localScale * 27f / 26f;
        }
        transform.localScale = localScale;
        name = special_name + row + "_" + col;
    }

    public void Hint()
    {
        animator.SetTrigger("Hint");
        isHint = true;
    }
    public void RemoveHint()
    {
        try
        {
            if (!isActiveAndEnabled)
                return;
            if (animator != null)
                animator.SetTrigger("RemoveHint");
            isHint = false;
        }
        catch (Exception e)
        {
            D.Error(e.ToString());
        }
    }


    public void Select()
    {
        GetComponent<SpriteRenderer>().color = new Color(188 / 255f, 255 / 255f, 196 / 255f);
        back.GetComponent<SpriteRenderer>().color = new Color(71 / 255f, 39 / 255f, 39 / 255f, 200 / 255f);
        if (selectPaticle == null)
        {
            selectPaticle = Instantiate(Resources.Load("Prefab/Select")) as GameObject;
            selectPaticle.transform.SetParent(transform);
            selectPaticle.transform.localPosition = new Vector3(0, 0, 0);
            selectPaticle.name = "Select";
        }
    }

    public void DeSelect()
    {
        GetComponent<SpriteRenderer>().color = Color.white;
        back.GetComponent<SpriteRenderer>().color = Color.white;
        if (selectPaticle != null)
            Destroy(selectPaticle);
    }

    public void Eat(bool isOffline)
    {
        GamePlayFacade.ChangeMapState(EMapState.Eating);
        GetComponent<Animator>().SetTrigger("Dissapear");
        StartCoroutine(Disappear(isOffline));
        SetState(GameDefines.STATE_DISAPPEARING);
        GamePlayFacade.UpdateHiddleState(POS);
        if (!isOffline)
        {
            GamePlayFacade.RemoveHidden(POS.R, POS.C, false);
        }
    }

    public void setOnlineId(int onlineId)
    {
        this.onlineId = onlineId;
    }

    public void setOrder()
    {
        int order = ((GamePlayFacade.GetRow() - 2 - POS.R) * (GamePlayFacade.GetRow() - 2) + POS.C) * 2;
        back.GetComponent<SpriteRenderer>().sortingOrder = order;
        GetComponent<SpriteRenderer>().sortingOrder = order + 1;
    }

    public void updateInfo(Vec2 POS, Vector3 pos, float time_move)
    {
        this.POS = POS;
        setOrder();
        name = "good_clone_" + POS.R + "_" + POS.C;
        gameObject.transform.DOMove(pos, time_move);


    }

    public void setFrozen(bool isFrozen)
    {
        this.isFrozen = isFrozen;
    }

    public void SetSpecialOrder()
    {
        GetComponent<SpriteRenderer>().sortingOrder = 1000;
    }

    public Sprite GetSprite(string spriteName)
    {
        foreach (Sprite sprite in sprites)
        {
            if (sprite != null && sprite.name == spriteName)
                return sprite;
        }
        return null;
    }

    IEnumerator Disappear(bool isOffline)
    {
        yield return new WaitForSeconds(0.4f);
        SetState(GameDefines.STATE_NORMAL);
        GamePlayFacade.RemoveGood(POS);
        GamePlayFacade.UpdateMap(isOffline);
        if (GamePlayFacade.GetMapState() == EMapState.Eating)
            GamePlayFacade.ChangeMapState(EMapState.Playing);
        if (!isOffline)
        {
            GamePlayFacade.SetIsCheckWrong(true);
            GamePlayFacade.CheckIdAdd(id);
        }
    }

    public void SetState(int state)
    {
        this.state = state;
    }

    internal int getState()
    {
        return state;
    }
}
