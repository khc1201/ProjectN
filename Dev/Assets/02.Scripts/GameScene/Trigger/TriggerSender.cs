using System.Collections.Generic;
using UnityEngine;

public class TriggerSender : MonoBehaviour {
    public List<string> SendTriggerList;
    public void SendTrigger()
    {
        foreach(var e in SendTriggerList)
        {
            //for test
            Debug.Log("STEP2");
            csEventManager.Instance.PostNotification(EVENT_TYPE.SEND_TRIGGER, this, e);
            //for test
            Debug.Log("STEP2-1");
        }
    }
}
