using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AnimalMove : MonoBehaviour {

    Tween animalMove;
    AnimalController animalController;

    public void Start()
    {
        animalController = GameObject.FindGameObjectWithTag("AnimalController").GetComponent<AnimalController>();
        animalMove = this.transform.DOLocalMoveZ(-20f, animalController.moveSpeed).SetEase(Ease.Linear).Pause();
    }
    public void StartMove()
    {
        this.transform.parent = animalController.startPos.transform;
        this.transform.localPosition = Vector3.zero;
        animalMove.Play().OnComplete(EndMove);
    }
    public void EndMove()
    {
        this.transform.parent = animalController.animalTrs.transform;
        animalController.animals.Add(this.gameObject);
        animalController.animals.Sort();
        this.transform.localPosition = Vector3.zero;
    }

}
