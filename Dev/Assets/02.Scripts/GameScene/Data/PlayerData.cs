using JsonFx.Json;
using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Collections;

public class PlayData
{
    //Default Data
    #region TotalPlay 
    public bool IsFirstPlay = true;
    public int PlayCount = 0;
    public bool IsPerchased = false;
    #endregion

    #region NowPlay
    public int NowView = 1000;
    public List<string> nowHaveItem = new List<string>();
    public int nowClick = 0;
    public int lowestClick = 0;
    #endregion

    #region Option
    public float SoundVolume = 0.5f;
    #endregion
}
public class PlayerData : MonoBehaviour, IListener
{
    public PlayData playData;
    public Dictionary<string,bool> initData;
    public static PlayerData singletone;
    private string dataPath;
    private string initdataPath;
    //private string itemdataPath;
    public void Awake()
    {
        dataPath = Application.persistentDataPath + "/playerData.json";
        initdataPath = Application.persistentDataPath + "/initData.json";
        /*
        if (!(File.Exists(Application.persistentDataPath)))
        {
            File.Create(Application.persistentDataPath);
            Directory.CreateDirectory(Path.GetDirectoryName(Application.persistentDataPath));
            //for test
            Debug.Log("Directory Created");
        }
        */
        if (singletone == null)
        {
            if (File.Exists(dataPath))
            {
                singletone = this;
                LoadData();
                LoadInitData();
            }
            else
            {
                singletone = this;
                CreateData();
                CreateInitData();
            }
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            DestroyImmediate(this.gameObject);
            return;
        }
    }
    public void Start()
    {
        csEventManager.Instance.AddListener(EVENT_TYPE.SHOW_HUD, this);
        csEventManager.Instance.AddListener(EVENT_TYPE.SAVE_OBJECT, this);
        StartCoroutine(LateStart());
    }
    public void OnEvent(EVENT_TYPE et, Component sender, object param = null)
    {
        switch (et)
        {
            case EVENT_TYPE.SHOW_HUD:
                {
                    playData.NowView = (param as ViewObj)._index;
                    break;
                }
                /*
            case EVENT_TYPE.GET_ITEM:
                {
                    Item temp = param as Item;
                    if (temp == null)
                    {
                        Debug.LogError("AddItem 에서 param의 인수 전달이 제대로 되지 않았습니다");
                        return;
                    }
                    AddItem(temp);
                    break;
                }
                /*
            case EVENT_TYPE.SELECT_ITEM:
                {
                    playData.nowItem = (int)param;
                    ItemList.singletone.RefeshNowSelection();
                    break;
                }*/

            case EVENT_TYPE.SAVE_OBJECT:
                {
                    SimpleInit temp = (SimpleInit)param;
                    SaveInitData(temp._index, temp._onloadvalue);
                    break;  
                }
        }
    }
    public IEnumerator LateStart()
    {
        yield return new WaitForFixedUpdate();
        //yield return new WaitForSecondsRealtime(2.0f);
        csEventManager.Instance.PostNotification(EVENT_TYPE.INIT_PLAYERDATA, this);
        yield return null;
    }
    public void CreateData()
    {
        playData = new PlayData();
        
        string stringData = JsonWriter.Serialize(playData);
        var sw = new StreamWriter(new FileStream(dataPath, FileMode.CreateNew));
        sw.WriteLine(stringData);
        sw.Close();
    }

    public void CreateInitData()
    {
        initData = new Dictionary<string, bool>();
        string stringData = JsonWriter.Serialize(initData);
        var sw = new StreamWriter(new FileStream(initdataPath, FileMode.CreateNew));
        sw.WriteLine(stringData);
    }

    public void LoadData()
    {
        var sr = new StreamReader(new FileStream(dataPath, FileMode.Open));
        string stringData = sr.ReadToEnd();
        sr.Close();
        playData = JsonReader.Deserialize<PlayData>(stringData);
    }

    public void LoadInitData()
    {
        var sr = new StreamReader(new FileStream(initdataPath, FileMode.Open));
        string stringData = sr.ReadToEnd();
        sr.Close();
        initData = JsonReader.Deserialize<Dictionary<string,bool>>(stringData);
    }

    public void SaveData()
    {
        string stringData = JsonWriter.Serialize(playData);
        var sw = new StreamWriter(new FileStream(dataPath, FileMode.Open));
        sw.WriteLine(stringData);
        sw.Close();
    }
    public void SaveInitData(string Index, bool OnLoadValue)
    {
        if (initData.ContainsKey(Index))
        {
            initData[Index] = OnLoadValue;
        }
        else
        {
            initData.Add(Index, OnLoadValue);
        }

        string stringData = JsonWriter.Serialize(initData);
        //for test
        //Debug.Log(stringData);
        var sw = new StreamWriter(new FileStream(initdataPath, FileMode.Open));
        sw.WriteLine(stringData);
        sw.Close();
    }
    public void AddItem(List<string> myInven)
    {
        //playData.nowHaveItem = null;
        //playData.nowHaveItem = myInven;
        playData.nowHaveItem = myInven;
        SaveData();
    }
    public void RemoveItem(Item item)
    {
        if (playData.nowHaveItem.Contains(item.Index))
        {
            playData.nowHaveItem.Remove(item.Index);
        }
        
        /*
        foreach(var e in playData.nowHaveItem)
        {
            if(e == item.Index)
            {
                playData.nowHaveItem.Remove(e);
            }
        }
        */
        SaveData();
    }

}
