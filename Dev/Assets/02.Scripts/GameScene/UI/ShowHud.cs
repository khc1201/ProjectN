using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowHud : MonoBehaviour, IListener {

    public GameObject btnleft, btnback, btnright;
    private List<GameObject> btns;
	// Use this for initialization
	void Start ()
    {
        csEventManager.Instance.AddListener(EVENT_TYPE.SHOW_HUD, this);
	}
    public IEnumerator LateStart()
    {
        //한 번 초기화 해 줌
        yield return new WaitForFixedUpdate();
        ViewObj NowView = GameObject.FindGameObjectWithTag("CameraController").GetComponent<CameraController>().nowView;
        csEventManager.Instance.PostNotification(EVENT_TYPE.SHOW_HUD, this, NowView);
        yield return null;
    }
	
    public void OnEvent(EVENT_TYPE et, Component sender, object param = null)
    {
        if (et == EVENT_TYPE.SHOW_HUD)
        {
            ViewObj target = param as ViewObj;
            if(target._name == null)
            {
                Debug.LogError("ShowHud 에서 taget 의 값이 없습니다. 인수가 제대로 전달되지 않았습니다.");
                return;
            }

            btnleft.SetActive(false);
            btnback.SetActive(false);
            btnright.SetActive(false);

            if(target._doback != 0)
            {
                btnback.SetActive(true);
            }
            if(target._doleft != 0)
            {
                btnleft.SetActive(true);
            }
            if (target._doright != 0)
            {
                btnright.SetActive(true);
            }
        }
    }
}
