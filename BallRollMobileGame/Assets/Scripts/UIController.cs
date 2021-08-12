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


    /// <summary>
    /// Pause menu for the game
    /// </summary>
    public GameObject pauseMenu;

    /// <summary>
    /// Settings menu that controls sensitivity and volume
    /// </summary>
    public GameObject settingsMenu;

    /// <summary>
    /// animator for the coin 
    /// </summary>
    public Animator coinAnim; 

    private SFXController sfx;

    #region -- UI Object References --

    [Tooltip("Text showing score while player is alive")]
    [SerializeField] private TextMeshProUGUI scoreText;

    [Tooltip("Text showing coins while player is alive")]
    [SerializeField] private TextMeshProUGUI coinsText;

    [Tooltip("The game object that holds the lives, score, and coins UI.")]
    [SerializeField] private GameObject playthroughDataParent;

    [Tooltip("Screen that shows up when player loses")]
    [SerializeField] private GameObject lossScreen;

    [Tooltip("Object that appears on loss screen when player gets a new high score")]
    [SerializeField] private GameObject newHighScoreObj;

    [Tooltip("Coins text on loss UI screen")]
    [SerializeField] private TextMeshProUGUI lossScreenOldCoinsText;

    [Tooltip("Coins text on loss UI screen")]
    [SerializeField] private TextMeshProUGUI lossScreenNewCoinsText;

    [Tooltip("Score text on loss UI screen")]
    [SerializeField] private TextMeshProUGUI lossScreenScoreText;

    [Tooltip("High score text on loss UI screen")]
    [SerializeField] private TextMeshProUGUI highScoreText;

    #endregion

    private void OnEnable()
    {
        // Subscribe to the OnPlayerDeath event so we 
        // display the loss screen on death.
        EventManager.OnPlayerDeath += ShowLossScreen;

        // Subscribes to OnCoinPickup to add a coin when player collides with one
        EventManager.OnCoinPickup += CollectCoin;
    }

    private void OnDisable()
    {
        EventManager.OnPlayerDeath -= ShowLossScreen;
        EventManager.OnCoinPickup -= CollectCoin;
    }

    void Start()
    {
        score = GameController.score;
        coins = GameController.coins;
        scoreText.text = "     " + score;
        coinsText.text = "x " + coins;
        oldHighScore = PlayerPrefs.GetInt("HighScore", 0);
        oldShopBalance = PlayerPrefs.GetInt("ShopBalance", 0);

        sfx = GameObject.FindGameObjectWithTag("SFX").GetComponent<SFXController>();

        coinAnim = GameObject.Find("coin_light_00000").GetComponent<Animator>();
    }

    /// <summary>
    /// Shows the loss screen when the player dies
    /// </summary>
    public void ShowLossScreen()
    {
        // Set loss screen texts
        lossScreenScoreText.text = scoreText.text;
        lossScreenOldCoinsText.text = oldShopBalance + "  +";
        lossScreenNewCoinsText.text = coinsText.text.Trim('x', ' ');

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


        PlayerPrefs.SetInt("ShopBalance", (coins + oldShopBalance));
        Debug.Log("Shop balance is now " + PlayerPrefs.GetInt("ShopBalance"));

        // Hide old texts and show loss screen
        playthroughDataParent.SetActive(false);
        
        lossScreen.SetActive(true);

        if (coins > 0)
        {
            StartCoroutine(CoinAddUp());
        }
    }


    IEnumerator CoinAddUp()
    {
        while (coins > 0)
        {
            coins--;
            oldShopBalance++;
            lossScreenOldCoinsText.text = oldShopBalance + " +";
            lossScreenNewCoinsText.text = " " + coins;
            sfx.PlayAddUp();

            yield return new WaitForSeconds(.1f);
        }
    }

    /// <summary>
    /// Loads scene based on given name
    /// </summary>
    public void LoadScene(string name)
    {
        StartCoroutine(ChangeScene(name));
    }

    public void revealPause(bool pause)
    {
        if(pause)
        {
            pauseMenu.SetActive(false);
            settingsMenu.SetActive(false);
        }
        else
        {
            pauseMenu.SetActive(true);
        }
        
    }

    public void revealSettings(bool reveal)
    {
        if(reveal)
        {
            settingsMenu.SetActive(false);
        }
        else
        {
            settingsMenu.SetActive(true);
        }
    }

    /// <summary>
    /// Increases score when player reaches end of a path tile
    /// </summary>
    public void IncreaseScore()
    {
        score += scoreIncreaseAmount;
        GameController.score = score;
        scoreText.text = "     " + score;
    }

    public void CollectCoin()
    {
        coins++;
        GameController.coins = coins;
        coinsText.text = "x " + coins;
        coinAnim.SetBool("coin", true);
        Invoke("resetCoin", 0.75f);
    }

    /// <summary>
    /// Will turn the parameter that plays the coin animation back false
    /// </summary>
    private void resetCoin()
    {
        coinAnim.SetBool("coin", false);
    }

    /// <summary>
    /// Waits a small amount of time before changing scene to allow button audio to play
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    IEnumerator ChangeScene(string name)
    {
        yield return new WaitForSecondsRealtime(.15f);
        SceneManager.LoadScene(name);
        Time.timeScale = 1;
        GameObject.FindGameObjectWithTag("Music").GetComponent<MusicHandler>().ChangeMusic(name);
    }

}
