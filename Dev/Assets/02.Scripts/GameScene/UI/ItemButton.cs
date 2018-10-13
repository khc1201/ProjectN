using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemButton : MonoBehaviour {
    public Button thisButton;
    public void Start()
    {
        thisButton = this.GetComponent<Button>();
        thisButton.onClick.RemoveAllListeners();
        thisButton.onClick.AddListener(OnSelect);
    }
    public void OnSelect()
    {
        this.transform.parent.GetComponent<InventorySlot>().SelectThis();
        //for test
        //Debug.Log("OnSelect 발생");
    }
}
