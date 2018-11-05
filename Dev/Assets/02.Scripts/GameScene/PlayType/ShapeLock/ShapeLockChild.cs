using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShapeLockChild : MonoBehaviour {
    public List<ShapeLockGrandChild> myChilds;
    public int childIndex;
    public int nowValue = 0;
    private ShapeLock mother;
    public void Start()
    {
        mother = this.transform.parent.gameObject.GetComponent<ShapeLock>();
        foreach (var e in this.GetComponentsInChildren<ShapeLockGrandChild>())
        {
            myChilds.Add(e);
            e.gameObject.SetActive(false);
        }
        myChilds[nowValue].gameObject.SetActive(true);
        
        this.GetComponent<Button>().onClick.AddListener(SetNextValue);
    }
    public void OnClick()
    {
        SetNextValue();
    }
    private void SetNextValue()
    {
        if(nowValue >= myChilds.Count - 1)
        {
            nowValue = 0;
        }
        else
        {
            nowValue++;
        }
        RefreshUI();
        mother.SetValue(childIndex, nowValue);
    }
    private void RefreshUI()
    {
        foreach (var e in myChilds)
        {
            e.gameObject.SetActive(false);
        }
        myChilds[nowValue].gameObject.SetActive(true);
    }
}
