using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    public GameObject target;
    public MOTION_TYPE motionType;
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

    public void OnClickMotion()
    {
    }
}
