using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{

    private float score;
    private int roundedScore;
    private int oldHighScore;
    [SerializeField] private float scoreIncreasePerSecond;

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject loseScreen;
    [SerializeField] private GameObject newHighScoreObj;
    [SerializeField] private TextMeshProUGUI loseScreenScoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        scoreText.text = "Score: " + Mathf.RoundToInt(score);
        oldHighScore = PlayerPrefs.GetInt("HighScore", 0);
    }

    private void FixedUpdate()
    {
        score += scoreIncreasePerSecond / 60f;
        scoreText.text = "Score: " + Mathf.RoundToInt(score);
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

    public void PlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
