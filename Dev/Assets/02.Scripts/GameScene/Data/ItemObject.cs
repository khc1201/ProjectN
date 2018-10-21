using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ItemObject : InitObject, IListener
{
    public GameObject target;
    public ButtonObject targetGetButtonObject;
    public string ItemIndex;
    public string SendTriggerIndex;

    public void Start()
    {
        csEventManager.Instance.AddListener(EVENT_TYPE.INIT_OBJECT, this);
        //targetGetButton.onClick.AddListener(OnClicked);
        //base.SaveValue();
    }
    public void OnClicked()
    {
        csEventManager.Instance.PostNotification(EVENT_TYPE.SEND_TRIGGER, this, SendTriggerIndex);
        //for test
        Debug.Log("클릭 발생 및 트리거 : " + SendTriggerIndex);
    }
    public override void InitObjects()
    {
        base.InitObjects();
        if (base.OnLoadValue)
        {
            ShowObject();
            foreach (Button b in targetGetButtonObject.targetButton)
            {
                b.onClick.AddListener(this.OnClicked);

                //for test
                Debug.Log("OnClick AddListener" + this.Index);
            }
            //targetGetButton.gameObject.SetActive(true);
        }
        else if (!base.OnLoadValue){
            HideObject();
            foreach (var b in targetGetButtonObject.targetButton)
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
        targetGetButtonObject.HideButton();
        HideObject();
    }
    public override void OnEvent(EVENT_TYPE et, Component sender, object param = null)
    {
        switch (et)
        {
          
            case EVENT_TYPE.INIT_OBJECT:
                {
                    //for test
                    //Debug.Log("Step 1  - OnEvent - Init Object");
                    base.LoadValue();
                    InitObjects();
                    //targetGetButton.InitObjects();
                    break;
                }
        }
    }
}
