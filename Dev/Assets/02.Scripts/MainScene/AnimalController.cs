using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalController : MonoBehaviour {
    public List<GameObject> animals;
    public GameObject startPos;
    public GameObject animalTrs;
    public float moveSpeed;
    public float spawnRapid;

    // Use this for initialization
    void Start () {
        foreach(var g in animalTrs.transform.GetComponentsInChildren<AnimalMove>())
        {
            animals.Add(g.transform.gameObject);
        }

        StartCoroutine(CoMove());
    }
    IEnumerator CoMove()
    {
        MoveAnimal();
        yield return new WaitForSecondsRealtime(spawnRapid * Time.deltaTime);
        yield return StartCoroutine(CoMove());
    }
    void MoveAnimal()
    {
        int rand = Random.Range(0, animals.Count - 1);
        animals[rand].GetComponent<AnimalMove>().StartMove();
        animals.Remove(animals[rand]);
        animals.Sort();
    }
	
}
