using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UseItem : MonoBehaviour {
    public Button thisButton;
    public string NeedItemString;
    public string SendTriggerIndex;
    public void Start()
    {
        thisButton = this.GetComponentInChildren<Button>();
        thisButton.onClick.AddListener(ClickAndCheck);
    }
    public void ClickAndCheck()
    {
        if (ItemList.singletone.nowSelectItem.Index == NeedItemString)
        {
            //for test  
            Debug.Log("아이템을 사용하셨습니다 : item index : " + NeedItemString);
            //for test  
            Debug.Log("트리거를 발동합니다 : " + SendTriggerIndex);
            csEventManager.Instance.PostNotification(EVENT_TYPE.SEND_TRIGGER, this, SendTriggerIndex);
        }
    }
}
