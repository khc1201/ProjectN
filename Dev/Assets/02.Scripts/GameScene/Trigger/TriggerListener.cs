using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerListener : MonoBehaviour, IListener {
    public bool IsReady;
    public List<TriggerUnit> Triggers;

    public void Start()
    {
        csEventManager.Instance.AddListener(EVENT_TYPE.SEND_TRIGGER, this);
        //for test
        Debug.Log("Step0");
        csEventManager.Instance.AddListener(EVENT_TYPE.SHOW_TRIGGER, this);
        csEventManager.Instance.AddListener(EVENT_TYPE.HIDE_TRIGGER, this);
    }

    public void OnEvent(EVENT_TYPE et, Component sender, object param = null)
    {
        //for test
        Debug.Log("STEP3-1");
        /*
        if (param == null)
        {
            Debug.LogError(string.Format("null param : {0} 이 전달받은 트리거의 인덱스가 없습니다. 신호자 : {1}", this.name, sender.name));
            return;
        }*/
        switch (et)
        {
            case EVENT_TYPE.SEND_TRIGGER:
                {
                    //for test
                    Debug.Log("STEP3-2");

                    StartTrigger(param.ToString());
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
        if(!target.IsValid) target.IsValid = true;
    }
    public void HideTrigger(string idx)
    {
        TriggerUnit target = Triggers.Find(x => x.index == idx);
        if(target.IsValid) target.IsValid = false;
    }

    public void StartTrigger(string idx)
    {
        TriggerUnit target = Triggers.Find(x => x.index == idx);
        
        if (!(target.IsValid))
        {
            Debug.LogError(string.Format("{0} 의 IsValid 가 false 입니다", target.index));
            return;
        }
        //for test
        Debug.Log("STEP4");
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
        //for test
        Debug.Log("STEP5");
        if (target.Play_MotionName == null)
        {
            Debug.LogError("Trigger_StartMotion 에서 모션을 실행하려고 했으나 target 의 모션명이 등록되어 있지 않습니다.");
            return;
        }
        csEventManager.Instance.PostNotification(EVENT_TYPE.MOTION_START, this, target.Play_MotionName);
        //for test
        Debug.Log("STEP5-1");
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
