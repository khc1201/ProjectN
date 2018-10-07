using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item : MonoBehaviour
{
    public string Name;
    public string Index;
    public Sprite Icon;
    
    public Item(string _name, string _index, string _icon)
    {
        Name = _name; Index = _index;
    }
}
