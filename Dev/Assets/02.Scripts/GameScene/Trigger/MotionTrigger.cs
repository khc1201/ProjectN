using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MotionTrigger : InitObject, IListener
{
    public bool IsMaintainAfterLoad = true;
    public List<MotionUnit> motionList;
    public bool isMotionSequence = false;
    public List<MotionUnit> motionSequence;

    public void Start()
    {
        csEventManager.Instance.AddListener(EVENT_TYPE.MOTION_START, this);
        csEventManager.Instance.AddListener(EVENT_TYPE.INIT_OBJECT, this);
        //for test
        //base.SaveValue();
    }
    public override void InitObjects()
    {
        base.LoadValue();
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
        if (!isMotionSequence)
        {
            foreach (var e in motionList)
            {
                StartCoroutine(PlayMotion(e));
            }
        }
        else if (isMotionSequence)
        {
            Sequence tweenSeq;
            /*
             * 2018 10 14 수정 중
            tweenSeq.AppendCallback(StartCoroutine=>
            */
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
                    foreach(var e in unit.target.GetComponentInChildren<Renderer>().materials)
                    {
                        e.DOFade(1, unit.fadeTime);
                    }
                    break;
                }
            case MOTION_TYPE.FADEOUT:
                {
                    foreach (var e in unit.target.GetComponentInChildren<Renderer>().materials)
                    {
                        e.DOFade(0, unit.fadeTime);
                    }
                    break;
                }
            case MOTION_TYPE.SHOWVIEW:
                {
                    unit.beforeView = GameObject.FindGameObjectWithTag("CameraController").GetComponent<CameraController>().nowView._cam;
                    unit.afterView = GameObject.FindGameObjectWithTag("CameraController").GetComponent<CameraController>().Views.Find(x => x._name == unit.afterViewName)._cam;
                    GameObject nowUI = GameObject.FindGameObjectWithTag("CameraController").GetComponent<CameraController>().nowView._ui;
                    nowUI.SetActive(false);
                    unit.beforeView.SetActive(false);
                    unit.afterView.SetActive(true);
                    yield return new WaitForSeconds(unit.showTime * Time.deltaTime);
                    nowUI.SetActive(true);
                    unit.afterView.SetActive(false);
                    unit.beforeView.SetActive(true);
                    yield return null;
                    break;
                }
        }

    }
}
