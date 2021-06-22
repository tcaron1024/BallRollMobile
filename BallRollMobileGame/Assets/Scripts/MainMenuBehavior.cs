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

    public void StartGame()
    {
        StartCoroutine("ChangeScene", "Gameplay");
    }
    IEnumerator ChangeScene(string name)
    {
        yield return new WaitForSeconds(.1f);
        SceneManager.LoadScene(name);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
