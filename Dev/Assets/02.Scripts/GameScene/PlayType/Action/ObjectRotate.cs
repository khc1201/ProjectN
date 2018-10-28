using UnityEngine;
using DG.Tweening;

public class ObjectRotate : MonoBehaviour {

    public GameObject targetObject;
    //public Vector3 targetInitRot;
    public Vector3 targetRot;
    public float rotTime;

    public void Start()
    {
        if(targetObject == null)
        {
            Debug.LogError("targetObject 가 없습니다. : " + this.gameObject.name);
            return;
        }
        
        targetObject.transform.DOLocalRotate(targetRot, rotTime).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo).Play();
    }

    
}
