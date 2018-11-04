using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class NumberLock : MonoBehaviour
{
    //public PLAY_TYPE playType;
    public string SendTriggerIndex;
    public int lockCount; // 풀어야 하는 개수
    public bool isShowInputText = true;
    public List<NumberUnit> numberUnits; // 입력 툴
    public List<NumberTarget> numberTargets; // 입력 시 변화
    public List<NumberImage> numberImages;
    public string rightValue;
    public string nowValue;

    public int MaxInputCount;
    private int NowInputCount = 0;
    public List<GameObject> MoveObjects;
    public string WrongSoundString;

    public void OnEnable()
    {
        ResetValue();
        NowInputCount = 0;
    }
    public void Start()
    {
        if(rightValue == null)
        {
            Debug.LogError(string.Format("{0} 의 rightValue 가 입력되어 있지 않습니다.", this.gameObject.name));
        }

        foreach(var e in numberUnits)
        {
            e.GetMother(this);
        }
        if (isShowInputText)
        {
            foreach (var e in numberTargets)
            {
                e.GetMother(this);
                e.targetIndex = numberTargets.IndexOf(e);
            }
        }
        ResetValue();
    }
    public void NoticeWrongNumber()
    {
        foreach (var e in MoveObjects)
        {
            e.transform.DOShakePosition(0.5f,0.2f,30).Play();
        }
        //WrongSoundString 을 Sound Event 로 보냄
        ResetValue();
        NowInputCount = 0;
    }
    public void SetValue(string value)
    {
        if(NowInputCount >= MaxInputCount)
        {
            NoticeWrongNumber();
            return;
        }
        NowInputCount++;
        if (nowValue.Length < rightValue.Length)
        {
            nowValue += value;
            RefreshUI();
        }
        if(nowValue.Length == rightValue.Length)
        {
            CheckAnswer();
        }
    }
    public void RefreshUI()
    {
        if (!(isShowInputText))   return;
        foreach (var e in numberTargets)
        {
            char[] value = nowValue.ToCharArray();
            char thischar = value[numberTargets.IndexOf(e)];
            e.ChangeValue(thischar.ToString());
        }
    }
    public void ResetValue()
    {
        nowValue = "";
        if (!(isShowInputText))   return;
        foreach (var e in numberTargets)
        {
            e.ClearValue();
        }
    }
    public void CheckAnswer()
    {
        StopCoroutine(CoCheckAnswer());
        StartCoroutine(CoCheckAnswer());
    }
    public IEnumerator CoCheckAnswer()
    {
        yield return new WaitForSeconds(1f * Time.deltaTime);
        if (CompareValue())
        {
            DoComplete();
        }
        yield return null;
    }
    private bool CompareValue()
    {
        if(nowValue == rightValue)
        {
            return true;
        }
        return false;
    }
    public void DoComplete()
    {
        //this.gameObject.GetComponent<TriggerSender>().SendTrigger();
        //this.GetComponent<ButtonObject>().HideButton();
        csEventManager.Instance.PostNotification(EVENT_TYPE.SEND_TRIGGER, this, SendTriggerIndex);
    }

}
