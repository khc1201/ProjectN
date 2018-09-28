using System.Collections.Generic;
using UnityEngine;

public class TriggerSender : MonoBehaviour {
    public List<string> SendTriggerList;
    public void SendTrigger()
    {
        foreach(var e in SendTriggerList)
        {
            csEventManager.Instance.PostNotification(EVENT_TYPE.SEND_TRIGGER, this, e);
        }
    }
}
