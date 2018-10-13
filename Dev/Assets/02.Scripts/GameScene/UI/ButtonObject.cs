using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonObject : InitObject, IListener
{
    public List<Button> targetButton;

    /*
    public void OnEnable()
    {
        if (!(this.OnLoadValue))
        {
            foreach(var e in targetButton)
            {
                e.gameObject.SetActive(false);
            }
        }
        else if (this.OnLoadValue)
        {
            foreach (var e in targetButton)
            {
                e.gameObject.SetActive(true);
            }
        }
    }
    */
    public override void InitObjects()
    {

        base.LoadValue();

        if (this.OnLoadValue)
        {
                ShowButton();

        }
        else if (!(this.OnLoadValue))
        {

                HideButton();

        }

    }
    public void ShowButton()
    {
        base.OnLoadValue = true;
        base.SaveValue();

            foreach (var b in targetButton)
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
