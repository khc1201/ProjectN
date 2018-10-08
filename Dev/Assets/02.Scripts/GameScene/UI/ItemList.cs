using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemList : MonoBehaviour, IListener
{
    public static ItemList singletone;
    public void Awake()
    {
        if(singletone == null)
        {
            singletone = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            DestroyImmediate(this);
            return;
        }
    }
    public List<GameObject> itemList;
    public List<Image> itemImage;
    public List<GameObject> nowItems;
    public List<Button> itemButton;
    public void Start()
    {
        csEventManager.Instance.AddListener(EVENT_TYPE.GET_ITEM, this);
        foreach (var e in itemList)
        {
            itemImage.Add(e.GetComponent<Image>());
            nowItems.Add(e.transform.GetChild(0).gameObject);
            itemButton.Add(e.GetComponentInChildren<Button>());
        }

        //RefeshItem();
        //RefeshNowSelection();
    }
    public void RefeshItem()
    {
        for(int i = 0; i < itemList.Count; i++)
        {
            itemImage[i] = GetIcon(PlayerData.singletone.playData.nowHaveItem[i]);
        }
    }
    private Image GetIcon(string target)
    {
        return Resources.Load("ItemIcon/" + target + ".png") as Image;
    }
    public void RefeshNowSelection()
    {
        foreach (var e in nowItems)
        {
            e.SetActive(false);
        }
        int value = PlayerData.singletone.playData.nowItem;
        nowItems[value].SetActive(true);
    }
    public void OnEvent(EVENT_TYPE et, Component sender, object param = null)
    {
        switch (et)
        {
            case EVENT_TYPE.GET_ITEM:
                {
                    RefeshItem();
                    break;
                }
        }
    }
    
}
