using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugManager : MonoBehaviour,IListener {
    public void Start()
    {
        ViewDebug.SetActive(IsViewDebug);
        csEventManager.Instance.AddListener(EVENT_TYPE.SHOW_HUD, this);
    }

    public bool IsViewDebug = true;
    public GameObject ViewDebug;
    public Text ViewIndex;
    public Text ViewDesc;

    public void RefreshDebug(string viewIndex, string viewDesc)
    {
        if (!IsViewDebug) return;

        ViewIndex.text = viewIndex;
        ViewDesc.text = viewDesc;
    }

    public void OnEvent(EVENT_TYPE et, Component sender, object param = null)
    {
        if(et == EVENT_TYPE.SHOW_HUD)
        {
            ViewObj target = param as ViewObj;
            RefreshDebug(target._index.ToString(), target._name);
        }
    }
}
