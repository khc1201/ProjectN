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
    public int NowView = 1000; // default
    public List<string> IsTriggeredList = new List<string>();
    public List<string> IsMotionTriggeredList = new List<string>();
    public List<Item> nowHaveItem = new List<Item>();
    public List<Item> completeHaveItem = new List<Item>();
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
    public static PlayerData singletone;
    private string dataPath;
    public void Awake()
    {
        dataPath = Application.persistentDataPath + "/playerData.json";
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
            }
            else
            {
                singletone = this;
                CreateData();
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
        StartCoroutine(LateStart());
    }
    public void OnEvent(EVENT_TYPE et, Component sender, object param = null)
    {
        if(et == EVENT_TYPE.SHOW_HUD)
        {
            playData.NowView = (param as ViewObj)._index;
        }
    }
    public IEnumerator LateStart()
    {
        yield return new WaitForFixedUpdate();
        csEventManager.Instance.PostNotification(EVENT_TYPE.INIT_PLAYERDATA, this, playData);
        TestGetItem();
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
    public void LoadData()
    {
        var sr = new StreamReader(new FileStream(dataPath, FileMode.Open));
        string stringData = sr.ReadToEnd();
        sr.Close();
        playData = JsonReader.Deserialize<PlayData>(stringData);
    }
    public void SaveData()
    {
        string stringData = JsonWriter.Serialize(playData);
        var sw = new StreamWriter(new FileStream(dataPath, FileMode.Open));
        sw.WriteLine(stringData);
        sw.Close();
    }
    public void SaveTrigger(string index)
    {
        //for test
        Debug.Log("playData " + playData.IsTriggeredList.Count);
        playData.IsTriggeredList.Add(index);
        SaveData();
        
    }
    public void SaveMotionTrigger(string index)
    {
        playData.IsMotionTriggeredList.Add(index);
        //playData.IsMotionTriggeredList[playData.IsMotionTriggeredList.Length] = index;
        SaveData();
    }

    public void TestGetItem()
    {
        AddItem(new Item("name", "index", "icon"));
    }
    public void AddItem(Item target)
    {
        playData.nowHaveItem.Add(target);
        SaveData();
    }
    public void RemoveItem(Item item)
    {
        playData.nowHaveItem.Remove(playData.nowHaveItem.Find(x => x == item));
    }

}
