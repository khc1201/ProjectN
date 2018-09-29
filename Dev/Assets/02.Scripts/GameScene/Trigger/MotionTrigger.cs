using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MotionTrigger : MonoBehaviour, IListener
{
    public List<MotionUnit> motionList;
    public string index;
    public void Start()
    {
        csEventManager.Instance.AddListener(EVENT_TYPE.MOTION_START, this);
        
    }
    public void OnEvent(EVENT_TYPE et, Component sender, object param = null)
    {
        if(et == EVENT_TYPE.MOTION_START && index == param as string)
        {
            StartMotion();
        }
    }
    public void StartMotion()
    {
        foreach(var e in motionList)
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
