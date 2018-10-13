using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumberLock : MonoBehaviour
{
    public PLAY_TYPE playType;
    public int lockCount; // 풀어야 하는 개수
    public List<NumberUnit> numberUnits; // 입력 툴
    public List<NumberTarget> numberTargets; // 입력 시 변화
    public List<NumberImage> numberImages;
    public string rightValue;
    public string nowValue;
    public void OnEnable()
    {
        ResetValue();
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
        if (playType != PLAY_TYPE.NUMBERLOCK_CLICKER)
        {
            foreach (var e in numberTargets)
            {
                e.GetMother(this);
                e.targetIndex = numberTargets.IndexOf(e);
            }
        }
        ResetValue();
    }
    public void SetValue(string value)
    {
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
        if (playType == PLAY_TYPE.NUMBERLOCK_CLICKER)   return;
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
        if (playType == PLAY_TYPE.NUMBERLOCK_CLICKER)   return;
        
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
        this.gameObject.GetComponent<TriggerSender>().SendTrigger();
        this.GetComponent<ButtonObject>().HideButton();
    }

}
