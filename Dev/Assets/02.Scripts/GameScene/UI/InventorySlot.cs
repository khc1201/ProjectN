using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour {
    public bool isSelected;
    public Image itemIcon;
    public GameObject nowSelect;
    public Item myItem;


    public void Start()
    {
        nowSelect.SetActive(false);
    }

    public void SetItem(Item item = null)
    {
        //for test
        //Debug.Log(item.Index);
        if(item == null)
        {
            itemIcon.enabled = false;
            myItem = null;
            return;
        }
        myItem = item;
        itemIcon.sprite = myItem.Icon;
        itemIcon.enabled = true;
    }

    public void SelectThis()
    {
        foreach(var e in ItemList.singletone.inventorySlots)
        {
            e.nowSelect.SetActive(false);
            e.isSelected = false;
        }
        this.isSelected = true;
        this.nowSelect.SetActive(true);
        ItemList.singletone.nowSelectItem = this.myItem;
    }
}
