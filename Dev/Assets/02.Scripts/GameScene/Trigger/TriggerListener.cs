using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerListener : InitObject, IListener {
    public bool IsReady;
    public List<TriggerUnit> Triggers;
    
    public void Start()
    {
        csEventManager.Instance.AddListener(EVENT_TYPE.SEND_TRIGGER, this);
        csEventManager.Instance.AddListener(EVENT_TYPE.SHOW_TRIGGER, this);
        csEventManager.Instance.AddListener(EVENT_TYPE.HIDE_TRIGGER, this);
        //csEventManager.Instance.AddListener(EVENT_TYPE.INIT_TRIGGER, this);
        csEventManager.Instance.AddListener(EVENT_TYPE.INIT_OBJECT, this);

        //for test
        base.SaveValue();
    }

    public override void OnEvent(EVENT_TYPE et, Component sender, object param = null)
    {
        switch (et)
        {
            case EVENT_TYPE.SEND_TRIGGER:
                {
                    StartTrigger(param.ToString());
                    break;
                }
            case EVENT_TYPE.SHOW_TRIGGER:
                {
                    ShowTrigger(param.ToString());
                    break;
                }
            case EVENT_TYPE.HIDE_TRIGGER:
                {
                    HideTrigger(param.ToString());
                    break;
                }
                
            case EVENT_TYPE.INIT_OBJECT:
                {
                    base.LoadValue();
                    break;
                }
        }
    }
    public void InitTrigger(string idx)
    {
        StartTrigger(idx, true);
    }
    public void ShowTrigger(string idx)
    {
        TriggerUnit target = Triggers.Find(x => x.index == idx);
        if(!target.IsValid) target.IsValid = true;
    }
    public void HideTrigger(string idx)
    {
        TriggerUnit target = Triggers.Find(x => x.index == idx);
        if(target.IsValid) target.IsValid = false;
    }

    public void StartTrigger(string idx, bool isInit = false)
    {
        TriggerUnit target = Triggers.Find(x => x.index == idx);
        if(target == null)
        {
            return;
        }
        if (isInit)
        {
            target.IsTriggered = true;
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
        if (target.IsShowTrigger)   Trigger_ShowTrigger(target, true);
        if (target.IsHideTrigger)   Trigger_ShowTrigger(target, false);
        if (target.IsShowObject)    Trigger_ShowObject(target, true);
        if (target.IsHideObject)    Trigger_ShowObject(target, false);
        if (target.IsPlayMotion)    Trigger_StartMotion(target);
        if (target.IsPlaySound)     Trigger_PlaySound(target.Play_SoundName);
        if (target.IsSendTrigger)   Trigger_SendTrigger(target.Send_TriggerName); 
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
