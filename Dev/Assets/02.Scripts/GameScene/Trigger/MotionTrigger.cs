using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MotionTrigger : InitObject, IListener
{
    public List<MotionUnit> motionList;
    public void Start()
    {
        csEventManager.Instance.AddListener(EVENT_TYPE.MOTION_START, this);
        csEventManager.Instance.AddListener(EVENT_TYPE.INIT_OBJECT, this);

        //for test
        base.SaveValue();
    }
    public override void OnEvent(EVENT_TYPE et, Component sender, object param = null)
    {
        if (et == EVENT_TYPE.MOTION_START)
        {
            if(this.OnLoadValue == false)
            {
                Debug.LogError("OnLoadValue 가 false 입니다." + this.gameObject.name);
                return;
            }
            if (this.Index == param.ToString())
            {
                StartMotion();
            }
        }
        else if(et == EVENT_TYPE.INIT_OBJECT)
        {
            base.LoadValue();
        }

    }
    public void StartMotion(bool isInit = false)
    {
        if (isInit)
        {
            //IsTriggered = true;
            OnLoadValue = true;
            SaveValue();
            foreach(var e in motionList)
            {
                e.fadeTime = 0.01f;
                e.motionDelay = 0.01f;
                e.moveSpeed = 0.01f;
                e.rotateSpeed = 0.01f;
            }
        }
        /*
        if (!IsTriggered)
        {
            if (!PlayerData.singletone.playData.IsMotionTriggeredList.Contains(Index))
            {
                PlayerData.singletone.SaveMotionTrigger(this.Index);
                IsTriggered = true;
            }
        }
        */
        foreach (var e in motionList)
        {
            StartCoroutine(PlayMotion(e));
        }
    }

    public IEnumerator PlayMotion(MotionUnit unit)
    {
        yield return new WaitForSeconds(unit.motionDelay);
        Tween tween;
        switch (unit.motionType)
        {
            case MOTION_TYPE.ROTATE:
                {
                    tween = unit.target.transform.DOLocalRotate(unit.rotateValue, unit.rotateSpeed).Play();
                    break;
                }
            case MOTION_TYPE.MOVE:
                {
                    tween = unit.target.transform.DOLocalMove(unit.movePos, unit.moveSpeed).Play();
                    break;
                }
            case MOTION_TYPE.FADEIN:
                {
                    break;
                }
            case MOTION_TYPE.FADEOUT:
                {
                    break;
                }
        }

    }
}
