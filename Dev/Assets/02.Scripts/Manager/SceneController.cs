using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class SceneController : MonoBehaviour {
    public static SceneController singletone;
    public GameObject loadingUI;
    public void Awake()
    {
        if (singletone == null)
        {
            singletone = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            DestroyImmediate(this);
            return;
        }
    }
    public void Start()
    {
        loadingUI = GameObject.FindGameObjectWithTag("LoadingUI");
    }
    public void OnLevelWasLoaded(int level)
    {
        loadingUI = GameObject.FindGameObjectWithTag("LoadingUI");
    }

    public void MainToGame()
    {
        StartCoroutine(OnLoading("GameScene"));
    }
    public void GameToMain()
    {
        StartCoroutine(OnLoading("MainScene"));
    }
    IEnumerator OnLoading(string SceneName)
    {
        loadingUI.transform.localPosition = Vector2.zero;
        AsyncOperation sceneLoad = SceneManager.LoadSceneAsync(SceneName);
        Slider slider = loadingUI.GetComponentInChildren<Slider>();
        while (!(sceneLoad.isDone))
        {
            slider.value = Mathf.Clamp01(sceneLoad.progress / 0.9f);
            yield return null;
        }
    }
}
