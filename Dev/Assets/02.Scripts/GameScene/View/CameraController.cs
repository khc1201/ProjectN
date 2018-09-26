using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public ViewObj nowView;
    public ViewObj nextView;
    public List<ViewObj> Views;
    public GameObject Cameras;
    public GameObject UIs;
    // Use this for initialization
	void Start ()
    {
        foreach(var v in Views)
        {
            v._cam = Cameras.transform.Find(v._name).gameObject;
            v._ui = UIs.transform.Find(v._name).gameObject;
        }
        nowView = Views.Find(x => x._index == 10);
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
