using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MotionTrigger : InitObject, IListener
{
    public bool IsMaintainAfterLoad = true;
    public List<MotionUnit> motionList;
    public void Start()
    {
        csEventManager.Instance.AddListener(EVENT_TYPE.MOTION_START, this);
        csEventManager.Instance.AddListener(EVENT_TYPE.INIT_OBJECT, this);

        //for test
        //base.SaveValue();
    }
    public override void InitObjects()
    {
        if(!(OnLoadValue) && IsMaintainAfterLoad)
        {
            foreach(var e in motionList)
            {
                e.fadeTime = 0.01f;
                e.motionDelay = 0.01f;
                e.moveSpeed = 0.01f;
                e.rotateSpeed = 0.01f;
            }
            StartMotion();
        }
    }
    public override void OnEvent(EVENT_TYPE et, Component sender, object param = null)
    {
        if (et == EVENT_TYPE.MOTION_START)
        {
            if (this.Index == param.ToString())
            {
                StartMotion();
            }
        }
        else if(et == EVENT_TYPE.INIT_OBJECT)
        {
            base.LoadValue();
            InitObjects();
        }

    }
    public void StartMotion()
    {
        OnLoadValue = false;
        SaveValue();
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
