using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    [Tooltip("Amount score increases after player passes a path")]
    [SerializeField] private int scoreIncreaseAmount = 100;

    /// <summary>
    /// Current player score
    /// </summary>
    private int score;

    /// <summary>
    /// Coins player has picked up this run
    /// </summary>
    private int coins;

    /// <summary>
    /// Player high score before current round finishes
    /// </summary>
    private int oldHighScore;

    /// <summary>
    /// Player shop balance before current round finishes
    /// </summary>
    private int oldShopBalance;

    #region -- UI Object References --

    [Tooltip("Text showing score while player is alive")]
    [SerializeField] private TextMeshProUGUI scoreText;

    [Tooltip("Text showing coins while player is alive")]
    [SerializeField] private TextMeshProUGUI coinsText;

    [Tooltip("Screen that shows up when player loses")]
    [SerializeField] private GameObject lossScreen;

    [Tooltip("Object that appears on loss screen when player gets a new high score")]
    [SerializeField] private GameObject newHighScoreObj;

    [Tooltip("Coins text on loss UI screen")]
    [SerializeField] private TextMeshProUGUI lossScreenCoinsText;

    [Tooltip("Score text on loss UI screen")]
    [SerializeField] private TextMeshProUGUI lossScreenScoreText;

    [Tooltip("High score text on loss UI screen")]
    [SerializeField] private TextMeshProUGUI highScoreText;

    #endregion

    void Start()
    {
        score = GameController.score;
        coins = GameController.coins;
        scoreText.text = "Score: " + score;
        coinsText.text = "Coins: " + coins;
        oldHighScore = PlayerPrefs.GetInt("HighScore", 0);
        oldShopBalance = PlayerPrefs.GetInt("ShopBalance", 0);
    }

    /// <summary>
    /// Shows the loss screen when the player dies
    /// </summary>
    public void ShowLossScreen()
    {
        // Set loss screen texts
        lossScreenScoreText.text = scoreText.text;
        lossScreenCoinsText.text = coinsText.text;

        // Check if player beat their high score
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
        
        // Hide old texts and show loss screen
        scoreText.gameObject.SetActive(false);
        coinsText.gameObject.SetActive(false);
        lossScreen.SetActive(true);
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

    public void CollectCoin()
    {
        coins++;
        GameController.coins = coins;
        coinsText.text = "Coins: " + coins;
    }

    /// <summary>
    /// Waits a small amount of time before changing scene to allow button audio to play
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    IEnumerator ChangeScene(int index)
    {
        yield return new WaitForSeconds(.1f);
        SceneManager.LoadScene(index);
    }
}
