using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour,IListener {
    public ViewObj nowView;
    public ViewObj nextView;
    public List<ViewObj> Views;
    public GameObject Cameras;
    public GameObject UIs;
    public GameObject Triggers;
    // Use this for initialization
	void Start ()
    {
        csEventManager.Instance.AddListener(EVENT_TYPE.INIT_PLAYERDATA, this);
        foreach(var v in Views)
        {
            v._cam = Cameras.transform.Find(v._name).gameObject;
            v._ui = UIs.transform.Find(v._name).gameObject;
            v._triggerListener = Triggers.transform.Find(v._name).gameObject;
        }
    }
    public void InitView(PlayData data)
    {
        nowView = Views.Find(x => x._index == data.NowView);
        OnMoveToView(nowView._index);
        //csEventManager.Instance.PostNotification(EVENT_TYPE.SHOW_HUD, this, nowView);
    }
    public void OnEvent(EVENT_TYPE et, Component sender, object param = null)
    {
        if (et == EVENT_TYPE.INIT_PLAYERDATA)
        {
            InitView(param as PlayData);
        }
    }
	public void OnMoveToView(int viewIndex)
    {
        nextView = Views.Find(x => x._index == viewIndex);
        DoButtonChange();
    }
    public void OnRightButton()
    {
        nextView = Views.Find(x => x._index == nowView._doright);
        DoButtonChange();
    }
    public void OnLeftButton()
    {
        nextView = Views.Find(x => x._index == nowView._doleft);
        DoButtonChange();
    }
    public void OnBackButton()
    {
        nextView = Views.Find(x => x._index == nowView._doback);
        DoButtonChange();
    }
    private void DoButtonChange()
    {
        nowView.DoHide();
        nextView.DoShow();
        nowView = nextView;
        csEventManager.Instance.PostNotification(EVENT_TYPE.SHOW_HUD, this, nowView);
        nextView = null;
    }
}
