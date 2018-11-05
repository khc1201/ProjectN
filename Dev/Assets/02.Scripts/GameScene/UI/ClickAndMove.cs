using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System;

public class ClickAndMove : MonoBehaviour
{
    public string SendTriggerIndex;
    public GameObject targetObj;
    private Button buttonUI;
    public List<Button> otherButtonUI;
    private Vector3 buttonInitPos;
    public float moveSpeed;
    public Vector3 initPos;
    public Vector3 movePos;
    public float moveY;
    public bool IsUsed = false;
    private Tween tween;
    public bool IsRotate = false;

    public void OnEnable()
    {
        IsUsed = false;
        //if (IsRotate) initPos = targetObj.transform.localEulerAngles;
        if(initPos==null && !(IsRotate))initPos = targetObj.transform.localPosition;
        if (IsRotate) initPos = targetObj.transform.localEulerAngles;
        buttonUI = this.gameObject.GetComponent<Button>();
     
        buttonInitPos = buttonUI.transform.localPosition;
        SetButton();
    }
    public void OnDisable()
    {
        tween.Pause();
        buttonUI.transform.localPosition = buttonInitPos;
        targetObj.transform.localPosition = initPos;
        IsUsed = false;
        foreach (var e in otherButtonUI)
        {
            e.enabled = true;
        }

        if (IsRotate)
        {
            tween = targetObj.transform.DOLocalRotate(initPos, moveSpeed).OnStart(OnMove).OnComplete(EndMove).Play();
        }
        else
        {
            buttonUI.transform.localPosition = new Vector2(this.transform.localPosition.x, buttonInitPos.y);
            tween = targetObj.transform.DOLocalMove(initPos, moveSpeed).OnStart(OnMove).OnComplete(EndMove).Play();
        }
    }
    private void SetButton()
    {
        buttonUI.onClick.RemoveListener(OnClickMove);
        buttonUI.onClick.AddListener(OnClickMove);
    }
    public void OnMove()
    {
        buttonUI.enabled = false;
    }
    public void EndMove()
    {
        buttonUI.enabled = true;
    }
    public void OnClickMove()
    {
        if (!IsUsed)
        {
            foreach(var e in otherButtonUI)
            {
                e.enabled = false;
            }
            //buttonUI.gameObject.transform.position = Camera.current.WorldToScreenPoint(movePos);
            if (IsRotate)
            {
                tween = targetObj.transform.DOLocalRotate(movePos, moveSpeed).OnStart(OnMove).OnComplete(EndMove).Play();
            }
            else
            {
                tween = targetObj.transform.DOLocalMove(movePos, moveSpeed).OnStart(OnMove).OnComplete(EndMove).Play();
                buttonUI.transform.localPosition = new Vector2(this.transform.localPosition.x, this.transform.localPosition.y + moveY);
            }
            IsUsed = true;
        }
        else
        {
            foreach (var e in otherButtonUI)
            {
                e.enabled = true;
            }
            //buttonUI.transform.position = Camera.current.WorldToScreenPoint(initPos);
            if (IsRotate)
            {
                tween = targetObj.transform.DOLocalRotate(Vector3.zero, moveSpeed).OnStart(OnMove).OnComplete(EndMove).Play();
            }
            else
            {
                buttonUI.transform.localPosition = new Vector2(this.transform.localPosition.x, buttonInitPos.y);
                tween = targetObj.transform.DOLocalMove(initPos, moveSpeed).OnStart(OnMove).OnComplete(EndMove).Play();
            }
            IsUsed = false;
        }
        if(SendTriggerIndex!="")
        {
            csEventManager.Instance.PostNotification(EVENT_TYPE.SEND_TRIGGER, this, SendTriggerIndex);
        }
    }

}
