using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
public class TriggerUnit 
{
    #region 기본 영역
    public string index;
    public string desc;
    public bool IsValid = false; // 로딩 영역에서 다루는 인수
    public bool IsTriggerd = false; // 로딩 영역에서 다루는 인수
    #endregion

    public bool IsShowButton;
    public GameObject[] Show_Button;
    public bool IsHideButton;
    public GameObject[] Hide_Button;
    public bool IsShowTrigger;
    public string[] Show_TriggerName;
    public bool IsHideTrigger;
    public string[] Hide_TriggerName;
    public bool IsShowObject;
    public GameObject[] Show_Object;
    public bool IsHideObject;
    public GameObject[] Hide_Object;
    public bool IsPlayMotion;
    public MotionTrigger Play_Motion;
    public bool IsPlaySound;
    public string Play_Sound;
    public bool IsPlayEffect;
    public string Play_Effect;
    public bool IsSendTrigger;
    public string Send_TriggerName;

}
