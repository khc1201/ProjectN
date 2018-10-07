using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemButton : MonoBehaviour {
    public Button thisButton;
    public int thisnum;
    public void Start()
    {
        thisButton = this.GetComponent<Button>();
        thisButton.onClick.RemoveAllListeners();
        thisButton.onClick.AddListener(OnSelect);
    }
    public void OnSelect()
    {
        //PlayerData.singletone.playData.nowClick = thisnum;
        csEventManager.Instance.PostNotification(EVENT_TYPE.SELECT_ITEM, this, thisnum);
        //this.transform.parent.gameObject.GetComponent<ItemList>().RefeshNowSelection();
    }
}
