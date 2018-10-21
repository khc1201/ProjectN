using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseUpItemUI : MonoBehaviour {
    public GameObject targetUI;
    public Button Exit;
    public Image ImageIcon;
    

    public void Start()
    {
        Exit.onClick.AddListener(HideUI);
        this.HideUI();
    }

    public void ShowUI()
    {
        if(ItemList.singletone.nowSelectItem == null)
        {
            return;
        }
        //Item nowItem = ItemList.singletone.nowSelectItem;
        ImageIcon.sprite = ItemList.singletone.nowSelectItem.Icon;
        targetUI.SetActive(true);
    }
    public void HideUI()
    {
        ImageIcon.sprite = null;
        targetUI.SetActive(false);
    }
}
