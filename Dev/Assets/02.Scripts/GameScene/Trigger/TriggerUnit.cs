using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TriggerUnit : MonoBehaviour
{
    #region 기본 영역
    public string index;
    public string desc;
    public bool IsValid = false; // 로딩 영역에서 다루는 인수
    public bool IsTriggerd = false; // 로딩 영역에서 다루는 인수
    #endregion

    public bool ShowButton;
    public GameObject[] Show_Button;
    public bool HideButton;
    public GameObject[] Hide_Button;
    public bool ShowTrigger;
    public string[] Show_TriggerName;
    public bool HideTrigger;
    public string[] Hide_TriggerName;
    public bool PlayMotion;
    public MotionTrigger Play_Motion;
    public bool PlaySound;
    public string Play_Sound;
    public bool PlayEffect;
    public string Play_Effect;
    public bool SendTrigger;
    public string Send_TriggerName;

    public void Start()
    {
        if (IsTriggerd)
        {
            // do Trigger;
        }
    }


}
