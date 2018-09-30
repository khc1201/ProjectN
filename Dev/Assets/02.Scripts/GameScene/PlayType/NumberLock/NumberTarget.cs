using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumberTarget : MonoBehaviour {
    public int targetIndex;
    private NumberLock mother;

    Text numberText;
    List<Image> list_Image;
    List<NumberImage> list_numberImage;


    public void Start()
    {
        numberText = this.GetComponent<Text>();
        ClearValue();
    }
    public void ClearValue()
    {
        numberText.text = "";
    }
    public void ChangeValue(string value)
    {
        numberText.text = value;
    }
    public void GetMother(NumberLock target)
    {
        mother = target;
        if(target.numberImages.Count != 0)
        {
            list_numberImage = target.numberImages;
        }
    }
}
