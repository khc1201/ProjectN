using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtomButton : MonoBehaviour, IListener {

    public GameObject btnright, btnleft, btnback;
    private Vector2 pos_btnright, pos_btnleft, pos_btnback;
    //private Vector2 pos_none = new Vector2(0, 1000f);

    void Start ()
    {
        csEventManager.Instance.AddListener(EVENT_TYPE.CHANGE_BUTTON, this);

        //pos_btnright = btnright.transform.localPosition;
        //pos_btnback = btnback.transform.localPosition;
        //pos_btnleft = btnleft.transform.localPosition;

        ViewObj target = GameObject.FindGameObjectWithTag("CameraController").GetComponent<CameraController>().nowView;

        csEventManager.Instance.PostNotification(EVENT_TYPE.CHANGE_BUTTON, this, target);

	}
    public void OnEvent(EVENT_TYPE et, Component sender, object param = null)
    {
        if(et == EVENT_TYPE.CHANGE_BUTTON)
        {
            ViewObj target = param as ViewObj;
            if (target._name == null) return;

            btnright.SetActive(false);
            btnleft.SetActive(false);
            btnback.SetActive(false);

            if (target._doright != 0)
            {
                btnright.SetActive(true);
            }
            if(target._doleft != 0)
            {
                btnleft.SetActive(true);
            }
            if(target._doback != 0)
            {
                btnback.SetActive(true);
            }

        }

    }
	
}
