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
        foreach(var e in numberTargets)
        {
            e.GetMother(this);
            e.targetIndex = numberTargets.IndexOf(e);
        }

        ResetValue();
    }
    public void SetValue(string value)
    {
        //for test
        Debug.Log("SetValue ! nowValue.Length = " + nowValue.Length);
        //for test
        Debug.Log("SetValue ! before nowValue = " + nowValue);

        if (nowValue.Length < rightValue.Length)
        {
            nowValue += value;
            RefreshUI();
            //for test
            Debug.Log("SetValue succeed! after nowValue = " + nowValue);
        }
        if(nowValue.Length == rightValue.Length)
        {
            CheckAnswer();
        }
    }
    public void RefreshUI()
    {
        foreach(var e in numberTargets)
        {
            char[] value = nowValue.ToCharArray();
            char thischar = value[numberTargets.IndexOf(e)];
            e.ChangeValue(thischar.ToString());
        }
    }
    public void ResetValue()
    {
        nowValue = "";
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
        //for test
        Debug.Log("성공을 이곳에서 처리!");
        //for test
        Debug.Log("STEP1");
        this.gameObject.GetComponent<TriggerSender>().SendTrigger();
        
    }

}
