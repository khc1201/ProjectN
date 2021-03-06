﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
public class TriggerUnit 
{
    #region 기본 영역
    public string index;
    public string desc;
    #endregion

    public bool IsShowButton;
    public GameObject[] Show_Button;
    public bool IsHideButton;
    public GameObject[] Hide_Button;
    public bool IsSwitchButton;
    public GameObject[] Switch_Button;
    public bool IsShowTrigger;
    public string[] Show_TriggerName;
    public bool IsHideTrigger;
    public string[] Hide_TriggerName;
    public bool IsShowObject;
    public GameObject[] Show_Object;
    public bool IsHideObject;
    public GameObject[] Hide_Object;
    public bool IsPlayMotion;
    public string Play_MotionName;
    public bool IsPlaySound;
    public string Play_SoundName;
    public bool IsPlayEffect;
    public string Play_Effect;
    public bool IsSendTrigger;
    public string Send_TriggerName;
    public bool IsShowTriggerObject;
    public TriggerListener[] Show_TriggerObject;
    public bool IsHideTriggerObject;
    public TriggerListener[] Hide_TriggerObject;
    public bool IsShowInitObject;
    public InitObject[] Show_InitObject;
    public bool IsHideInitObject;
    public InitObject[] Hide_InitObject;
    public bool IsShowButtonObject;
    public ButtonObject[] Show_ButtonObject;
    public bool IsHideButtonObject;
    public ButtonObject[] Hide_ButtonObject;
    public bool IsShowItemObject;
    public ItemObject[] Show_ItemObject;
    public bool IsHideItemObject;
    public ItemObject[] Hide_ItemObject;
    public bool IsGetItem;
    public ItemObject itemObject;
    public ButtonObject buttonObject;
    public bool IsUseItem;
    public ItemObject useItemObject;
    public ButtonObject useButtonObject;

}
