using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerListener : MonoBehaviour, IListener {
    public bool IsReady;
    public List<TriggerUnit> Triggers;
    public void Start()
    {
        csEventManager.Instance.AddListener(EVENT_TYPE.SEND_TRIGGER, this);
        csEventManager.Instance.AddListener(EVENT_TYPE.SHOW_TRIGGER, this);
        csEventManager.Instance.AddListener(EVENT_TYPE.HIDE_TRIGGER, this);
    }
    public void OnEvent(EVENT_TYPE et, Component sender, object param = null)
    {
        if (param == null)
        {
            Debug.LogError(string.Format("null param : {0} 이 전달받은 트리거의 인덱스가 없습니다. 신호자 : {1}", this.name, sender.name));
            return;
        }
        switch (et)
        {
            case EVENT_TYPE.SEND_TRIGGER:
                {
                    StartTrigger(param as string);
                    break;
                }
            case EVENT_TYPE.SHOW_TRIGGER:
                {
                    ShowTrigger(param as string);
                    break;
                }
            case EVENT_TYPE.HIDE_TRIGGER:
                {
                    HideTrigger(param as string);
                    break;
                }
        }
    }
    public void ShowTrigger(string idx)
    {
        TriggerUnit target = Triggers.Find(x => x.index == idx);
        target.IsValid = true;
    }
    public void HideTrigger(string idx)
    {
        TriggerUnit target = Triggers.Find(x => x.index == idx);
        target.IsValid = false;
    }

    public void StartTrigger(string idx)
    {
        TriggerUnit target = Triggers.Find(x => x.index == idx);
        if(target.IsShowButton)     Trigger_ShowButton(target, true);
        if(target.IsHideButton)     Trigger_ShowButton(target, false);
        if(target.IsShowTrigger)    Trigger_ShowTrigger(target, true);
        if(target.IsHideTrigger)    Trigger_ShowTrigger(target, false);
        if (target.IsShowObject) Trigger_ShowObject(target, true);
        if (target.IsHideObject) Trigger_ShowObject(target, false);
        //if(target.IsPlayMotion)   구현 예정
        //if(target.IsPlaySound)    구현 예정
        if(target.IsSendTrigger)    Trigger_SendTrigger(target.Send_TriggerName); 
    }
    private void Trigger_SendTrigger(string idx)
    {
        csEventManager.Instance.PostNotification(EVENT_TYPE.SEND_TRIGGER, this, idx);
    }
    private void Trigger_ShowButton(TriggerUnit target, bool isShow)
    {
        foreach (var e in target.Show_Button)
        {
            e.SetActive(isShow);
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
    }

}
