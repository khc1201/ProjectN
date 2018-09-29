using UnityEngine;

[System.Serializable]
public class ViewObj
{
    public int _index;
    public int _doright;
    public int _doleft;
    public int _doback;
    public string _name;
    public GameObject _cam;
    public GameObject _ui;
    public GameObject _triggerListener;

    public void DoHide()
    {
        if(_cam!=null) _cam.SetActive(false);
        if(_ui!=null) _ui.SetActive(false);
    }
    public void DoShow()
    {
        if (_cam != null) _cam.SetActive(true);
        if (_ui != null) _ui.SetActive(true);
    }
}
