using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MotionUnit{

    #region MOTION_TYPE : 공통
    public string index;
    public string desc;

    public GameObject target;
    public MOTION_TYPE motionType;
    public float motionDelay = 0f;

    public bool IsHaveTrigger;
    public string endTrigger; // 모션 완료 후 발동 할 트리거
    #endregion

    #region MOTION_TYPE : ROTATE
    public float rotateSpeed;
    public Vector3 rotateValue;
    #endregion

    #region MOTION_TYPE : FADE
    public float fadeTime;
    #endregion

    #region MOTION_TYPE : MOVE
    public float moveSpeed;
    public Vector3 movePos;
    #endregion
}
