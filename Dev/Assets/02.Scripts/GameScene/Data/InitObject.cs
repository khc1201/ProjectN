using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimpleInit
{
    public string _index;
    public bool _onloadvalue;
    public SimpleInit(string idx, bool value)
    {
        _index = idx;
        _onloadvalue = value;
    }
}
[System.Serializable]
public class InitObject : MonoBehaviour, IListener {

    public string Name;
    public string Index;
    public bool OnLoadValue = true;
    public bool isTrueOnInit = true;
    public GameObject targetGameObject;
    public void LoadValue()
    {  
        if (PlayerData.singletone.initData.ContainsKey(Index))
        {
            OnLoadValue = PlayerData.singletone.initData[Index];
        }
        else if (!(PlayerData.singletone.initData.ContainsKey(Index)))
        {
            //for test
            //Debug.Log("InitData 안에 이 오브젝트가 없는데? : 새로 등록할게 " + this.gameObject);
            PlayerData.singletone.SaveInitData(this.Index, this.OnLoadValue);
            LoadValue();
        }
    }
    public void SaveValue()
    {
        //디폴트 파일을 생성한 후에, 해당 함수는 값이 변할 때 호출하는 함수로 수정 : 그땐 lateSEt 쓰지 않아도 될 듯, item, triggerlistener, motiontrigger 에 각각 
        csEventManager.Instance.PostNotification(EVENT_TYPE.SAVE_OBJECT, this, new SimpleInit(this.Index, this.OnLoadValue));
        //StartCoroutine(LateSet());
    }
    public IEnumerator LateSet()
    {
        yield return new WaitForFixedUpdate();
        csEventManager.Instance.PostNotification(EVENT_TYPE.SAVE_OBJECT, this, new SimpleInit(this.Index, this.OnLoadValue));
    }
    public virtual void OnEvent(EVENT_TYPE et, Component sender, object param = null)
    {
        
    }
    public virtual void InitObjects()
    {
        LoadValue();
        if(this.targetGameObject != null)
        {
            if (OnLoadValue)
            {
                ShowInitObject();
            }
            else if (!OnLoadValue)
            {
                HideInitObject();
            }
        }
    }
    public void ShowInitObject()
    {
        this.OnLoadValue = true;
        this.targetGameObject.SetActive(true);
        this.SaveValue();
    }
    public void HideInitObject()
    {
        this.OnLoadValue = false;
        this.targetGameObject.SetActive(false);
        this.SaveValue();
    }
}
