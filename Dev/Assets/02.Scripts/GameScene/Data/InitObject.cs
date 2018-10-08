using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public bool OnLoadValue = false;
    
    public void LoadValue()
    {  
        if (!(PlayerData.singletone.initData.ContainsKey(this.Index)))
        {
            Debug.LogError("InitData 안에 이 오브젝트가 없는데? : " + this.gameObject);
            return;
        }

        //for test
        OnLoadValue = PlayerData.singletone.initData[Index];
    }
    public void SaveValue()
    {
        //디폴트 파일을 생성한 후에, 해당 함수는 값이 변할 때 호출하는 함수로 수정 : 그땐 lateSEt 쓰지 않아도 될 듯, item, triggerlistener, motiontrigger 에 각각 
        //csEventManager.Instance.PostNotification(EVENT_TYPE.SAVE_OBJECT, this, new SimpleInit(this.Index, this.OnLoadValue));
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
    public virtual void OnLoadScene()
    {
        //for test
        Debug.Log("기본적으로 이곳에서 Value 를 처리함");
        if (OnLoadValue)
        {
            //for test
            Debug.Log("OnLoadValue 가 참인 경우의 처리");
        }
        else
        {
            //for test
            Debug.Log("OnLoadValue 가 거짓인 경우의 처리");
        }
    }
}
