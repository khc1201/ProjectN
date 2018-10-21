using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class NumberUnit : MonoBehaviour
{
    public string thisValue;
    public bool IsClearButton = false;
    public bool IsClickable = true;
    public GameObject targetObj;
    private Button buttonObj;
    private NumberLock mother;
    private Tween tween1;
    public Vector3 movePos;


    public void Start()
    {
        buttonObj = this.gameObject.GetComponent<Button>();
        buttonObj.onClick.RemoveAllListeners();
        buttonObj.onClick.AddListener(OnClick);
        //csEventManager.Instance.AddListener(EVENT_TYPE.INIT_OBJECT, this);
    }
    public void SetClickable()
    {
        IsClickable = true;
        buttonObj.enabled = true;
        
    }
    public void SetClickDisable()
    {
        IsClickable = false;
        buttonObj.enabled = false;
    }
    public void GetMother(NumberLock target)
    {
        mother = target;

        if(!(mother.isShowInputText))
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
            if (!(mother.isShowInputText) && IsClickable)
            {
                //for test
                //Debug.Log("클릭버튼");

                targetObj.transform.DOLocalMove(movePos, 0.1f).SetLoops(2, LoopType.Yoyo).OnStart(SetClickDisable).OnComplete(SetClickable).Play();
         
                mother.SetValue(thisValue);
            }
            else if (mother.isShowInputText)
            {
                mother.SetValue(thisValue);
            }
        }
    }

}
