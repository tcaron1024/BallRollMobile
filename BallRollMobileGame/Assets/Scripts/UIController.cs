using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    [SerializeField] private int scoreIncreaseAmount = 100;
    private int score;
    private int oldHighScore;

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject loseScreen;
    [SerializeField] private GameObject newHighScoreObj;
    [SerializeField] private TextMeshProUGUI loseScreenScoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;

    void Start()
    {
        score = GameController.score;
        scoreText.text = "Score: " + score;
        oldHighScore = PlayerPrefs.GetInt("HighScore", 0);
    }

    public void ShowLoseScreen()
    {
        loseScreenScoreText.text = scoreText.text;
        if (oldHighScore < score)
        {
            newHighScoreObj.SetActive(true);
            highScoreText.text = "High Score: " + score;
            PlayerPrefs.SetInt("HighScore", score);
            
        }
        else
        {
            highScoreText.text = "High Score: " + oldHighScore;
        }
        scoreText.gameObject.SetActive(false);
        loseScreen.SetActive(true);
    }

    /// <summary>
    /// Restarts game
    /// </summary>
    public void PlayAgain()
    {
        StartCoroutine("ChangeScene", SceneManager.GetActiveScene().buildIndex);
    }

    /// <summary>
    /// Sends player to main menu
    /// </summary>
    public void GoToMainMenu()
    {
        StartCoroutine("ChangeScene", 0);
    }

    /// <summary>
    /// Increases score when player reaches end of a path tile
    /// </summary>
    public void IncreaseScore()
    {
        score += scoreIncreaseAmount;
        GameController.score = score;
        scoreText.text = "Score: " + score;
    }

    IEnumerator ChangeScene(int index)
    {
        yield return new WaitForSeconds(.1f);
        SceneManager.LoadScene(index);
    }
}
