using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item : InitObject, IListener
{
    //public string Name;
    //public string Index;
    public string Icon;
    public GameObject target;

    public void Start()
    {
        csEventManager.Instance.AddListener(EVENT_TYPE.INIT_OBJECT, this);

        //for test
        base.SaveValue();
    }
    public override void OnEvent(EVENT_TYPE et, Component sender, object param = null)
    {
        switch (et)
        {
            case EVENT_TYPE.INIT_OBJECT:
                {
                    base.LoadValue();
                    break;
                }
        }
    }

    public Item(string _name, string _index, string _icon)
    {
        Name = _name; Index = _index; Icon = _icon;
    }
}
