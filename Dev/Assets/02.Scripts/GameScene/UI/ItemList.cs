using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemList : MonoBehaviour,IListener
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

    public List<Item> itemList;
    public List<string> inventoryItem = new List<string>();
    public List<GameObject> ListinventorySlots = new List<GameObject>();
    public List<InventorySlot> inventorySlots = new List<InventorySlot>();
    public Item nowSelectItem;
    
    public void OnEvent(EVENT_TYPE et, Component sender, object param = null)
    {
        switch (et)
        {
            case EVENT_TYPE.INIT_PLAYERDATA:
                {
                    OnLoadItem();
                    break;
                }
        }
    }

    public void Start()
    {
        //현재 가지고 있는 아이템 리스트를 불러 옴
        //for test
        //inventoryItem.Add("I0001");
        //inventoryItem.Add("I0002");
        csEventManager.Instance.AddListener(EVENT_TYPE.INIT_PLAYERDATA, this);
        foreach(var e in ListinventorySlots)
        {
            inventorySlots.Add(e.GetComponent<InventorySlot>());
        }
        //UpdateUI();
    }
    public void HideSelect()
    {
        foreach (var e in inventorySlots)
        {
            e.nowSelect.SetActive(false);
        }
    }

    public void OnLoadItem()
    {
        inventoryItem = PlayerData.singletone.playData.nowHaveItem;
        if(inventoryItem == null)
        {
            inventoryItem = new List<string>();
        }
        
        UpdateUI();

        //for test
        //AddItem("I0001");
        //AddItem("I0002");

    }

    public void SelectItem(InventorySlot input = null)
    {
        nowSelectItem = input.myItem;
    }
    public void SaveNowList()
    {
        PlayerData.singletone.AddItem(inventoryItem);
    }
    public void AddItem(string itemIndex)
    {
        inventoryItem.Add(itemIndex);
        inventoryItem.Sort();
        UpdateUI();
    }
    public void RemoveItem(string itemIndex)
    {
        inventoryItem.Remove(itemIndex);
        inventoryItem.Sort();
        UpdateUI();
    }
    public void UpdateUI()
    {

        for(int i = 0; i < inventoryItem.Count; i++)
        {
            inventorySlots[i].SetItem(itemList.Find(x => x.Index == inventoryItem[i]));
        }
        SaveNowList();
    }

}
