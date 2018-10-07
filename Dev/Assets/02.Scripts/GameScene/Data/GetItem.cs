using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetItem : MonoBehaviour {
    public GameObject target;
    public Item targetItem;
    public void Start()
    {
        targetItem = target.GetComponent<Item>();
        if(targetItem == null)
        {
            Debug.LogError("target 이 없습니다.");
            return;
        }
    }
    private void OnEnable()
    {
        if (target.activeInHierarchy)
        {
            this.GetComponent<Button>().onClick.RemoveAllListeners();
            this.GetComponent<Button>().onClick.AddListener(OnClick);
        }
    }

    public void OnClick()
    {
        //PlayerData.singletone.AddItem(targetItem);
        //for test
        Debug.Log("클릭 발생!");
        csEventManager.Instance.PostNotification(EVENT_TYPE.GET_ITEM, this, targetItem);
        target.gameObject.SetActive(false);
        //저장 시킴
    }
}
