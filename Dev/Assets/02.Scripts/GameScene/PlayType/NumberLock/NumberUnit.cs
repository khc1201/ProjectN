using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class NumberUnit : MonoBehaviour
{
    public string thisValue;
    public bool IsClearButton = false;
    public bool IsClickable = true;
    public GameObject buttonObj;
    private NumberLock mother;
    private Tween tween1;


    public void Start()
    {
        this.GetComponent<Button>().onClick.RemoveAllListeners();
        this.GetComponent<Button>().onClick.AddListener(OnClick);

    }
    public void SetClickable()
    {
        IsClickable = true;
        this.GetComponent<Button>().enabled = true;
        
    }
    public void SetClickDisable()
    {
        IsClickable = false;
        this.GetComponent<Button>().enabled = false;
    }
    public void GetMother(NumberLock target)
    {
        mother = target;

        if(mother.playType == PLAY_TYPE.NUMBERLOCK_CLICKER)
        {
            if (buttonObj == null)
            {
                Debug.LogError(this.gameObject.name + " 의 buttonObj 가 지정되어 있지 않습니다.");
            }
        }
    }
    public void OnClick()
    {
        if (IsClearButton)
        {
            mother.ResetValue();
        }
        else
        {
            if (mother.playType == PLAY_TYPE.NUMBERLOCK_CLICKER && IsClickable)
            {
                buttonObj.transform.DOLocalMoveZ(0.005f, 0.1f).SetLoops(2, LoopType.Yoyo).OnStart(SetClickDisable).OnComplete(SetClickable).Play();
                mother.SetValue(thisValue);
            }
            else if (mother.playType != PLAY_TYPE.NUMBERLOCK_CLICKER) { mother.SetValue(thisValue); }
        }
    }

}
