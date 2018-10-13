using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonObject : InitObject, IListener
{
    public List<Button> targetButton;
    public void Start()
    {
        //csEventManager.Instance.AddListener(EVENT_TYPE.INIT_OBJECT, this);
        //base.SaveValue();
    }
    public void OnEnable()
    {
        if (!(OnLoadValue))
        {
            foreach(var e in targetButton)
            {
                e.gameObject.SetActive(false);
            }
        }
    }

    public override void InitObjects()
    {

        base.LoadValue();

        if (this.OnLoadValue)
        {
            ShowButton();

        }
        else if (!this.OnLoadValue)
        {
            HideButton();
        }

    }
    public void ShowButton()
    {
        base.OnLoadValue = true;
        base.SaveValue();
        foreach(var b in targetButton)
        {
            b.enabled = true;
        }
    }
    public void HideButton()
    {
        base.OnLoadValue = false;
        base.SaveValue();
        foreach (var b in targetButton)
        {
            b.enabled = false;
            b.onClick.RemoveAllListeners();
        }
    }
    public override void OnEvent(EVENT_TYPE et, Component sender, object param = null)
    {
    }
}
