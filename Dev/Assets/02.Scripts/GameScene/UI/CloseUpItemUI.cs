using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseUpItemUI : MonoBehaviour {
    public GameObject targetUI;
    public Button Exit;
    public Image ImageIcon;

    public static CloseUpItemUI singletone;

    public void Awake()
    {
        if(singletone == null)
        {
            singletone = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            DestroyImmediate(this);
            return;
        }
    }

    public void Start()
    {
        Exit.onClick.AddListener(HideUI);
        this.HideUI();
    }

    public void ShowUI(Item item)
    {
        if(item == null)
        {
            return;
        }
        //Item nowItem = ItemList.singletone.nowSelectItem;
        ImageIcon.sprite = item.Icon;
        targetUI.SetActive(true);
    }
    public void HideUI()
    {
        ImageIcon.sprite = null;
        targetUI.SetActive(false);
    }
}
