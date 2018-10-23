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
        base.InitObjects();
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
        this.OnLoadValue = true;
        this.SaveValue();

        if (isTrueOnInit)
        {
            foreach (var b in targetButton)
            {
                b.enabled = true;
                b.gameObject.GetComponent<Image>().enabled = true;
                b.transform.GetComponentInChildren<Text>().enabled = true;

            }
        }
        else
        {
            foreach (var b in targetButton)
            {
                b.enabled = false;
                b.gameObject.GetComponent<Image>().enabled = false;
                b.transform.GetComponentInChildren<Text>().enabled = false;

            }
        }
        
    }
    public void HideButton()
    {
        this.OnLoadValue = false;
        this.SaveValue();
            foreach (var b in targetButton)
            {

                b.enabled = false;
                b.gameObject.GetComponent<Image>().enabled = false;
                b.transform.GetComponentInChildren<Text>().enabled = false;
                //b.onClick.RemoveAllListeners();
            }
        
    }
    public override void OnEvent(EVENT_TYPE et, Component sender, object param = null)
    {
    }
}
