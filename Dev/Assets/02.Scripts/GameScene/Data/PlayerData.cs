﻿using JsonFx.Json;
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
    public int NowView = 10; // default
    public bool[] IsTriggered;
    public int nowClick = 0;
    public int lowestClick = 0;
    #endregion

    #region Option
    public float SoundVolume = 0.5f;
    #endregion
}
public class PlayerData : MonoBehaviour, IListener
{
    public PlayData playData = new PlayData();
    public static PlayerData singletone;
    private string dataPath;
    public void Awake()
    {
        dataPath = Application.persistentDataPath + "/playerData.json";
        if (singletone == null)
        {
            if (File.Exists(dataPath))
            {
                LoadData();
            }
            else
            {
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
        yield return null;
    }
    public void CreateData()
    {
        string stringData = JsonWriter.Serialize(playData);
        var sw = new StreamWriter(new FileStream(dataPath, FileMode.Create));
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

}