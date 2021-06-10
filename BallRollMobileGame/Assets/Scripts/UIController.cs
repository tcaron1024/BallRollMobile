using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    [SerializeField] private float scoreIncreaseAmount = 100;
    private float score;
    private int roundedScore;
    private int oldHighScore;

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject loseScreen;
    [SerializeField] private GameObject newHighScoreObj;
    [SerializeField] private TextMeshProUGUI loseScreenScoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;

    void Start()
    {
        score = 0;
        scoreText.text = "Score: " + Mathf.RoundToInt(score);
        oldHighScore = PlayerPrefs.GetInt("HighScore", 0);
    }

    public void ShowLoseScreen()
    {
        roundedScore = Mathf.RoundToInt(score);
        loseScreenScoreText.text = scoreText.text;
        if (oldHighScore < roundedScore)
        {
            newHighScoreObj.SetActive(true);
            highScoreText.text = "High Score: " + roundedScore;
            PlayerPrefs.SetInt("HighScore", roundedScore);
            
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /// <summary>
    /// Sends player to main menu
    /// </summary>
    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    /// <summary>
    /// Increases score when player reaches end of a path tile
    /// </summary>
    public void IncreaseScore()
    {
        score += scoreIncreaseAmount;
        scoreText.text = "Score: " + score;
    }
}
