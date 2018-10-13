using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonObject : InitObject, IListener
{
    public Button targetButton;
    public void Start()
    {
        //csEventManager.Instance.AddListener(EVENT_TYPE.INIT_OBJECT, this);
        //base.SaveValue();
    }

    public override void InitObjects()
    {
        //for test
        base.LoadValue();
        //Debug.Log("Buttons onloadvalue = "+ OnLoadValue);
        //for test  
        //Debug.Log("buttonObject OnEvent");
        if (this.OnLoadValue)
        {
            ShowButton();
            //targetGetButton.gameObject.SetActive(true);
        }
        else if (!this.OnLoadValue)
        {
            HideButton();
            //targetGetButton.gameObject.SetActive(false);
        }
        //targetButton.enabled = OnLoadValue;
        /*
        if (PlayerData.singletone.initData.ContainsKey(Index))
        {
            targetButton.enabled = PlayerData.singletone.initData[Index];
        }
        else
        {
            PlayerData.singletone.SaveInitData(this.Index, this.OnLoadValue);
        }
        */
    }
    public void ShowButton()
    {
        base.OnLoadValue = true;
        base.SaveValue();
        this.targetButton.enabled = true;
        this.gameObject.SetActive(true);
    }
    public void HideButton()
    {
        base.OnLoadValue = false;
        base.SaveValue();
        this.targetButton.enabled = false;
        this.targetButton.onClick.RemoveAllListeners();
        this.gameObject.SetActive(false);
    }
    public override void OnEvent(EVENT_TYPE et, Component sender, object param = null)
    {/*
        switch (et)
        {
            case EVENT_TYPE.INIT_OBJECT:
                {
                    //for test  
                    Debug.Log("buttonObject OnEvent");
                    base.LoadValue();
                    InitObjects();
                    break;
                }
        }*/
    }
}
