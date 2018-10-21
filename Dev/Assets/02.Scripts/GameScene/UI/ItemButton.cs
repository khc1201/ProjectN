using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemButton : MonoBehaviour {
    public Button thisButton;
    public CloseUpItemUI closeUpItemUI;
    public void Start()
    {
        thisButton = this.GetComponent<Button>();
        thisButton.onClick.RemoveAllListeners();
        thisButton.onClick.AddListener(OnSelect);
    }
    public void OnSelect()
    {
        if (this.transform.parent.GetComponent<InventorySlot>().myItem != null
            && ItemList.singletone.nowSelectItem != null
            && this.transform.parent.GetComponent<InventorySlot>().myItem == ItemList.singletone.nowSelectItem)
        {
            closeUpItemUI.ShowUI();
            return;
        }
        else
        {
            this.transform.parent.GetComponent<InventorySlot>().SelectThis();
        }
        //for test
        //Debug.Log("OnSelect 발생");
    }
}
