using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeLock : MonoBehaviour {

    public int[] RightNumber;
    public int[] NowNumber;
    private ShapeLockChild[] myChilds;
    public string SendTriggerString;

    public void Start()
    {
        myChilds = this.GetComponentsInChildren<ShapeLockChild>();
        //RightNumber = new int[myChilds.Length];

        if (RightNumber.Length != myChilds.Length)
        {
            Debug.LogError("RightNumber 의 길이와 myChild 의 길이가 같지 않습니다. 둘은 같아야 합니다.");
        }
        NowNumber = new int[myChilds.Length];
        for(int i = 0; i < myChilds.Length; i++)
        {
            myChilds[i].childIndex = i;
        }
    }

    public void SetValue(int childIndex, int value)
    {
        NowNumber[childIndex] = value;
        CompareValue();
    }

    public void CompareValue()
    {
        for(int i = 0; i < RightNumber.Length; i++)
        {
            if(RightNumber[i] != NowNumber[i])
            {
                return;
            }
        }

        //for test
        Debug.Log("정답");
        csEventManager.Instance.PostNotification(EVENT_TYPE.SEND_TRIGGER, this, SendTriggerString);
    }
}
