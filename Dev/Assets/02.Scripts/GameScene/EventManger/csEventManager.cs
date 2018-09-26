using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
 * 기능 
 *  1. 리스너들에게 이벤트를 보내기 위한 이벤트 매니저 싱글턴
 *  2. IListener 구현과 함께 작동한다.
 * 수정 : 17.04.16
 */

public class csEventManager : MonoBehaviour
{
    #region C# 프로퍼티
    // 인스턴스에 접근하기 위한 public 프로퍼티
    public static csEventManager Instance
    {
        get { return instance; }
        set { }
    }
    #endregion

    #region 변수
    // 이벤트 매니저 인스턴스에 대한 내부 참조(싱글턴 디자인 패턴)
    private static csEventManager instance = null;

    // 리스너 오브젝트의 배열(모든 오브젝트가 이벤트 수신을 위해 등록이 되어있다)
    private Dictionary<EVENT_TYPE, List<IListener>> Listeners =
                                            new Dictionary<EVENT_TYPE, List<IListener>>();
    #endregion

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            DestroyImmediate(this);
    }

    #region AddListener : 리스너 배열에 지정된 오브젝트를 추가하기 위한 함수
    /*
     * 기능 : 리스너 배열에 지정된 리스너 오브젝트를 추가하기 위한 함수
     *  1. event_type : 수신할 이벤트
     *  2. Listener : 이벤트를 수신할 오브젝트
     * 수정 : 17.03.07
     */
    public void AddListener(EVENT_TYPE event_type, IListener Listener)
    {
        List<IListener> ListenList = null; //이 이벤트를 수신할 리스너의 리스트, 왜 널 값으로 초기화 한거지?
        if(Listeners.TryGetValue(event_type, out ListenList)) // 이벤트 형식 키가 있는지 검사한다, 존재하면 이것을 리스트에 추가한다.
        {            
            ListenList.Add(Listener); //리스트가 존재하면 해당 Listener을 추가한다.
            return;
        }
        // 그게 아니라면 새로운 리스트를 생성한다.
        ListenList = new List<IListener>();
        ListenList.Add(Listener);
        Listeners.Add(event_type, ListenList); // 내부의 리스너 리스트에 추가한다.
    }
    #endregion
    #region PostNotification : 이벤트를 리스너에 전달하기 위한 함수
    /* 
     * 기능 : 이벤트를 리스너에 전달하기 위한 함수
     *  1. event_type : 불려질 이벤트
     *  2. Sender : 이벤트를 부르는 오브젝트
     *  3. Param : 선택 가능한 파라미터
     * 수정 : 17.03.07
     */
    public void PostNotification(EVENT_TYPE event_type, Component Sender, object Param = null)
    {
        //모든 리스너에게 알린다!

        //이 이벤트를 수신할 리스너들의 리스트
        List<IListener> ListenList = null;
        if (!Listeners.TryGetValue(event_type, out ListenList))
        {
            return;
        } // 이벤트 항목이 없으면, 알릴 리스너가 없으므로 종료한다.

        // 항목이 존재하면, 적합한 리스너에게 알려준다.
        for (int i = 0; i < ListenList.Count; i++)
        {
            // 오브젝트가 null 이 아니면 인터페이스를 통해 메시지를 보낸다
            if (!ListenList[i].Equals(null))
            {
                ListenList[i].OnEvent(event_type, Sender, Param);
            }
        }
    }
    #endregion
    #region RemoveEvent : 이벤트 종류와 리스너 항목을 딕셔너리에서 제거
        /*
         * 기능 : 이벤트 종류와 리스너 항목을 딕셔너리에서 제거
         *  1. event_type : 삭제될 이벤트 타입
         */
    public void RemoveEvent(EVENT_TYPE event_type)
    {
        Listeners.Remove(event_type);
    }
    #endregion
    #region RemoveRedundancies : 딕셔너리에서 쓸모없는 항목을 제거
    public void RemoveRedundancies()
    {
        Dictionary<EVENT_TYPE, List<IListener>> TmpListeners =
                                             new Dictionary<EVENT_TYPE, List<IListener>>();
        foreach(KeyValuePair<EVENT_TYPE, List<IListener>> Item in Listeners) // 리스트의 모든 리스너 오브젝트를 순회하며 null 오브젝트를 제거한다.
        {
            for(int i = Item.Value.Count - 1; i>=0; i--)
            {        
                if (Item.Value[i].Equals(null))
                {
                    Item.Value.RemoveAt(i); // null 이면 항목을 지운다.
                }
            }

            if(Item.Value.Count > 0)
            {
                TmpListeners.Add(Item.Key, Item.Value); // 알림을 받기 위한 항목들만 리스트에 남으면 이 항목들을 임시 딕셔너리에 담는다.
            }
        }

        Listeners = TmpListeners; // 새로 최적화된 딕셔너리로 교체한다.
    }
    #endregion
    /*
     * 더이상 사용되지 않을 클래스라고 해서 임시 처리
    #region OnLevelWasLoad : 씬이 변경될 때 호출된다. 딕셔너리를 청소하는 역할
    public void OnLevelWasLoaded()
    {
        RemoveRedundancies();
    }
    #endregion
    */
}
