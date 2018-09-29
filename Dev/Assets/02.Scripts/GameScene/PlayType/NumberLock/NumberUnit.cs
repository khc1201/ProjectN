using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumberUnit : MonoBehaviour
{
    public string thisValue;
    public bool IsClearButton = false;
    private NumberLock mother;

    public void Start()
    {
        this.GetComponent<Button>().onClick.RemoveAllListeners();
        this.GetComponent<Button>().onClick.AddListener(OnClick);
    }

    public void GetMother(NumberLock target)
    {
        mother = target;
    }
    public void OnClick()
    {
        if (IsClearButton)
        {
            mother.ResetValue();
        }
        else
        {
            mother.SetValue(thisValue);
        }
    }

}
