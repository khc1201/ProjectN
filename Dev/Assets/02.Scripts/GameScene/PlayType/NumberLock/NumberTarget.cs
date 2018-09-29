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
        switch (mother.playType)
        {
            case PLAY_TYPE.NUMBERLOCK_NUMBER:
                {
                    numberText.text = "";
                    break;
                }
        }
    }
    public void ChangeValue(string value)
    {
        switch (mother.playType)
        {
            case PLAY_TYPE.NUMBERLOCK_NUMBER:
                {
                    numberText.text = value;
                    break;
                }
        }
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
