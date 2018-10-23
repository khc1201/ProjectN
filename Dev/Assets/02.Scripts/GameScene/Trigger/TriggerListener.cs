using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TriggerListener : InitObject, IListener {
    public bool IsOnceTrigger = true;
    public List<TriggerUnit> Triggers;
    
    public void Start()
    {
        csEventManager.Instance.AddListener(EVENT_TYPE.SEND_TRIGGER, this);
        csEventManager.Instance.AddListener(EVENT_TYPE.SHOW_TRIGGER, this);
        csEventManager.Instance.AddListener(EVENT_TYPE.HIDE_TRIGGER, this);
        csEventManager.Instance.AddListener(EVENT_TYPE.INIT_OBJECT, this);
    }
    public override void InitObjects()
    {
        base.InitObjects();
        if(!OnLoadValue && IsOnceTrigger)
        {
            foreach(var e in Triggers)
            {
                StartTrigger(e.index);
            }
        }
        if (OnLoadValue)
        {
            ShowThisTrigger();
        }
        else if (!OnLoadValue)
        {
            HideThisTrigger();
        }
    }

    public override void OnEvent(EVENT_TYPE et, Component sender, object param = null)
    {
        switch (et)
        {
            case EVENT_TYPE.SEND_TRIGGER:
                {
                    StartTrigger(param as string);
                    break;
                }
            case EVENT_TYPE.SHOW_TRIGGER:
                {
                    //ShowTrigger(param.ToString());
                    break;
                }
            case EVENT_TYPE.HIDE_TRIGGER:
                {
                    //HideTrigger(param.ToString());
                    break;
                }
                
            case EVENT_TYPE.INIT_OBJECT:
                {
                    base.LoadValue();
                    InitObjects();
                    break;
                }
                /*
            case EVENT_TYPE.USE_ITEM:
                {
                    //StartTrigger((param as UseItem).SendTriggerIndex, (param as UseItem).NeedItemString);
                    StartTrigger(param as string);
                    break;
                }*/
        }
    }/*
    public void ShowTrigger(string idx)
    {
        TriggerUnit target = Triggers.Find(x => x.index == idx);
        if(!target.IsValid) target.IsValid = true;
    }
    public void HideTrigger(string idx)
    {
        TriggerUnit target = Triggers.Find(x => x.index == idx);
        if(target.IsValid) target.IsValid = false;
    }*/

    public void ShowThisTrigger()
    {
        OnLoadValue = true;
        this.enabled = true;
        SaveValue();
    }
    public void HideThisTrigger()
    {
        OnLoadValue = false;
        this.enabled = false;
        SaveValue();
    }

    public void StartTrigger(string idx, string itemString = "")
    {
        //for test
        //Debug.Log(this.gameObject.name + " 의 stratTrigger : " + idx.ToString());
        TriggerUnit target = Triggers.Find(x => x.index.ToString() == idx.ToString());
        if(target == null || !(this.OnLoadValue))
        {
            //for test
            //Debug.Log("target 이 지정되지 않았습니다. idx : " + idx);
            return;
        }
        /*
        if (target.IsTriggered==false)
        {
            if (!PlayerData.singletone.playData.IsTriggeredList.Contains(target.index))
            {
                PlayerData.singletone.SaveTrigger(target.index);
                target.IsTriggered = true;
            }
        }
        */
        if (target.IsShowButton)    Trigger_ShowButton(target, true);
        if (target.IsHideButton)    Trigger_ShowButton(target, false);
        if (target.IsSwitchButton) Trigger_SwitchButton(target);
        if (target.IsShowObject)    Trigger_ShowObject(target, true);
        if (target.IsHideObject)    Trigger_ShowObject(target, false);
        if (target.IsSendTrigger) Trigger_SendTrigger(target.Send_TriggerName);

        if (OnLoadValue)
        {
            if (target.IsPlayMotion) Trigger_StartMotion(target);
            if (target.IsGetItem) Trigger_GetItem(target);
            if (target.IsUseItem) Trigger_UseItem(target);
            if (target.IsPlaySound) Trigger_PlaySound(target.Play_SoundName);
            if (target.IsShowTriggerObject) Trigger_ShowTriggerObject(target, true);
            if (target.IsHideTriggerObject) Trigger_ShowTriggerObject(target, false);
            if (target.IsShowInitObject) Trigger_InitObject(target, true);
            if (target.IsHideInitObject) Trigger_InitObject(target, false);
            if (target.IsShowButtonObject) Trigger_ButtonObject(target, true);
            if (target.IsHideButtonObject) Trigger_ButtonObject(target, false);
            if (target.IsShowItemObject) Trigger_ItemObject(target, true);
            if (target.IsHideItemObject) Trigger_ItemObject(target, false);
        }
    }
    public void Trigger_ShowTriggerObject(TriggerUnit target, bool isShow)
    {
        if (isShow)
        {
            foreach (var t in target.Show_TriggerObject)
            {
                t.ShowThisTrigger();
            }
        }
        else
        {
            foreach (var t in target.Hide_TriggerObject)
            {
                t.HideThisTrigger();
            }
        }
    }

    public void Trigger_ButtonObject(TriggerUnit target, bool isShow)
    {
        if (isShow)
        {
            foreach (var e in target.Show_ButtonObject)
            {
                e.ShowButton();
            }
        }
        else if (!isShow)
        {
            foreach (var e in target.Hide_ButtonObject)
            {
                e.HideButton();
            }
        }
    }

    public void Trigger_ItemObject(TriggerUnit target, bool isShow)
    {
        if (isShow)
        {
            foreach (var e in target.Show_ItemObject)
            {
                e.ShowObject();
            }
        }
        else if (!isShow)
        {
            foreach (var e in target.Hide_ItemObject)
            {
                e.HideObject();
            }
        }
    }
    public void Trigger_InitObject(TriggerUnit target, bool isShow)
    {
        if (isShow)
        {
            foreach(var e in target.Show_InitObject)
            {
                e.ShowInitObject();
            }
        }
        else if (!isShow)
        {
            foreach(var e in target.Hide_InitObject)
            {
                e.HideInitObject();
            }
        }
    }
    public void Trigger_UseItem(TriggerUnit target)
    {
        //for test
        Debug.Log("step 1");
        ItemList.singletone.RemoveItem(target.useItemObject.ItemIndex);
        target.useButtonObject.HideButton();
    }
    public void Trigger_SwitchButton(TriggerUnit target)
    {
        foreach (var e in target.Switch_Button)
        {

            ButtonObject btnobj = e.transform.parent.GetComponent<ButtonObject>();
            if (btnobj != null)
            {
                if(btnobj.OnLoadValue)
                {
                    if (e.activeInHierarchy)
                    {
                        e.SetActive(false);
                        e.GetComponent<Image>().enabled = false;
                        e.GetComponent<Button>().enabled = false;
                    }
                    else if (!e.activeInHierarchy)
                    {
                        e.SetActive(true);
                        e.GetComponent<Image>().enabled = true;
                        e.GetComponent<Button>().enabled = true;
                    }
                }
                else
                {
                    e.SetActive(false);
                    e.GetComponent<Image>().enabled = false;
                    e.GetComponent<Button>().enabled = false;
                }
            }
            else
            {
                if (e.activeInHierarchy)
                {
                    e.SetActive(false);
                    e.GetComponent<Image>().enabled = false;
                    e.GetComponent<Button>().enabled = false;
                }
                else if (!e.activeInHierarchy)
                {
                    e.SetActive(true);
                    e.GetComponent<Image>().enabled = true;
                    e.GetComponent<Button>().enabled = true;
                }
            }

        }
    }
    public void Trigger_GetItem(TriggerUnit target)
    {
        //for test
        Debug.Log("Trigger_GetItem // target : " + target.index);
        target.itemObject.ClickAndHide();
        target.buttonObject.HideButton();
        CloseUpItemUI.singletone.ShowUI(ItemList.singletone.itemList.Find(x => x.Index == target.itemObject.ItemIndex));
    }
    private void Trigger_PlaySound(string soundName)
    {
        AudioManager.singletone.Play(soundName);
    }
    private void Trigger_StartMotion(TriggerUnit target)
    {
        if (target.Play_MotionName == null)
        {
            Debug.LogError("Trigger_StartMotion 에서 모션을 실행하려고 했으나 target 의 모션명이 등록되어 있지 않습니다.");
            return;
        }
        csEventManager.Instance.PostNotification(EVENT_TYPE.MOTION_START, this, target.Play_MotionName);
    }

    private void Trigger_SendTrigger(string idx)
    {
        csEventManager.Instance.PostNotification(EVENT_TYPE.SEND_TRIGGER, this, idx);
    }

    private void Trigger_ShowButton(TriggerUnit target, bool isShow)
    {
        if (isShow)
        {
            foreach (var e in target.Show_Button)
            {
                e.GetComponent<ButtonObject>().ShowButton();
            }
        }
        else if (!isShow)
        {
            foreach(var e in target.Hide_Button)
            {
                e.GetComponent<ButtonObject>().HideButton();
            }
        }
    }

    private void Trigger_ShowObject(TriggerUnit target, bool isShow)
    {
        if (isShow)
        {
            foreach(var e in target.Show_Object)
            {
                e.SetActive(true);
            }
        }
        else if (!isShow)
        {
            foreach(var e in target.Hide_Object)
            {
                e.SetActive(false);
            }
        }
    }

    /*
    private void Trigger_ShowTrigger(TriggerUnit target, bool isShow)
    {
        if (isShow)
        {
            foreach (var e in target.Show_TriggerName)
            {
                csEventManager.Instance.PostNotification(EVENT_TYPE.SHOW_TRIGGER, this, e);
            }
        }
        else if (!isShow)
        {
            foreach (var e in target.Show_TriggerName)
            {
                csEventManager.Instance.PostNotification(EVENT_TYPE.HIDE_TRIGGER, this, e);
            }
        }
    }*/

}
