/*****************************************************************************
// File Name :         LevelLoader.cs
// Author :            Kyle Grenier
// Creation Date :     08/16/2021
//
// Brief Description : Handles 
*****************************************************************************/
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    public static LevelLoader instance;

    [SerializeField] private GameObject background;
    [SerializeField] private TextMeshProUGUI progressText;
    [SerializeField] private Slider progressSlider;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public void LoadLevel(string sceneName)
    {
        StartCoroutine(LoadLevelAsynchronously(sceneName));
    }

    public void LoadLevelAdditive(string sceneName)
    {
        StartCoroutine(LoadLvlAsyncAdditive(sceneName));
    }




    private IEnumerator LoadLevelAsynchronously(string sceneName)
    {
        background.SetActive(true);

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            progressSlider.value = progress;
            progressText.text = System.Math.Round(progress * 100f, 2) + "%";

            yield return null;
        }

        if (Time.timeScale != 1)
            Time.timeScale = 1;

        GameObject.FindGameObjectWithTag("Music").GetComponent<MusicHandler>().ChangeMusic(name);
        background.SetActive(false);
    }

    private IEnumerator LoadLvlAsyncAdditive(string sceneName)
    {
        background.SetActive(true);

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            progressSlider.value = progress;
            progressText.text = System.Math.Round(progress * 100f, 2) + "%";

            yield return null;
        }

        if (Time.timeScale != 1)
            Time.timeScale = 1;

        GameObject.FindGameObjectWithTag("Music").GetComponent<MusicHandler>().ChangeMusic(name);
        background.SetActive(false);
    }
}