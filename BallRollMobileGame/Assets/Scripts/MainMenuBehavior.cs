using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuBehavior : MonoBehaviour
{
    void Start()
    {
        // Checks if player has a current high score and sets it to 0 if not
        if (!PlayerPrefs.HasKey("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", 0);
        }
        // Checks if player has a current shop balance and sets it to 0 if not
        if (!PlayerPrefs.HasKey("ShopBalance"))
        {
            PlayerPrefs.SetInt("ShopBalance", 0);
        }
    }

    IEnumerator ChangeScene(string name)
    {
        yield return new WaitForSeconds(.15f);
        SceneManager.LoadScene(name);
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine("ChangeScene", sceneName);

        // Change music to appropriate for scene being loaded
        GameObject.FindObjectOfType<MusicHandler>().ChangeMusic(sceneName);
    }

    public void LoadSceneAdditive(string sceneName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);

        // Change music to appropriate for scene being loaded
        GameObject.FindObjectOfType<MusicHandler>().ChangeMusic(sceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
