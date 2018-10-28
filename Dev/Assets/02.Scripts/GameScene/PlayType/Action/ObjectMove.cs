using UnityEngine;
using DG.Tweening;

public class ObjectMove : MonoBehaviour {

    public GameObject targetObject;
    //public Vector3 targetInitRot;
    public Vector3 targetMove;
    public float moveTime;

    public void Start()
    {
        if (targetObject == null)
        {
            Debug.LogError("targetObject 가 없습니다. : " + this.gameObject.name);
            return;
        }
        
        targetObject.transform.DOLocalMove(targetMove, moveTime).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo).Play();
    }
}
