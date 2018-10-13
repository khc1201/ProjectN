using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ItemObject : InitObject, IListener
{
    public GameObject target;
    public ButtonObject targetGetButton;
    public string ItemIndex;
    public string SendTriggerIndex;

    public void Start()
    {
        csEventManager.Instance.AddListener(EVENT_TYPE.INIT_OBJECT, this);
        //base.SaveValue();
    }
    public void OnClicked()
    {
        csEventManager.Instance.PostNotification(EVENT_TYPE.SEND_TRIGGER, this, SendTriggerIndex);
    }
    public override void InitObjects()
    {
        if (this.OnLoadValue)
        {
            ShowObject();
            foreach (var b in targetGetButton.targetButton)
            {
                b.onClick.AddListener(OnClicked);
            }
            //targetGetButton.gameObject.SetActive(true);
        }
        else if (!this.OnLoadValue){
            HideObject();
            foreach (var b in targetGetButton.targetButton)
            {
                b.onClick.RemoveAllListeners();
            }
            //targetGetButton.targetButton.onClick.RemoveAllListeners();
            //targetGetButton.gameObject.SetActive(false);
        }
    }
    public void ShowObject()
    {
        target.SetActive(true);
        base.OnLoadValue = true;
        this.SaveValue();
    }
    public void HideObject()
    {
        base.OnLoadValue = false;
        target.SetActive(false);
        this.SaveValue();
    }

    public void ClickAndHide()
    {
        ItemList.singletone.AddItem(this.ItemIndex);
        targetGetButton.HideButton();
        HideObject();
    }
    public override void OnEvent(EVENT_TYPE et, Component sender, object param = null)
    {
        switch (et)
        {
          
            case EVENT_TYPE.INIT_OBJECT:
                {
                    base.LoadValue();
                    InitObjects();
                    //targetGetButton.InitObjects();
                    break;
                }
        }
    }
}
