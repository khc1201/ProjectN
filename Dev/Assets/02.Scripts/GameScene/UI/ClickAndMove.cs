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

    public void OnEnable()
    {
        IsUsed = false;
        initPos = targetObj.transform.localPosition;
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
    }
    private void SetButton()
    {
        buttonUI.onClick.RemoveAllListeners();
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
            tween = targetObj.transform.DOLocalMove(movePos, moveSpeed).OnStart(OnMove).OnComplete(EndMove).Play();
            buttonUI.transform.localPosition = new Vector2(this.transform.localPosition.x, this.transform.localPosition.y + moveY);
            IsUsed = true;
        }
        else
        {
            foreach (var e in otherButtonUI)
            {
                e.enabled = true;
            }
            //buttonUI.transform.position = Camera.current.WorldToScreenPoint(initPos);
            buttonUI.transform.localPosition = new Vector2(this.transform.localPosition.x, buttonInitPos.y);
            tween = targetObj.transform.DOLocalMove(initPos, moveSpeed).OnStart(OnMove).OnComplete(EndMove).Play();
            IsUsed = false;
        }
        if(SendTriggerIndex!="")
        {
            csEventManager.Instance.PostNotification(EVENT_TYPE.SEND_TRIGGER, this, SendTriggerIndex);
        }
    }

}
